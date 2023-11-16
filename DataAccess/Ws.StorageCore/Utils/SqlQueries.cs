using Ws.StorageCore.Common;
namespace Ws.StorageCore.Utils;

public static class SqlQueries
{
    #region Public and private methods

    public static string GetTopRecords(int topRecords) => topRecords == 0 ? string.Empty : $"TOP {topRecords}";
    
    public static string GetWhereIsMarked(SqlEnumIsMarked isMarked) => isMarked switch
        {
            SqlEnumIsMarked.ShowOnlyActual => "WHERE [IS_MARKED] = 0",
            SqlEnumIsMarked.ShowOnlyHide => "WHERE [IS_MARKED] = 1",
            _ => string.Empty
        };
    
    public static string GetWhereScaleId(ushort scaleId) => scaleId > 0 ? $"WHERE [SCALE_ID]={scaleId}" : string.Empty;

    public static string GetWherePluNumber(ushort pluNumber) => pluNumber > 0 ? $"WHERE [PLU_NUMBER]={pluNumber}" : string.Empty;
    
    public static string GetWherePluNumbers(List<ushort> pluNumbers, bool isAddAnd) => 
        pluNumbers.Any() ? $"{(isAddAnd ? "AND" : "WHERE")} [PLU_NUMBER] IN ({(string.Join(", ", pluNumbers))})" : string.Empty;

    public static string TrimQuery(string queryString) =>
        queryString.TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    #endregion
}