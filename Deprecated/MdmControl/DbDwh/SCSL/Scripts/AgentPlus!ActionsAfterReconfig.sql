------------------------------------------------------------------------------------
-- Справочник торговых точек
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference76'))
BEGIN
	ALTER TABLE [dbo].[_Reference76] ADD CONSTRAINT [PK_Reference76_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference76]'))
BEGIN
	ALTER TABLE [dbo].[_Reference76] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO


-----------------------------------------------------------------------------------------------------------
-- Справочник номенклатуры

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference52'))
BEGIN
	ALTER TABLE [dbo].[_Reference52] ADD CONSTRAINT [PK_Reference52_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference52]'))
BEGIN
	ALTER TABLE [dbo].[_Reference52] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO


-----------------------------------------------------------------------------------------------------------
--Справочник агентов

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference68'))
BEGIN
	ALTER TABLE [dbo].[_Reference68] ADD CONSTRAINT [PK_Reference68_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference68]'))
BEGIN
	ALTER TABLE [dbo].[_Reference68] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO

-----------------------------------------------------------------------------------
-- Справочник контрагентов

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference47'))
BEGIN
	ALTER TABLE [dbo].[_Reference47] ADD CONSTRAINT [PK_Reference47_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference47]'))
BEGIN
	ALTER TABLE [dbo].[_Reference47] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO


-----------------------------------------------------------------------------------
-- Справочник подразделений

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference156'))
BEGIN
	ALTER TABLE [dbo].[_Reference156] ADD CONSTRAINT [PK_Reference156_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference156]'))
BEGIN
	ALTER TABLE [dbo].[_Reference156] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO

-----------------------------------------------------------------------------------
-- Загрузка нормализованных торговых точек
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference75'))
BEGIN
	ALTER TABLE [dbo].[_Reference75] ADD CONSTRAINT [PK_Reference75_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference75]'))
BEGIN
	ALTER TABLE [dbo].[_Reference75] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO


-----------------------------------------------------------------------------------
-- Загрузка нормализованных контрагентов

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference120'))
BEGIN
	ALTER TABLE [dbo].[_Reference120] ADD CONSTRAINT [PK_Reference120_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference120]'))
BEGIN
	ALTER TABLE [dbo].[_Reference120] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO

-----------------------------------------------------------------------------------
-- Загрузка нормализованных агентов

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference67'))
BEGIN
	ALTER TABLE [dbo].[_Reference67] ADD CONSTRAINT [PK_Reference67_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference67]'))
BEGIN
	ALTER TABLE [dbo].[_Reference67] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO

-----------------------------------------------------------------------------------
-- Загрузка справочника нормализованной номенклатуры

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference124'))
BEGIN
	ALTER TABLE [dbo].[_Reference124] ADD CONSTRAINT [PK_Reference124_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference124]'))
BEGIN
	ALTER TABLE [dbo].[_Reference124] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO

-----------------------------------------------------------------------------------
-- Загрузка данных по продажам из Агент + --

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Document206'))
BEGIN
	ALTER TABLE [dbo].[_Document206] ADD CONSTRAINT [PK_Document206_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Document206]'))
BEGIN
	ALTER TABLE [dbo].[_Document206] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO


-----------------------------------------------------------------------------------
-- Загрузка данных по заказам из Агент + --

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Document196'))
BEGIN
	ALTER TABLE [dbo].[_Document196] ADD CONSTRAINT [PK_Document196_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Document196]'))
BEGIN
	ALTER TABLE [dbo].[_Document196] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO
-----------------------------------------------------------------------------------
-- Загрузка данных по возвратам из Агент + --

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Document194'))
BEGIN
	ALTER TABLE [dbo].[_Document194] ADD CONSTRAINT [PK_Document194_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Document194]'))
BEGIN
	ALTER TABLE [dbo].[_Document194] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO

