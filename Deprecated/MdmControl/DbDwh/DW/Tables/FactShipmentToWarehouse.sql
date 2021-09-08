CREATE TABLE [DW].[FactShipmentToWarehouse]
(
	[DocNum] nvarchar(15),
	[DocDate] datetime,
	[DocType] nvarchar(50),
	[Marked] bit,
	[Posted] bit,

	[DateID]				int NOT NULL,
	[OrgInIS]				varbinary(16) NOT NULL,
	[StorageInIS]			varbinary(16) NOT NULL,
	[NomenclatureInIS]		varbinary(16) NOT NULL,
	[WeightPostInIS]		varbinary(16) NULL,

	[_DateID]				date NOT NULL,
	[_OrgID]				int NOT NULL,
	[_StorageID]			int NOT NULL,
	[_NomenclatureID]		int NOT NULL,

	[Storage]				nvarchar(200),
	[Nomenclature]			nvarchar(200),
	[WeightPost]			nvarchar(200),

	[NetTotal]				decimal(15,3),
	[GrossTotal]			decimal(15,3),
	[UnitTotal]				decimal(15,3),
	[BarCode]				nvarchar(200),

	[ID]					BIGINT NOT NULL IDENTITY(-9223372036854775808,1),
	[CreateDate]			datetime NOT NULL,
	[DLM]					datetime  NOT NULL,
	[InformationSystemID]	int NOT NULL,
	[CodeInIS]				varbinary(16) NOT NULL,

    PRIMARY KEY CLUSTERED ([InformationSystemID] ASC, [_DateID] ASC, [_NomenclatureID] ASC, [ID] ASC)

) ON [FACTFileGrooup]
GO

