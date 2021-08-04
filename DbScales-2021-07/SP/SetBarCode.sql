CREATE PROCEDURE [db_scales].[SetBarCode]

	@BarCodeTypeId				INT  
	,@NomenclatureId			INT  
	,@NomenclatureUnitId		INT  = NULL
	,@ContragentId				INT  = NULL
	,@Value						NVARCHAR(150)
	,@ID int OUTPUT

AS
BEGIN

	BEGIN TRAN;

	MERGE [db_scales].[BarCodes] AS target  
    USING (
	SELECT 
		@BarCodeTypeId		,
		@NomenclatureId		,
		@NomenclatureUnitId	,
		@ContragentId		,
		@Value					
	) AS source (
		[BarCodeTypeId]			,
		[NomenclatureId]		,
		[NomenclatureUnitId]	,
		[ContragentId]			,
		[Value]				
	)  
    ON (
			target.[BarCodeTypeId]			 = source.[BarCodeTypeId]		
			AND target.[NomenclatureId]		 = source.[NomenclatureId]	
			AND target.[NomenclatureUnitId]	 = source.[NomenclatureUnitId]
			AND target.[ContragentId]		 = source.[ContragentId]		
		) 
	
	WHEN MATCHED THEN
        UPDATE SET 
			[BarCodeTypeId]			=	source.[BarCodeTypeId]			,
			[NomenclatureId]		=	source.[NomenclatureId]			,
			[NomenclatureUnitId]	=	source.[NomenclatureUnitId]		,
			[ContragentId]			=	source.[ContragentId]			,
			[Value]					=	source.[Value]					,

			[ModifiedDate]			 = GETDATE()

    WHEN NOT MATCHED THEN 
	
        INSERT (
			[BarCodeTypeId]					,
			[NomenclatureId]				,
			[NomenclatureUnitId]			,
			[ContragentId]					,
			[Value]					
		)  
        VALUES (
			source.[BarCodeTypeId]			,
			source.[NomenclatureId]			,
			source.[NomenclatureUnitId]		,
			source.[ContragentId]			,
			source.[Value]					
		) ;

	SELECT @ID = @@IDENTITY;

	COMMIT TRAN;

	RETURN 0;

END
GO

GRANT EXECUTE ON [db_scales].[SetBarCode]
    TO  [db_scales_users]; 
GO
