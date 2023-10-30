namespace WsStorageCore.Utils;

public static class WsSqlQueriesDiags
{
    public static class Views
    {
        public static string GetViewTablesSizes(int records = 0) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {WsSqlQueries.GetTopRecords(records)}
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
        
        public static string GetViewPlusScales(ushort scaleId, IEnumerable<ushort> pluNumbers, int records = 0) =>
            WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {WsSqlQueries.GetTopRecords(records)}
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
FROM [REF].[VIEW_PLUS_SCALES] {WsSqlQueries.GetWhereScaleId(scaleId)} {WsSqlQueries.GetWherePluNumbers(pluNumbers.ToList(), true)}
ORDER BY [SCALE_ID], [PLU_NUMBER];");
        
        public static string GetViewPlusStorageMethods(int records = 0) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {WsSqlQueries.GetTopRecords(records)}
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
        
        public static string GetViewPlusNesting29(ushort pluNumber) => WsSqlQueries.TrimQuery($@"
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
        
        public static string GetViewPlusNesting32(ushort pluNumber) => WsSqlQueries.TrimQuery($@"
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

        public static string GetLogs(int records, string? logType, string? currentLine)
        {
            logType = logType != null ? $"LOG_TYPE = '{logType}'" : "1=1";
            currentLine = currentLine != null ? $"AND LINE = '{currentLine}'" : "AND 1=1";
            return WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {WsSqlQueries.GetTopRecords(records)}
	 [UID]
	,[CREATE_DT]
	,[LINE_NAME]
	,[DEVICE_NAME]
	,[APP_NAME]
	,[VERSION]
	,[FILE_NAME]
	,[CODE_LINE]
	,[MEMBER]
	,[LOG_TYPE]
	,[MESSAGE]
from [DIAG].[VIEW_LOGS_DEVICES]
WHERE
{logType}
{currentLine}
order by [CREATE_DT] DESC");
        }

        public static string GetBarcodes(int records, WsSqlEnumIsMarked isMarked) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {WsSqlQueries.GetTopRecords(records)}
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

        public static string GetPluLabels(int records, WsSqlEnumIsMarked isMarked) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
-- Table LOGS diagram summary
SELECT {WsSqlQueries.GetTopRecords(records)}
		 [UID]
		,[CREATE_DT]
		,[IS_MARKED]
		,[PROD_DT]
		,[WEIGHING_DT]
		,[LINE]
		,[PLU_NAME]
		,[PLU_NUMBER]
		,[WORKSHOP]
FROM [db_scales].[VIEW_PLUS_LABELS]
{WsSqlQueries.GetWhereIsMarked(isMarked)}
ORDER BY [CREATE_DT] DESC");

        public static string GetPluWeightings(int records, WsSqlEnumIsMarked isMarked) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {WsSqlQueries.GetTopRecords(records)}
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

        public static string GetDevices(int records, WsSqlEnumIsMarked isMarked) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {WsSqlQueries.GetTopRecords(records)}
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

        public static string GetLabelsAggr(int records) => WsSqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {WsSqlQueries.GetTopRecords(records)}
		 [CREATE_DT]
		,[WITHOUT_WEIGHT_COUNT]
		,[WEIGHT_COUNT]
		,[COUNT_TOTAL]
FROM [PRINT].[VIEW_PLUS_LABELS_AGGR]
ORDER BY [CREATE_DT] DESC;");
    }
}