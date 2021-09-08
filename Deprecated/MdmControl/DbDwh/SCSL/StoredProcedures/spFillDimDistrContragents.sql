CREATE PROCEDURE [SCSL].[spFillDimDistrContragents]
	@DistrContragentIDInIS		varbinary(16),
	@Marked						bit,
	@NormContragentIDinIS		varbinary(16),
	@DistrCodeContragent		nvarchar(100),
	@DistrNameContragent		nvarchar(250),
	@DistrNameFullContragent	nvarchar(1024),
	@INN						nvarchar(15),
	@KPP						nvarchar(15),
	@UseInReport				int,
	@CHECKSUMM					BIGINT,
	@StatusID					int,
	@InformationSystemID		int,
	@SYS_CHANGE_VERSION			bigint,
	@SYS_CHANGE_OPERATION		nchar(1)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [SCSL].[DimDistrContragents] as target
	USING ( 
	SELECT 
		@Marked					
		,@NormContragentIDinIS	
		,@DistrCodeContragent	
		,@DistrNameContragent	
		,@DistrNameFullContragent
		,@INN					
		,@KPP					
		,@UseInReport			
		,@CHECKSUMM				
		,@StatusID			
		,@InformationSystemID
		,@DistrContragentIDInIS	

	) AS source (
		[Marked]				
		,[NormContragentIDinIS]	
		,[DistrCodeContragent]	
		,[DistrNameContragent]	
		,[DistrNameFullContragent]
		,[INN]					
		,[KPP]					
		,[UseInReport]			
		,[CHECKSUMM]
		,[StatusID]			
		,[InformationSystemID]
		,[DistrContragentIDInIS]	
	)  
	ON (target.[DistrContragentIDInIS] = source.[DistrContragentIDInIS] 
		AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		[Marked]				 = source.[Marked]
		,[NormContragentIDinIS]	 = source.[NormContragentIDinIS]
		,[DistrCodeContragent]	 = source.[DistrCodeContragent]
		,[DistrNameContragent]	 = source.[DistrNameContragent]
		,[DistrNameFullContragent] = source.[DistrNameFullContragent]
		,[INN]					 = source.[INN]
		,[KPP]					 = source.[KPP]
		,[UseInReport]			 = source.[UseInReport]
		,[CHECKSUMM]			 = source.[CHECKSUMM]
		,[StatusID]				 = source.StatusID
		,[DLM]					 = @DLM
		
	WHEN NOT MATCHED THEN INSERT (

		[Marked]				
		,[NormContragentIDinIS]	
		,[DistrCodeContragent]	
		,[DistrNameContragent]	
		,[DistrNameFullContragent]
		,[INN]					
		,[KPP]					
		,[UseInReport]			
		,[CHECKSUMM]
		,[StatusID]			
		,[InformationSystemID]
		,[DistrContragentIDInIS]	
		,[CreateDate]		
		,[DLM]				

		) VALUES  (

		source.[Marked]
		,source.[NormContragentIDinIS]
		,source.[DistrCodeContragent]
		,source.[DistrNameContragent]
		,source.[DistrNameFullContragent]
		,source.[INN]
		,source.[KPP]
		,source.[UseInReport]
		,source.[CHECKSUMM]
		,source.StatusID
		,source.[InformationSystemID]
		,source.[DistrContragentIDInIS]
		,@CreateDate
		,@DLM

		);

END
GO