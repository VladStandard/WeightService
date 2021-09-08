-- Table [db_scales].[Labels]
SELECT 
	[Id]
   ,[WeithingFactId]
   ,[CreateDate]
   ,CONVERT(NVARCHAR(MAX), [Label], 0) [Label_Str]
FROM [db_scales].[Labels]
ORDER BY [CreateDate] DESC
