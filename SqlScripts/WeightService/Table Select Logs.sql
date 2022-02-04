------------------------------------------------------------------------------------------------------------------------
-- Table Select Logs
------------------------------------------------------------------------------------------------------------------------
SELECT
	[L].[UID]
   ,[L].[CREATE_DT]
   ,[H].[NAME] [HOST]
   ,[A].[NAME] [APP]
   ,[L].[VERSION]
   ,[L].[FILE]
   ,[L].[LINE]
   ,[L].[MEMBER]
   ,[LT].[ICON] [LOG_TYPE]
   ,[L].[MESSAGE]
FROM [DB_SCALES].[LOGS] [L]
LEFT JOIN [DB_SCALES].[HOSTS] [H] ON [H].[ID]=[L].[HOST_ID]
LEFT JOIN [db_scales].[APPS] [A] ON [A].[UID]=[L].[APP_UID]
LEFT JOIN [db_scales].[LOG_TYPES] [LT] ON [LT].[UID]=[L].[LOG_TYPE_UID]
ORDER BY [L].[CREATE_DT]
------------------------------------------------------------------------------------------------------------------------
