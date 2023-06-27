// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
    
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

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
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 7) break;
            result.Add(new(Guid.Parse(Convert.ToString(item[i++])), Convert.ToDateTime(item[i++]),
                Convert.ToString(item[i++]), Convert.ToString(item[i++]), Convert.ToString(item[i++]),
                Convert.ToInt16(item[i++]), Convert.ToInt16(item[i++])));
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
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 8) break;
            result.Add(new(
                Convert.ToString(item[i++]),
                Convert.ToString(item[i++]),
                Convert.ToString(item[i++]),
                Convert.ToUInt32(item[i++]),
                Convert.ToUInt16(item[i++]),
                Convert.ToUInt16(item[i++]),
                Convert.ToUInt16(item[i++]),
                Convert.ToString(item[i++])
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
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 19) break;
            result.Add(new(Guid.Parse(Convert.ToString(item[i++])),
                Convert.ToDateTime(item[i++]), Convert.ToDateTime(item[i++]),
                Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]),
                Convert.ToUInt16(item[i++]), Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]),
                Guid.Parse(Convert.ToString(item[i++])), Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]),
                Convert.ToUInt16(item[i++]), Convert.ToString(item[i++]),
                Convert.ToString(item[i++]), Convert.ToString(item[i++]), Convert.ToString(item[i++]),
                Convert.ToUInt16(item[i++]), Convert.ToBoolean(item[i++]), Convert.ToString(item[i++])
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
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 22) break;
            result.Add(new(Guid.Parse(Convert.ToString(item[i++])), Guid.Parse(Convert.ToString(item[i++])),
                Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]), Convert.ToUInt16(item[i++]), 
                Convert.ToString(item[i++]), Convert.ToString(item[i++]), Convert.ToString(item[i++]), 
                Convert.ToString(item[i++]), Guid.Parse(Convert.ToString(item[i++])), 
                Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]),
                Convert.ToInt16(item[i++]), Convert.ToInt16(item[i++]), Convert.ToBoolean(item[i++]), 
                Convert.ToBoolean(item[i++]), Guid.Parse(Convert.ToString(item[i++])), 
                Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]),
                Convert.ToUInt16(item[i++]), Convert.ToBoolean(item[i++]), 
                Convert.ToString(item[i++])
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
        
        string query = WsSqlQueriesDiags.Views.GetViewPlusNesting32(pluNumber);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] item || item.Length < 32) break;
            int i = 0;
            result.Add(new(Guid.Parse(Convert.ToString(item[i++])),
                Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]),
                Convert.ToInt16(item[i++]), Convert.ToDecimal(item[i++]), Convert.ToDecimal(item[i++]), 
                Convert.ToDecimal(item[i++]), Guid.Parse(Convert.ToString(item[i++])), 
                Guid.Parse(Convert.ToString(item[i++])), Convert.ToBoolean(item[i++]), 
                Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]), 
                Convert.ToUInt16(item[i++]), Convert.ToString(item[i++]), Convert.ToInt16(item[i++]), 
                Convert.ToString(item[i++]), Convert.ToString(item[i++]), Convert.ToString(item[i++]), 
                Guid.Parse(Convert.ToString(item[i++])), Guid.Parse(Convert.ToString(item[i++])), 
                Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]), Convert.ToDecimal(item[i++]),
                Guid.Parse(Convert.ToString(item[i++])), Guid.Parse(Convert.ToString(item[i++])), 
                Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]), Convert.ToDecimal(item[i++]), 
                Convert.ToDecimal(item[i++]), Convert.ToDateTime(item[i++]),
                Convert.ToBoolean(item[i++]), Convert.ToString(item[i++])));
        }

        if (!result.Any())
        {
            query = WsSqlQueriesDiags.Views.GetViewPlusNesting29(pluNumber);
            objects = SqlCore.GetArrayObjectsNotNullable(query);
            foreach (object obj in objects)
            {
                if (obj is not object[] item || item.Length < 29) break;
                int i = 0;
                result.Add(new(Guid.Parse(Convert.ToString(item[i++])),
                    Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]),
                    Convert.ToInt16(item[i++]), Convert.ToDecimal(item[i++]), Convert.ToDecimal(item[i++]),
                    Convert.ToDecimal(item[i++]), Guid.Parse(Convert.ToString(item[i++])),
                    Guid.Parse(Convert.ToString(item[i++])), Convert.ToBoolean(item[i++]),
                    Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]),
                    Convert.ToUInt16(item[i++]), Convert.ToString(item[i++]), Convert.ToInt16(item[i++]),
                    Convert.ToString(item[i++]), Convert.ToString(item[i++]), Convert.ToString(item[i++]),
                    Guid.Parse(Convert.ToString(item[i++])), Guid.Parse(Convert.ToString(item[i++])),
                    Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]), Convert.ToDecimal(item[i++]),
                    Guid.Parse(Convert.ToString(item[i++])), Guid.Parse(Convert.ToString(item[i++])),
                    Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]), Convert.ToDecimal(item[i++]),
                    Convert.ToDecimal(item[i++]), DateTime.MinValue, false, string.Empty));
            }
        }

        return result;
    }

    public List<WsSqlViewDeviceModel> GetListViewDevices(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewDeviceModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetDevices(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 8 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new() 
            {
                IdentityValueUid = uid,
                IsMarked = Convert.ToBoolean(item[i++]),
                LoginDate = Convert.ToDateTime(item[i++]),
                LogoutDate = Convert.ToDateTime(item[i++]),
                Name = item[i++] as string ?? string.Empty,
                TypeName = item[i++] as string ?? string.Empty,
                Ip = item[i++] as string ?? string.Empty,
                Mac = item[i++] as string ?? string.Empty
            });
        }
        return result;
    }
    
    public List<WsSqlViewLineModel> GetListViewLines(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewLineModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetLines(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 8) break;
            result.Add(new()
            {
                IdentityValueId = Convert.ToInt64(item[i++]),
                IsMarked = Convert.ToBoolean(item[i++]),
                Name = item[i++] as string ?? string.Empty,
                Number = Convert.ToInt32(item[i++]),
                HostName = item[i++] as string ?? string.Empty,
                Printer = item[i++] as string ?? string.Empty,
                WorkShop = item[i++] as string ?? string.Empty,
                Counter = Convert.ToInt32(item[i++])
            });
        }
        return result;
    }
    
    public List<WsSqlViewLogModel> GetListViewLogs(WsSqlCrudConfigModel sqlCrudConfig, string? logType, string? line)
    {
        List<WsSqlViewLogModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetLogs(sqlCrudConfig.SelectTopRowsCount, logType, line);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 11 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new()
            {
                IdentityValueUid = uid,
                CreateDt = Convert.ToDateTime(item[i++]),
                Line = item[i++] as string ?? string.Empty,
                Host = item[i++] as string ?? string.Empty,
                App = item[i++] as string ?? string.Empty,
                Version = item[i++] as string ?? string.Empty,
                File = item[i++] as string ?? string.Empty,
                CodeLine = Convert.ToInt32(item[i++]),
                Member = item[i++] as string ?? string.Empty,
                LogType = item[i++] as string ?? string.Empty,
                Message = item[i++] as string ?? string.Empty
            });
        }
        return result;
    }
    
    public List<WsSqlViewWebLogModel> GetListViewWebLogs(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewWebLogModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetWebLogs(sqlCrudConfig.SelectTopRowsCount);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 8 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new()
            {
                IdentityValueUid = uid,
                CreateDt = Convert.ToDateTime(item[i++]),
                RequestUrl = item[i++] as string ?? string.Empty,
                RequestCountAll = Convert.ToInt32(item[i++]),
                ResponseCountSuccess = Convert.ToInt32(item[i++]),
                ResponseCountError = Convert.ToInt32(item[i++]),
                AppVersion = item[i++] as string ?? string.Empty
            });
        }
        return result;
    }
    
    public List<WsSqlViewBarcodeModel> GetListViewBarcodes(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewBarcodeModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetBarcodes(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 7 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new()
            {
                IdentityValueUid = uid,
                IsMarked = Convert.ToBoolean(item[i++]),
                CreateDt = Convert.ToDateTime(item[i++]),
                PluNumber = Convert.ToInt32(item[i++]),
                ValueTop = item[i++] as string ?? string.Empty,
                ValueRight = item[i++] as string ?? string.Empty,
                ValueBottom = item[i++] as string ?? string.Empty
            });
        }
        return result;
    }
    
    public List<WsSqlViewPluLabelModel> GetListViewPlusLabels(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewPluLabelModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetPluLabels(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 10 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new()
            {
                IdentityValueUid = uid,
                CreateDt = Convert.ToDateTime(item[i++]),
                IsMarked = Convert.ToBoolean(item[i++]),
                ProductDate = Convert.ToDateTime(item[i++]),
                ExpirationDate = Convert.ToDateTime(item[i++]),
                WeightingDate = Convert.ToDateTime(item[i++]),
                Line = item[i++] as string ?? string.Empty,
                PluNumber = Convert.ToInt32(item[i++]),
                PluName = item[i++] as string ?? string.Empty,
                Template = item[i++] as string ?? string.Empty
            });
        }
        return result;
    }
    
    public List<WsSqlViewPluWeighting> GetListViewPluWeightings(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewPluWeighting> result = new();
        string query = WsSqlQueriesDiags.Views.GetPluWeightings(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 8 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new()
            {
                IdentityValueUid = uid,
                IsMarked = Convert.ToBoolean(item[i++]),
                CreateDt = Convert.ToDateTime(item[i++]),
                Line = item[i++] as string ?? string.Empty,
                PluNumber = Convert.ToInt32(item[i++]),
                PluName = item[i++] as string ?? string.Empty,
                TareWeight = Convert.ToDecimal(item[i++]),
                NettoWeight = Convert.ToDecimal(item[i++])
            });
        }
        return result;
    }
    
    public List<WsSqlViewWeightingAggrModel> GetListViewWeightingsAggr(int topRecords)
    {
        List<WsSqlViewWeightingAggrModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetWeightingsAggr(topRecords);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 5) break;
            result.Add(new(
                Convert.ToDateTime(item[i++]),
                Convert.ToInt32(item[i++]),
                Convert.ToString(item[i++]),
                Convert.ToString(item[i++]),
                Convert.ToInt32(item[i++]))
            );
        }
        return result;
    }
    
    #endregion
}