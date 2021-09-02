CREATE PROCEDURE [SCSL].[spFillDimNormAgents]
	
	@NormCodeAgent				nvarchar(50),
	@NormNameAgent				nvarchar(150),
	@WorkStartDate				date,

	@Marked						bit,
	@CHECKSUMM					BIGINT,
	@StatusID					int  ,
	@InformationSystemID		int  ,
	@NormAgentIDInIS			binary(16),

	@SYS_CHANGE_VERSION			bigint,
	@SYS_CHANGE_OPERATION		nchar(1)



AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [SCSL].[DimNormAgents] as target
	USING ( 
	SELECT 
		@NormCodeAgent		
		,@NormNameAgent		
		,@WorkStartDate		
		,NULL --@AgentInUPP			
		,NULL --@WorkStartDateInUPP	
		,NULL --@ParentAgentID	
		
		,@Marked					
		,@CHECKSUMM				
		,@StatusID				
		,@InformationSystemID	
		,@NormAgentIDInIS	

	) AS source (

		 [NormCodeAgent]		
		,[NormNameAgent]	
		,[WorkStartDate]		
		,[AgentInUPP]		
		,[WorkStartDateInUPP]
		,[ParentAgentID]	

		,[Marked]					
		,[CHECKSUMM]				
		,[StatusID]				
		,[InformationSystemID]	
		,[NormAgentIDInIS]	

	)  
	ON (target.[NormAgentIDInIS] = source.[NormAgentIDInIS] 
		AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		
		[NormCodeAgent]			= source.[NormCodeAgent],
		[NormNameAgent]			= source.[NormNameAgent],	
		[WorkStartDate]			= source.[WorkStartDate],	
		[AgentInUPP]			= source.[AgentInUPP],		
		[WorkStartDateInUPP]	= source.[WorkStartDateInUPP],
		[ParentAgentID]			= source.[ParentAgentID],

		[Marked]				= source.[Marked],
		[CHECKSUMM]				= source.[CHECKSUMM],	
		[StatusID]				= source.[StatusID],
		[DLM]					= @DLM
		
	WHEN NOT MATCHED THEN INSERT (

		[NormCodeAgent]		
		,[NormNameAgent]		
		,[WorkStartDate]		
		,[AgentInUPP]		
		,[WorkStartDateInUPP]
		,[ParentAgentID]		

		,[Marked]
		,[CHECKSUMM]
		,[CreateDate]
		,[DLM]
		,[StatusID]
		,[InformationSystemID]
		,[NormAgentIDInIS]

		) VALUES  (

		source.[NormCodeAgent],		
		source.[NormNameAgent],		
		source.[WorkStartDate],	
		source.[AgentInUPP],		
		source.[WorkStartDateInUPP],
		source.[ParentAgentID],		

		source.[Marked],	 
		source.[CHECKSUMM],
		@CreateDate,
		@DLM,
		source.[StatusID],
		source.[InformationSystemID],
		source.[NormAgentIDInIS]

		);

END

GO
