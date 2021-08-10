// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace WeightCore.Utils
{
    public static class SqlQueries
    {
        public static string GetInstance => @"
select serverproperty('InstanceName') [InstanceName]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string GetHostId => @"
select [ID]
from [db_scales].[Hosts] 
where [Name]=@host and [IdRRef]=@idrref
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string GetScaleId => @"
select [ID]
from [db_scales].[SCALES]
where [DESCRIPTION]=@scale
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string GetTaskUid => @"
select [tasks].[UID]
from [db_scales].[TASKS] [tasks]
left join [db_scales].[TASKS_TYPES] [types] on [types].[UID]=[tasks].[TASK_UID]
where [types].[NAME]=@task_type
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string GetTaskTypeUid => @"
select [UID]
from [db_scales].[TASKS_TYPES] 
where [NAME]=@task_type
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string GetTasks => @"
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
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string GetTask => @"
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
where [types].[UID]=@task_type_uid and [scales].[Id]=@scale_id
order by [SCALE], [TASK]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string AddApp => @"
if not exists (select 1 from [db_scales].[APPS] where [NAME]=@app) begin
	insert into [db_scales].[APPS]([NAME]) values(@app)
end
select [UID]
from [db_scales].[APPS]
where [NAME]=@app
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string AddLog => @"
declare @log_type_uid uniqueidentifier = (select [UID] from [db_scales].[LOG_TYPES] where [NUMBER]=@logNumber)
insert into [db_scales].[LOGS]([HOST_ID],[APP_UID],[VERSION],[FILE],[LINE],[MEMBER],[LOG_TYPE_UID],[MESSAGE]) 
values (@hostId,@appUid,@version,@file,@line,@member,@log_type_uid,@message)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string AddLogType => @"
insert into [db_scales].[LOG_TYPES]([NUMBER],[ICON]) 
values (@number,@icon)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string IsTaskExists => @"
if not exists (select 1 from [db_scales].[TASKS] where [TASK_UID]=@task_type_uid and [SCALE_ID]=@scale_id) begin
    select [UID] from [db_scales].[TASKS] where [TASK_UID]=@task_type_uid and [SCALE_ID]=@scale_id
end
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string AddTask => @"
insert into [db_scales].[TASKS]([TASK_UID],[SCALE_ID],[ENABLED])
values(@task_type_uid,@scale_id,@enabled)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string AddTaskType => @"
insert into [db_scales].[TASKS_TYPES]([NAME])
values(@name)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");
    }
}