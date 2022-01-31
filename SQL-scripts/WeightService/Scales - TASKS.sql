------------------------------------------------------------------------------------------------------------------------
-- Table [TASKS]
------------------------------------------------------------------------------------------------------------------------
SELECT
	[TASKS].[UID] [TASK_UID]
   ,[SCALES].[id] [SCALE_ID]
   ,[SCALES].[DESCRIPTION] [SCALE]
   ,[TYPES].[UID] [TASK_TYPE_UID]
   ,[TYPES].[NAME] [TASK]
   ,[TASKS].[ENABLED]
FROM [db_scales].[TASKS] [TASKS]
LEFT JOIN [db_scales].[TASKS_TYPES] [TYPES] ON [TYPES].[UID] = [TASKS].[TASK_UID]
LEFT JOIN [db_scales].[SCALES] [SCALES] ON [SCALES].[id] = [TASKS].[SCALE_ID]
ORDER BY [SCALE], [TASK]
------------------------------------------------------------------------------------------------------------------------
