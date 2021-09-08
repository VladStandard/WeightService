CREATE PROCEDURE [SCSL].[spFillDimSubdivisions]

	@SubdivisionCode			nvarchar(50),
	@SubdivisionName			nvarchar(150),
	@UseInReport				int NULL,
	--@ConsolidatedClientID			int NULL,
	--@EmployeeID					int NULL,

	@Marked						bit,
	@CHECKSUMM					BIGINT,
	@StatusID					int ,
	@InformationSystemID		int,
	@SubdivisionIDinIS			binary(16),

	@SYS_CHANGE_VERSION			bigint,
	@SYS_CHANGE_OPERATION		nchar(1)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [SCSL].[DimSubdivisions] as target
	USING ( 
	SELECT 

		@SubdivisionCode		,
		@SubdivisionName		,
		@UseInReport			,
		NULL,--@ConsolidatedClientID	,
		NULL,--@EmployeeID				,

		@Marked,					
		@CHECKSUMM,			
		@StatusID,				
		@InformationSystemID,	
		@SubdivisionIDinIS

	) AS source (

		[SubdivisionCode]		,
		[SubdivisionName]		,
		[UseInReport]			,
		[ConsolidatedClientID]	,
		[EmployeeID]			,

		[Marked]				,					
		[CHECKSUMM]				,				
		[StatusID]				,			
		[InformationSystemID]	,	
		[SubdivisionIDinIS]	

	)  
	ON (target.[SubdivisionIDinIS]  = source.[SubdivisionIDinIS] 
		AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		
		[SubdivisionCode]				= source.[SubdivisionCode]		,
		[SubdivisionName]				= source.[SubdivisionName]		,	
		[UseInReport]					= source.[UseInReport]			,	
		[ConsolidatedClientID]			= source.[ConsolidatedClientID]	,
		[EmployeeID]					= source.[EmployeeID]			,		
		
		[Marked]						= source.[Marked]				,
		[CHECKSUMM]						= source.[CHECKSUMM]			,	
		[StatusID]						= source.[StatusID]				,
		[DLM]							= @DLM
		
	WHEN NOT MATCHED THEN INSERT (

		[SubdivisionCode]		,
		[SubdivisionName]		,
		[UseInReport]			,
		[ConsolidatedClientID]	,
		[EmployeeID]			,

		[Marked]				,
		[CHECKSUMM]				,
		[CreateDate]			,
		[DLM]					,
		[StatusID]				,
		[InformationSystemID]	,
		[SubdivisionIDinIS]

		) VALUES  (

		source.[SubdivisionCode]		,
		source.[SubdivisionName]		,
		source.[UseInReport]			,
		source.[ConsolidatedClientID]	,
		source.[EmployeeID]				,

		source.[Marked]					,	 
		source.[CHECKSUMM]				,
		@CreateDate						,
		@DLM							,
		source.[StatusID]				,
		source.[InformationSystemID]	,
		source.[SubdivisionIDinIS]

		);

END

GO
