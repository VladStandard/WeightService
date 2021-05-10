-- Таблица Hosts. Свободные

select [Hosts].[Id], [Hosts].[Name]
from [db_scales].[Hosts]
order by [Hosts].[Id]

select [HostId]
from [db_scales].[Scales]
where [Scales].[HostId] is not null
order by [Scales].[HostId]

select [Id]
      ,[CreateDate]
      ,[ModifiedDate]
      ,[Name]
      ,[IP]
      ,[MAC]
      ,[IdRRef]
      ,[Marked]
      ,[SettingsFile]
from [db_scales].[Hosts]
where [Hosts].[Id] not in (select [HostId] from [db_scales].[Scales] where [Scales].[HostId] is not null)
order by [Hosts].[Id]
