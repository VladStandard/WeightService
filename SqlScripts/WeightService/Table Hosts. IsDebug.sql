-- Table Hosts. IsDebug
select [ID]
      ,[NAME]
      ,[IS_DEBUG]
      ,[SETTINGSFILE] [XML]
from [db_scales].[Hosts]
where [NAME]='PC208'
--where [NAME]='{host}'
