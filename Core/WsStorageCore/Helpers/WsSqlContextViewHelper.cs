// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.Aggregations;
using WsStorageCore.ViewScaleModels;

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник представлений.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlContextViewHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlContextViewHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlContextViewHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor
    
    private WsSqlAccessCoreHelper AccessCore => WsSqlAccessCoreHelper.Instance;

    #endregion

    #region Public and private methods - Get list view

    /// <summary>
    /// Получить логи памяти из представления [diag].[VIEW_LOGS_MEMORIES].
    /// </summary>
    /// <returns></returns>
    public List<WsSqlViewLogMemoryModel> GetListViewLogsMemory(int topRecords = 0)
    {
        List<WsSqlViewLogMemoryModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetViewLogsMemory(topRecords);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] { Length: 7 } item) break;
            result.Add(new(Guid.Parse(Convert.ToString(item[0])), Convert.ToDateTime(item[1]),
                Convert.ToString(item[2]), Convert.ToString(item[3]), Convert.ToString(item[4]),
                Convert.ToInt16(item[5]), Convert.ToInt16(item[6])));
        }
        return result;
    }

    /// <summary>
    /// Получить логи размеров таблиц из представления [diag].[VIEW_TABLES_SIZES].
    /// </summary>
    /// <returns></returns>
    public List<WsSqlViewTableSizeModel> GetListViewTablesSizes(int topRecords = 0)
    {
        List<WsSqlViewTableSizeModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetViewTablesSizes(topRecords);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] { Length: 8 } item) break;
            result.Add(new(
                Convert.ToString(item[0]),
                Convert.ToString(item[1]),
                Convert.ToString(item[2]),
                Convert.ToUInt32(item[3]),
                Convert.ToUInt16(item[4]),
                Convert.ToUInt16(item[5]),
                Convert.ToUInt16(item[6]),
                Convert.ToString(item[7])
            ));
        }
        return result;
    }

    /// <summary>
    /// Получить список ПЛУ линий из представления [REF].[VIEW_PLUS_SCALES].
    /// </summary>
    /// <param name="scaleId"></param>
    /// <param name="topRecords"></param>
    /// <returns></returns>
    public List<WsSqlViewPluLineModel> GetListViewPlusScales(ushort scaleId = 0, int topRecords = 0) =>
        GetListViewPlusScales(scaleId, new List<ushort>(), topRecords);

    /// <summary>
    /// Получить список ПЛУ линий из представления [REF].[VIEW_PLUS_SCALES].
    /// </summary>
    /// <param name="scaleId"></param>
    /// <param name="pluNumber"></param>
    /// <param name="topRecords"></param>
    /// <returns></returns>
    public List<WsSqlViewPluLineModel> GetListViewPlusScales(ushort scaleId, ushort pluNumber, int topRecords) =>
        GetListViewPlusScales(scaleId, new List<ushort> { pluNumber }, topRecords);

    /// <summary>
    /// Получить список ПЛУ линий из представления [REF].[VIEW_PLUS_SCALES].
    /// </summary>
    /// <param name="scaleId"></param>
    /// <param name="pluNumbers"></param>
    /// <param name="topRecords"></param>
    /// <returns></returns>
    public List<WsSqlViewPluLineModel> GetListViewPlusScales(ushort scaleId, List<ushort> pluNumbers, int topRecords)
    {
        List<WsSqlViewPluLineModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetViewPlusScales(scaleId, pluNumbers, topRecords);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] { Length: 19 } item) break;
            result.Add(new(Guid.Parse(Convert.ToString(item[0])),
                Convert.ToDateTime(item[1]), Convert.ToDateTime(item[2]),
                Convert.ToBoolean(item[3]), Convert.ToBoolean(item[4]),
                Convert.ToUInt16(item[5]), Convert.ToBoolean(item[6]), Convert.ToString(item[7]),
                Guid.Parse(Convert.ToString(item[8])), Convert.ToBoolean(item[9]), Convert.ToBoolean(item[10]),
                Convert.ToUInt16(item[11]), Convert.ToString(item[12]),
                Convert.ToString(item[13]), Convert.ToString(item[14]), Convert.ToString(item[15]),
                Convert.ToUInt16(item[16]), Convert.ToBoolean(item[17]), Convert.ToString(item[18])
            ));
        }
        return result;
    }

    /// <summary>
    /// Получить список способов хранения ПЛУ из представления [REF].[VIEW_PLUS_STORAGE_METHODS].
    /// </summary>
    /// <param name="topRecords"></param>
    /// <returns></returns>
    public List<WsSqlViewPluStorageMethodModel> GetListViewPlusStorageMethods(int topRecords = 0)
    {
        List<WsSqlViewPluStorageMethodModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetViewPlusStorageMethods(topRecords);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] { Length: 22 } item) break;
            result.Add(new(Guid.Parse(Convert.ToString(item[0])), Guid.Parse(Convert.ToString(item[1])),
                Convert.ToBoolean(item[2]), Convert.ToBoolean(item[3]), Convert.ToUInt16(item[4]), 
                Convert.ToString(item[5]), Convert.ToString(item[6]), Convert.ToString(item[7]), 
                Convert.ToString(item[8]), Guid.Parse(Convert.ToString(item[9])), 
                Convert.ToBoolean(item[10]), Convert.ToString(item[11]),
                Convert.ToInt16(item[12]), Convert.ToInt16(item[13]), Convert.ToBoolean(item[14]), 
                Convert.ToBoolean(item[15]), Guid.Parse(Convert.ToString(item[16])), 
                Convert.ToBoolean(item[17]), Convert.ToString(item[18]),
                Convert.ToUInt16(item[19]), Convert.ToBoolean(item[20]), 
                Convert.ToString(item[21])
            ));
        }
        return result;
    }

    /// <summary>
    /// Получить список вложенностей ПЛУ из представления [REF].[VIEW_PLUS_NESTING].
    /// </summary>
    /// <param name="pluNumber"></param>
    /// <returns></returns>
    public List<WsSqlViewPluNestingModel> GetListViewPlusNesting(ushort pluNumber = 0)
    {
        List<WsSqlViewPluNestingModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetViewPlusNesting(pluNumber);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] { Length: 29 } item) break;
            result.Add(new(Guid.Parse(Convert.ToString(item[0])),
                Convert.ToBoolean(item[1]), Convert.ToBoolean(item[2]),
                Convert.ToInt16(item[3]), Convert.ToDecimal(item[4]), Convert.ToDecimal(item[5]), 
                Convert.ToDecimal(item[6]), Guid.Parse(Convert.ToString(item[7])), Guid.Parse(Convert.ToString(item[8])), 
                Convert.ToBoolean(item[9]), Convert.ToBoolean(item[10]), Convert.ToBoolean(item[11]), 
                Convert.ToUInt16(item[12]), Convert.ToString(item[13]), Convert.ToInt16(item[14]), 
                Convert.ToString(item[15]), Convert.ToString(item[16]), Convert.ToString(item[17]), 
                Guid.Parse(Convert.ToString(item[18])), Guid.Parse(Convert.ToString(item[19])), 
                Convert.ToBoolean(item[20]), Convert.ToString(item[21]), Convert.ToDecimal(item[22]),
                Guid.Parse(Convert.ToString(item[23])), Guid.Parse(Convert.ToString(item[24])), 
                Convert.ToBoolean(item[25]), Convert.ToString(item[26]), Convert.ToDecimal(item[27]), 
                Convert.ToDecimal(item[28])));
        }
        return result;
    }

    public List<WsSqlViewDeviceModel> GetListViewDevices(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewDeviceModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetDevices(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 8 } item ||
                !Guid.TryParse(Convert.ToString(item[0]), out var uid))
                continue;
            
            result.Add(new() 
            {
                IdentityValueUid = uid,
                IsMarked = Convert.ToBoolean(item[1]),
                LoginDate = Convert.ToDateTime(item[2]),
                LogoutDate = Convert.ToDateTime(item[3]),
                Name = item[4] as string ?? string.Empty,
                TypeName = item[5] as string ?? string.Empty,
                Ip = item[6] as string ?? string.Empty,
                Mac = item[7] as string ?? string.Empty
            });
        }
        return result;
    }
    
    public List<WsSqlViewLineModel> GetListViewLines(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewLineModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetLines(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 8 } item)
                continue;

            result.Add(new WsSqlViewLineModel
            {
                IdentityValueId = Convert.ToInt64(item[0]),
                IsMarked = Convert.ToBoolean(item[1]),
                Name = item[2] as string ?? string.Empty,
                Number = Convert.ToInt32(item[3]),
                HostName = item[4] as string ?? string.Empty,
                Printer = item[5] as string ?? string.Empty,
                WorkShop = item[6] as string ?? string.Empty,
                Counter = Convert.ToInt32(item[7])
            });
        }
        return result;
    }
    
    public List<WsSqlViewLogModel> GetListViewLogs(WsSqlCrudConfigModel sqlCrudConfig, string? logType, string? line)
    {
        List<WsSqlViewLogModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetLogs(sqlCrudConfig.SelectTopRowsCount, logType, line);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            
            if (obj is not object[] { Length: 11 } item ||
                !Guid.TryParse(Convert.ToString(item[0]), out var uid))
                continue;
            
            result.Add(new WsSqlViewLogModel
            {
                IdentityValueUid = uid,
                CreateDt = Convert.ToDateTime(item[1]),
                Line = item[2] as string ?? string.Empty,
                Host = item[3] as string ?? string.Empty,
                App = item[4] as string ?? string.Empty,
                Version = item[5] as string ?? string.Empty,
                File = item[6] as string ?? string.Empty,
                CodeLine = Convert.ToInt32(item[7]),
                Member = item[8] as string ?? string.Empty,
                LogType = item[9] as string ?? string.Empty,
                Message = item[10] as string ?? string.Empty
            });
        }
        return result;
    }
    
    public List<WsSqlViewWebLogModel> GetListViewWebLogs(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewWebLogModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetWebLogs(sqlCrudConfig.SelectTopRowsCount);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 8 } item ||
                !Guid.TryParse(Convert.ToString(item[0]), out var uid))
                continue;
            
            result.Add(new WsSqlViewWebLogModel
            {
                IdentityValueUid = uid,
                CreateDt = Convert.ToDateTime(item[1]),
                RequestUrl = item[2] as string ?? string.Empty,
                RequestCountAll = Convert.ToInt32(item[3]),
                ResponseCountSuccess = Convert.ToInt32(item[4]),
                ResponseCountError = Convert.ToInt32(item[5]),
                AppVersion = item[7] as string ?? string.Empty
            });
        }
        return result;
    }
    
    public List<WsSqlViewBarcodeModel> GetListViewBarcodes(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewBarcodeModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetBarcodes(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 7 } item ||
                !Guid.TryParse(Convert.ToString(item[0]), out var uid))
                continue;
            
            result.Add(new WsSqlViewBarcodeModel
            {
                IdentityValueUid = uid,
                IsMarked = Convert.ToBoolean(item[1]),
                CreateDt = Convert.ToDateTime(item[2]),
                PluNumber = Convert.ToInt32(item[3]),
                ValueTop = item[4] as string ?? string.Empty,
                ValueRight = item[5] as string ?? string.Empty,
                ValueBottom = item[6] as string ?? string.Empty
            });
        }
        return result;
    }
    
    public List<WsSqlViewPluLabelModel> GetListViewPlusLabels(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewPluLabelModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetPluLabels(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 10 } item ||
                !Guid.TryParse(Convert.ToString(item[0]), out var uid))
                continue;
            
            result.Add(new WsSqlViewPluLabelModel
            {
                IdentityValueUid = uid,
                CreateDt = Convert.ToDateTime(item[1]),
                IsMarked = Convert.ToBoolean(item[2]),
                ProductDate = Convert.ToDateTime(item[3]),
                ExpirationDate = Convert.ToDateTime(item[4]),
                WeightingDate = Convert.ToDateTime(item[5]),
                Line = item[6] as string ?? string.Empty,
                PluNumber = Convert.ToInt32(item[7]),
                PluName = item[8] as string ?? string.Empty,
                Template = item[9] as string ?? string.Empty
            });
        }
        return result;
    }
    
    public List<WsSqlViewPluWeighting> GetListViewPluWeightings(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewPluWeighting> result = new();
        string query = WsSqlQueriesDiags.Views.GetPluWeightings(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 8 } item ||
                !Guid.TryParse(Convert.ToString(item[0]), out var uid))
                continue;
            
            result.Add(new WsSqlViewPluWeighting
            {
                IdentityValueUid = uid,
                IsMarked = Convert.ToBoolean(item[1]),
                CreateDt = Convert.ToDateTime(item[2]),
                Line = item[3] as string ?? string.Empty,
                PluNumber = Convert.ToInt32(item[4]),
                PluName = item[5] as string ?? string.Empty,
                TareWeight = Convert.ToDecimal(item[6]),
                NettoWeight = Convert.ToDecimal(item[7])
            });
        }
        return result;
    }
    
    public List<WsSqlViewWeightingAggrModel> GetListViewWeightingsAggr(int topRecords)
    {
        List<WsSqlViewWeightingAggrModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetWeightingsAggr(topRecords);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 4 } item)
                continue;
            
            result.Add(new(
                Convert.ToDateTime(item[0]),
                Convert.ToInt32(item[1]),
                Convert.ToString(item[2]),
                Convert.ToString(item[3]))
            );
        }
        return result;
    }
    
    #endregion
}