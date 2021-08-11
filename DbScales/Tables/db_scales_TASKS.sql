set nocount on
drop table [db_scales].[TASKS]
drop table [db_scales].[TASKS_TYPES]

create table [db_scales].[TASKS_TYPES] (
    [UID] uniqueidentifier default (newid()) not null,
    [NAME] nvarchar (32) not null,
    primary key clustered ([UID] asc) on [ScalesFileGroup],
    unique nonclustered ([NAME] asc) on [ScalesFileGroup]
) on [ScalesFileGroup];
print N'[+] Create table [TASKS_TYPES] - success'

create table [db_scales].[TASKS] (
    [UID] uniqueidentifier default (newid()) not null,
    [TASK_UID] uniqueidentifier not null,
    [SCALE_ID] int not null,
    [ENABLED] bit default (0) not null,
    foreign key ([TASK_UID]) references [db_scales].[TASKS_TYPES] ([UID]),
    foreign key ([SCALE_ID]) references [db_scales].[SCALES] ([ID]),
    primary key clustered ([UID] asc) on [ScalesFileGroup],
	unique nonclustered ([TASK_UID],[SCALE_ID] asc) on [ScalesFileGroup]
) on [ScalesFileGroup];
print N'[+] Create table [TASKS] - success'

grant select on object::[db_scales].[TASKS_TYPES] to [db_scales_users] as [scales_owner];
grant select on object::[db_scales].[TASKS] to [db_scales_users] as [scales_owner];
print N'[+] Grant - success'

insert into [db_scales].[TASKS_TYPES]([NAME]) values('MassaManager')
insert into [db_scales].[TASKS_TYPES]([NAME]) values('DeviceManager')
insert into [db_scales].[TASKS_TYPES]([NAME]) values('MemoryManager')
insert into [db_scales].[TASKS_TYPES]([NAME]) values('PrintManager')
insert into [db_scales].[TASKS_TYPES]([NAME]) values('ZabbixManager')
print N'[+] Insert data into [TASKS_TYPES] - success'

set nocount off
