CREATE PROCEDURE [DW].[spFillInstallationDiscountsNomenclatures]

	@ContragentID		varbinary(16),
	@DeliveryPlaceID	varbinary(16),
	@DocNumber			nvarchar(15),
	@DocumentDate		datetime,
	@DateStart			datetime,
	@DateEnd			datetime,
	@DiscountPercent	decimal(15,6),
	@Comment			nvarchar(1000),
	@Marked				bit,
	@Posted				bit,

    @CHECKSUMM			BIGINT ,

	@StatusID				int,
	@InformationSystemID	int 

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();
	DECLARE @DateID int = CONVERT(INT,FORMAT(@DateStart,'yyyyMMdd'));

	MERGE [DW].[FactInstallationDiscountsNomenclatures] as target
	USING ( 
	SELECT 
		@DateID,
		@ContragentID,
		@DeliveryPlaceID,
		@DocNumber,
		@DocumentDate,
		@DateStart,
		@DateEnd,
		@DiscountPercent,
		@Comment,
		@Marked,
		@Posted,

		@CHECKSUMM,
		@CreateDate,
		@DLM,
		@StatusID,
		@InformationSystemID,

		CAST(@DateStart as date),	
		(select top(1) ID from [DW].[DimContragents] where [CodeInIS] = @ContragentID),
		(select top(1) ID from [DW].[DimDeliveryPlaces] where [CodeInIS] = @DeliveryPlaceID)

	) AS source (
		[DateID],
		[ContragentID],
		[DeliveryPlaceID],
		[DocNumber],
		[DocumentDate],
		[DateStart],
		[DateEnd],
		[DiscountPercent],
		[Comment],
		[Marked],
		[Posted],
		[CHECKSUMM],
		[CreateDate],
		[DLM],
		[StatusID],
		[InformationSystemID],
		[_DateID]			,
		[_ContragentID]		,
		[_DeliveryPlaceID]

	)  
	ON (	target.[InformationSystemID] = source.[InformationSystemID]
		AND target.[ContragentID]		= source.[ContragentID]
		AND target.[DeliveryPlaceID]	= source.[DeliveryPlaceID]
		AND target.[DateStart]			= source.[DateStart]
		AND target.[DateID]				= source.[DateID]
	) 

	WHEN MATCHED THEN  UPDATE SET 
		[DateID]								= source.DateID,
		[ContragentID]						= source.[ContragentID],
		[DeliveryPlaceID]					= source.[DeliveryPlaceID],
		[DocNumber]							= source.[DocNumber],
		[DocumentDate]						= source.[DocumentDate],
		[DateStart]							= source.[DateStart],
		[DateEnd]							= source.[DateEnd],
		[DiscountPercent]					= source.[DiscountPercent],
		[Posted]							= source.[Posted],
		[Marked]							= source.[Marked],
		[Comment]							= source.[Comment],
		[CHECKSUMM]								= source.CHECKSUMM,
		[CreateDate]							= source.CreateDate,
		[InformationSystemID]					= source.InformationSystemID,
		[StatusID]								= source.StatusID,
		[DLM]									= source.DLM,
		[_DateID]								= source.[_DateID],
		[_ContragentID]							= source.[_ContragentID],
		[_DeliveryPlaceID]						= source.[_DeliveryPlaceID]

		
	WHEN NOT MATCHED THEN INSERT (
		[DateID],
		[ContragentID],
		[DeliveryPlaceID],
		[DocNumber],
		[DocumentDate],
		[DateStart],
		[DateEnd],
		[DiscountPercent],
		[Comment],
		[Posted],
		[Marked],

		[CHECKSUMM],
		[CreateDate],
		[DLM],
		[StatusID],
		[InformationSystemID],

		[_DateID]			,
		[_ContragentID]		,
		[_DeliveryPlaceID]

		) VALUES  (
		source.[DateID],
		source.[ContragentID],
		source.[DeliveryPlaceID],
		source.[DocNumber],
		source.[DocumentDate],
		source.[DateStart],
		source.[DateEnd],
		source.[DiscountPercent],
		source.[Comment],
		source.[Posted],
		source.[Marked],

		source.[CHECKSUMM],
		source.[CreateDate],
		source.[DLM],
		source.[StatusID],
		source.[InformationSystemID],

		source.[_DateID],
		source.[_ContragentID],
		source.[_DeliveryPlaceID]
		);

END
GO

