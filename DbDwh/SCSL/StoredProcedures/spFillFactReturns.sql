CREATE PROCEDURE [SCSL].[spFillFactReturns]

	@DocumentNumber					nvarchar(50) NULL,
	@DocumentDate					date NULL,
	@DistrTradePointIDinIS			binary(16) NULL,
	@DistrNomenclatureIDinIS		binary(16) NULL,
	@SubdivisionIDinIS				binary(16) NULL,
	@AgentDistrIDinIS				binary(16) NULL,
	@ContragentIDinIS				binary(16) NULL,
	--@TradePointID					int NULL,
	--@NomenclatureID					int NULL,
	--@SubdivisionID					int NULL,
	--@AgentID						int NULL,
	--@ContragentID					int NULL,
	@Qty							decimal(15, 3) NULL,
	@Price							decimal(15, 3) NULL,
	@Cost							decimal(15, 3) NULL,
	@Volume							decimal(15, 3) NULL,
	@UseNDS							int NULL,
	@PriceWithNDS					int NULL,

	@Marked						bit,
	@Posted						bit,
	@CHECKSUMM					BIGINT,
	@StatusID					int ,
	@InformationSystemID		int,
	@IDinIS						binary(16),
	@LineNo						int,

	@SYS_CHANGE_VERSION			bigint,
	@SYS_CHANGE_OPERATION		nchar(1)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();
	DECLARE @DateID int = CONVERT(INT,FORMAT(@DocumentDate,'yyyyMMdd'));

	DECLARE 
		@TradePointID int
		,@NomenclatureID	int
		,@SubdivisionID	int
		,@AgentID int
		,@ContragentID int

	select TOP 1 @TradePointID = NormTradePointID 
	from [SCSL].[DimDistrTradePoints]
	where [DistrTradePointIDinIS] = @DistrTradePointIDinIS

	select TOP 1 @NomenclatureID = NormNomenclatureIdInIS 
	from [SCSL].[DimDistrNomenclatures]
	where [DistrCodeNomenclature] = @DistrNomenclatureIDinIS

	select TOP 1 @ContragentID = [NormContragentID] 
	from [SCSL].[DimDistrContragents]
	where [DistrContragentIDInIS] = @ContragentIDinIS

	select TOP 1 @AgentID = [NormAgentID] 
	from [SCSL].[DimDistrAgents]
	where [DistrAgentIDInIS] = @AgentDistrIDinIS

	select TOP 1 @SubdivisionID = [SubdivisionID] 
	from [SCSL].[DimSubdivisions]
	where [SubdivisionIDinIS] = @SubdivisionIDinIS

	MERGE [SCSL].[FactReturns] as target
	USING ( 
	SELECT 
		@DocumentNumber			,
		@DocumentDate			,
		@DistrTradePointIDinIS	,
		@DistrNomenclatureIDinIS,
		@SubdivisionIDinIS		,
		@AgentDistrIDinIS		,
		@ContragentIDinIS		,
		@TradePointID			,
		@NomenclatureID			,
		@SubdivisionID			,
		@AgentID				,
		@ContragentID			,
		@Qty					,
		@Price					,
		@Cost					,
		@Volume					,
		@UseNDS					,
		@PriceWithNDS			,

		@Marked					,
		@Posted					,
		@CHECKSUMM				,			
		@StatusID				,				
		@InformationSystemID	,	
		@IDinIS,
		@LineNo


	) AS source (

		[DocumentNumber]			,
		[DocumentDate]				,
		[DistrTradePointIDinIS]		,
		[DistrNomenclatureIDinIS]	,
		[SubdivisionIDinIS]			,
		[AgentDistrIDinIS]			,
		[ContragentIDinIS]			,
		[TradePointID]				,
		[NomenclatureID]			,
		[SubdivisionID]				,
		[AgentID]					,
		[ContragentID]				,
		[Qty]						,
		[Price]						,
		[Cost]						,
		[Volume]					,
		[UseNDS]					,
		[PriceWithNDS]				,

		[Marked]					,		
		[Posted]					,
		[CHECKSUMM]					,				
		[StatusID]					,			
		[InformationSystemID]		,	
		[IDinIS],
		[_LineNo] 	

	)  
	ON (target.[IDinIS]  = source.[IDinIS] 
		AND target.[InformationSystemID] = source.[InformationSystemID]) 
		AND target.[LineNo] = source.[_LineNo] 


	WHEN MATCHED THEN  UPDATE SET 
		
		[DocumentNumber]					= source.[DocumentNumber]			,
		[DocumentDate]						= source.[DocumentDate]				,
		[DistrTradePointIDinIS]				= source.[DistrTradePointIDinIS]	,
		[DistrNomenclatureIDinIS]			= source.[DistrNomenclatureIDinIS]	,	
		[SubdivisionIDinIS]					= source.[SubdivisionIDinIS]		,	
		[AgentDistrIDinIS]					= source.[AgentDistrIDinIS]			,	
		[ContragentIDinIS]					= source.[ContragentIDinIS]			,
		[TradePointID]						= source.[TradePointID]				,
		[NomenclatureID]					= source.[NomenclatureID]			,
		[SubdivisionID]						= source.[SubdivisionID]			,
		[AgentID]							= source.[AgentID]					,	
		[ContragentID]						= source.[ContragentID]				,
		[Qty]								= source.[Qty]						,
		[Price]								= source.[Price]					,
		[Cost]								= source.[Cost]						,	
		[Volume]							= source.[Volume]					,
		[UseNDS]							= source.[UseNDS]					,
		[PriceWithNDS]						= source.[PriceWithNDS]				,
		
		[Marked]							= source.[Marked]					,
		[Posted]							= source.[Posted]					,
		[CHECKSUMM]							= source.[CHECKSUMM]				,	
		[StatusID]							= source.[StatusID]					,
		[DLM]								= @DLM,
		[DateID]							= @DateID,
		[Active]							= 1

	WHEN NOT MATCHED THEN INSERT (

		[DocumentNumber]			,
		[DocumentDate]				,
		[DistrTradePointIDinIS]		,
		[DistrNomenclatureIDinIS]	,
		[SubdivisionIDinIS]			,
		[AgentDistrIDinIS]			,
		[ContragentIDinIS]			,
		[TradePointID]				,
		[NomenclatureID]			,
		[SubdivisionID]				,
		[AgentID]					,
		[ContragentID]				,
		[Qty]						,
		[Price]						,
		[Cost]						,
		[Volume]					,
		[UseNDS]					,
		[PriceWithNDS]				,

		[Marked]					,
		[Posted]					,
		[CHECKSUMM]					,
		[CreateDate]				,
		[DLM]						,
		[StatusID]					,
		[InformationSystemID]		,
		[IDinIS],
		[DateID],
		[LineNo],
		[Active]

		) VALUES  (

		source.[DocumentNumber]				,
		source.[DocumentDate]				,
		source.[DistrTradePointIDinIS]		,
		source.[DistrNomenclatureIDinIS]	,
		source.[SubdivisionIDinIS]			,
		source.[AgentDistrIDinIS]			,
		source.[ContragentIDinIS]			,
		source.[TradePointID]				,
		source.[NomenclatureID]				,
		source.[SubdivisionID]				,
		source.[AgentID]					,
		source.[ContragentID]				,
		source.[Qty]						,
		source.[Price]						,
		source.[Cost]						,
		source.[Volume]						,
		source.[UseNDS]						,
		source.[PriceWithNDS]				,

		source.[Marked]					,	
		source.[Posted]					,
		source.[CHECKSUMM]				,
		@CreateDate						,
		@DLM							,
		source.[StatusID]				,
		source.[InformationSystemID]	,
		source.[IDinIS],
		@DateID,
		source.[_LineNo],
		1
		);

END

GO
