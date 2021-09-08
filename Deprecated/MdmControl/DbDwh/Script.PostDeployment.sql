/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


IF NOT EXISTS (SELECT * FROM [ETL].[Statuses]) BEGIN
INSERT INTO [ETL].[Statuses] ([StatusID],[Name])
VALUES
	(0, 'Remoted'),
	(1, 'Active'),
	(2, 'Conclusion'),
	(3, 'Archival'),
	(4, 'Draft')
END
GO


IF NOT EXISTS (SELECT * FROM [DW].[TerritorialUnits]) BEGIN
INSERT INTO [DW].[TerritorialUnits] ([TerritorialUnitID],[TerritorialUnit]) VALUES 
(1,'Центральный ФО'),
(2,'Южный ФО'),
(3,'Северо-Западный ФО'),
(4,'Дальневосточный ФО'),
(5,'Сибирский ФО'),
(6,'Уральский ФО'),
(7,'Приволжский ФО'),
(8,'Северо-Кавказский ФО'),
(9,'Не определено'),
(10,'Прочие Республики')
END
GO


IF NOT EXISTS (SELECT * FROM [ETL].[ErrorLevels]) BEGIN
	INSERT INTO [ETL].[ErrorLevels]
	([Name],[ErrorLevelID])
	VALUES
		('OFF',		0),
		('FATAL',	100),
		('ERROR',	200),
		('WARN',	300),
		('INFO',	400),
		('DEBUG',	500),
		('TRACE',	600)
END

GO

IF NOT EXISTS (SELECT * FROM [ETL].[InformationSystems]) BEGIN
	INSERT INTO [ETL].[InformationSystems] ([InformationSystemID],[Name],[StatusID])
		 VALUES
			   (7,'MDM',1),
			   (1,'1С:Мясокомбинат',1),
			   (2,'1С:Владфиш',1),
			   (3,'1С:ВС',1)
END
GO


IF NOT EXISTS (SELECT * FROM [dw].[DimCalendar]) BEGIN
	DELETE FROM [dw].[DimCalendar]
	DECLARE @StartDate datetime = CONVERT(datetime,'20150101',104)
	DECLARE @EndDate datetime = CONVERT(datetime,'20350101',104)
	EXECUTE  [dw].[spPopulateDateDimension]  @StartDate ,@EndDate
END
GO

