CREATE PROCEDURE [SCSL].[spFillDimNormTradePoints]

	@NormCodeTradePoint				nvarchar(50)  ,
	@NormAddressTradePoint			nvarchar(500) ,
	@NormNameTradePoint				nvarchar(500) ,
	@TradeAgent						nvarchar(250) ,
	@ChainName						nvarchar(250) ,
	@SalesChannel					nvarchar(250) ,
	@FormatTT						nvarchar(250) ,
	--@RegionID						int = NULL,
	--@CityID							int = NULL,
	--@SalesChannelID					int = NULL,
	--@ChainID						int = NULL,

	@Marked						bit,
	@CHECKSUMM					BIGINT,
	@StatusID					int ,
	@InformationSystemID		int,
	@NormTradePointIDinIS			binary(16),

	@SYS_CHANGE_VERSION			bigint,
	@SYS_CHANGE_OPERATION		nchar(1)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [SCSL].[DimNormTradePoints] as target
	USING ( 
	SELECT 

		@NormCodeTradePoint	,	
		@NormAddressTradePoint	,
		@NormNameTradePoint	,	
		@TradeAgent			,	
		@ChainName			,	
		@SalesChannel		,	
		@FormatTT			,
		NULL, --@RegionID			,	
		NULL, --@CityID				,	
		NULL, --@SalesChannelID		,	
		NULL, --@ChainID			,	

		@Marked,					
		@CHECKSUMM,			
		@StatusID,				
		@InformationSystemID,	
		@NormTradePointIDinIS

	) AS source (

		[NormCodeTradePoint]	,
		[NormAddressTradePoint]	,
		[NormNameTradePoint]	,
		[TradeAgent]			,
		[ChainName]				,
		[SalesChannel]			,
		[FormatTT]				,
		[RegionID]				,
		[CityID]				,
		[SalesChannelID]		,
		[ChainID]				,

		[Marked],					
		[CHECKSUMM],				
		[StatusID]	,			
		[InformationSystemID],	
		[NormTradePointIDinIS]	

	)  
	ON (target.[NormTradePointIDinIS]  = source.[NormTradePointIDinIS] 
		AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		
		[NormCodeTradePoint]			= source.[NormCodeTradePoint]	,
		[NormAddressTradePoint]			= source.[NormAddressTradePoint],	
		[NormNameTradePoint]			= source.[NormNameTradePoint]	,	
		[TradeAgent]					= source.[TradeAgent]			,
		[ChainName]						= source.[ChainName]			,		
		[SalesChannel]					= source.[SalesChannel]			,
		[RegionID]						= source.[RegionID]				,
		[CityID]						= source.[CityID]				,
		[SalesChannelID]				= source.[SalesChannelID]		,
		[ChainID]						= source.[ChainID]				,
		[FormatTT]						= source.[FormatTT]				,
		
		[Marked]					= source.[Marked],
		[CHECKSUMM]					= source.[CHECKSUMM],	
		[StatusID]					= source.[StatusID],
		[DLM]						= @DLM
		
	WHEN NOT MATCHED THEN INSERT (

		[NormCodeTradePoint]	,
		[NormAddressTradePoint]	,
		[NormNameTradePoint]	,
		[TradeAgent]			,
		[ChainName]				,
		[SalesChannel]			,
		[RegionID]				,
		[CityID]				,
		[SalesChannelID]		,
		[ChainID]				,
		[FormatTT]				,

		[Marked],
		[CHECKSUMM],
		[CreateDate],
		[DLM],
		[StatusID],
		[InformationSystemID],
		[NormTradePointIDinIS]

		) VALUES  (

		source.[NormCodeTradePoint]		,
		source.[NormAddressTradePoint]	,
		source.[NormNameTradePoint]		,
		source.[TradeAgent]				,
		source.[ChainName]				,
		source.[SalesChannel]			,
		source.[RegionID]				,
		source.[CityID]					,
		source.[SalesChannelID]			,
		source.[ChainID]				,
		source.[FormatTT]				,

		source.[Marked],	 
		source.[CHECKSUMM],
		@CreateDate,
		@DLM,
		source.[StatusID],
		source.[InformationSystemID],
		source.[NormTradePointIDinIS]

		);

END

GO
