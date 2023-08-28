namespace WsStorageCore.Utils;

/// <summary>
/// SQL queries for Scales.
/// </summary>
public static class WsSqlQueriesScales
{
    public static class Functions
    {
        public static string GetCurrentProductSeriesV2 => WsSqlQueries.TrimQuery(@"
DECLARE @SSCC VARCHAR(50)
DECLARE @WeithingDate DATETIME
DECLARE @XML XML

EXECUTE [db_scales].[SP_SET_PRODUCT_SERIES_V2] @SCALE_ID, @SSCC OUTPUT, @XML OUTPUT

SELECT [ID], [CREATE_DT], [UUID], [SSCC], [COUNT_UNIT],[TOTAL_NET_WEIGHT], [TOTAL_TARE_WEIGHT], [IS_MARKED]
FROM [db_scales].[FN_GET_PRODUCT_SERIES_V2](@SCALE_ID)");
    }
}