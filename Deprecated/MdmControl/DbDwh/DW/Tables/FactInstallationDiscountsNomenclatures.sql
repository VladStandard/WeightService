CREATE TABLE [DW].[FactInstallationDiscountsNomenclatures]
(
	[DateID]			int NOT NULL,
	[Marked]			bit,
	[Posted]			bit,
	[ContragentID]		varbinary(16) NOT NULL,
	[DeliveryPlaceID]	varbinary(16),

	[_DateID]				date,
    [_ContragentID]			int,
    [_DeliveryPlaceID]		int,

	[DocNumber]			nvarchar(15),
	[DocumentDate]		datetime,
	[DateStart]			datetime  NOT NULL,
	[DateEnd]			datetime  NULL,
	[DiscountPercent]	decimal(15,6),
	[Comment]			nvarchar(1000),
    [CHECKSUMM]			BIGINT NOT NULL,

	[ID] BIGINT NOT NULL IDENTITY(-9223372036854775808,1),
	[CreateDate]			datetime NOT NULL,
	[DLM]					datetime NOT NULL,
	[StatusID]				int NOT NULL,
	[InformationSystemID]	int NOT NULL,

	PRIMARY KEY CLUSTERED (
		[InformationSystemID] ASC, 
		[DateID] ASC,
		[ContragentID] ASC,
		[DeliveryPlaceID] ASC,
		[DateStart] ASC,
		[ID] ASC
	)

 ) on [FACTFileGrooup]
