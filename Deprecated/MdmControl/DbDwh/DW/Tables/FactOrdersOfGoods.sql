CREATE TABLE [DW].[FactOrdersOfGoods]
(

	[DocNum] nvarchar(15),
	[DocDate] datetime,
	[DocType] nvarchar(50),
	[Marked] bit,
	[Posted] bit,

	[DateID] int NOT NULL,
    [OrgID]				varbinary(16) NOT NULL,
    [ContragentID]		varbinary(16) NOT NULL,
    [DeliveryPlaceID]	varbinary(16) NOT NULL,
    [NomenclatureID]	varbinary(16) NOT NULL,

	[_DateID]           date,
    [_OrgID]			int,
    [_ContragentID]		int,
    [_DeliveryPlaceID]	int,
    [_NomenclatureID]	int,

    [VATRate] nvarchar (10),
    [Qty]		decimal(15,3),
    [Price]		decimal(15,2),
    [PriceVAT]  decimal(15,2),
    [Cost]		decimal(15,2),
    [CostVAT]   decimal(15,2),

	[DateOfPayment] datetime,		--Документ.ЗаказПокупателя.Реквизит.ДатаОплаты
	[OrderDate]		datetime,		--Документ.ЗаказПокупателя.Реквизит.ВС_ЗаказДата
	[ShippingDate]	datetime,		--Документ.ЗаказПокупателя.Реквизит.ДатаОтгрузки


    [PriceBase] decimal(15,2),
    [GlobeNumber] nvarchar(100),

	[ID] BIGINT NOT NULL IDENTITY(-9223372036854775808,1),
	[CreateDate] datetime NOT NULL,
	[DLM] datetime  NOT NULL,
	[StatusID] int  NOT NULL,
	[InformationSystemID] int NOT NULL,
	[CodeInIS] varbinary(16) NOT NULL,
	[_LineNo] int NOT NULL, 
    [CHECKSUMM] BIGINT NOT NULL,

	[Active] BIT NULL DEFAULT 1, 
    PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIS] ASC,[_LineNo] ASC, [ID] ASC)
)
GO

CREATE NONCLUSTERED INDEX [DWFactOrdersOfGoods_Dim01] ON [DW].[FactOrdersOfGoods]
(
	[InformationSystemID] ASC,
	[DateID] ASC,
    [OrgID]	 ASC,
    [ContragentID]	 ASC,
    [DeliveryPlaceID] ASC,
    [NomenclatureID] ASC

)
GO
