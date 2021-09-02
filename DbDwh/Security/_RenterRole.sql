
CREATE ROLE [RenterRole] 
GO
GRANT CONNECT TO [RenterRole]
GO


--[DW].SP
GRANT EXECUTE ON [DW].[spDeliveryPlaces] TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillBrands] TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillContragents] TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillDepartments]   TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillDimPriceTypes]  TO [RenterRole]; 
GO


GRANT EXECUTE ON [DW].[spFillDocJournal] TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillEmployees]    TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillExpenditures]   TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillFactBalance] TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillFactOrders] TO [RenterRole]; 
GO


GRANT EXECUTE ON [DW].[spFillFactPrices] TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillFactReceiptOfGoods] TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillFactSalesOfGoods] TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillNomenclatureGroups]    TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillNomenclatures] TO [RenterRole]; 
GO

GRANT EXECUTE ON [DW].[spFillOrganizations]    TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillRegionDimension]    TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillStorages]    TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillTypesOfNomenclature]    TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spPopulateDateDimension]   TO [RenterRole]; 
GO

GRANT EXECUTE ON [DW].[spFillFactAccountsReceivable]   TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillFactAccountsReceivable2]   TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spFillFactAccountsReceivableXML]   TO [RenterRole]; 
GO

GRANT EXECUTE ON [DW].[spFillInstallationDiscountsNomenclatures]   TO [RenterRole]; 
GO

GRANT EXECUTE ON [DW].[spFillCommercialNetwork]   TO [RenterRole]; 
GO

GRANT EXECUTE ON [DW].[spConsolidateContragentsRebuild]   TO [RenterRole]; 
GO

GRANT EXECUTE ON [DW].[spFillFactReturns]    TO [RenterRole]; 
GO

GRANT EXECUTE ON [DW].[spMineBaseSalesOfGoodsAsMedian] TO [RenterRole]; 
GO
GRANT EXECUTE ON [DW].[spMineBaseSalesOfGoodsAsMode] TO [RenterRole]; 
GO

GRANT EXECUTE ON [DW].[spFillFactProdApplications] TO [RenterRole]; 
GO

GRANT EXECUTE ON [DW].[spFillFactProductionOutput] TO [RenterRole]; 
GO






-- [ETL].SP
GRANT EXECUTE ON [ETL].[SetObjectStatus]  TO [RenterRole]; 
GO
GRANT EXECUTE ON [ETL].[spSetLastDate]    TO [RenterRole]; 
GO
GRANT EXECUTE ON [ETL].[spSetLastDateID]  TO [RenterRole]; 
GO
-- [ETL].FN
GRANT SELECT ON [ETL].[fnGetActiveInformationSystems()] TO [RenterRole] ;
GO
GRANT EXECUTE ON [ETL].[fnGetLastDate] TO [RenterRole] ;
GO
GRANT EXECUTE ON [ETL].[fnGetLastDateID] TO [RenterRole] ;
GO
GRANT EXECUTE ON [ETL].[fnGetLastVarID] TO [RenterRole] ;
GO

GRANT EXECUTE ON [ETL].[GetTCVarID] TO [RenterRole] ;
GO
GRANT EXECUTE ON [ETL].[SetTCVarID] TO [RenterRole] ;
GO



-- [ETL].TABLES
GRANT SELECT ON [ETL].[ErrorLevels] TO [RenterRole] ;
GO
GRANT SELECT ON [ETL].[ExchangeLogs] TO [RenterRole] ;
GO
GRANT SELECT ON [ETL].[InformationSystems] TO [RenterRole] ;
GO
GRANT SELECT ON [ETL].[ObjectStatuses] TO [RenterRole] ;
GO
GRANT SELECT ON [ETL].[Statuses] TO [RenterRole] ;
GO

GRANT EXECUTE ON [DW].[fnCalcINN] TO [RenterRole];
GO
GRANT EXECUTE ON [DW].[fnCheckINN] TO [RenterRole];
GO