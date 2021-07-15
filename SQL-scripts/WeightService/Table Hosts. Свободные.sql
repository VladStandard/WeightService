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
where [h].[Id] not in (select [HostId] from [db_scales].[Scales] [s] where [s].[HostId] is not null)
order by [h].[Name]
