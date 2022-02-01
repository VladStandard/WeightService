------------------------------------------------------------------------------------------------------------------------
-- Table Select BarCodes
------------------------------------------------------------------------------------------------------------------------
SELECT
	 [BarCodes].[Id]
	,[BarCodes].[CreateDate]
	,[BarCodes].[ModifiedDate]
	,[BarCodes]..[BarCodeTypeId]
	,[BarCodeTypes].[name] [BARCODETYPES_NAME]
	,[BarCodes].[NomenclatureId]
	,[Nomenclature].[name] [NOMENCLATURE_NAME]
	,[BarCodes].[NomenclatureUnitId]
	,[NomenclatureUnits].[name] [NOMENCLATUREUNITS_NAME]
	,[BarCodes].[ContragentId]
	,[Contragents].[name] [CONTRAGENTS_NAME]
	,[BarCodes].[value]
FROM [db_scales].[BarCodes]
LEFT JOIN [db_scales].[BarCodeTypes] ON [db_scales].[BarCodes].[BarCodeTypeId] = [db_scales].[BarCodeTypes].[Id]
LEFT JOIN [db_scales].[Nomenclature] ON [db_scales].[BarCodes].[NomenclatureId] = [db_scales].[Nomenclature].[Id]
LEFT JOIN [db_scales].[NomenclatureUnits] ON [db_scales].[BarCodes].[NomenclatureUnitId] = [db_scales].[NomenclatureUnits].[Id]
LEFT JOIN [db_scales].[Contragents] ON [db_scales].[BarCodes].[ContragentId] = [db_scales].[Contragents].[Id]
ORDER BY [BarCodes].[Id]
------------------------------------------------------------------------------------------------------------------------
