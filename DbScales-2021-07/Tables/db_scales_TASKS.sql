drop table [db_scales].[TASKS]
drop table [db_scales].[TASKS_TYPES]

create table [db_scales].[TASKS_TYPES] (
    [UID] uniqueidentifier default (newid()) not null,
    [NAME] nvarchar (32) not null,
    primary key clustered ([UID] asc) on [ScalesFileGroup],
    unique nonclustered ([NAME] asc) on [ScalesFileGroup]
) on [ScalesFileGroup];
go
create table [db_scales].[TASKS] (
    [UID] uniqueidentifier default (newid()) not null,
    [TASK_UID] uniqueidentifier not null,
    [SCALE_ID] int not null,
    [ENABLED] bit default (0) not null,
    foreign key ([TASK_UID]) references [db_scales].[TASKS_TYPES] ([UID]),
    foreign key ([SCALE_ID]) references [db_scales].[SCALES] ([ID]),
    primary key clustered ([UID] asc) on [ScalesFileGroup],
) on [ScalesFileGroup];
go
grant select on object::[db_scales].[TASKS_TYPES] to [db_scales_users] as [scales_owner];
grant select on object::[db_scales].[TASKS] to [db_scales_users] as [scales_owner];

insert into [db_scales].[TASKS_TYPES]([NAME]) values('MassaManager')
insert into [db_scales].[TASKS_TYPES]([NAME]) values('DeviceManager')
insert into [db_scales].[TASKS_TYPES]([NAME]) values('MemoryManager')
insert into [db_scales].[TASKS_TYPES]([NAME]) values('PrintManager')

declare @scale_id int = (select [ID] from [db_scales].[SCALES] where [Description]='Весы разработчика')
declare @task_uid uniqueidentifier

set @task_uid = (select [UID] from [db_scales].[TASKS_TYPES] where [NAME]='MassaManager')
insert into [db_scales].[TASKS]([TASK_UID],[SCALE_ID],[ENABLED]) values(@task_uid,@scale_id,0)
set @task_uid = (select [UID] from [db_scales].[TASKS_TYPES] where [NAME]='DeviceManager')
insert into [db_scales].[TASKS]([TASK_UID],[SCALE_ID],[ENABLED]) values(@task_uid,@scale_id,1)
set @task_uid = (select [UID] from [db_scales].[TASKS_TYPES] where [NAME]='MemoryManager')
insert into [db_scales].[TASKS]([TASK_UID],[SCALE_ID],[ENABLED]) values(@task_uid,@scale_id,1)
set @task_uid = (select [UID] from [db_scales].[TASKS_TYPES] where [NAME]='PrintManager')
insert into [db_scales].[TASKS]([TASK_UID],[SCALE_ID],[ENABLED]) values(@task_uid,@scale_id,1)
