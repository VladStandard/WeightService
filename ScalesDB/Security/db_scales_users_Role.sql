
CREATE ROLE [db_scales_users] AUTHORIZATION [dbo]
GO

GRANT SELECT ON [db_scales].[OrderTypes] TO [db_scales_users];
GO
GRANT SELECT ON [db_scales].[Contragents] TO [db_scales_users];
GO
GRANT SELECT ON [db_scales].[Nomenclature] TO [db_scales_users];
GO
GRANT SELECT ON [db_scales].[Organization] TO [db_scales_users];
GO
GRANT SELECT ON [db_scales].[BarCodeTypes] TO [db_scales_users];
GO


GRANT SELECT,INSERT,UPDATE, DELETE ON [db_scales].[OrderStatus] TO [db_scales_users];
GO
GRANT SELECT,INSERT,UPDATE, DELETE ON [db_scales].[Orders] TO [db_scales_users];
GO
GRANT SELECT,INSERT,UPDATE, DELETE ON [db_scales].[PLU] TO [db_scales_users];
GO
GRANT SELECT,INSERT,UPDATE, DELETE ON [db_scales].[Scales] TO [db_scales_users];
GO
GRANT SELECT,INSERT,UPDATE, DELETE ON [db_scales].[Templates] TO [db_scales_users];
GO
GRANT SELECT,INSERT,UPDATE, DELETE ON [db_scales].[WeithingFact] TO [db_scales_users];
GO
GRANT SELECT,UPDATE  ON [db_sscc].[SSCCStorage] TO [db_scales_users];
GO
GRANT SELECT,INSERT,UPDATE, DELETE ON [db_scales].[ProductSeries] TO [db_scales_users];
GO
---------------------------------------------------------------------
-- Добавленные объекты с 26.11.2020
---------------------------------------------------------------------
GRANT SELECT, INSERT,UPDATE, DELETE ON [db_scales].[ProductionFacility] TO [db_scales_users];
GO
GRANT SELECT, INSERT,UPDATE, DELETE ON [db_scales].[WorkShop] TO [db_scales_users];
GO


GRANT SELECT, INSERT ON [db_scales].[Labels] TO [db_scales_users];
GO

GRANT SELECT,INSERT,UPDATE,DELETE ON [db_scales].[ZebraPrinterResourceRef] TO [db_scales_users];
GO
GRANT SELECT,INSERT,UPDATE,DELETE ON [db_scales].[ZebraPrinter]	TO [db_scales_users];
GO
GRANT SELECT ON [db_scales].[ZebraPrinterType] TO [db_scales_users];
GO
GRANT SELECT,INSERT,UPDATE,DELETE ON [db_scales].[Hosts] TO [db_scales_users];
GO


GRANT SELECT ON [db_scales].[vwProductionDynamics] TO [db_scales_users];
GO