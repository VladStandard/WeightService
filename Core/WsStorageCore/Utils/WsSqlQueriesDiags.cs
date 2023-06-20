// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

public static class WsSqlQueriesDiags
{
    public static class Views
        {
            /// <summary>
            /// Получить логи памяти из представления [diag].[VIEW_LOGS_MEMORIES].
            /// </summary>
            /// <param name="topRecords"></param>
            /// <returns></returns>
            public static string GetViewLogsMemory(int topRecords = 0) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
	 [UID]
	,[CREATE_DT]
	,[APP_NAME]
	,[DEVICE_NAME]
	,[SCALE_NAME]
	,[SIZE_APP_MB]
	,[SIZE_FREE_MB]
FROM [diag].[VIEW_LOGS_MEMORIES]
ORDER BY [CREATE_DT] DESC;");

            /// <summary>
            /// Получить логи размеров таблиц из представления [diag].[VIEW_TABLES_SIZES].
            /// </summary>
            /// <param name="topRecords"></param>
            /// <returns></returns>
            public static string GetViewTablesSizes(int topRecords = 0) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
	 [SCHEMA_TABLE]
	,[SCHEMA]
	,[TABLE]
	,[ROWS_COUNT]
	,[USED_SPACE_MB]
	,[UNUSED_SPACE_MB]
	,[TOTAL_SPACE_MB]
    ,[FILENAME]
FROM [diag].[VIEW_TABLES_SIZES]
ORDER BY [USED_SPACE_MB] DESC");

            /// <summary>
            /// Получить список ПЛУ линий из представления [REF].[VIEW_PLUS_SCALES].
            /// </summary>
            /// <param name="scaleId"></param>
            /// <param name="pluNumber"></param>
            /// <param name="topRecords"></param>
            /// <returns></returns>
            public static string GetViewPlusScales(ushort scaleId, ushort pluNumber, int topRecords = 0) =>
                GetViewPlusScales(scaleId, new List<ushort> { pluNumber }, topRecords);

            /// <summary>
            /// Получить список ПЛУ линий из представления [REF].[VIEW_PLUS_SCALES].
            /// </summary>
            /// <param name="scaleId"></param>
            /// <param name="pluNumbers"></param>
            /// <param name="topRecords"></param>
            /// <returns></returns>
            public static string GetViewPlusScales(ushort scaleId, List<ushort> pluNumbers, int topRecords = 0) => 
                WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
     [UID]
	,[CREATE_DT]
	,[CHANGE_DT]
	,[IS_MARKED]
	,[IS_ACTIVE]
	,[SCALE_ID]
	,[SCALE_IS_MARKED]
	,[SCALE_NAME]
	,[PLU_UID]
	,[PLU_IS_MARKED]
	,[PLU_IS_WEIGHT]
	,[PLU_NUMBER]
	,[PLU_NAME]
	,[PLU_GTIN]
	,[PLU_EAN13]
	,[PLU_ITF14]
    ,[TEMPLATE_ID]
    ,[TEMPLATE_IS_MARKED]
    ,[TEMPLATE_NAME]
FROM [REF].[VIEW_PLUS_SCALES] {WsSqlQueries.GetWhereScaleId(scaleId)} {WsSqlQueries.GetWherePluNumbers(pluNumbers, true)}
ORDER BY [SCALE_ID], [PLU_NUMBER];");

            /// <summary>
            /// Получить список способов хранения ПЛУ из представления [REF].[VIEW_PLUS_STORAGE_METHODS].
            /// </summary>
            /// <param name="topRecords"></param>
            /// <returns></returns>
            public static string GetViewPlusStorageMethods(int topRecords = 0) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
     [UID]
	,[PLU_UID]
	,[PLU_IS_MARKED]
	,[PLU_IS_WEIGHT]
	,[PLU_NUMBER]
	,[PLU_NAME]
	,[PLU_GTIN]
	,[PLU_EAN13]
	,[PLU_ITF14]
	,[STORAGE_METHOD_UID]
	,[STORAGE_METHOD_IS_MARKED]
	,[STORAGE_METHOD_NAME]
	,[MIN_TEMP]
	,[MAX_TEMP]
	,[IS_LEFT]
	,[IS_RIGHT]
	,[RESOURCE_UID]
	,[RESOURCE_IS_MARKED]
	,[RESOURCE_NAME]
	,[TEMPLATE_ID]
	,[TEMPLATE_IS_MARKED]
	,[TEMPLATE_NAME]
FROM [REF].[VIEW_PLUS_STORAGE_METHODS]
ORDER BY [PLU_NUMBER], [PLU_NAME];");

