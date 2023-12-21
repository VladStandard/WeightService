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