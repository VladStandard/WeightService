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

        #region Apps
        
        public static string AddApp => @"
if not exists (select 1 from [db_scales].[APPS] where [NAME]=@app) begin
	insert into [db_scales].[APPS]([NAME]) values(@app)
end
select [UID]
from [db_scales].[APPS]
where [NAME]=@app
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " "); 
        
        #endregion

        #region Hosts
        
        public static string GetHostId => @"
select [ID]
from [db_scales].[Hosts] 
where [Name]=@host and [IdRRef]=@idrref
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string GetHostByUid => @"
select
	 [HOSTS].[ID]
	,[HOSTS].[NAME]
	,[HOSTS].[IP]
	,[HOSTS].[MAC]
	,[HOSTS].[IDRREF]
	,[HOSTS].[MARKED]
	,[HOSTS].[SETTINGSFILE]
	,[SCALES].[ID] [SCALE_ID]
	,[SCALES].[DESCRIPTION] [SCALE_DESCRIPTION]
from [db_scales].[HOSTS] [HOSTS]
left join [db_scales].[SCALES] [SCALES] on [HOSTS].[ID] = [SCALES].[HOSTID]
where [HOSTS].[MARKED]=0 and [HOSTS].[IDRREF]=@idrref
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        #endregion

        #region Logs

        public static string AddLog => @"
declare @log_type_uid uniqueidentifier = (select [UID] from [db_scales].[LOG_TYPES] where [NUMBER]=@logNumber)
insert into [db_scales].[LOGS]([HOST_ID],[APP_UID],[VERSION],[FILE],[LINE],[MEMBER],[LOG_TYPE_UID],[MESSAGE]) 
values (@hostId,@appUid,@version,@file,@line,@member,@log_type_uid,@message)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string AddLogType => @"
insert into [db_scales].[LOG_TYPES]([NUMBER],[ICON]) 
values (@number,@icon)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " "); 
        
        #endregion

        #region Scales
        
        public static string GetScaleId => @"
select [ID]
from [db_scales].[SCALES]
where [DESCRIPTION]=@scale
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string GetScaleDescription => @"
select [DESCRIPTION]
from [db_scales].[SCALES]
where [ID]=@scale_id
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string GetScaleById => @"
select
	 [s].[Id]
	,[s].[CreateDate]
	,[s].[ModifiedDate]
	,[s].[OrganizationId]
	,[s].[Description]
	,[s].[DeviceIP]
	,[s].[DevicePort]
	,[s].[DeviceMAC]
	,[s].[DeviceSendTimeout]
	,[s].[DeviceReceiveTimeout]
	,[s].[DeviceComPort]
	,[s].[ZebraIP]
	,[s].[ZebraPort]
	,[s].[ZebraPrinterId]
	,[s].[UseOrder]
	,[s].[VerScalesUI]
	,[s].[DeviceNumber]
	,[s].[TemplateIdDefault]
	,[s].[TemplateIdSeries]
	,[s].[ScaleFactor]
	,[s].[Marked]
	,[s].[HostId]
	,[lt].[ICON]
from [db_scales].[Scales] [s]
left join [db_scales].[LOG_TYPES] [lt] on [lt].[UID]=[s].[LOG_TYPE_UID]
where [Id]=@id
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        #endregion

        #region TaskTypes

        public static string GetTaskTypeUid => @"
select [UID]
from [db_scales].[TASKS_TYPES] 
where [NAME]=@task_type
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string GetTasksTypes => @"
select
	 [UID]
	,[NAME]
from [db_scales].[TASKS_TYPES]
order by [NAME]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string GetTasksTypesByName => @"
select
	 [UID]
	,[NAME]
from [db_scales].[TASKS_TYPES]
where [NAME]=@task_name
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string GetTasksTypesByUid => @"
select
	 [UID]
	,[NAME]
from [db_scales].[TASKS_TYPES]
where [UID]=@task_uid
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string AddTaskType => @"
insert into [db_scales].[TASKS_TYPES]([NAME])
values(@name)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        #endregion

        #region Tasks

        public static string GetTaskUid => @"
select [tasks].[UID]
from [db_scales].[TASKS] [tasks]
left join [db_scales].[TASKS_TYPES] [types] on [types].[UID]=[tasks].[TASK_UID]
where [types].[NAME]=@task_type
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

        public static string GetTaskByTypeAndScale => @"
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

        public static string GetTaskByUid => @"
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
where [tasks].[UID]=@task_uid
order by [SCALE], [TASK]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        public static string AddTask => @"
insert into [db_scales].[TASKS]([TASK_UID],[SCALE_ID],[ENABLED])
values(@task_type_uid,@scale_id,@enabled)
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t').Replace(Environment.NewLine, " ");

        #endregion
    }
}