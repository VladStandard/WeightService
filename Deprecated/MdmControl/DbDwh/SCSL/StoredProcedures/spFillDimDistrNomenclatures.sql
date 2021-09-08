CREATE PROCEDURE [SCSL].[spFillDimDistrNomenclatures]

	@DistrCodeNomenclature		nvarchar(50),
	@DistrNameNomenclature		nvarchar(500),
	@DistrArticleNomenclature	nvarchar(50),
	@UseInReport				int,
	@NormNomenclatureIdInIS		binary(16),
	

	@Marked						bit,
	@CHECKSUMM					BIGINT,
	@StatusID					int,
	@InformationSystemID		int,
	@DistrNomenclatureIDinIS	binary(16),

	@SYS_CHANGE_VERSION			bigint,
	@SYS_CHANGE_OPERATION		nchar(1)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [SCSL].[DimDistrNomenclatures] as target
	USING ( 
	SELECT 
		@DistrCodeNomenclature		
		,@DistrNameNomenclature		
		,@DistrArticleNomenclature	
		,@UseInReport	
		,@NormNomenclatureIdInIS
		,@Marked						
		,@CHECKSUMM					
		,@StatusID					
		,@InformationSystemID		
		,@DistrNomenclatureIDinIS	
	) AS source (

		[DistrCodeNomenclature]
		,[DistrNameNomenclature]	
		,[DistrArticleNomenclature]
		,[UseInReport]
		,[NormNomenclatureIdInIS]
		,[Marked]				
		,[CHECKSUMM]
		,[StatusID]				
		,[InformationSystemID]
		,[DistrNomenclatureIDinIS]

	)  
	ON (target.[DistrNomenclatureIDinIS] = source.[DistrNomenclatureIDinIS] 
		AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		
		[Marked]						= source.[Marked],
		[DistrCodeNomenclature]			= source.[DistrCodeNomenclature],
		[DistrNameNomenclature]			= source.[DistrNameNomenclature],	
		[DistrArticleNomenclature]		= source.[DistrArticleNomenclature],
		[UseInReport]					= source.[UseInReport],
		[NormNomenclatureIdInIS]		= source.[NormNomenclatureIdInIS],
		[CHECKSUMM]						= source.[CHECKSUMM],	
		[StatusID]						= source.StatusID,
		[DLM]							= @DLM
		
	WHEN NOT MATCHED THEN INSERT (

		[DistrCodeNomenclature]
		,[DistrNameNomenclature]	
		,[DistrArticleNomenclature]
		,[UseInReport]
		,[NormNomenclatureIdInIS]
		,[Marked]
		,[CHECKSUMM]
		,[CreateDate]
		,[DLM]
		,[StatusID]
		,[InformationSystemID]
		,[DistrNomenclatureIDinIS]

		) VALUES  (

		source.[DistrCodeNomenclature],
		source.[DistrNameNomenclature],	
		source.[DistrArticleNomenclature],
		source.[UseInReport],
		source.[NormNomenclatureIdInIS],
		source.[Marked],	 
		source.[CHECKSUMM],
		@CreateDate,
		@DLM,
		source.[StatusID],
		source.[InformationSystemID],
		source.[DistrNomenclatureIDinIS]
		);

END

GO
