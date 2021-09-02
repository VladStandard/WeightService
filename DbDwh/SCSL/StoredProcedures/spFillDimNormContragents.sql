CREATE PROCEDURE [SCSL].[spFillDimNormContragents]

	@NormCodeContragent			nvarchar(50),
	@NormNameContragent			nvarchar(150),
	@NormNameFullContragent		nvarchar(300),
	@INN						nvarchar(50),
	@KPP						nvarchar(50),

	@Marked						bit,
	@CHECKSUMM					BIGINT,
	@StatusID					int ,
	@InformationSystemID		int,
	@NormContragentIDinIS		binary(16),

	@SYS_CHANGE_VERSION			bigint,
	@SYS_CHANGE_OPERATION		nchar(1)



AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [SCSL].[DimNormContragents] as target
	USING ( 
	SELECT 

		@NormCodeContragent,		
		@NormNameContragent	,	
		@NormNameFullContragent	,
		@INN,					
		@KPP,					
		
		@Marked,					
		@CHECKSUMM,			
		@StatusID,				
		@InformationSystemID,	
		@NormContragentIDinIS	

	) AS source (

		 [NormCodeContragent]	
		,[NormNameContragent]	
		,[NormNameFullContragent]
		,[INN]					
		,[KPP]					

		,[Marked]					
		,[CHECKSUMM]				
		,[StatusID]				
		,[InformationSystemID]	
		,[NormContragentIDinIS]	

	)  
	ON (target.[NormContragentIDinIS] = source.[NormContragentIDinIS] 
		AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		
		[NormCodeContragent]		= source.[NormCodeContragent],	
		[NormNameContragent]		= source.[NormNameContragent]	,
		[NormNameFullContragent]	= source.[NormNameFullContragent],
		[INN]						= source.[INN],					
		[KPP]						= source.[KPP],	

		[Marked]					= source.[Marked],
		[CHECKSUMM]					= source.[CHECKSUMM],	
		[StatusID]					= source.[StatusID],
		[DLM]						= @DLM
		
	WHEN NOT MATCHED THEN INSERT (

		[NormCodeContragent],	
		[NormNameContragent],	
		[NormNameFullContragent],
		[INN],					
		[KPP],					

		[Marked],
		[CHECKSUMM],
		[CreateDate],
		[DLM],
		[StatusID],
		[InformationSystemID],
		[NormContragentIDinIS]

		) VALUES  (

		source.[NormCodeContragent],	
		source.[NormNameContragent]	,
		source.[NormNameFullContragent],
		source.[INN],			
		source.[KPP],					

		source.[Marked],	 
		source.[CHECKSUMM],
		@CreateDate,
		@DLM,
		source.[StatusID],
		source.[InformationSystemID],
		source.[NormContragentIDinIS]

		);

END

GO
