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

        /// <summary>
        /// Представления.
        /// </summary>
        public static class Views
        {
            private static string TrimQuery(string queryString)
            {
                return queryString.TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
            }

            /// <summary>
            /// Получить логи памяти.
            /// </summary>
            /// <param name="topRecords"></param>
            /// <returns></returns>
            public static string GetViewLogsMemories(int topRecords)
            {
                return TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
	 [CREATE_DT]
	,[APP_NAME]
	,[DEVICE_NAME]
	,[SCALE_NAME]
	,[SIZE_APP_MB]
	,[SIZE_FREE_MB]
FROM [diag].[VIEW_LOGS_MEMORIES]
ORDER BY [CREATE_DT] DESC");
            }

            /// <summary>
            /// Получить логи размеров таблиц.
            /// </summary>
            /// <param name="topRecords"></param>
            /// <returns></returns>
            public static string GetViewTablesSizes(int topRecords)
            {
                return TrimQuery($@"
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
ORDER BY [SCHEMA], [TABLE];");
            }

            public static string GetLogs(int topRecords, string? logType, string? currentLine)
            {
                logType = logType != null ? $"LOG_TYPE = '{logType}'" : "1=1"; 
                currentLine = currentLine != null ? $"AND LINE = '{currentLine}'" : "AND 1=1";
                return TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {WsSqlQueries.GetTopRecords(topRecords)}
	 [UID]
	,[CREATE_DT]
	,[LINE]
	,[HOSTNAME]
	,[APP]
	,[VERSION]
	,[FILE]
	,[CODE_LINE]
	,[MEMBER]
	,[LOG_TYPE]
	,[MESSAGE]
from [db_scales].[VIEW_LOGS]
WHERE
{logType}
{currentLine}
order by [CREATE_DT] DESC");
            }
            
            public static string GetWebLogs(int topRecords)
            {
                return TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {WsSqlQueries.GetTopRecords(topRecords)}
		 [UID]
		,[CREATE_DT]
		,[REQUEST_URL]
		,[REQUEST_COUNT_ALL]
		,[RESPONSE_COUNT_SUCCESS]
		,[RESPONSE_COUNT_ERROR]
		,[LOG_TYPE]
		,[APP_VERSION]
FROM [diag].[VIEW_LOGS_WEBS]
order by [CREATE_DT] DESC");
            }
            
            public static string GetLines(int topRecords, bool isShowMarkedItems)
            {
                string marked = isShowMarkedItems ? "" : "WHERE IS_MARKED = 0";
                return TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {WsSqlQueries.GetTopRecords(topRecords)}
		 [ID]
		,[IS_MARKED]
		,[NAME]
		,[NUMBER]
		,[HOST_NAME]
		,[PRINTER]
		,[WORKSHOP]
	FROM [db_scales].[VIEW_LINES]
    {marked}
ORDER BY [NAME] ASC");
            }
            
            public static string GetBarcodes(int topRecords, bool isShowMarkedItems)
            {
                string marked = isShowMarkedItems ? "" : "WHERE IS_MARKED = 0";
                return TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {WsSqlQueries.GetTopRecords(topRecords)}
		 [UID]
		,[IS_MARKED]
		,[CREATE_DT]
		,[PLU_NUMBER]
		,[VALUE_TOP]
		,[VALUE_RIGHT]
		,[VALUE_BOTTOM]
	FROM [db_scales].[VIEW_BARCODES]
    {marked}
ORDER BY [CREATE_DT] DESC");
            }
            
            public static string GetPluLabels(int topRecords, bool isShowMarkedItems)
            {
                string marked = isShowMarkedItems ? "" : "WHERE IS_MARKED = 0";
                return TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
-- Table LOGS diagram summary
select {WsSqlQueries.GetTopRecords(topRecords)}
		 [UID]
		,[CREATE_DT]
		,[IS_MARKED]
		,[PROD_DT]
		,[EXPIRATION_DT]
		,[WEIGHING_DT]
		,[LINE]
		,[PLU_NUMBER]
		,[PLU_NAME]
		,[TEMPLATE_TITLE]
	FROM [db_scales].[VIEW_PLUS_LABELS]
    {marked}
	ORDER BY [CREATE_DT] DESC");
            }
            
            public static string GetPluWeightings(int topRecords, bool isShowMarkedItems)
            {
                string marked = isShowMarkedItems ? "" : "WHERE IS_MARKED = 0";
                return TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {WsSqlQueries.GetTopRecords(topRecords)}
			 [UID]
		,[IS_MARKED]
		,[CREATE_DT]
		,[LINE]
		,[PLU_NUMBER]
		,[PLU_NAME]
		,[TARE_WEIGHT]
		,[NETTO_WEIGHT]
	FROM [db_scales].[VIEW_PLUS_WEIGHTINGS]
    {marked}
	ORDER BY [CREATE_DT] DESC");
            }
            
            public static string GetDevices(int topRecords, bool isShowMarkedItems)
            {
                string marked = isShowMarkedItems ? "" : "WHERE IS_MARKED = 0";
                return TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {WsSqlQueries.GetTopRecords(topRecords)}
		 [UID]
		,[IS_MARKED]
		,[LOGIN_DT]
		,[LOGOUT_DT]
		,[NAME]
		,[TYPE_NAME]
		,[IP]
		,[MAC]
	FROM [db_scales].[VIEW_DEVICES]
    {marked}
ORDER BY [NAME] ASC");
            }
        }
    }
}