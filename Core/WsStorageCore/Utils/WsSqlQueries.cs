// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

public static class WsSqlQueries
{
    #region Public and private methods

    public static string GetTopRecords(int topRecords) =>
            topRecords == 0 ? string.Empty : $"TOP {topRecords}";

    public static string GetWhereAppName(string appName) => string.IsNullOrEmpty(appName) ? string.Empty : $"WHERE [APP_NAME] = '{appName}'";
    
    public static string GetWhereIsMarked(WsSqlEnumIsMarked isMarked) => isMarked switch
        {
            WsSqlEnumIsMarked.ShowOnlyActual => "WHERE [IS_MARKED]=0",
            WsSqlEnumIsMarked.ShowOnlyHide => "WHERE [IS_MARKED]=1",
            _ => string.Empty
        };
    
    public static string GetWhereIsMarked(WsSqlEnumIsMarked isMarked, string alias) => isMarked switch
        {
            WsSqlEnumIsMarked.ShowOnlyActual => $"WHERE {alias}.[IS_MARKED]=0",
            WsSqlEnumIsMarked.ShowOnlyHide => $"WHERE {alias}.[IS_MARKED]=1",
            _ => string.Empty
        };
    
    public static string GetWhereIsMarkedAndNumber(WsSqlEnumIsMarked isMarked, string aliasLog, string aliasLogType,
        Guid logTypeUid) => isMarked switch
        {
            WsSqlEnumIsMarked.ShowAll => logTypeUid != Guid.Empty
                ? $"WHERE {aliasLogType}.[UID]='{logTypeUid}'" : string.Empty,
            WsSqlEnumIsMarked.ShowOnlyActual => logTypeUid != Guid.Empty
                ? $"WHERE {aliasLog}.[IS_MARKED]=0 AND {aliasLogType}.[UID]='{logTypeUid}'" : $"WHERE {aliasLog}.[IS_MARKED]=0",
            WsSqlEnumIsMarked.ShowOnlyHide => logTypeUid != Guid.Empty
                ? $"WHERE {aliasLog}.[IS_MARKED]=0 AND {aliasLogType}.[UID]='{logTypeUid}'" : $"WHERE {aliasLog}.[IS_MARKED]=1",
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