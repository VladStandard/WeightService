CREATE TABLE [DW].[FactPrices]
(
	[DateID] int NOT NULL,
	[DocNum]  nvarchar(15),
	[DocDate] datetime,
	[DocType] nvarchar(100) NOT NULL,
	[Marked]  bit,
	[Posted]  bit,

    [NomenclatureID]	varbinary(16) NOT NULL,
	[PriceTypeID]		varbinary(16) NOT NULL,
    [DeliveryPlaceID]	varbinary(16) NULL,


	[_DateID]           date,
    [_NomenclatureID]	int,
    [_PriceTypeID]		int,
    [_ContragentID]		int,
    [_DeliveryPlaceID]	int,


    [Price]		decimal(15,2) NOT NULL,
	[IsAction]  bit NULL,

	[StartDate] datetime NOT NULL,
	[EndDate]   datetime NULL,

	[ID] BIGINT NOT NULL IDENTITY(-9223372036854775808,1),
	[CreateDate] datetime NOT NULL,
	[DLM] datetime  NOT NULL,
	[StatusID] int  NOT NULL,
	[InformationSystemID] int NOT NULL,
	[CodeInIS] varbinary(16) NOT NULL,
	[_LineNo] int NOT NULL, 
    [CHECKSUMM] BIGINT NOT NULL,
	[Comment]  nvarchar(1000) 

	PRIMARY KEY CLUSTERED ([InformationSystemID] ASC, [CodeInIS] ASC, [_LineNo] ASC, [ID] ASC)
 ) on [FACTFileGrooup]
GO

CREATE NONCLUSTERED INDEX [DWFactPrices_Dim01] ON [DW].[FactPrices]
(
	[InformationSystemID] ASC,
	[DateID] ASC,
	[PriceTypeID] asc,
    [NomenclatureID] ASC,
    [DeliveryPlaceID] ASC
 ) on [FACTFileGrooup]
GO

CREATE NONCLUSTERED INDEX [DWFactPrices_Dim02] ON [DW].[FactPrices]
(
	[InformationSystemID] ASC,
	[DateID] ASC,
	[PriceTypeID] asc,
    [NomenclatureID] ASC
 ) on [FACTFileGrooup]
GO


