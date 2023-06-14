// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

/// <summary>
/// SQL table names.
/// </summary>
public static class WsSqlViewsUtils
{
    #region Public and private fields, properties, constructor

    public static string AggrWeightings => "VIEW_AGGR_WEIGHTINGS";
    public static string Barcodes => "VIEW_BARCODES";
    public static string Devices => "VIEW_DEVICES";
    public static string Lines => "VIEW_LINES";
    public static string Logs => "VIEW_LOGS";
    public static string PlusLabels => "VIEW_PLUS_LABELS";
    public static string PlusWeightings => "VIEW_PLUS_WEIGHTINGS";
    public static string LogsMemories => "VIEW_LOGS_MEMORIES";
    public static string LogsWebs => "VIEW_LOGS_WEBS";
    public static string TablesSizes => "VIEW_TABLES_SIZES";
    public static string DevicesSettings => "VIEW_DEVICES_SETTINGS";
    public static string PlusNesting => "VIEW_PLUS_NESTING";
    public static string PlusScales => "VIEW_PLUS_SCALES";
    public static string PlusStorageMethods => "VIEW_PLUS_STORAGE_METHODS";

    #endregion
}