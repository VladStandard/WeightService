-- Table Logs diagram summary
--delete from [db_scales].[LOGS] where [UID]='31B51BB9-19B3-4C0C-9C62-189661BBE8A3'
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
	,[l].[ICON]
	,[l].[MESSAGE]
from [db_scales].[LOGS] [l]
left join [db_scales].[Hosts] [h] on [h].[ID]=[l].[HOST_ID]
left join [db_scales].[Scales] [s] on [s].[HostId]=[h].[ID]
left join [db_scales].[APPS] [a] on [a].[UID]=[l].[APP_UID]
order by [l].[CREATE_DT] desc
