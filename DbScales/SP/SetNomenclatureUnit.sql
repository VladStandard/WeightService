CREATE PROCEDURE [db_scales].[SetNomenclatureUnit]
	@IDRRef				uniqueidentifier
	,@Name					NVARCHAR(150) 
	,@NomenclatureId		INT
	,@Marked				BIT
	,@PackWeight			DECIMAL(10,3)
	,@PackQuantly			INT
	,@PackTypeId			INT
	,@ID int OUTPUT

AS
BEGIN

	BEGIN TRAN;

	MERGE [db_scales].[NomenclatureUnits] AS target  
    USING (
	SELECT 
		@IDRRef
		,@Name			
		,@NomenclatureId
		,@Marked		
		,@PackWeight	
		,@PackQuantly	
		,@PackTypeId	

	) AS source (

		[1CRRefID]			,
		[Name]				,
		[NomenclatureId]	,
		[Marked]			,
		[PackWeight]		,
		[PackQuantly]		,
		[PackTypeId]		

	)  
    ON (target.IDRRef = source.[1CRRefID]) 
	
	WHEN MATCHED THEN
        UPDATE SET 
			IDRRef				 = source.[1CRRefID]		,
			[Name]					 = source.[Name]			,
			[NomenclatureId]		 = source.[NomenclatureId]	,
			[Marked]				 = source.[Marked]			,
			[PackWeight]			 = source.[PackWeight]		,
			[PackQuantly]			 = source.[PackQuantly]		,
			[PackTypeId]			 = source.[PackTypeId]		,
			[ModifiedDate]			 = GETDATE()

    WHEN NOT MATCHED THEN 
	
        INSERT (
			IDRRef				,
			[Name]					,
			[NomenclatureId]		,
			[Marked]				,
			[PackWeight]			,
			[PackQuantly]			,
			[PackTypeId]	

		)  
        VALUES (
			source.[1CRRefID]			,
			source.[Name]				,
			source.[NomenclatureId]	,
			source.[Marked]			,
			source.[PackWeight]		,
			source.[PackQuantly]		,
			source.[PackTypeId]		

		) ;

	SELECT @ID = @@IDENTITY;

	COMMIT TRAN;

	RETURN 0;

END
GO

GRANT EXECUTE ON [db_scales].[SetNomenclatureUnit]
    TO  [db_scales_users]; 
GO
