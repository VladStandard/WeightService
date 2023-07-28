// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://github.com/nhibernate/fluent-nhibernate/wiki/Database-configuration
// https://docs.microsoft.com/ru-ru/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring

using WsStorageCore.Tables.TableScaleModels.PlusLabels;
using WsStorageCore.Tables.TableScaleModels.PlusWeighings;

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник методов доступа.
/// Базовый слой доступа к БД.
/// </summary>
public sealed class WsSqlCoreHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlCoreHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlCoreHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private object LockerSessionFactory { get; } = new();
    private object LockerSelect { get; } = new();
    private object LockerExecute { get; } = new();

    public static WsJsonSettingsHelper JsonSettings => WsJsonSettingsHelper.Instance;
    
    public ISessionFactory? SessionFactory { get; private set; }

    public FluentNHibernate.Cfg.Db.MsSqlConfiguration? SqlConfiguration { get; private set; }

    private FluentConfiguration? FluentConfiguration { get; set; }

    private void SetFluentConfiguration()
    {
        if (SqlConfiguration is null)
            throw new ArgumentNullException(nameof(SqlConfiguration));

        FluentConfiguration = Fluently.Configure().Database(SqlConfiguration);
        AddConfigurationMappings(FluentConfiguration);
        // Будь осторожен. Ошибки маппингов ведут к Exception!
        //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg).Execute(false, true));
        //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Create(false, true));
        FluentConfiguration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
    }

    public void SetSessionFactory(bool isShowSql)
    {
        lock (LockerSessionFactory)
        {
            SetSqlConfiguration(isShowSql);
            SetFluentConfiguration();
            SessionFactory = FluentConfiguration?.BuildSessionFactory();
        }
    }

    // Будь осторожен. Настройка SqlConfiguration.DefaultSchema ведёт к Exception!
    // Вместо dbo используются: db_scales, ref, diag.
    private void SetSqlConfiguration(bool isShowSql)
    {
        string connectionString = GetConnectionString();
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException(nameof(connectionString));

        SqlConfiguration = FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2012.ConnectionString(connectionString);
        if (isShowSql)
            SqlConfiguration.ShowSql();
        SqlConfiguration.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>();
    }

    private void Close()
    {
        lock (LockerSessionFactory)
        {
            SessionFactory?.Close();
            SessionFactory?.Dispose();
        }
    }

    ~WsSqlCoreHelper()
    {
        Close();
    }

    #endregion

    #region Public and private methods - Base

    /// <summary>
    /// Получить строку подключения к БД.
    /// </summary>
    /// <returns></returns>
    private string GetConnectionString() =>
        JsonSettings.IsRemote
        ? $"Data Source={JsonSettings.Remote.Sql.DataSource}; " +
          $"Initial Catalog={JsonSettings.Remote.Sql.InitialCatalog}; " +
          $"Persist Security Info={JsonSettings.Remote.Sql.PersistSecurityInfo}; " +
          $"Integrated Security={JsonSettings.Remote.Sql.PersistSecurityInfo}; " +
          (JsonSettings.Remote.Sql.IntegratedSecurity ? "" : $"User ID={JsonSettings.Remote.Sql.UserId}; Password={JsonSettings.Remote.Sql.Password}; ") +
          $"TrustServerCertificate={JsonSettings.Remote.Sql.TrustServerCertificate}; "
        : $"Data Source={JsonSettings.Local.Sql.DataSource}; " +
          $"Initial Catalog={JsonSettings.Local.Sql.InitialCatalog}; " +
          $"Persist Security Info={JsonSettings.Local.Sql.PersistSecurityInfo}; " +
          $"Integrated Security={JsonSettings.Local.Sql.PersistSecurityInfo}; " +
          (JsonSettings.Local.Sql.IntegratedSecurity ? "" : $"User ID={JsonSettings.Local.Sql.UserId}; Password={JsonSettings.Local.Sql.Password}; ") +
          $"TrustServerCertificate={JsonSettings.Local.Sql.TrustServerCertificate}; ";

    public void AddConfigurationMappings(FluentConfiguration fluentConfiguration)
    {
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlAccessMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlAppMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlBarCodeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlBoxMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlBrandMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlBundleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlClipMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlContragentMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlDeviceMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlDeviceScaleFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlDeviceSettingsMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlDeviceSettingsFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlDeviceTypeFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlDeviceTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlLogMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlLogMemoryMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlLogTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlLogWebFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlLogWebMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlOrderMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlOrderWeighingMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlOrganizationMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluBrandFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluBundleFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluCharacteristicMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluCharacteristicsFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluClipFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluGroupFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluGroupMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluLabelMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluNestingFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluScaleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluStorageMethodFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluStorageMethodMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluTemplateFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluWeighingMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPrinterMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPrinterResourceFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPrinterTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlProductionFacilityMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlProductSeriesMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlScaleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlScaleScreenShotMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlTaskMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlTaskTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlTemplateMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlTemplateResourceMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlVersionMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlWorkshopMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPlu1CFkMap>());
    }

    #endregion

    #region Public and private methods - Base

    private ICriteria GetCriteria<T>(ISession session, WsSqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        ICriteria criteria = session.CreateCriteria(typeof(T));
        if (sqlCrudConfig.SelectTopRowsCount > 0)
            criteria.SetMaxResults(sqlCrudConfig.SelectTopRowsCount);
        if (sqlCrudConfig.Filters.Any())
            criteria.SetCriteriaFilters(sqlCrudConfig.Filters);
        if (sqlCrudConfig.Orders.Any())
        {
            List<WsSqlFieldOrderModel> orders = sqlCrudConfig.Orders.Where(item => !string.IsNullOrEmpty(item.Name)).ToList();
            if (orders.Any())
                criteria.SetCriteriaOrder(orders);
        }
        return criteria;
    }

    private ICriteria GetCriteriaFirst<T>(ISession session, WsSqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        ICriteria criteria = session.CreateCriteria(typeof(T));
        criteria.SetMaxResults(1);
        if (sqlCrudConfig.Filters.Any())
            criteria.SetCriteriaFilters(sqlCrudConfig.Filters);
        if (sqlCrudConfig.Orders.Any())
        {
            List<WsSqlFieldOrderModel> orders = sqlCrudConfig.Orders.Where(item => !string.IsNullOrEmpty(item.Name)).ToList();
            if (orders.Any())
                criteria.SetCriteriaOrder(orders);
        }
        return criteria;
    }

    /// <summary>
    /// Select in isolated session.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    private WsSqlCrudResultModel ExecuteSelectCore(Action<ISession> action)
    {
        if (SessionFactory is null)
            throw new ArgumentException(nameof(SessionFactory));
        lock (LockerSelect)
        {
            using ISession session = SessionFactory.OpenSession();
            session.FlushMode = FlushMode.Manual;
            try
            {
                action(session);
                session.Clear();
                return new(true);
            }
            catch (Exception ex)
            {
                return new(ex);
            }
            finally
            {
                session.Disconnect();
                session.Close();
                session.Dispose();
            }
        }
    }

    /// <summary>
    /// New transaction with action.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    private WsSqlCrudResultModel ExecuteTransactionCore(Action<ISession> action)
    {
        if (SessionFactory is null)
            throw new ArgumentException(nameof(SessionFactory));
        lock (LockerExecute)
        {
            using ISession session = SessionFactory.OpenSession();
            session.FlushMode = FlushMode.Commit;
            using ITransaction transaction = session.BeginTransaction();
            try
            {
                action(session);
                //session.Flush(); // call in next step
                transaction.Commit();
                session.Clear();
                return new(true);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new(ex);
            }
            finally
            {
                transaction.Dispose();
                session.Disconnect();
                session.Close();
                session.Dispose();
            }
        }
    }

    public bool IsConnected()
    {
        bool result = false;
        WsSqlCrudResultModel dbResult = ExecuteSelectCore(session =>
        {
            result = session.IsConnected;
        });
        if (!dbResult.IsOk) result = false;
        return result;
    }

    private ISQLQuery? GetSqlQuery(ISession session, string query, List<SqlParameter> parameters)
    {
        if (string.IsNullOrEmpty(query)) return null;

        ISQLQuery sqlQuery = session.CreateSQLQuery(query);
        foreach (SqlParameter parameter in parameters)
        {
            if (parameter.Value is byte[] imagedata)
                sqlQuery.SetParameter(parameter.ParameterName, imagedata);
            else
                sqlQuery.SetParameter(parameter.ParameterName, parameter.Value);
        }
        return sqlQuery;
    }

    public WsSqlCrudResultModel ExecQueryNative(string query, List<SqlParameter> parameters)
    {
        if (string.IsNullOrEmpty(query)) return new(new ArgumentException());
        return ExecuteTransactionCore(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
            if (sqlQuery is not null)
            {
                _ = sqlQuery.ExecuteUpdate();
            }
        });
    }

    public WsSqlCrudResultModel ExecQueryNative(string query, SqlParameter parameter) =>
        ExecQueryNative(query, new List<SqlParameter> { parameter });

    public void Save<T>(T? item, WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.Isolated) where T : WsSqlTableBase
    {
        if (item is null) throw new ArgumentException();

        item.ClearNullProperties();
        item.CreateDt = DateTime.Now;
        item.ChangeDt = DateTime.Now;
        
        switch (sessionType)
        {
            case WsSqlEnumSessionType.Isolated:
                ExecuteTransactionCore(session => session.Save(item));
                break;
            case WsSqlEnumSessionType.IsolatedAsync:
                ExecuteTransactionCore(session => session.SaveAsync(item));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sessionType), sessionType, null);
        }
    }

    public void Save<T>(T? item, WsSqlFieldIdentityModel? identity, WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.Isolated) 
        where T : WsSqlTableBase
    {
        if (item is null) throw new ArgumentException();

        item.ClearNullProperties();
        item.CreateDt = DateTime.Now;
        item.ChangeDt = DateTime.Now;

        object? id = identity?.GetValueAsObjectNullable();
        if (Equals(identity?.Name, WsSqlEnumFieldIdentity.Uid) && Equals(id, Guid.Empty))
            id = Guid.NewGuid();
        
        switch (sessionType)
        {
            case WsSqlEnumSessionType.Isolated:
                if (id is null)
                    ExecuteTransactionCore(session => session.Save(item));
                else
                    ExecuteTransactionCore(session => session.Save(item, id));
                break;
            case WsSqlEnumSessionType.IsolatedAsync:
                if (id is null)
                    ExecuteTransactionCore(session => session.SaveAsync(item));
                else
                    ExecuteTransactionCore(session => session.SaveAsync(item, id));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sessionType), sessionType, null);
        }
    }

    public void Update<T>(T? item, WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.Isolated) where T : WsSqlTableBase
    {
        if (item is null) throw new ArgumentException();

        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;

        switch (sessionType)
        {
            case WsSqlEnumSessionType.Isolated:
                ExecuteTransactionCore(session => session.Update(item));
                break;
            case WsSqlEnumSessionType.IsolatedAsync:
                ExecuteTransactionCore(session => session.UpdateAsync(item));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sessionType), sessionType, null);
        }
    }

    public void Delete<T>(T? item, WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.Isolated) where T : WsSqlTableBase
    {
        if (item is null) throw new ArgumentException();

        switch (sessionType)
        {
            case WsSqlEnumSessionType.Isolated:
                ExecuteTransactionCore(session => session.Delete(item));
                break;
            case WsSqlEnumSessionType.IsolatedAsync:
                ExecuteTransactionCore(session => session.DeleteAsync(item));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sessionType), sessionType, null);
        }
        
    }

    public void Mark<T>(T? item, WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.Isolated) where T : WsSqlTableBase
    {
        if (item is null) throw new ArgumentException();

        item.IsMarked = !item.IsMarked;

        switch (sessionType)
        {
            case WsSqlEnumSessionType.Isolated:
                ExecuteTransactionCore(session => session.Update(item));
                break;
            case WsSqlEnumSessionType.IsolatedAsync:
                ExecuteTransactionCore(session => session.UpdateAsync(item));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sessionType), sessionType, null);
        }
    }

    #endregion
    
    #region Public and private methods - GetItem

    # region Private
    
    private T? GetItemNullableByCrud<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        T? item = null;
        WsSqlCrudResultModel dbResult = ExecuteSelectCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            item = criteria.UniqueResult<T>();
            if (sqlCrudConfig.IsFillReferences)
                FillReferences(item);
        });
        if (!dbResult.IsOk) 
            return null;
        return item;
    }
    
    private T? GetItemNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new()
    {
        if (uid == null) 
            return null;
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(new() { Name = nameof(WsSqlTableBase.IdentityValueUid), Value = uid });
        return GetItemNullableByCrud<T>(sqlCrudConfig);
    }
    
    private T? GetItemNullableById<T>(long? id) where T : WsSqlTableBase, new()
    {
        if (id == null) 
            return new();
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(new() { Name = nameof(WsSqlTableBase.IdentityValueId), Value = id });
        return GetItemNullableByCrud<T>(sqlCrudConfig);
    }

    private T? GetItemNullableByIdentity<T>(WsSqlFieldIdentityModel? identity) where T : WsSqlTableBase, new()
    {
        return identity?.Name switch
        {
            WsSqlEnumFieldIdentity.Uid => GetItemNullableByUid<T>(identity.Uid),
            WsSqlEnumFieldIdentity.Id => GetItemNullableById<T>(identity.Id),
            _ => null
        };
    }
    
    #endregion

    #region Public

    public T GetItemByCrud<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        return GetItemNullableByCrud<T>(sqlCrudConfig) ?? GetItemNewEmpty<T>();
    }
    
    public T GetItemByUid<T>(Guid? uid) where T : WsSqlTableBase, new()
    {
        return GetItemNullableByUid<T>(uid) ?? GetItemNewEmpty<T>();
    }
    
    public T GetItemById<T>(long? id) where T : WsSqlTableBase, new()
    {
        return GetItemNullableById<T>(id) ?? GetItemNewEmpty<T>();
    }

    public T GetItemByIdentity<T>(WsSqlFieldIdentityModel? identity) where T : WsSqlTableBase, new()
    {
        return GetItemNullableByIdentity<T>(identity) ?? GetItemNewEmpty<T>();
    }
    
    public T GetItemNewEmpty<T>() where T : WsSqlTableBase, new()
    {
        T result = new();
        result.FillProperties();
        return result;
    }

    #endregion

    // TODO: исправить здесь
    // public WsSqlCrudResultModel IsItemExists<T>(T? item) where T : WsSqlTableBase
    // {
    //     if (item is null) return new(false);
    //     bool result = false;
    //     WsSqlCrudResultModel dbResult = ExecuteSelectCore(session =>
    //     {
    //         result = session.Query<T>().Any(item2 => item2.IsAny(item));    //
                                                                               //         //result = session.Query<T>().Any(item => item.Identity.Equals(item.Identity));

    //
    //         //IQueryable<T> query = session.Query<T>().Where(item => item.Equals(item));
    //         //result = query.IsAny();
    //     });
    //     if (!result) 
    //         dbResult = dbResult with { IsOk = false };
    //     return dbResult;
    // }
    
    //public bool IsItemExists<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    //{
    //    bool result = false;
    //    WsSqlCrudResultModel dbResult = ExecuteSelectCore(session =>
    //    {
    //        int saveCount = JsonSettings.Local.SelectTopRowsCount;
    //        JsonSettings.Local.SelectTopRowsCount = 1;
    //        ICriteria criteria = GetCriteriaFirst<T>(session, sqlCrudConfig);
    //        result = criteria.IsAny();
    //        JsonSettings.Local.SelectTopRowsCount = saveCount;
    //    });
    //    if (!dbResult.IsOk) result = false;
    //    return result;
    //}

    #endregion

    #region Public and private methods - GetArray

    public T[]? GetArrayNullable<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        T[]? items = null;
        WsSqlCrudResultModel dbResult = ExecuteSelectCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            items = criteria.List<T>().ToArray();
            foreach (T item in items)
                if (sqlCrudConfig.IsFillReferences)
                    FillReferences(item);
        });
        if (!dbResult.IsOk) items = null;
        return items;
    }

    public T[]? GetNativeArrayNullable<T>(string query, List<SqlParameter> parameters) where T : WsSqlTableBase, new()
    {
        T[]? result = null;
        WsSqlCrudResultModel dbResult = ExecuteSelectCore(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
            if (sqlQuery is not null)
            {
                //sqlQuery.AddEntity(typeof(T));
                result = sqlQuery.List<T>().ToArray();
            }
        });
        if (!dbResult.IsOk) result = null;
        return result;
    }

    public T? GetNativeItemNullable<T>(string query, List<SqlParameter> parameters) where T : WsSqlTableBase, new()
    {
        T? result = null;
        WsSqlCrudResultModel dbResult = ExecuteSelectCore(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
            if (sqlQuery is not null)
            {
                //sqlQuery.AddEntity(typeof(T));
                IList<T>? list = sqlQuery.List<T>();
                result = list.First();
            }
        });
        if (!dbResult.IsOk) result = null;
        return result;
    }

    private object[]? GetNativeArrayObjectsNullable(string query, List<SqlParameter> parameters)
    {
        object[]? result = null;
        WsSqlCrudResultModel dbResult = ExecuteSelectCore(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
            if (sqlQuery is not null)
            {
                System.Collections.IList? listEntities = sqlQuery.List();
                result = new object[listEntities.Count];
                for (int i = 0; i < result.Length; i++)
                {
                    if (listEntities[i] is object[] records)
                        result[i] = records;
                    else
                        result[i] = listEntities[i];
                }
            }
        });
        if (!dbResult.IsOk) result = null;
        return result;
    }

    public object[] GetArrayObjectsNotNullable(string query) => GetArrayObjectsNotNullable(query, new());

    public object[] GetArrayObjectsNotNullable(string query, List<SqlParameter> parameters) =>
        GetNativeArrayObjectsNullable(query, parameters) ?? Array.Empty<object>();

    public object[] GetArrayObjectsNotNullable(WsSqlCrudConfigModel sqlCrudConfig) =>
        GetNativeArrayObjectsNullable(sqlCrudConfig.NativeQuery, sqlCrudConfig.NativeParameters) ?? Array.Empty<object>();

    #endregion

    #region Public and private methods - GetList

    public List<T> GetListNotNullable<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        T[]? items = GetArrayNullable<T>(sqlCrudConfig);
        List<T> result = items?.ToList() ?? new List<T>();
        return result;
    }

    #endregion

    #region Public and private methods - Fill references
    
    private void FillReferences<T>(T? item) where T : WsSqlTableBase, new()
    {
        // TODO: Следует перенести в клиентский слой доступа к данным! + Tests
        switch (item)
        {
            case WsXmlDeviceModel xmlDevice:
                xmlDevice.Scale = GetItemByIdentity<WsSqlScaleModel>(xmlDevice.Scale.Identity);
                break;
            case WsSqlLogModel log:
                log.App = GetItemByIdentity<WsSqlAppModel>(log.App?.Identity);
                log.Device = GetItemNullableByIdentity<WsSqlDeviceModel>(log.Device?.Identity);
                log.LogType = GetItemByIdentity<WsSqlLogTypeModel>(log.LogType?.Identity);
                break;
            // Scales.
            case WsSqlBarCodeModel barcode:
                barcode.PluLabel = GetItemByIdentity<WsSqlPluLabelModel>(barcode.PluLabel.Identity);
                break;
            case WsSqlDeviceTypeFkModel deviceTypeFk:
                deviceTypeFk.Device = GetItemByIdentity<WsSqlDeviceModel>(deviceTypeFk.Device.Identity);
                deviceTypeFk.Type = GetItemByIdentity<WsSqlDeviceTypeModel>(deviceTypeFk.Type.Identity);
                break;
            case WsSqlDeviceScaleFkModel deviceScaleFk:
                deviceScaleFk.Device = GetItemByIdentity<WsSqlDeviceModel>(deviceScaleFk.Device.Identity);
                deviceScaleFk.Scale = GetItemByIdentity<WsSqlScaleModel>(deviceScaleFk.Scale.Identity);
                break;
            case WsSqlDeviceSettingsFkModel deviceSettingsFk:
                deviceSettingsFk.Device = GetItemByIdentity<WsSqlDeviceModel>(deviceSettingsFk.Device.Identity);
                deviceSettingsFk.Setting = GetItemByIdentity<WsSqlDeviceSettingsModel>(deviceSettingsFk.Setting.Identity);
                break;
            case WsSqlLogMemoryModel logMemory:
                logMemory.App = GetItemByIdentity<WsSqlAppModel>(logMemory.App.Identity);
                logMemory.Device = GetItemByIdentity<WsSqlDeviceModel>(logMemory.Device.Identity);
                break;
            case WsSqlLogWebFkModel logWebFk:
                logWebFk.LogWebRequest = GetItemByIdentity<WsSqlLogWebModel>(logWebFk.LogWebRequest.Identity);
                logWebFk.LogWebResponse = GetItemByIdentity<WsSqlLogWebModel>(logWebFk.LogWebResponse.Identity);
                logWebFk.App = GetItemByIdentity<WsSqlAppModel>(logWebFk.App.Identity);
                logWebFk.LogType = GetItemByIdentity<WsSqlLogTypeModel>(logWebFk.LogType.Identity);
                logWebFk.Device = GetItemByIdentity<WsSqlDeviceModel>(logWebFk.Device.Identity);
                break;
            case WsSqlPluFkModel pluFk:
                pluFk.Plu = GetItemByIdentity<WsSqlPluModel>(pluFk.Plu.Identity);
                pluFk.Parent = GetItemByIdentity<WsSqlPluModel>(pluFk.Parent.Identity);
                pluFk.Category = GetItemByIdentity<WsSqlPluModel>(pluFk.Category?.Identity);
                break;
            case WsSqlPluBrandFkModel pluBrandFk:
                pluBrandFk.Plu = GetItemByIdentity<WsSqlPluModel>(pluBrandFk.Plu.Identity);
                pluBrandFk.Brand = GetItemByIdentity<WsSqlBrandModel>(pluBrandFk.Brand.Identity);
                break;
            case WsSqlPluBundleFkModel pluBundle:
                pluBundle.Plu = GetItemByIdentity<WsSqlPluModel>(pluBundle.Plu.Identity);
                pluBundle.Bundle = GetItemByIdentity<WsSqlBundleModel>(pluBundle.Bundle.Identity);
                break;
            case WsSqlPluClipFkModel pluClip:
                pluClip.Clip = GetItemByIdentity<WsSqlClipModel>(pluClip.Clip.Identity);
                pluClip.Plu = GetItemByIdentity<WsSqlPluModel>(pluClip.Plu.Identity);
                break;
            case WsSqlPluLabelModel pluLabel:
                pluLabel.PluWeighing = GetItemNullableByIdentity<WsSqlPluWeighingModel>(pluLabel.PluWeighing?.Identity);
                pluLabel.PluScale = GetItemByIdentity<WsSqlPluScaleModel>(pluLabel.PluScale.Identity);
                break;
            case WsSqlPluScaleModel pluScale:
                pluScale.Plu = GetItemByIdentity<WsSqlPluModel>(pluScale.Plu.Identity);
                pluScale.Line = GetItemByIdentity<WsSqlScaleModel>(pluScale.Line.Identity);
                break;
            case WsSqlPluTemplateFkModel pluTemplateFk:
                pluTemplateFk.Plu = GetItemByIdentity<WsSqlPluModel>(pluTemplateFk.Plu.Identity);
                pluTemplateFk.Template = GetItemByIdentity<WsSqlTemplateModel>(pluTemplateFk.Template.Identity);
                break;
            case WsSqlPluWeighingModel pluWeighing:
                pluWeighing.PluScale = GetItemByIdentity<WsSqlPluScaleModel>(pluWeighing.PluScale.Identity);
                pluWeighing.Series = GetItemNullableByIdentity<WsSqlProductSeriesModel>(pluWeighing.Series?.Identity);
                break;
            case WsSqlPluNestingFkModel pluNestingFk:
                pluNestingFk.PluBundle = GetItemByIdentity<WsSqlPluBundleFkModel>(pluNestingFk.PluBundle.Identity);
                pluNestingFk.Box = GetItemByIdentity<WsSqlBoxModel>(pluNestingFk.Box.Identity);
                break;
            case WsSqlPluGroupFkModel pluGroupFk:
                pluGroupFk.PluGroup = GetItemByIdentity<WsSqlPluGroupModel>(pluGroupFk.PluGroup.Identity);
                pluGroupFk.Parent = GetItemByIdentity<WsSqlPluGroupModel>(pluGroupFk.Parent.Identity);
                break;
            case WsSqlPluCharacteristicsFkModel pluCharacteristicsFk:
                pluCharacteristicsFk.Plu = GetItemByIdentity<WsSqlPluModel>(pluCharacteristicsFk.Plu.Identity);
                pluCharacteristicsFk.Characteristic = GetItemByIdentity<WsSqlPluCharacteristicModel>(pluCharacteristicsFk.Characteristic.Identity);
                break;
            case WsSqlPluStorageMethodFkModel pluStorageMethod:
                pluStorageMethod.Plu = GetItemByIdentity<WsSqlPluModel>(pluStorageMethod.Plu.Identity);
                pluStorageMethod.Method = GetItemByIdentity<WsSqlPluStorageMethodModel>(pluStorageMethod.Method.Identity);
                pluStorageMethod.Resource = GetItemByIdentity<WsSqlTemplateResourceModel>(pluStorageMethod.Resource.Identity);
                break;
            case WsSqlOrderWeighingModel orderWeighing:
                orderWeighing.Order = GetItemByIdentity<WsSqlOrderModel>(orderWeighing.Order.Identity);
                orderWeighing.PluWeighing = GetItemByIdentity<WsSqlPluWeighingModel>(orderWeighing.PluWeighing.Identity);
                break;
            case WsSqlPrinterModel printer:
                printer.PrinterType = GetItemByIdentity<WsSqlPrinterTypeModel>(printer.PrinterType.Identity);
                break;
            case WsSqlPrinterResourceFkModel printerResource:
                printerResource.Printer = GetItemByIdentity<WsSqlPrinterModel>(printerResource.Printer.Identity);
                printerResource.TemplateResource = GetItemByIdentity<WsSqlTemplateResourceModel>(printerResource.TemplateResource.Identity);
                break;
            case WsSqlProductSeriesModel product:
                product.Scale = GetItemByIdentity<WsSqlScaleModel>(product.Scale.Identity);
                break;
            case WsSqlScaleModel scale:
                scale.PrinterMain = GetItemNullableByIdentity<WsSqlPrinterModel>(scale.PrinterMain?.Identity);
                scale.PrinterShipping = GetItemNullableByIdentity<WsSqlPrinterModel>(scale.PrinterShipping?.Identity);
                scale.WorkShop = GetItemNullableByIdentity<WsSqlWorkShopModel>(scale.WorkShop?.Identity);
                break;
            case WsSqlScaleScreenShotModel scaleScreenShot:
                scaleScreenShot.Scale = GetItemByIdentity<WsSqlScaleModel>(scaleScreenShot.Scale.Identity);
                break;
            case WsSqlTaskModel task:
                task.TaskType = GetItemByIdentity<WsSqlTaskTypeModel>(task.TaskType.Identity);
                task.Scale = GetItemByIdentity<WsSqlScaleModel>(task.Scale.Identity);
                break;
            case WsSqlWorkShopModel workshop:
                workshop.ProductionFacility = GetItemByIdentity<WsSqlProductionFacilityModel>(workshop.ProductionFacility.Identity);
                break;
            case WsSqlPlu1CFkModel plu1cFk:
                plu1cFk.Plu = GetItemByIdentity<WsSqlPluModel>(plu1cFk.Plu.Identity);
                break;
        }
    }

    #endregion
}