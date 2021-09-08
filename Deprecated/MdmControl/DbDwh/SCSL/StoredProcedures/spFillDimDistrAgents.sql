CREATE PROCEDURE [SCSL].[spFillDimDistrAgents]

	@Marked				bit,
	@NormAgentIDInIS	binary(16),
	@DistrAgentCode		nvarchar(50),
	@DistrAgentName		nvarchar(150),
	@UseInReport		int,

	@CHECKSUMM				BIGINT,
	@StatusID				int,
	@InformationSystemID	int,
	@DistrAgentIDInIS		varbinary(16),
	@SYS_CHANGE_VERSION		bigint,
	@SYS_CHANGE_OPERATION	nchar(1)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [SCSL].[DimDistrAgents] as target
	USING ( 
	SELECT 
		 @Marked				
		,@NormAgentIDInIS	
		,@DistrAgentCode		
		,@DistrAgentName		
		,@UseInReport		
		,@CHECKSUMM			
		,@StatusID			
		,@InformationSystemID
		,@DistrAgentIDInIS			

	) AS source (
		 [Marked]				
		,[NormAgentIDInIS]	
		,[DistrAgentCode]		
		,[DistrAgentName]		
		,[UseInReport]	
		,[CHECKSUMM]			
		,[StatusID]			
		,[InformationSystemID]
		,[DistrAgentIDInIS]			
	)  
	ON (target.[DistrAgentIDInIS] = source.[DistrAgentIDInIS] AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		
		[Marked]		  = source.[Marked],
		[NormAgentIDInIS] = source.[NormAgentIDInIS],
		[DistrAgentCode]  = source.DistrAgentCode,
		[DistrAgentName]  = source.DistrAgentName,
		[UseInReport]	  = source.[UseInReport],
		[CHECKSUMM]		  = source.[CHECKSUMM],	
		[StatusID]		  = source.StatusID,
		[DLM]			  = @DLM
		
	WHEN NOT MATCHED THEN INSERT (
			[Marked]		 ,	 
			[NormAgentIDInIS],
			[DistrAgentCode] ,
			[DistrAgentName] ,
			[UseInReport]	 ,
			[CHECKSUMM]		 ,
			[CreateDate]	 ,
			[DLM],
			[StatusID],
			[InformationSystemID],
			[DistrAgentIDInIS]
		) VALUES  (
			source.[Marked]	,	 
			source.[NormAgentIDInIS],
			source.[DistrAgentCode] ,
			source.[DistrAgentName] ,
			source.[UseInReport]	 ,
			source.[CHECKSUMM]		 ,
			@CreateDate,
			@DLM,
			source.[StatusID],
			source.[InformationSystemID],
			source.[DistrAgentIDInIS]
		);

END

GO
