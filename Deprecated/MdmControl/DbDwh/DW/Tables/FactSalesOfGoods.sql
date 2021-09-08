CREATE TABLE [DW].[FactSalesOfGoods]
(
	
   	[DocNum] nvarchar(15),
	[DocDate] datetime,
	[DocType] nvarchar(50),
	[Marked] bit,
	[Posted] bit,

    [DateID] int NOT NULL,
    [OrgID]			  varbinary(16) NOT NULL,
    [ContragentID]    varbinary(16) NOT NULL,
    [DeliveryPlaceID] varbinary(16) NOT NULL,
    [NomenclatureID]  varbinary(16) NOT NULL,

	[_DateID]           date,
    [_OrgID]			int,
    [_ContragentID]		int,
    [_DeliveryPlaceID]	int,
    [_NomenclatureID]	int,

    [Qty]		decimal(15,3),
    [Price]		decimal(15,2),
    [Cost]		decimal(15,2),
	[VATRate]	nvarchar(10),
    [CostVAT]	decimal(15,2),
    [BasePrice]		decimal(15,2),

	[OrderID]				binary(16),
	[DiscountCondition]		nvarchar(20),
	[PercentageDiscounts]	numeric(5,2),

	[ID] BIGINT NOT NULL  IDENTITY(-9223372036854775808,1),
	[CreateDate] datetime NOT NULL,
	[DLM] datetime  NOT NULL,
	[StatusID] int  NOT NULL,
	[InformationSystemID] int NOT NULL,
	[CodeInIS] varbinary(16) NOT NULL,
	[_LineNo] int NOT NULL,
	[CHECKSUMM] BIGINT NOT NULL,
	[Active] BIT NULL DEFAULT 1, 
    PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIS] ASC,[_LineNo] ASC, [ID] ASC)


) on [FACTFileGrooup]
GO

CREATE NONCLUSTERED INDEX [DWFactSalesOfGoods_Dim01] ON [DW].[FactSalesOfGoods]
(
	[InformationSystemID] ASC,
	[DateID] ASC,
    [OrgID]	 ASC,
    [ContragentID]	 ASC,
    [DeliveryPlaceID] ASC,
    [NomenclatureID] ASC

) on [FACTFileGrooup]
GO



CREATE NONCLUSTERED INDEX [DWFactSalesOfGoods_Dim02] ON [DW].[FactSalesOfGoods]
(
	[_DateID] ASC,
    [_ContragentID]	 ASC,
    [_DeliveryPlaceID] ASC,
    [_NomenclatureID] ASC

) on [FACTFileGrooup]
GO
