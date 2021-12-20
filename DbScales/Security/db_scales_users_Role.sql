
create role [db_scales_users] authorization [dbo]
go
grant select on [db_scales].[APPS] to [db_scales_users]
go
grant select on [db_scales].[ACCESS] to [db_scales_users]
go
grant select on [db_scales].[LOGS] to [db_scales_users]
go
grant select on [db_scales].[LOG_TYPES] to [db_scales_users]
go
grant select on [db_scales].[OrderTypes] to [db_scales_users]
go
grant select on [db_scales].[Contragents] to [db_scales_users]
go
grant select on [db_scales].[Nomenclature] to [db_scales_users]
go
grant select on [db_scales].[BarCodeTypes] to [db_scales_users]
go
grant SELECT,INSERT,UPDATE, DELETE ON [db_scales].[OrderStatus] to [db_scales_users]
go
grant SELECT,INSERT,UPDATE, DELETE ON [db_scales].[Orders] to [db_scales_users]
go
grant SELECT,INSERT,UPDATE, DELETE ON [db_scales].[PLU] to [db_scales_users]
go
grant SELECT,INSERT,UPDATE, DELETE ON [db_scales].[Scales] to [db_scales_users]
go
grant SELECT,INSERT,UPDATE, DELETE ON [db_scales].[Templates] to [db_scales_users]
go
grant SELECT,INSERT,UPDATE, DELETE ON [db_scales].[WeithingFact] to [db_scales_users]
go
grant SELECT,UPDATE  ON [db_sscc].[SSCCStorage] to [db_scales_users]
go
grant SELECT,INSERT,UPDATE, DELETE ON [db_scales].[ProductSeries] to [db_scales_users]
go
---------------------------------------------------------------------
-- Added from 2020-11-26
---------------------------------------------------------------------
grant SELECT, INSERT,UPDATE, DELETE ON [db_scales].[ProductionFacility] to [db_scales_users]
go
grant SELECT, INSERT,UPDATE, DELETE ON [db_scales].[WorkShop] to [db_scales_users]
go
grant SELECT, INSERT ON [db_scales].[Labels] to [db_scales_users]
go
grant SELECT,INSERT,UPDATE,DELETE ON [db_scales].[ZebraPrinterResourceRef] to [db_scales_users]
go
grant SELECT,INSERT,UPDATE,DELETE ON [db_scales].[ZebraPrinter]	TO [db_scales_users]
go
grant select on [db_scales].[ZebraPrinterType] to [db_scales_users]
go
grant SELECT,INSERT,UPDATE,DELETE ON [db_scales].[Hosts] to [db_scales_users]
go
grant select on [db_scales].[vwProductionDynamics] to [db_scales_users]
go
---------------------------------------------------------------------
-- Added from 2021-12-20
---------------------------------------------------------------------
grant SELECT,INSERT,UPDATE, DELETE ON [db_scales].[TASKS] to [db_scales_users]
go
grant SELECT,INSERT,UPDATE, DELETE ON [db_scales].[TASKS_TYPES] to [db_scales_users]
go
grant SELECT,INSERT,UPDATE, DELETE ON [db_scales].[Organization] to [db_scales_users]
go
