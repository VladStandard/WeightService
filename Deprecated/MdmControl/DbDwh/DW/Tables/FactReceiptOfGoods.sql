CREATE TABLE [DW].[FactReceiptOfGoods]
(
	[DocNum] nvarchar(15),
	[DocDate] datetime,
	[DocType] nvarchar(50),
	[Marked] bit,
	[Posted] bit,

	[DateID] int NOT NULL,
    [OrgID]				varbinary(16) NOT NULL,
    [ContragentID]		varbinary(16) NOT NULL,
    [StorageID]			varbinary(16) NOT NULL,
    [NomenclatureID]	varbinary(16) NOT NULL,

	[_DateID]           date,
    [_OrgID]			int,
    [_ContragentID]		int,
    [_StorageID]	    int,
    [_NomenclatureID]	int,


    [VATRate]	varchar (10),
    [Qty]		decimal(15,3),
    [Price]		decimal(15,2),
    [Cost]		decimal(15,2),
    [CostVAT]   decimal(15,2),

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

 ) on [FACTFileGrooup]
GO

CREATE NONCLUSTERED INDEX [DWFactReceiptOfGoods_Dim01] ON [DW].[FactReceiptOfGoods]
(
	[InformationSystemID] ASC,
	[DateID] ASC,
    [OrgID]	 ASC,
    [ContragentID] ASC,
    [StorageID] ASC,
    [NomenclatureID] ASC
 ) on [FACTFileGrooup]
GO


