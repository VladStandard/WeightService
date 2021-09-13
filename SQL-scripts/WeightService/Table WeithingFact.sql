-- Таблица WeithingFact
SELECT
	[Id]
   ,[PluId]
   ,[ScaleId]
   ,[SeriesId]
   ,[OrderId]
   ,[SSCC]
   ,[WeithingDate]
   ,[NetWeight]
   ,[TareWeight]
   ,[UUID]
   ,[ProductDate]
   ,[RegNum]
   ,[Kneading]
FROM [db_scales].[WeithingFact]
ORDER BY [WeithingDate] DESC
