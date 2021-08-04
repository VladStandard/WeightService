CREATE PROCEDURE [db_scales].[SetWorkShop]

	@Name 					nvarchar(150),
	@ProductionFacilityID	int	,
	@IDRRefID				uniqueidentifier,
	@ID						int OUTPUT

AS
BEGIN

	BEGIN TRAN;

	MERGE [db_scales].[WorkShop] AS target  
    USING (
	SELECT 
		@IDRRefID,
		@ProductionFacilityID,
		@Name

	) AS source (

		[1CRRefID],
		[ProductionFacilityID],
		[Name]

	)  
    ON (target.[IDRRef] = source.[1CRRefID]) 
	
	WHEN MATCHED THEN
        UPDATE SET 
			[Name]					= source.[Name] ,
			[ProductionFacilityID]	= source.[ProductionFacilityID],
			[ModifiedDate]	= GETDATE()

    WHEN NOT MATCHED THEN 
	
        INSERT (
			[IDRRef],
			[ProductionFacilityID],
			[Name]
		)  
        VALUES 
		(
			source.[1CRRefID],
			source.[ProductionFacilityID],
			source.[Name]
		) ;

	SELECT @ID = @@IDENTITY;

	COMMIT TRAN;

	RETURN 0;

END
GO
GRANT EXECUTE
    ON OBJECT::[db_scales].[SetWorkShop] TO [db_scales_users]
    AS [scales_owner];

