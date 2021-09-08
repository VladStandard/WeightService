CREATE ROLE [TerraSoftRole]
GO

GRANT CONNECT TO [TerraSoftRole]
GO

GRANT EXECUTE ON [IIS].[fnGetContragentByID] TO [TerraSoftRole]
GO
GRANT EXECUTE ON [IIS].[fnGetContragentChangesList] TO [TerraSoftRole]
GO

GRANT EXECUTE ON [IIS].[fnGetNomenclatureByID] TO [TerraSoftRole]
GO
GRANT EXECUTE ON [IIS].[fnGetNomenclatureByCode] TO [TerraSoftRole]
GO
GRANT EXECUTE ON [IIS].[fnGetNomenclatureChangesList] TO [TerraSoftRole]
GO
GRANT EXECUTE ON [IIS].[fnGetNomenclatureList] TO [TerraSoftRole]
GO

GRANT EXECUTE ON [IIS].[fnGetSummaryList] TO [TerraSoftRole]
GO

GRANT EXECUTE ON [IIS].[fnGetShipmentChangesList] TO [TerraSoftRole]
GO
GRANT EXECUTE ON [IIS].[fnGetShipmentByID] TO [TerraSoftRole]
GO
GRANT EXECUTE ON [IIS].[fnGetSipmentListByDocDate] TO [TerraSoftRole]
GO
GRANT EXECUTE ON [IIS].[fnGetSipmentListByShippingDate] TO [TerraSoftRole]
GO

GRANT EXECUTE ON [IIS].[GetShipments] TO [TerraSoftRole]
GO
GRANT EXECUTE ON [IIS].[GetRefShipmentsByDLM] TO [TerraSoftRole]
GO
GRANT EXECUTE ON [IIS].[GetRefShipmentsById] TO [TerraSoftRole]
GO
GRANT EXECUTE ON [IIS].[GetRefShipmentsByDocDate] TO [TerraSoftRole]
GO

GRANT EXECUTE ON [DW].[fnCalcINN] TO [TerraSoftRole]
GO
GRANT EXECUTE ON [DW].[fnCheckINN] TO [TerraSoftRole]
GO
