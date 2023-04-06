// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core.Utils;

public static class WsSqlQueries
{
    #region Public and private methods

    public static string GetTopRecords(int topRecords) =>
            topRecords == 0 ? string.Empty : $"TOP {topRecords}";

    public static string GetWhereIsMarked(bool isShowMarkedItems, string alias) =>
        isShowMarkedItems ? string.Empty : $"WHERE {alias}.[IS_MARKED] = 0";

    public static string GetWhereIsMarkedAndNumber(bool isShowMarkedItems, string aliasLog, string aliasLogType,
        Guid logTypeUid)
    {
        if (isShowMarkedItems)
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

    #endregion
}
