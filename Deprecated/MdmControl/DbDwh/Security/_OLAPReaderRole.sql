CREATE ROLE [OLAPReaderRole]
GO
GRANT CONNECT TO [OLAPReaderRole]
GO

GRANT SELECT ON [DW].[FactAccountsReceivable] TO [OLAPReaderRole]
GO

GRANT SELECT ON [DW].[FactInstallationDiscountsNomenclatures] TO [OLAPReaderRole]
GO

GRANT SELECT ON [DW].[FactOrdersOfGoods] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[FactSalesOfGoods] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[FactBalance] TO [OLAPReaderRole]
GO

GRANT SELECT ON [DW].[FactReturns] TO [OLAPReaderRole]
GO

GRANT SELECT ON [DW].[DocJournal] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[FactReceiptOfGoods] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimOrganizations] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimBrands] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimCalendar] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimDeliveryPlaces] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimRegions] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimPriceTypes] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimTypesOfNomenclature] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimNomenclatures] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimDepartments] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimNomenclatureGroups] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[FactPrices] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimEmployees] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimContragents] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimStorages] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[DimExpenditures] TO [OLAPReaderRole]
GO

GRANT SELECT ON [DW].[DimCommercialNetwork] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[FactProductionOutput] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[FactTechnicalSpecification] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[FactProdApplications] TO [OLAPReaderRole]
GO

GRANT SELECT ON [DW].[vwDimNomenclatures] TO [OLAPReaderRole]
GO
GRANT SELECT ON [DW].[vwDimContragents] TO [OLAPReaderRole]
GO



GRANT SELECT ON [DW].[MineBaseSalesOfGoods] TO [OLAPReaderRole]
GO

GRANT EXECUTE ON [DW].[fnCalcINN] TO [OLAPReaderRole]
GO
GRANT EXECUTE ON [DW].[fnCheckINN] TO [OLAPReaderRole]
GO

GRANT SELECT ON [DW].[FactShipmentToWarehouse] TO [OLAPReaderRole]
GO

GRANT SELECT ON [DW].[FactPlannedCost] TO [OLAPReaderRole]
GO
