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
