CREATE PROCEDURE [db_scales].[SetProductionFacility]

	@1CRRefID varchar(38),
	@Name nvarchar(150),
	@ID int OUTPUT

AS
BEGIN

	BEGIN TRAN;

	MERGE [db_scales].[ProductionFacility] AS target  
    USING (
	SELECT 
		UPPER(@1CRRefID),
		@Name

	) AS source (

		[1CRRefID],
		[Name]

	)  
    ON (target.IdRRef = source.[1CRRefID]) 
	
	WHEN MATCHED THEN
        UPDATE SET 
			[Name]			= source.[Name] ,
			[ModifiedDate]	= GETDATE()

    WHEN NOT MATCHED THEN 
	
        INSERT (
			[IdRRef],
			[Name]
		)  
        VALUES 
		(
			source.[1CRRefID],
			source.[Name]
		) ;

	SELECT @ID = @@IDENTITY;

	COMMIT TRAN;

	RETURN 0;

END
GO

GRANT EXECUTE ON [db_scales].[SetProductionFacility]
    TO  [db_scales_users]; 
GO
