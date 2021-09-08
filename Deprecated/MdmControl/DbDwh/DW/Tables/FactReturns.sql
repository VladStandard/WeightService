CREATE TABLE [DW].[FactReturns]
(
	[DocNum] nvarchar(15),
	[DocDate] datetime,
	[DocType] nvarchar(50),
	[Marked] bit,
	[Posted] bit,

	[DateID]				int NOT NULL,
	[SalesCodeInIS]			varbinary(16) NOT NULL,
	[ContragentID]			varbinary(16) NOT NULL,
	[OrgID]					varbinary(16) NOT NULL,
	[DeliveryPlaceID]		varbinary(16) NOT NULL,
	[NomenclatureID]		varbinary(16) NOT NULL,

	[_DateID]           date,
    [_OrgID]			int,
    [_ContragentID]		int,
    [_DeliveryPlaceID]	int,
    [_NomenclatureID]	int,
	[_SalesCodeID]		bigint,

	[VATRate]				nvarchar (10),
	[Qty]					decimal(15,3),
	[Price]					decimal(15,2),
	[Cost]					decimal(15,2),
	[CostVAT]				decimal(15,2),
	[OrderID]				varbinary(16),

	[QtyNotFixed]					decimal(15,3),
	[PriceNotFixed]					decimal(15,2),
	[CostNotFixed]					decimal(15,2),
	[CostVATNotFixed]				decimal(15,2),

	[QtyBeforeChange]					decimal(15,3),
	[PriceBeforeChange]					decimal(15,2),
	[CostBeforeChange]					decimal(15,2),
	[CostVATBeforeChange]				decimal(15,2),



	[ID]					BIGINT NOT NULL IDENTITY(-9223372036854775808,1),
	[CreateDate]			datetime NOT NULL,
	[DLM]					datetime  NOT NULL,
	[StatusID]				int  NOT NULL,
	[InformationSystemID]	int NOT NULL,
	[CodeInIS]				varbinary(16) NOT NULL,
	[_LineNo]				int NOT NULL, 
    [CHECKSUMM]				BIGINT NOT NULL,
	[Active]				BIT NULL DEFAULT 1, 

    PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIS] ASC,[_LineNo] ASC, [ID] ASC)

) ON [FACTFileGrooup]
GO

CREATE NONCLUSTERED INDEX [FactReturns_Dim01] ON [DW].[FactReturns]
(
	[InformationSystemID] ASC,
	[DateID] ASC,
    [OrgID]	 ASC,
    [ContragentID]	 ASC,
    [DeliveryPlaceID] ASC,
    [NomenclatureID] ASC,
	[ID] ASC
)  ON [FACTFileGrooup]
GO

CREATE NONCLUSTERED INDEX [FactReturns_Dim02] ON [DW].[FactReturns]
(
	[InformationSystemID] ASC,
	[DateID] ASC,
	[SalesCodeInIS] ASC,
	[ID] ASC
) ON [FACTFileGrooup]
GO

CREATE NONCLUSTERED INDEX [FactReturns_Dim03] ON [DW].[FactReturns]
(

	[InformationSystemID] ASC,
	[SalesCodeInIS] ASC

) ON [FACTFileGrooup]
GO

