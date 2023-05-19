// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

public static class WsSqlQueries
{
    #region Public and private methods

    public static string GetTopRecords(int topRecords) =>
            topRecords == 0 ? string.Empty : $"TOP {topRecords}";

    public static string GetWhereIsMarked(WsSqlIsMarked isMarked) => isMarked switch
        {
            WsSqlIsMarked.ShowOnlyActual => "WHERE [IS_MARKED]=0",
            WsSqlIsMarked.ShowOnlyHide => "WHERE [IS_MARKED]=1",
            _ => string.Empty
        };
    
    public static string GetWhereIsMarked(WsSqlIsMarked isMarked, string alias) => isMarked switch
        {
            WsSqlIsMarked.ShowOnlyActual => $"WHERE {alias}.[IS_MARKED]=0",
            WsSqlIsMarked.ShowOnlyHide => $"WHERE {alias}.[IS_MARKED]=1",
            _ => string.Empty
        };
    
    public static string GetWhereIsMarkedAndNumber(WsSqlIsMarked isMarked, string aliasLog, string aliasLogType,
        Guid logTypeUid) => isMarked switch
        {
            WsSqlIsMarked.ShowAll => logTypeUid != Guid.Empty
                ? $"WHERE {aliasLogType}.[UID]='{logTypeUid}'" : string.Empty,
            WsSqlIsMarked.ShowOnlyActual => logTypeUid != Guid.Empty
                ? $"WHERE {aliasLog}.[IS_MARKED]=0 AND {aliasLogType}.[UID]='{logTypeUid}'" : $"WHERE {aliasLog}.[IS_MARKED]=0",
            WsSqlIsMarked.ShowOnlyHide => logTypeUid != Guid.Empty
                ? $"WHERE {aliasLog}.[IS_MARKED]=0 AND {aliasLogType}.[UID]='{logTypeUid}'" : $"WHERE {aliasLog}.[IS_MARKED]=1",
            _ => string.Empty
        };

    public static string GetWhereScaleId(ushort scaleId) => scaleId > 0 ? $"WHERE [SCALE_ID]={scaleId}" : string.Empty;

    public static string TrimQuery(string queryString) =>
        queryString.TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    #endregion
}