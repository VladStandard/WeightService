// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

public static class WsSqlQueries
{
    #region Public and private methods

    public static string GetTopRecords(int topRecords) =>
            topRecords == 0 ? string.Empty : $"TOP {topRecords}";

    public static string GetWhereIsMarked(bool isShowMarked) =>
        isShowMarked ? string.Empty : $"WHERE [IS_MARKED]=0";

    public static string GetWhereIsMarked(bool isShowMarked, string alias) =>
        isShowMarked ? string.Empty : $"WHERE {alias}.[IS_MARKED]=0";

    public static string GetWhereIsMarkedAndNumber(bool isShowMarked, string aliasLog, string aliasLogType,
        Guid logTypeUid)
    {
        if (isShowMarked)
        {
            if (logTypeUid != Guid.Empty)
                return $"WHERE {aliasLogType}.[UID] = '{logTypeUid}'";
        }
        else
        {
            if (logTypeUid != Guid.Empty)
                return $"WHERE {aliasLog}.[IS_MARKED] = 0 AND {aliasLogType}.[UID] = '{logTypeUid}'";
            return $"WHERE {aliasLog}.[IS_MARKED] = 0";
        }

        return string.Empty;
    }

    public static string TrimQuery(string queryString) =>
        queryString.TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    #endregion
}