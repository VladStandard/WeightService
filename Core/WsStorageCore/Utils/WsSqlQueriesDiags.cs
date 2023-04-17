// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

public static class WsSqlQueriesDiags
{
    public static class Tables
    {
        public static string GetErrors(int topRecords) => $@"
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
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
select {WsSqlQueries.GetTopRecords(topRecords)}
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
{WsSqlQueries.GetWhereIsMarkedAndNumber(isShowMarkedItems, "[l]", "[lt]", logTypeUid)}
order by [l].[CREATE_DT] desc
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
    }

    /// <summary>
    /// Представления.
    /// </summary>
    public static class Views
    {
        /// <summary>
        /// Получить логи памяти.
        /// </summary>
        /// <param name="topRecords"></param>
        /// <returns></returns>
        public static string GetViewLogsMemories(int topRecords) => $@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
	 [CREATE_DT]
	,[APP_NAME]
	,[DEVICE_NAME]
	,[SCALE_NAME]
	,[SIZE_APP_MB]
	,[SIZE_FREE_MB]
FROM [diag].[VIEW_LOGS_MEMORIES]
ORDER BY [CREATE_DT] DESC;
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

        /// <summary>
        /// Получить логи размеров таблиц.
        /// </summary>
        /// <param name="topRecords"></param>
        /// <returns></returns>
        public static string GetViewTablesSizes(int topRecords) => $@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
	 [SCHEMA_TABLE]
	,[SCHEMA]
	,[TABLE]
	,[ROWS_COUNT]
	,[USED_SPACE_MB]
	,[UNUSED_SPACE_MB]
	,[TOTAL_SPACE_MB]
FROM [diag].[VIEW_TABLES_SIZES]
ORDER BY [SCHEMA], [TABLE];
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
    }
}