            /// <summary>
            /// Получить список вложенностей ПЛУ из представления [REF].[VIEW_PLUS_NESTING].
            /// </summary>
            /// <param name="pluNumber"></param>
            /// <returns></returns>
            public static string GetViewPlusNesting(ushort pluNumber) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT 
	 [UID]
	,[IS_MARKED]
	,[IS_DEFAULT]
	,[BUNDLE_COUNT]
	,[WEIGHT_MAX]
	,[WEIGHT_MIN]
	,[WEIGHT_NOM]
	,[PLU_UID]
	,[PLU_UID_1C]
	,[PLU_IS_MARKED]
	,[PLU_IS_WEIGHT]
	,[PLU_IS_GROUP]
	,[PLU_NUMBER]
	,[PLU_NAME]
	,[PLU_SHELF_LIFE_DAYS]
	,[PLU_GTIN]
	,[PLU_EAN13]
	,[PLU_ITF14]
	,[BUNDLE_UID]
	,[BUNDLE_UID_1C]
	,[BUNDLE_IS_MARKED]
	,[BUNDLE_NAME]
	,[BUNDLE_WEIGHT]
	,[BOX_UID]
	,[BOX_UID_1C]
	,[BOX_IS_MARKED]
	,[BOX_NAME]
	,[BOX_WEIGHT]
	,[TARE_WEIGHT]
FROM [REF].[VIEW_PLUS_NESTING] {WsSqlQueries.GetWherePluNumber(pluNumber)}
ORDER BY [PLU_NUMBER], [PLU_NAME];");

            public static string GetLogs(int topRecords, string? logType, string? currentLine)
            {
                logType = logType != null ? $"LOG_TYPE = '{logType}'" : "1=1"; 
                currentLine = currentLine != null ? $"AND LINE = '{currentLine}'" : "AND 1=1";
                return WsSqlQueries.TrimQuery($@"
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
order by [CREATE_DT] DESC");}
            
            public static string GetWebLogs(int topRecords) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {WsSqlQueries.GetTopRecords(topRecords)}
	 [UID]
	,[CREATE_DT]
	,[REQUEST_URL]
	,[REQUEST_COUNT_ALL]
	,[RESPONSE_COUNT_SUCCESS]
	,[RESPONSE_COUNT_ERROR]
	,[APP_NAME]
	,[APP_VERSION]
FROM [diag].[VIEW_LOGS_WEB_SERVICES]
order by [CREATE_DT] DESC");

            public static string GetLines(int topRecords, WsSqlEnumIsMarked isMarked) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {WsSqlQueries.GetTopRecords(topRecords)}
	 [ID]
	,[IS_MARKED]
	,[NAME]
	,[NUMBER]
	,[HOST_NAME]
	,[PRINTER]
	,[WORKSHOP]
	,[COUNTER]
FROM [db_scales].[VIEW_LINES]
{WsSqlQueries.GetWhereIsMarked(isMarked)}
ORDER BY [NAME] ASC");

            public static string GetBarcodes(int topRecords, WsSqlEnumIsMarked isMarked) => WsSqlQueries.TrimQuery($@"
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
{WsSqlQueries.GetWhereIsMarked(isMarked)}
ORDER BY [CREATE_DT] DESC");

            public static string GetPluLabels(int topRecords, WsSqlEnumIsMarked isMarked) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
-- Table LOGS diagram summary
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
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
{WsSqlQueries.GetWhereIsMarked(isMarked)}
ORDER BY [CREATE_DT] DESC");

            public static string GetPluWeightings(int topRecords, WsSqlEnumIsMarked isMarked) => WsSqlQueries.TrimQuery($@"
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
{WsSqlQueries.GetWhereIsMarked(isMarked)}
ORDER BY [CREATE_DT] DESC");

            public static string GetDevices(int topRecords, WsSqlEnumIsMarked isMarked) => WsSqlQueries.TrimQuery($@"
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
{WsSqlQueries.GetWhereIsMarked(isMarked)}
ORDER BY [NAME] ASC");
            
            public static string GetWeightingsAggr(int topRecords) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {WsSqlQueries.GetTopRecords(topRecords)}
		 [CHANGE_DT]
		,[COUNT]
		,[LINE]
        ,[PLU_NAME]
        ,[PLU_NUMBER]
FROM [db_scales].[VIEW_AGGR_WEIGHTINGS]
ORDER BY [CHANGE_DT] DESC;");
            
        }
    
}