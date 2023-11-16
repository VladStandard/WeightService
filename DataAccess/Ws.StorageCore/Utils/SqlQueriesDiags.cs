namespace Ws.StorageCore.Utils;

public static class SqlQueriesDiags
{
    public static class Views
    {
        public static string GetViewTablesSizes(int records = 0) => SqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {SqlQueries.GetTopRecords(records)}
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
            SqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {SqlQueries.GetTopRecords(records)}
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
FROM [REF].[VIEW_PLUS_SCALES] {SqlQueries.GetWhereScaleId(scaleId)} {SqlQueries.GetWherePluNumbers(pluNumbers.ToList(), true)}
ORDER BY [SCALE_ID], [PLU_NUMBER];");
        
        public static string GetViewPlusStorageMethods(int records = 0) => SqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {SqlQueries.GetTopRecords(records)}
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
        
        public static string GetViewPlusNesting29(ushort pluNumber) => SqlQueries.TrimQuery($@"
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
FROM [REF].[VIEW_PLUS_NESTING] {SqlQueries.GetWherePluNumber(pluNumber)}
ORDER BY [PLU_NUMBER], [PLU_NAME];");
        
        public static string GetViewPlusNesting32(ushort pluNumber) => SqlQueries.TrimQuery($@"
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
FROM [REF].[VIEW_PLUS_NESTING] {SqlQueries.GetWherePluNumber(pluNumber)}
ORDER BY [PLU_NUMBER], [PLU_NAME];");

        public static string GetBarcodes(int records, SqlEnumIsMarked isMarked) => SqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {SqlQueries.GetTopRecords(records)}
	 [UID]
	,[IS_MARKED]
	,[CREATE_DT]
	,[PLU_NUMBER]
	,[VALUE_TOP]
	,[VALUE_RIGHT]
	,[VALUE_BOTTOM]
FROM [db_scales].[VIEW_BARCODES]
{SqlQueries.GetWhereIsMarked(isMarked)}
ORDER BY [CREATE_DT] DESC");

        public static string GetPluLabels(int records, SqlEnumIsMarked isMarked) => SqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
-- Table LOGS diagram summary
SELECT {SqlQueries.GetTopRecords(records)}
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
{SqlQueries.GetWhereIsMarked(isMarked)}
ORDER BY [CREATE_DT] DESC");

        public static string GetPluWeightings(int records, SqlEnumIsMarked isMarked) => SqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select {SqlQueries.GetTopRecords(records)}
	 [UID]
	,[IS_MARKED]
	,[CREATE_DT]
	,[LINE]
	,[PLU_NUMBER]
	,[PLU_NAME]
	,[TARE_WEIGHT]
	,[NETTO_WEIGHT]
FROM [db_scales].[VIEW_PLUS_WEIGHTINGS]
{SqlQueries.GetWhereIsMarked(isMarked)}
ORDER BY [CREATE_DT] DESC");

        public static string GetLabelsAggr(int records) => SqlQueries.TrimQuery($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT {SqlQueries.GetTopRecords(records)}
		 [CREATE_DT]
		,[WITHOUT_WEIGHT_COUNT]
		,[WEIGHT_COUNT]
		,[COUNT_TOTAL]
FROM [PRINT].[VIEW_PLUS_LABELS_AGGR]
ORDER BY [CREATE_DT] DESC;");
    }
}