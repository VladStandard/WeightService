:setvar dbname "AgentP_dvi" 
-------------------------------------------------------------------------------------
USE $(dbname);

-------------------------------------------------------------------------------------
-- Справочник торговых точек
DROP FUNCTION IF EXISTS [Source1C].[fnGetDistrTradePoints];
IF EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference76]'))
BEGIN
	ALTER TABLE [dbo].[_Reference76] DISABLE CHANGE_TRACKING  
	ALTER TABLE [dbo].[_Reference76] DROP CONSTRAINT [PK_Reference76_IDRRef]
END

-------------------------------------------------------------------------------------
-- Справочник номенклатуры
DROP FUNCTION IF EXISTS [Source1C].[fnGetDistrNomenclatures];
IF EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference52]'))
BEGIN
	ALTER TABLE [dbo].[_Reference52] DISABLE CHANGE_TRACKING  
	ALTER TABLE [dbo].[_Reference52] DROP CONSTRAINT [PK_Reference52_IDRRef]
END

-------------------------------------------------------------------------------------
-- Справочник агентов
DROP FUNCTION IF EXISTS [Source1C].[fnGetDistrAgents];
IF EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference68]'))
BEGIN
	ALTER TABLE [dbo].[_Reference68] DISABLE CHANGE_TRACKING  
	ALTER TABLE [dbo].[_Reference68] DROP CONSTRAINT [PK_Reference68_IDRRef]
END

-----------------------------------------------------------------------------------
-- Справочник контрагентов
DROP FUNCTION IF EXISTS [Source1C].[fnGetDistrContragents];
IF EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference47]'))
BEGIN
	ALTER TABLE [dbo].[_Reference47] DISABLE CHANGE_TRACKING  
	ALTER TABLE [dbo].[_Reference47] DROP CONSTRAINT [PK_Reference47_IDRRef]
END

-----------------------------------------------------------------------------------
-- Справочник  нормализованных торговых точек
DROP FUNCTION IF EXISTS [Source1C].[fnGetSubdivisions];
IF EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference156]'))
BEGIN
	ALTER TABLE [dbo].[_Reference156] DISABLE CHANGE_TRACKING  
	ALTER TABLE [dbo].[_Reference156] DROP CONSTRAINT [PK_Reference156_IDRRef]
END
-----------------------------------------------------------------------------------
-- Справочник  нормализованных торговых точек
DROP FUNCTION IF EXISTS [Source1C].[fnGetTradePoints];
IF EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference75]'))
BEGIN
	ALTER TABLE [dbo].[_Reference75] DISABLE CHANGE_TRACKING  
	ALTER TABLE [dbo].[_Reference75] DROP CONSTRAINT [PK_Reference75_IDRRef]
END

-----------------------------------------------------------------------------------
-- Справочник  нормализованных контрагентов
DROP FUNCTION IF EXISTS [Source1C].[fnGetNormContragents];
IF EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference120]'))
BEGIN
	ALTER TABLE [dbo].[_Reference120] DISABLE CHANGE_TRACKING  
	ALTER TABLE [dbo].[_Reference120] DROP CONSTRAINT [PK_Reference120_IDRRef]
END


-----------------------------------------------------------------------------------
-- Справочник  нормализованных агентов
DROP FUNCTION IF EXISTS [Source1C].[fnGetNormAgents];
IF EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference67]'))
BEGIN
	ALTER TABLE [dbo].[_Reference67] DISABLE CHANGE_TRACKING  
	ALTER TABLE [dbo].[_Reference67] DROP CONSTRAINT [PK_Reference67_IDRRef]
END

-----------------------------------------------------------------------------------
-- справочника нормализованной номенклатуры
DROP FUNCTION IF EXISTS [Source1C].[fnGetNormNomenclatures];
IF EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference124]'))
BEGIN
	ALTER TABLE [dbo].[_Reference124] DISABLE CHANGE_TRACKING  
	ALTER TABLE [dbo].[_Reference124] DROP CONSTRAINT [PK_Reference124_IDRRef]
END

-----------------------------------------------------------------------------------
-- Загрузка данных по продажам из Агент +
DROP FUNCTION IF EXISTS [Source1C].[fnGetSales];
IF EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Document206]'))
BEGIN
	ALTER TABLE [dbo].[_Document206] DISABLE CHANGE_TRACKING  
	ALTER TABLE [dbo].[_Document206] DROP CONSTRAINT [PK_Document206_IDRRef]
END

-----------------------------------------------------------------------------------
-- Загрузка данных по заказам из Агент +
DROP FUNCTION IF EXISTS [Source1C].[fnGetOrders];
IF EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Document196]'))
BEGIN
	ALTER TABLE [dbo].[_Document196] DISABLE CHANGE_TRACKING  
	ALTER TABLE [dbo].[_Document196] DROP CONSTRAINT [PK_Document196_IDRRef]
END

-----------------------------------------------------------------------------------
-- Загрузка данных по возвратам из Агент +
DROP FUNCTION IF EXISTS [Source1C].[fnGetReturns];
IF EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Document194]'))
BEGIN
	ALTER TABLE [dbo].[_Document194] DISABLE CHANGE_TRACKING  
	ALTER TABLE [dbo].[_Document194] DROP CONSTRAINT [PK_Document194_IDRRef]
END


--------------------------------------------------------------------------------------
-- выключить CHANGE_TRACKING
ALTER DATABASE $(dbname) SET CHANGE_TRACKING = OFF;
--
-- удалить пользователя
USE $(dbname);
IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = N'SsisUserR') DROP USER [SsisUserR];

--
-- удалить схему
IF EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'Source1C')
BEGIN
	EXEC( 'DROP SCHEMA [Source1C]');
END

--
-- удалить login
USE [master];
IF NOT Exists (select loginname from master.dbo.syslogins where name = 'SsisUserR')
BEGIN
	DROP LOGIN [SsisUserR] 
END
