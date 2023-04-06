// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core.Utils;

public static class WsSqlQueriesDiags
{
	public static class Errors
	{
		public static string GetErrors(int topRecords) => $@"
SELECT {SqlQueries.GetTopRecords(topRecords)}
[Id]
,[CreatedDate]
,[ModifiedDate]
,[FilePath]
,[LineNumber]
,[MemberName]
,[Exception]
,[InnerException]
FROM [db_scales].[Errors]
ORDER BY [CreatedDate] DESC
".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
	}

	public static class Logs
	{
		public static string AddLogType => @"
insert into [db_scales].[LOG_TYPES]([NUMBER],[ICON]) 
values (@number,@icon)
	".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

		public static string GetLogTypes => @"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
-- Table LOG_TYPES
select 
[UID]
,[NUMBER]
,[ICON]
from [db_scales].[LOG_TYPES]
order by [NUMBER]
".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

		public static string GetLogs(int topRecords, bool isShowMarkedItems, Guid logTypeUid) => $@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
-- Table LOGS diagram summary
select {SqlQueries.GetTopRecords(topRecords)}
[l].[UID]
,[l].[CREATE_DT]
,[s].[Description] [SCALE]
,[d].[NAME] [HOSTNAME]
,[d].[PRETTY_NAME] [HOST_PRETTY_NAME]
,[a].[NAME] [APP]
,[l].[VERSION]
,[l].[FILE]
,[l].[LINE]
,[l].[MEMBER]
,[lt].[ICON]
,[l].[MESSAGE]
from [db_scales].[LOGS] [l]
left join [db_scales].[DEVICES] [d] on [d].[UID] = [l].[DEVICE_UID]
left join [db_scales].[DEVICES_SCALES_FK] [ds] on [ds].[DEVICE_UID] = [d].[UID]
left join [db_scales].[Scales] [s] on [s].[Id] = [ds].[SCALE_ID]
left join [db_scales].[APPS] [a] on [a].[UID] = [l].[APP_UID]
left join [db_scales].[LOG_TYPES] [lt] on [lt].[UID] = [l].[LOG_TYPE_UID]
{SqlQueries.GetWhereIsMarkedAndNumber(isShowMarkedItems, "[l]", "[lt]", logTypeUid)}
order by [l].[CREATE_DT] desc
".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
	}
}