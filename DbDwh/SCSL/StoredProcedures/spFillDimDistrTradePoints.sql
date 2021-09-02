CREATE PROCEDURE [SCSL].[spFillDimDistrTradePoints]
	
	@NormTradePointIDinIS		binary(16),
--	@NormTradePointID			int,
	@DistrCodeTradePoint		nvarchar(50),
	@DistrNameTradePoint		nvarchar(500),
	@DistrAddressTradePoint		nvarchar(500),
	@UseInRerort				int,

	@Marked						bit,
	@CHECKSUMM					BIGINT,
	@StatusID					int, 
	@InformationSystemID		int,
	@DistrTradePointIDinIS		binary(16),

	@SYS_CHANGE_VERSION			bigint,
	@SYS_CHANGE_OPERATION		nchar(1)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [SCSL].[DimDistrTradePoints] as target
	USING ( 
	SELECT 
		@NormTradePointIDinIS	
		,NULL --@NormTradePointID		
		,@DistrCodeTradePoint	
		,@DistrNameTradePoint	
		,@DistrAddressTradePoint	
		,@UseInRerort			
	
		,@Marked					
		,@CHECKSUMM				
		,@StatusID				
		,@InformationSystemID	
		,@DistrTradePointIDinIS	

	) AS source (

		[NormTradePointIDinIS]
		,[NormTradePointID]		
		,[DistrCodeTradePoint]	
		,[DistrNameTradePoint]	
		,[DistrAddressTradePoint]	
		,[UseInRerort]			
	
		,[Marked]					
		,[CHECKSUMM]				
		,[StatusID]				
		,[InformationSystemID]	
		,[DistrTradePointIDinIS]	

	)  
	ON (target.[DistrTradePointIDinIS] = source.[DistrTradePointIDinIS] 
		AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		
		[NormTradePointIDinIS]			= source.[NormTradePointIDinIS],
		[NormTradePointID]				= source.[NormTradePointID],
		[DistrCodeTradePoint]			= source.[DistrCodeTradePoint],
		[DistrNameTradePoint]			= source.[DistrNameTradePoint],
		[DistrAddressTradePoint]		= source.[DistrAddressTradePoint],
		[UseInRerort]					= source.[UseInRerort],
		[Marked]						= source.[Marked],

		[CHECKSUMM]						= source.[CHECKSUMM],	
		[StatusID]						= source.[StatusID],
		[DLM]							= @DLM
		
	WHEN NOT MATCHED THEN INSERT (

		[NormTradePointIDinIS]	
		,[NormTradePointID]		
		,[DistrCodeTradePoint]	
		,[DistrNameTradePoint]	
		,[DistrAddressTradePoint]
		,[UseInRerort]			

		,[Marked]
		,[CHECKSUMM]
		,[CreateDate]
		,[DLM]
		,[StatusID]
		,[InformationSystemID]
		,[DistrTradePointIDinIS]

		) VALUES  (

		source.[NormTradePointIDinIS],
		source.[NormTradePointID],
		source.[DistrCodeTradePoint],
		source.[DistrNameTradePoint],
		source.[DistrAddressTradePoint],
		source.[UseInRerort],

		source.[Marked],	 
		source.[CHECKSUMM],
		@CreateDate,
		@DLM,
		source.[StatusID],
		source.[InformationSystemID],
		source.[DistrTradePointIDinIS]

		);

END

GO
