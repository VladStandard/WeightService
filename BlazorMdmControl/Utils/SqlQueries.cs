// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace MdmControlBlazor.Utils
{
    public static class SqlQueries
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
        .TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetAccessUser(string userName) => @$"
select
	 [UID]
	,[CREATE_DT]
	,[CHANGE_DT]
	,[USER]
	,[LEVEL]
from [db_scales].[ACCESS]
where [USER]=N'{userName}'
        .TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        public static string GetLogs => @"
-- Table Logs diagram summary
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
            .TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
	}
}
