CREATE PROCEDURE [SCSL].[spFillDimNormNomenclatures]

	@NormCodeNomenclature			nvarchar(50),
	@NormNameNomenclature			nvarchar(500),
	@NormArticleNomenclature		nvarchar(50),
	@NormWeightNomenclature			decimal(15, 3),
	--@CodeInUPP						nvarchar(50),
	--@NomenclatureIDinUPP			int,

	@Marked						bit,
	@CHECKSUMM					BIGINT,
	@StatusID					int ,
	@InformationSystemID		int,
	@NormNomenclatureIDinIS		binary(16),

	@SYS_CHANGE_VERSION			bigint,
	@SYS_CHANGE_OPERATION		nchar(1)



AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [SCSL].[DimNormNomenclatures] as target
	USING ( 
	SELECT 

		@NormCodeNomenclature	
		,@NormNameNomenclature
		,@NormArticleNomenclature
		,@NormWeightNomenclature
		,NULL --@CodeInUPP		
		,NULL --@NomenclatureIDinUPP	
		
		,@Marked
		,@CHECKSUMM
		,@StatusID
		,@InformationSystemID
		,@NormNomenclatureIDinIS	

	) AS source (

		[NormCodeNomenclature],		
		[NormNameNomenclature]	,	
		[NormArticleNomenclature],	
		[NormWeightNomenclature],	
		[CodeInUPP]	,				
		[NomenclatureIDinUPP],		

		[Marked],					
		[CHECKSUMM],				
		[StatusID]	,			
		[InformationSystemID],	
		[NormNomenclatureIDinIS]	

	)  
	ON (target.[NormNomenclatureIDinIS]  = source.[NormNomenclatureIDinIS] 
		AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		
		[NormCodeNomenclature]			= source.[NormCodeNomenclature]	,	
		[NormNameNomenclature]			= source.[NormNameNomenclature]	,	
		[NormArticleNomenclature]		= source.[NormArticleNomenclature],	
		[NormWeightNomenclature]		= source.[NormWeightNomenclature],	
		[CodeInUPP]						= source.[CodeInUPP],					
		[NomenclatureIDinUPP]			= source.[NomenclatureIDinUPP]	,	
		
		[Marked]					= source.[Marked],
		[CHECKSUMM]					= source.[CHECKSUMM],	
		[StatusID]					= source.[StatusID],
		[DLM]						= @DLM
		
	WHEN NOT MATCHED THEN INSERT (

		[NormCodeNomenclature],		
		[NormNameNomenclature],		
		[NormArticleNomenclature],	
		[NormWeightNomenclature],	
		[CodeInUPP],					
		[NomenclatureIDinUPP],		

		[Marked],
		[CHECKSUMM],
		[CreateDate],
		[DLM],
		[StatusID],
		[InformationSystemID],
		[NormNomenclatureIDinIS]

		) VALUES  (

		source.[NormCodeNomenclature],		
		source.[NormNameNomenclature],		
		source.[NormArticleNomenclature],	
		source.[NormWeightNomenclature]	,
		source.[CodeInUPP]	,				
		source.[NomenclatureIDinUPP],		

		source.[Marked],	 
		source.[CHECKSUMM],
		@CreateDate,
		@DLM,
		source.[StatusID],
		source.[InformationSystemID],
		source.[NormNomenclatureIDinIS]

		);

END

GO
