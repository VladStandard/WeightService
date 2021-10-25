// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataProjectsCore.DAL
{
    public static class SqlQueries
    {
        public static class DbSystem
        {
            public static class Properties
            {
                public static string GetInstance => @"
select serverproperty('InstanceName') [InstanceName]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
            }
        }

        public static class DbServiceManaging
        {
            public static class Tables
            {
                public static class Access
                {
                    public static string GetAccess => @"
-- Table Access diagram summary
select
	 [UID]
	,[CREATE_DT]
	,[CHANGE_DT]
	,[USER]
	,[LEVEL]
from [db_scales].[ACCESS]
order by [USER] desc
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetAccessUser(string userName) => @$"
select
	 [UID]
	,[CREATE_DT]
	,[CHANGE_DT]
	,[USER]
	,[LEVEL]
from [db_scales].[ACCESS]
where [USER]=N'{userName}'
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class Apps
                {
                    public static string AddApp => @"
if not exists (select 1 from [db_scales].[APPS] where [NAME]=@app) begin
	insert into [db_scales].[APPS]([NAME]) values(@app)
end
select [UID]
from [db_scales].[APPS]
where [NAME]=@app
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class Logs
                {
                    public static string AddLog => @"
declare @log_type_uid uniqueidentifier = (select [UID] from [db_scales].[LOG_TYPES] where [NUMBER]=@logNumber)
insert into [db_scales].[LOGS]([HOST_ID],[APP_UID],[VERSION],[FILE],[LINE],[MEMBER],[LOG_TYPE_UID],[MESSAGE]) 
values (@hostId,@appUid,@version,@file,@line,@member,@log_type_uid,@message)
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string AddLogType => @"
insert into [db_scales].[LOG_TYPES]([NUMBER],[ICON]) 
values (@number,@icon)
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetLogTypes => @"
-- Table LOG_TYPES
select 
	 [UID]
	,[NUMBER]
	,[ICON]
from [db_scales].[LOG_TYPES]
order by [NUMBER]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetLogs => @"
-- Table LOGS diagram summary
select 
	 [l].[UID]
	,[l].[CREATE_DT]
	,[s].[Description] [SCALE]
	,[h].[Name] [HOST]
	,[a].[NAME] [APP]
	,[l].[VERSION]
	,[l].[FILE]
	,[l].[LINE]
	,[l].[MEMBER]
	,[lt].[ICON]
	,[l].[MESSAGE]
from [db_scales].[LOGS] [l]
left join [db_scales].[Hosts] [h] on [h].[ID]=[l].[HOST_ID]
left join [db_scales].[Scales] [s] on [s].[HostId]=[h].[ID]
left join [db_scales].[APPS] [a] on [a].[UID]=[l].[APP_UID]
left join [db_scales].[LOG_TYPES] [lt] on [lt].[UID]=[l].[LOG_TYPE_UID]
order by [l].[CREATE_DT] desc
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }
            }
        }

        public static class DbScales
        {
            public static class Tables
            {
                public static class Hosts
                {
                    public static string GetBusyHosts => @"
-- Get busy hosts
select [h].[Id]
      ,[h].[CreateDate]
      ,[h].[ModifiedDate]
      ,[h].[Name]
      ,[s].[Description]
      ,[h].[IP]
      ,[h].[MAC]
      ,[h].[IdRRef]
      ,[h].[Marked]
      ,[h].[SettingsFile]
from [db_scales].[Hosts] [h]
left join [db_scales].[Scales] [s] on [h].[Id] = [s].[HostId]
where [h].[Id] in (select [HostId] from [db_scales].[Scales] where [Scales].[HostId] is not null and [s].[Marked] = 0) and [h].[Marked] = 0
order by [h].[Name]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetFreeHosts => @"
-- Get free hosts
select [h].[Id]
      ,[h].[CreateDate]
      ,[h].[ModifiedDate]
      ,[h].[Name]
      ,[h].[IP]
      ,[h].[MAC]
      ,[h].[IdRRef]
      ,[h].[Marked]
      ,[h].[SettingsFile]
from [db_scales].[Hosts] [h]
where [h].[Id] not in (select [HostId] from [db_scales].[Scales] [s] where [s].[HostId] is not null and [s].[Marked] = 0) and [h].[Marked] = 0
order by [h].[Name]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetHostId => @"
select [ID]
from [db_scales].[Hosts] 
where [Name]=@host and [IdRRef]=@idrref
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

					public static string GetHostIdByIdRRef => @"
select [ID]
from [db_scales].[Hosts] 
where [IdRRef]=@idrref
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

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
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class Labels
                {
                    public static string Save => @"
INSERT INTO [db_scales].[Labels] ([WeithingFactId], [Label])
	VALUES (@ID, CONVERT(VARBINARY(MAX), @LABEL))
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                    
					public static string SaveZpl => @"
INSERT INTO [db_scales].[Labels] ([WeithingFactId], [ZPL])
	VALUES (@ID, @ZPL)
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
				}

                public static class Scales
                {
                    public static string GetScaleId => @"
select [ID]
from [db_scales].[SCALES]
where [DESCRIPTION]=@scale
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetScaleDescription => @"
SELECT
	[DESCRIPTION]
FROM [db_scales].[Scales]
WHERE [id] = @scale_id
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

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
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string UpdateScale => @"
EXECUTE [db_scales].[UpdateScale]
@ID,
@Description,
@IP,
@Port,
@MAC,
@SendTimeout,
@ReceiveTimeout,
@ComPort,
@UseOrder,
@VerScalesUI,
@ScaleFactor;
			".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string UpdateScaleDirect => @"
UPDATE [db_scales].[SCALES]
SET [Description] = @Description
	--,[DeviceIP] = @IP
	,[DevicePort] = @Port
	--,[DeviceMAC] = @MAC
	,[DeviceSendTimeout] = @SendTimeout
	,[DeviceReceiveTimeout] = @ReceiveTimeout
	,[DeviceComPort] = @ComPort
	,[UseOrder] = @UseOrder
	,[VerScalesUI] = @VerScalesUI
	,[ModifiedDate] = GETDATE()
	,[ScaleFactor] = @ScaleFactor
WHERE [Id] = @ID
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

					public static string QueryFindGuid => @"
IF EXISTS (SELECT 1 FROM [DB_SCALES].[SCALES] WHERE [DB_SCALES].[SCALES].[1CRREFID] = @GUID)
	SELECT 'TRUE' [RESULT]
ELSE
	SELECT 'FALSE' [RESULT]
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
				}

				public static class Tasks
                {
                    public static string GetTaskUid => @"
select [tasks].[UID]
from [db_scales].[TASKS] [tasks]
left join [db_scales].[TASKS_TYPES] [types] on [types].[UID]=[tasks].[TASK_UID]
where [types].[NAME]=@task_type
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

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
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

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
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

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
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string InsertOrUpdateTask => @"
if exists(select 1 from [db_scales].[TASKS] where [UID]=@uid) begin
	update [db_scales].[TASKS] set [ENABLED]=@enabled where [UID]=@uid
end else begin
	insert into [db_scales].[TASKS]([TASK_UID],[SCALE_ID],[ENABLED])
	values(@task_type_uid,@scale_id,@enabled)
end
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string InsertTask => @"
insert into [db_scales].[TASKS]([TASK_UID],[SCALE_ID],[ENABLED])
values(@task_type_uid,@scale_id,@enabled)
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string UpdateTask => @"
update [db_scales].[TASKS] set [ENABLED]=@enabled where [UID]=@uid
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class TaskTypes
                {
                    public static string GetTaskTypeUid => @"
select [UID]
from [db_scales].[TASKS_TYPES] 
where [NAME]=@task_type
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

					public static string GetTasksTypes => @"
select
		[UID]
	,[NAME]
from [db_scales].[TASKS_TYPES]
order by [NAME]
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

					public static string GetTasksTypesByName => @"
select
		[UID]
	,[NAME]
from [db_scales].[TASKS_TYPES]
where [NAME]=@task_name
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

					public static string GetTasksTypesByUid => @"
select
		[UID]
	,[NAME]
from [db_scales].[TASKS_TYPES]
where [UID]=@task_uid
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

					public static string AddTaskType => @"
insert into [db_scales].[TASKS_TYPES]([NAME])
values(@name)
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class WeithingFacts
                {
                    public static string GetWeithingFacts => @"
-- Table WeithingFact diagram summary
select
	 cast([wf].[WeithingDate] as date) [WeithingDate]
	,count(*) [Count]
	,[s].[Description] [Scale]
	,[h].[Name] [Host]
	,[p].[Name] [Printer]
from [db_scales].[WeithingFact] [wf]
left join [db_scales].[Scales] [s] on [wf].[ScaleId] = [s].[Id]
left join [db_scales].[Hosts] [h] on [s].[HostId] = [h].[Id]
left join [db_scales].[ZebraPrinter] [p] on [s].[ZebraPrinterId] = [p].[Id]
group by cast([WeithingDate] as date), [s].[Description], [h].[Name], [p].[Name]
order by [WeithingDate] desc
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

					public static string Save => @"
DECLARE @SSCC varchar(50);
DECLARE @WeithingDate datetime;
DECLARE @xmldata xml;
DECLARE @ID int;
EXECUTE [db_scales].[SetWeithingFact] @OrderID,@ScaleID,@PLU,@NetWeight,@TareWeight,@ProductDate,@Kneading,@SSCC OUTPUT,@WeithingDate OUTPUT,@xmldata OUTPUT,@ID OUTPUT;
SELECT  @SSCC, @WeithingDate, convert(varchar(max), @xmldata) xmldata, @ID;
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
				}
            }
        }
    }
}
