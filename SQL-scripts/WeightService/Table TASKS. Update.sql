-- Table [TASKS]. Update
declare @uid uniqueidentifier = '37FBEAC5-CE54-4E1E-9419-1DAF81160B45'
declare @enabled bit = 0
declare @update bit = 1
--
if (@update=1) begin
	update [db_scales].[TASKS] set [ENABLED]=@enabled where [UID]=@uid
end
--
select
	 [tasks].[UID] [TASK_UID]
	,[scales].[ID] [SCALE_ID]
	,[scales].[DESCRIPTION] [SCALE]
	,[types].[UID] [TASK_TYPE_UID]
	,[types].[NAME] [TASK]
	,[tasks].[ENABLED]
from [db_scales].[TASKS] [tasks]
left join [db_scales].[TASKS_TYPES] [types] on [types].[UID]=[tasks].[TASK_UID]
left join [db_scales].[SCALES] [scales] on [scales].[ID]=[tasks].[SCALE_ID]
order by [SCALE], [TASK]
