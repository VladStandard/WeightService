-- Table Labels
SELECT 
	[L].[Id]
   ,[L].[CreateDate]
   ,[WF].[PluId]
   ,[WF].[ScaleId]
   ,[WF].[WeithingDate]
   ,[WF].[NetWeight]
   ,[WF].[TareWeight]
   ,[WF].[ProductDate]
   ,[WF].[RegNum]
   ,[WF].[Kneading]
   --,CONVERT(NVARCHAR(MAX), [L].[Label], 0) [Label]
   ,REPLACE(REPLACE([L].[ZPL], CHAR(13), ''), CHAR(10), '') [ZPL2]
FROM [db_scales].[Labels] [L]
LEFT JOIN [db_scales].[WeithingFact] [WF] ON [L].[WeithingFactId]=[WF].[Id]
ORDER BY [CreateDate] DESC
