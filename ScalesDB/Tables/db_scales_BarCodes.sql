CREATE TABLE [db_scales].[BarCodes]
(
	[Id]					INT  NOT NULL PRIMARY KEY IDENTITY(1,1),
	[CreateDate]			datetime NULL DEFAULT(GETDATE()),
	[ModifiedDate]			datetime NULL DEFAULT(GETDATE()), 
	[BarCodeTypeId]			INT  NOT NULL FOREIGN KEY REFERENCES [db_scales].[BarCodeTypes]  (Id),
	[NomenclatureId]		INT  NOT NULL FOREIGN KEY REFERENCES [db_scales].[Nomenclature]  (Id),
	[NomenclatureUnitId]	INT  NULL FOREIGN KEY REFERENCES [db_scales].[NomenclatureUnits] (Id),
	[ContragentId]			INT  NULL FOREIGN KEY REFERENCES [db_scales].[Contragents] (Id),
	[Value]					NVARCHAR(150) NOT NULL

) ON [ScalesFileGroup]

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Список вариантов ШК для номенклатуры',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'BarCodes',
    @level2type = NULL,
    @level2name = NULL