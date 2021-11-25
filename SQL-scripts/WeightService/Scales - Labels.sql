-- Table Labels
-- Connect from PALYCH\LUTON
SELECT
	[L].[Id]
   ,[L].[CreateDate]
   ,[S].[Description]
   ,[WF].[PluId]
   ,[WF].[ScaleId]
   ,[WF].[WeithingDate]
   ,[WF].[NetWeight]
   ,[WF].[TareWeight]
   ,[WF].[ProductDate]
   ,[WF].[RegNum]
   ,[WF].[Kneading]
   ,[L].[ZPL]
   ,REPLACE(REPLACE([L].[ZPL], CHAR(13), ''), CHAR(10), '') [ZPL_STR]
FROM [db_scales].[Labels] [L]
LEFT JOIN [db_scales].[WeithingFact] [WF]
	ON [L].[WeithingFactId] = [WF].[Id]
LEFT JOIN [db_scales].[Scales] [S]
	ON [WF].[ScaleId] = [S].[Id]
ORDER BY [CreateDate] DESC
