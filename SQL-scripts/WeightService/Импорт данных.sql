-- Импорт данных.
-- Версия 0.0.10.
USE [ScalesDB]
SET NOCOUNT	ON
DECLARE @FILE_EXISTS INT
DECLARE @STR_COUNT INT
DECLARE @FILE NVARCHAR(255)
-- [db_scales].[AttributeDefinationList]
BEGIN TRANSACTION
BEGIN TRY
	TRUNCATE TABLE [db_scales].[AttributeDefinationList]
	BULK INSERT [db_scales].[AttributeDefinationList]
		FROM 'd:\DevSource\Job\SVN-Scales\ScalesSQL\Data\db_scales.AttributeDefinationList.csv'
		WITH (CODEPAGE = '1251', FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\n', TABLOCK)
	COMMIT TRANSACTION
	SET @STR_COUNT = (SELECT COUNT(*) FROM [db_scales].[AttributeDefinationList])
	PRINT CONVERT(CHAR(38), '[db_scales].[AttributeDefinationList]') + ' [+] Импортировано ' + CONVERT(NVARCHAR(10), @STR_COUNT) + ' строк '
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT CONVERT(CHAR(38), '[db_scales].[AttributeDefinationList]') + ' [!] Ошибка импорта, транзакция отменена!'
END CATCH
-- [db_scales].[AttributeValues]
BEGIN TRANSACTION
BEGIN TRY
	TRUNCATE TABLE [db_scales].[AttributeValues]
	BULK INSERT [db_scales].[AttributeValues]
		FROM 'd:\DevSource\Job\SVN-Scales\ScalesSQL\Data\db_scales.AttributeValues.csv'
		WITH (CODEPAGE = '1251', FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\n', TABLOCK)
	COMMIT TRANSACTION
	SET @STR_COUNT = (SELECT COUNT(*) FROM [db_scales].[AttributeValues])
	PRINT CONVERT(CHAR(38), '[db_scales].[AttributeValues]') + ' [+] Импортировано ' + CONVERT(NVARCHAR(10), @STR_COUNT) + ' строк '
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT CONVERT(CHAR(38), '[db_scales].[AttributeValues]') + ' [!] Ошибка импорта, транзакция отменена!'
END CATCH
-- [db_scales].[Orders]
BEGIN TRANSACTION
BEGIN TRY
	TRUNCATE TABLE [db_scales].[Orders]
	BULK INSERT [db_scales].[Orders]
		FROM 'd:\DevSource\Job\SVN-Scales\ScalesSQL\Data\db_scales.Orders.csv'
		WITH (CODEPAGE = '1251', FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\n', TABLOCK)
	COMMIT TRANSACTION
	SET @STR_COUNT = (SELECT COUNT(*) FROM [db_scales].[Orders])
	PRINT CONVERT(CHAR(38), '[db_scales].[Orders]') + ' [+] Импортировано ' + CONVERT(NVARCHAR(10), @STR_COUNT) + ' строк '
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT CONVERT(CHAR(38), '[db_scales].[Orders]') + ' [!] Ошибка импорта, транзакция отменена!'
END CATCH
-- [db_scales].[OrderStatus]
BEGIN TRANSACTION
BEGIN TRY
	TRUNCATE TABLE [db_scales].[OrderStatus]
	BULK INSERT [db_scales].[OrderStatus]
		FROM 'd:\DevSource\Job\SVN-Scales\ScalesSQL\Data\db_scales.OrderStatus.csv'
		WITH (CODEPAGE = '1251', FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\n', TABLOCK)
	COMMIT TRANSACTION
	SET @STR_COUNT = (SELECT COUNT(*) FROM [db_scales].[OrderStatus])
	PRINT CONVERT(CHAR(38), '[db_scales].[OrderStatus]') + ' [+] Импортировано ' + CONVERT(NVARCHAR(10), @STR_COUNT) + ' строк '
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT CONVERT(CHAR(38), '[db_scales].[OrderStatus]') + ' [!] Ошибка импорта, транзакция отменена!'
END CATCH
-- [db_scales].[OrderTypes]
BEGIN TRANSACTION
BEGIN TRY
	TRUNCATE TABLE [db_scales].[OrderTypes]
	BULK INSERT [db_scales].[OrderTypes]
		FROM 'd:\DevSource\Job\SVN-Scales\ScalesSQL\Data\db_scales.OrderTypes.csv'
		WITH (CODEPAGE = '1251', FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\n', TABLOCK)
	COMMIT TRANSACTION
	SET @STR_COUNT = (SELECT COUNT(*) FROM [db_scales].[OrderTypes])
	PRINT CONVERT(CHAR(38), '[db_scales].[OrderTypes]') + ' [+] Импортировано ' + CONVERT(NVARCHAR(10), @STR_COUNT) + ' строк '
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT CONVERT(CHAR(38), '[db_scales].[OrderTypes]') + ' [!] Ошибка импорта, транзакция отменена!'
END CATCH
-- [db_scales].[PLU]
BEGIN TRANSACTION
BEGIN TRY
	TRUNCATE TABLE [db_scales].[PLU]
	BULK INSERT [db_scales].[PLU]
		FROM 'd:\DevSource\Job\SVN-Scales\ScalesSQL\Data\db_scales.PLU.csv'
		WITH (CODEPAGE = '1251', FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\n', TABLOCK)
	COMMIT TRANSACTION
	SET @STR_COUNT = (SELECT COUNT(*) FROM [db_scales].[PLU])
	PRINT CONVERT(CHAR(38), '[db_scales].[PLU]') + ' [+] Импортировано ' + CONVERT(NVARCHAR(10), @STR_COUNT) + ' строк '
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT CONVERT(CHAR(38), '[db_scales].[PLU]') + ' [!] Ошибка импорта, транзакция отменена!'
END CATCH
-- [db_scales].[Scales]
BEGIN TRANSACTION
BEGIN TRY
	TRUNCATE TABLE [db_scales].[Scales]
	BULK INSERT [db_scales].[Scales]
		FROM 'd:\DevSource\Job\SVN-Scales\ScalesSQL\Data\db_scales.Scales.csv'
		WITH (CODEPAGE = '1251', FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\n', TABLOCK)
	COMMIT TRANSACTION
	SET @STR_COUNT = (SELECT COUNT(*) FROM [db_scales].[Scales])
	PRINT CONVERT(CHAR(38), '[db_scales].[Scales]') + ' [+] Импортировано ' + CONVERT(NVARCHAR(10), @STR_COUNT) + ' строк '
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT CONVERT(CHAR(38), '[db_scales].[Scales]') + ' [!] Ошибка импорта, транзакция отменена!'
END CATCH
-- [db_scales].[SSCCStorage]
BEGIN TRANSACTION
BEGIN TRY
	TRUNCATE TABLE [db_scales].[SSCCStorage]
	BULK INSERT [db_scales].[SSCCStorage]
		FROM 'd:\DevSource\Job\SVN-Scales\ScalesSQL\Data\db_scales.SSCCStorage.csv'
		WITH (CODEPAGE = '1251', FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\n', TABLOCK)
	COMMIT TRANSACTION
	SET @STR_COUNT = (SELECT COUNT(*) FROM [db_scales].[SSCCStorage])
	PRINT CONVERT(CHAR(38), '[db_scales].[SSCCStorage]') + ' [+] Импортировано ' + CONVERT(NVARCHAR(10), @STR_COUNT) + ' строк '
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT CONVERT(CHAR(38), '[db_scales].[SSCCStorage]') + ' [!] Ошибка импорта, транзакция отменена!'
END CATCH
-- [db_scales].[TemplateResources]
BEGIN TRANSACTION
BEGIN TRY
	TRUNCATE TABLE [db_scales].[TemplateResources]
	BULK INSERT [db_scales].[TemplateResources]
		FROM 'd:\DevSource\Job\SVN-Scales\ScalesSQL\Data\db_scales.TemplateResources.csv'
		WITH (CODEPAGE = '1251', FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\n', TABLOCK)
	COMMIT TRANSACTION
	SET @STR_COUNT = (SELECT COUNT(*) FROM [db_scales].[TemplateResources])
	PRINT CONVERT(CHAR(38), '[db_scales].[TemplateResources]') + ' [+] Импортировано ' + CONVERT(NVARCHAR(10), @STR_COUNT) + ' строк '
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT CONVERT(CHAR(38), '[db_scales].[TemplateResources]') + ' [!] Ошибка импорта, транзакция отменена!'
END CATCH
-- [db_scales].[Templates]
BEGIN TRANSACTION
BEGIN TRY
	TRUNCATE TABLE [db_scales].[Templates]
	BULK INSERT [db_scales].[Templates]
		FROM 'd:\DevSource\Job\SVN-Scales\ScalesSQL\Data\db_scales.Templates.csv'
		WITH (CODEPAGE = '1251', FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\n', TABLOCK)
	COMMIT TRANSACTION
	SET @STR_COUNT = (SELECT COUNT(*) FROM [db_scales].[Templates])
	PRINT CONVERT(CHAR(38), '[db_scales].[Templates]') + ' [+] Импортировано ' + CONVERT(NVARCHAR(10), @STR_COUNT) + ' строк '
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT CONVERT(CHAR(38), '[db_scales].[Templates]') + ' [!] Ошибка импорта, транзакция отменена!'
END CATCH
-- [db_scales].[WeithingFact]
BEGIN TRANSACTION
BEGIN TRY
	TRUNCATE TABLE [db_scales].[WeithingFact]
	BULK INSERT [db_scales].[WeithingFact]
		FROM 'd:\DevSource\Job\SVN-Scales\ScalesSQL\Data\db_scales.WeithingFact.csv'
		WITH (CODEPAGE = '1251', FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\n', TABLOCK)
	COMMIT TRANSACTION
	SET @STR_COUNT = (SELECT COUNT(*) FROM [db_scales].[WeithingFact])
	PRINT CONVERT(CHAR(38), '[db_scales].[WeithingFact]') + ' [+] Импортировано ' + CONVERT(NVARCHAR(10), @STR_COUNT) + ' строк '
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT CONVERT(CHAR(38), '[db_scales].[WeithingFact]') + ' [!] Ошибка импорта, транзакция отменена!'
END CATCH
-- [db_sscc].[SSCCStorage]
BEGIN TRANSACTION
BEGIN TRY
	TRUNCATE TABLE [db_sscc].[SSCCStorage]
	BULK INSERT [db_sscc].[SSCCStorage]
		FROM 'd:\DevSource\Job\SVN-Scales\ScalesSQL\Data\db_sscc.SSCCStorage.csv'
		WITH (CODEPAGE = '1251', FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\n', TABLOCK)
	COMMIT TRANSACTION
	SET @STR_COUNT = (SELECT COUNT(*) FROM [db_sscc].[SSCCStorage])
	PRINT CONVERT(CHAR(38), '[db_sscc].[SSCCStorage]') + ' [+] Импортировано ' + CONVERT(NVARCHAR(10), @STR_COUNT) + ' строк '
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT CONVERT(CHAR(38), '[db_sscc].[SSCCStorage]') + ' [!] Ошибка импорта, транзакция отменена!'
END CATCH
--
SET NOCOUNT	OFF
