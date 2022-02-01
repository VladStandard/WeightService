------------------------------------------------------------------------------------------------------------------------
-- Table Select Tasks
------------------------------------------------------------------------------------------------------------------------
SELECT
	[TASKS].[UID] [TASK_UID]
   ,[SCALES].[ID] [SCALE_ID]
   ,[SCALES].[DESCRIPTION] [SCALE]
   ,[TYPES].[UID] [TASK_TYPE_UID]
   ,[TYPES].[NAME] [TASK]
   ,[TASKS].[ENABLED]
FROM [DB_SCALES].[TASKS] [TASKS]
LEFT JOIN [DB_SCALES].[TASKS_TYPES] [TYPES] ON [TYPES].[UID] = [TASKS].[TASK_UID]
LEFT JOIN [DB_SCALES].[SCALES] [SCALES] ON [SCALES].[ID] = [TASKS].[SCALE_ID]
ORDER BY [SCALE], [TASK]
------------------------------------------------------------------------------------------------------------------------
