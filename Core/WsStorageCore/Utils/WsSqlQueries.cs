namespace WsStorageCore.Utils;

public static class WsSqlQueries
{
    #region Public and private methods

    public static string GetTopRecords(int topRecords) =>
            topRecords == 0 ? string.Empty : $"TOP {topRecords}";

    public static string GetWhereAppDeviceName(string appName, string deviceName)
    {
        if (string.IsNullOrEmpty(appName) && string.IsNullOrEmpty(deviceName))
            return string.Empty;
        if (string.IsNullOrEmpty(appName))
            return $"WHERE [DEVICE_NAME] = '{deviceName}' ";
        return string.IsNullOrEmpty(deviceName) 
            ? $"WHERE [APP_NAME] = '{appName}' " 
            : $"WHERE [APP_NAME] = '{appName}' AND [DEVICE_NAME] = '{deviceName}' ";
    }

    public static string GetWhereAppDeviceNameCreateDt(string appName, string deviceName, DateTime? createDt) =>
        string.IsNullOrEmpty(appName) || createDt is null ? GetWhereAppDeviceName(appName, deviceName) 
        : $"WHERE [APP_NAME] = '{appName}' AND CAST([CREATE_DT] AS DATE) = '{createDt:yyyy-MM-dd}'";
    
    public static string GetWhereAppDeviceNameToday(string appName, string deviceName)
    {
        string strWhere = GetWhereAppDeviceName(appName, deviceName);
        string strWhereAdd = $"CAST([CREATE_DT] AS DATE) = '{DateTime.Now:yyyy-MM-dd}'";
        return string.IsNullOrEmpty(strWhere) ? $"WHERE {strWhereAdd}" : $"{strWhere} AND {strWhereAdd}";

    }

    public static string GetWhereAppDeviceNameDay(string appName, string deviceName)
    {
        string strWhere = GetWhereAppDeviceName(appName, deviceName);
        string strWhereAdd = $"DAY(CAST([CREATE_DT] AS DATE)) = {DateTime.Now.Day} " +
            $"AND MONTH(CAST([CREATE_DT] AS DATE)) = {DateTime.Now.Month} " +
            $"AND YEAR(CAST([CREATE_DT] AS DATE)) = {DateTime.Now.Year} ";
        return string.IsNullOrEmpty(strWhere) ? $"WHERE {strWhereAdd}" : $"{strWhere} AND {strWhereAdd}";
    }

    public static string GetWhereAppNameMonth(string appName, string deviceName)
    {
        string strWhere = GetWhereAppDeviceName(appName, deviceName);
        string strWhereAdd = $"MONTH(CAST([CREATE_DT] AS DATE)) = {DateTime.Now.Month} " +
            $"AND YEAR(CAST([CREATE_DT] AS DATE)) = {DateTime.Now.Year} ";
        return string.IsNullOrEmpty(strWhere) ? $"WHERE {strWhereAdd}" : $"{strWhere} AND {strWhereAdd}";
    }

    public static string GetWhereAppNameYear(string appName, string deviceName)
    {
        string strWhere = GetWhereAppDeviceName(appName, deviceName);
        string strWhereAdd = $"YEAR(CAST([CREATE_DT] AS DATE)) = {DateTime.Now.Year} ";
        return string.IsNullOrEmpty(strWhere) ? $"WHERE {strWhereAdd}" : $"{strWhere} AND {strWhereAdd}";
    }

    public static string GetWhereEmpty() => "WHERE 1 = 0";

    public static string GetWhereIsMarked(WsSqlEnumIsMarked isMarked) => isMarked switch
        {
            WsSqlEnumIsMarked.ShowOnlyActual => "WHERE [IS_MARKED] = 0",
            WsSqlEnumIsMarked.ShowOnlyHide => "WHERE [IS_MARKED] = 1",
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