CREATE PROCEDURE [db_scales].[SetContragent]
	@1CRRefID				VARCHAR(38)
	,@Name					NVARCHAR(150) 
	,@Marked				BIT
	,@ID int OUTPUT

AS
BEGIN

	BEGIN TRAN;

	MERGE [db_scales].[Contragents] AS target  
    USING (
	SELECT 
		UPPER(@1CRRefID)
		,@Name			
		,@Marked		

	) AS source (

		[1CRRefID]			,
		[Name]				,
		[Marked]			

	)  
    ON (target.[1CRRefID] = source.[1CRRefID]) 
	
	WHEN MATCHED THEN
        UPDATE SET 
			[1CRRefID]				 = source.[1CRRefID]		,
			[Name]					 = source.[Name]			,
			[Marked]				 = source.[Marked]			,
			[ModifiedDate]			 = GETDATE()

    WHEN NOT MATCHED THEN 
	
        INSERT (
			[1CRRefID]				,
			[Name]					,
			[Marked]				

		)  
        VALUES (
			source.[1CRRefID]			,
			source.[Name]				,
			source.[Marked]			

		) ;

	SELECT @ID = @@IDENTITY;

	COMMIT TRAN;

	RETURN 0;

END
GO

GRANT EXECUTE ON [db_scales].[SetContragent]
    TO  [db_scales_users]; 
GO
