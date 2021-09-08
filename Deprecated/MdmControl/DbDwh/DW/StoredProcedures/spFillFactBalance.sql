CREATE PROCEDURE [DW].[spFillFactBalance]
	@IsActualPeriod			bit, 
	@TotalPeriod			datetime, 
	@CurrentDate			datetime, 
	@OrgID					binary(16), 
	@NomenclatureID			binary(16), 
	@StorageID				binary(16),
	@Balance				decimal(15,3),
	@CHECKSUMM				bigint,
	@StatusID				int,
	@InformationSystemID	int 

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();
	DECLARE @DateID int = CONVERT(INT,FORMAT(@CurrentDate,'yyyyMMdd'));

	MERGE [DW].[FactBalance] as target
	USING ( 
	SELECT 
		@DateID,
		@IsActualPeriod	, 
		@TotalPeriod, 
		@CurrentDate, 
		@OrgID, 
		@NomenclatureID	, 
		@StorageID,
		@Balance,
		@CHECKSUMM,
		@CreateDate,
		@DLM,
		@StatusID,
		@InformationSystemID,

		CAST(@CurrentDate as date),
		(select top(1) ID from [DW].[DimOrganizations] where [CodeInIS] = @OrgID),
		(select top(1) ID from [DW].[DimNomenclatures] where [CodeInIS] = @NomenclatureID),
		(select top(1) ID from [DW].[DimStorages] where [CodeInIS] = @StorageID)

	) AS source (
		[DateID],
		[IsActualPeriod], 
		[TotalPeriod], 
		[CurrentDate], 
		[OrgID], 
		[NomenclatureID], 
		[StorageID],
		[Balance],
		[CHECKSUMM],
		[CreateDate],
		[DLM],
		[StatusID],
		[InformationSystemID],
		[_DateID],
		[_OrgID],
		[_NomenclatureID],
		[_StorageID]
		
	)  
	ON (	target.[InformationSystemID] = source.[InformationSystemID]
		AND target.[OrgID]				= source.[OrgID]
		AND target.[NomenclatureID]		= source.[NomenclatureID]
		AND target.[StorageID]			= source.[StorageID]
		AND target.[DateID]				= source.[DateID]
	) 

	WHEN MATCHED THEN  UPDATE SET 
		[DateID]								= @DateID,
		[IsActualPeriod]	 					= @IsActualPeriod, 
		[TotalPeriod]							= @TotalPeriod, 
		[CurrentDate] 							= @CurrentDate, 
		[OrgID] 								= @OrgID, 
		[NomenclatureID]	 					= @NomenclatureID, 
		[StorageID]								= @StorageID,
		[Balance]								= @Balance,
		[CHECKSUMM]								= @CHECKSUMM,
		[CreateDate]							= @CreateDate,
		[InformationSystemID]					= @InformationSystemID,
		[StatusID]								= @StatusID,
		[DLM]									= @DLM,

		[_DateID] 							= source._DateID, 
		[_OrgID] 							= source._OrgID, 
		[_NomenclatureID]	 				= source._NomenclatureID, 
		[_StorageID]						= source._StorageID


		
	WHEN NOT MATCHED THEN INSERT (
		[DateID],
		[IsActualPeriod], 
		[TotalPeriod], 
		[CurrentDate], 
		[OrgID], 
		[NomenclatureID], 
		[StorageID],
		[Balance],
		[CHECKSUMM],
		[CreateDate],
		[DLM],
		[StatusID],
		[InformationSystemID],

		[_DateID] 			,
		[_OrgID] 			,
		[_NomenclatureID]	,
		[_StorageID]				

		) VALUES  (
		@DateID,
		@IsActualPeriod, 
		@TotalPeriod, 
		@CurrentDate, 
		@OrgID, 
		@NomenclatureID, 
		@StorageID,
		@Balance,
		@CHECKSUMM,
		@CreateDate,
		@DLM,
		@StatusID,
		@InformationSystemID,

		source.[_DateID] 			,
		source.[_OrgID] 			,
		source.[_NomenclatureID]	,
		source.[_StorageID]				
		);

END
GO

