CREATE TABLE [STG].[FactShipmentToWarehouse]
(
	[DocNum]				nvarchar(15),
	[DocDate]				datetime,
	[DocType]				nvarchar(50),
	[Marked]				bit,
	[Posted]				bit,

	[DateID]				int NOT NULL,
	[OrgInIS]				varbinary(16) NOT NULL,
	[StorageInIS]			varbinary(16) NOT NULL,
	[NomenclatureInIS]		varbinary(16) NOT NULL,
	[WeightPostInIS]		varbinary(16) NULL,

	[_DateID]				date NULL,
	[_OrgID]				int  NULL,
	[_StorageID]			int  NULL,
	[_NomenclatureID]		int  NULL,

	[Storage]				nvarchar(200),
	[Nomenclature]			nvarchar(200),
	[WeightPost]			nvarchar(200),

	[NetTotal]				decimal(15,3),
	[GrossTotal]			decimal(15,3),
	[UnitTotal]				decimal(15,3),
	[BarCode]				nvarchar(200),

	[InformationSystemID]	int NOT NULL,
	[CodeInIS]				varbinary(16) NOT NULL,

    PRIMARY KEY ([InformationSystemID] ASC, [CodeInIS] ASC,  NomenclatureInIS ASC)

) ON [FACTFileGrooup]
GO

