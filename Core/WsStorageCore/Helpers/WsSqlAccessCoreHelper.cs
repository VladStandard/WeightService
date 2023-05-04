// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://github.com/nhibernate/fluent-nhibernate/wiki/Database-configuration
// https://docs.microsoft.com/ru-ru/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник методов доступа.
/// Базовый слой доступа к БД.
/// </summary>
internal sealed class WsSqlAccessCoreHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlAccessCoreHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlAccessCoreHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public static WsJsonSettingsHelper JsonSettings => WsJsonSettingsHelper.Instance;
    
    private ISessionFactory? _sessionFactory;
    public ISessionFactory SessionFactory
    {
        get
        {
            if (_sessionFactory is null)
                throw new ArgumentNullException(nameof(SessionFactory));
            return _sessionFactory;
        }
        set => _sessionFactory = value;
    }

    private readonly object _lockerSessionFactory = new();

    public FluentNHibernate.Cfg.Db.MsSqlConfiguration? SqlConfiguration { get; private set; }

    // Be careful. If setup SqlConfiguration.DefaultSchema, this line will make an Exception!
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

    private FluentConfiguration? _fluentConfiguration;
    private FluentConfiguration FluentConfiguration
    {
        get
        {
            if (_fluentConfiguration is null)
                throw new ArgumentNullException(nameof(FluentConfiguration));
            return _fluentConfiguration;
        }
        set => _fluentConfiguration = value;
    }

    // Be careful. If there are errors in the mapping, this line will make an Exception!
    private void SetupFluentConfiguration()
    {
        if (SqlConfiguration is null)
            throw new ArgumentNullException(nameof(SqlConfiguration));

        FluentConfiguration = Fluently.Configure().Database(SqlConfiguration);
        AddConfigurationMappings(FluentConfiguration);
        //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg).Execute(false, true));
        //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Create(false, true));
        FluentConfiguration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
    }

    public void SetupSessionFactory(bool isShowSql, ISessionFactory? sessionFactory = null)
    {
        lock (_lockerSessionFactory)
        {
            SetSqlConfiguration(isShowSql);
            SetupFluentConfiguration();
            SessionFactory = sessionFactory ?? FluentConfiguration.BuildSessionFactory();
        }
    }
    
    private ISession? _sessionSelect;
    private ISession SessionSelect
    {
        get
        {
            if (_sessionSelect is null || !_sessionSelect.IsOpen)
            {
                _sessionSelect = SessionFactory.OpenSession();
                _sessionSelect.FlushMode = FlushMode.Manual;
            }
            return _sessionSelect;
        }
    }
    private ISession? _sessionExecute;
    private ISession SessionExecute
    {
        get
        {
            if (_sessionExecute is null || !_sessionExecute.IsOpen)
            {
                _sessionExecute = SessionFactory.OpenSession();
                _sessionExecute.FlushMode = FlushMode.Commit;
            }
            return _sessionExecute;
        }
    }

    private void Close()
    {
        SessionSelect.Disconnect();
        SessionSelect.Close();
        SessionSelect.Dispose();

        SessionExecute.Disconnect();
        SessionExecute.Close();
        SessionExecute.Dispose();

        SessionFactory.Close();
        SessionFactory.Dispose();
    }

    ~WsSqlAccessCoreHelper()
    {
        Close();
    }

    #endregion

    #region Public and private methods - Base

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
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BarCodeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BoxMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BrandMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BundleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ClipMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ContragentMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<DeviceMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<DeviceScaleFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<DeviceTypeFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<DeviceTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogMemoryMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogWebFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogWebMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<OrderMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<OrderWeighingMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<OrganizationMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluBrandFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluBundleFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluCharacteristicMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluCharacteristicsFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluClipFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluGroupFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluGroupMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluLabelMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluNestingFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluScaleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluStorageMethodFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluStorageMethodMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluTemplateFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluWeighingMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PrinterMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PrinterResourceFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PrinterTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ProductionFacilityMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ProductSeriesMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ScaleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ScaleScreenShotMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<TaskMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<TaskTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<TemplateMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<TemplateResourceMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<VersionMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WorkShopMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPlu1CFkMap>());
    }

    #endregion

    #region Public and private methods - Base

    private ICriteria GetCriteria<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        ICriteria criteria = session.CreateCriteria(typeof(T));
        if (JsonSettings.Local.MaxCount > 0 && sqlCrudConfig.IsResultShowOnlyTop || JsonSettings.Local.MaxCount == 1)
            criteria.SetMaxResults(JsonSettings.Local.MaxCount);
        if (sqlCrudConfig.Filters.Any())
            criteria.SetCriteriaFilters(sqlCrudConfig.Filters);
        if (sqlCrudConfig.Orders.Any())
        {
            List<SqlFieldOrderModel> orders = sqlCrudConfig.Orders.Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            if (orders.Any())
                criteria.SetCriteriaOrder(orders);
        }
        return criteria;
    }

    /// <summary>
    /// Quick select in one session.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    private WsSqlCrudResultModel ExecuteSelectCore(Action<ISession> action)
    {
        try
        {
            action(SessionSelect);
            SessionSelect.Clear();
        }
        catch (Exception ex)
        {
            return new(ex);
        }
        return new(true);
    }

    /// <summary>
    /// Transaction with action.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    private WsSqlCrudResultModel ExecuteTransactionCore(Action<ISession> action)
    {
        ITransaction transaction = SessionExecute.BeginTransaction();
        try
        {
            action(SessionExecute);
            SessionExecute.Flush();
            transaction.Commit();
            SessionExecute.Clear();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return new(ex);
        }
        finally
        {
            transaction.Dispose();
        }
        return new(true);
    }

    public bool IsConnected()
    {
        bool result = false;
        WsSqlCrudResultModel crudResult = ExecuteSelectCore(session =>
        {
            result = session.IsConnected;
        });
        if (!crudResult.IsOk) result = false;
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

    public WsSqlCrudResultModel Save<T>(T? item) where T : WsSqlTableBase
    {
        if (item is null) return new(new ArgumentException());

        item.ClearNullProperties();
        item.CreateDt = DateTime.Now;
        item.ChangeDt = DateTime.Now;
        return ExecuteTransactionCore(session => session.Save(item));
    }

    public async Task<WsSqlCrudResultModel> SaveAsync<T>(T? item) where T : WsSqlTableBase
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        return Save(item);
    }

    public WsSqlCrudResultModel Save<T>(T? item, SqlFieldIdentityModel? identity) where T : WsSqlTableBase
    {
        if (item is null) return new(new ArgumentException());

        item.ClearNullProperties();
        item.CreateDt = DateTime.Now;
        item.ChangeDt = DateTime.Now;
        object? id = identity?.GetValueAsObjectNullable();
        if (Equals(identity?.Name, WsSqlFieldIdentity.Uid) && Equals(id, Guid.Empty))
            id = Guid.NewGuid();
        return id is null
            ? ExecuteTransactionCore(session => session.Save(item))
            : ExecuteTransactionCore(session => session.Save(item, id));
    }

    public WsSqlCrudResultModel Update<T>(T? item) where T : WsSqlTableBase
    {
        if (item is null) return new(new ArgumentException());

        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
        return ExecuteTransactionCore(session => session.Update(item));
    }

    public WsSqlCrudResultModel Delete<T>(T? item) where T : WsSqlTableBase
    {
        if (item is null) return new(new ArgumentException());

        return ExecuteTransactionCore(session => session.Delete(item));
    }

    public WsSqlCrudResultModel Mark<T>(T? item) where T : WsSqlTableBase
    {
        if (item is null) return new(new ArgumentException());

        item.IsMarked = !item.IsMarked;
        return ExecuteTransactionCore(session => session.SaveOrUpdate(item));
    }

    #endregion

    #region Public and private methods - App & Device

    public WsSqlAppModel GetItemAppOrCreateNew(string appName)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), appName, false, false);
        WsSqlAppModel app = GetItemNotNullable<WsSqlAppModel>(sqlCrudConfig);
        if (app.IsNew)
        {
            app = new()
            {
                Name = appName,
                CreateDt = DateTime.Now,
                ChangeDt = DateTime.Now
            };
            Save(app);
        }
        else
        {
            app.ChangeDt = DateTime.Now;
            Update(app);
        }
        return app;
    }

    public WsSqlAppModel? GetItemAppNullable(string appName)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), appName, false, false);
        return GetItemNullable<WsSqlAppModel>(sqlCrudConfig);
    }

    public LogTypeModel? GetItemLogTypeNullable(LogType logType)
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                { new() { Name = nameof(LogTypeModel.Number), Value = (byte)logType } },
            true, true, false, false, false);
        return GetItemNullable<LogTypeModel>(sqlCrudConfig);
    }

    public LogTypeModel GetItemLogTypeNotNullable(LogType logType) =>
        GetItemLogTypeNullable(logType) ?? new();

    public List<LogTypeModel> GetListLogTypesNotNullable()
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(),
            false, false, false, true, false);
        sqlCrudConfig.AddOrders(new() { Name = nameof(LogTypeModel.Number), Direction = WsSqlOrderDirection.Asc });
        return GetListNotNullable<LogTypeModel>(sqlCrudConfig);
    }

    #endregion

    #region Public and private methods - GetItem

    public T? GetItemNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        T? item = null;
        WsSqlCrudResultModel crudResult = ExecuteSelectCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            item = criteria.UniqueResult<T>();
        });
        if (!crudResult.IsOk) return null;
        FillReferences(item, sqlCrudConfig.IsFillReferences);
        return item;
    }

    public T? GetItemNullable<T>(object? value) where T : WsSqlTableBase, new()
    {
        SqlCrudConfigModel? sqlCrudConfig = value switch
        {
            Guid uid => new(new List<SqlFieldFilterModel> { new() { Name = nameof(WsSqlTableBase.IdentityValueUid), Value = uid } },
                true, false, false, false, false),
            long id => new(new List<SqlFieldFilterModel> { new() { Name = nameof(WsSqlTableBase.IdentityValueId), Value = id } },
                true, false, false, false, false),
            _ => null
        };
        return sqlCrudConfig is not null ? GetItemNullable<T>(sqlCrudConfig) : null;
    }

    public T GetItemNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        T? item = GetItemNullable<T>(sqlCrudConfig);
        return item ?? new();
    }

    public T GetItemNotNullable<T>(object? value) where T : WsSqlTableBase, new()
    {
        T? item = value switch
        {
            Guid uid => GetItemNullable<T>(uid),
            long id => GetItemNullable<T>(id),
            _ => new()
        };
        return item ?? new();
    }

    public T? GetItemNullable<T>(SqlFieldIdentityModel identity) where T : WsSqlTableBase, new() =>
        identity.Name switch
        {
            WsSqlFieldIdentity.Uid => GetItemNullable<T>(identity.Uid),
            WsSqlFieldIdentity.Id => GetItemNullable<T>(identity.Id),
            _ => new()
        };

    public T GetItemNotNullable<T>(SqlFieldIdentityModel identity) where T : WsSqlTableBase, new() =>
        GetItemNullable<T>(identity) ?? new();

    public T? GetItemNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        GetItemNullable<T>(uid);

    public T GetItemNotNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        GetItemNullableByUid<T>(uid) ?? new();

    public T? GetItemNullableById<T>(long? id) where T : WsSqlTableBase, new() =>
        GetItemNullable<T>(id);

    public T GetItemNotNullableById<T>(long? id) where T : WsSqlTableBase, new() =>
        GetItemNullableById<T>(id) ?? new();

    public bool IsItemExists<T>(T? item) where T : WsSqlTableBase, new()
    {
        if (item is null) return false;
        bool result = false;
        WsSqlCrudResultModel crudResult = ExecuteSelectCore(session =>
        {
            result = session.Query<T>().Any(x => x.IsAny(item));
        });
        if (!crudResult.IsOk) result = false;
        return result;
    }

    public bool IsItemExists<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        bool result = false;
        WsSqlCrudResultModel crudResult = ExecuteSelectCore(session =>
        {
            int saveCount = JsonSettings.Local.SelectTopRowsCount;
            JsonSettings.Local.SelectTopRowsCount = 1;
            result = GetCriteria<T>(session, sqlCrudConfig).List<T>().Any();
            JsonSettings.Local.SelectTopRowsCount = saveCount;
        });
        if (!crudResult.IsOk) result = false;
        return result;
    }

    public T GetItemNewEmpty<T>() where T : WsSqlTableBase, new()
    {
        T result = new() { Name = LocaleCore.Table.FieldEmpty, Description = LocaleCore.Table.FieldEmpty };
        result.FillProperties();
        return result;
    }

    #endregion

    #region Public and private methods - GetArray

    public T[]? GetArrayNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        T[]? items = null;
        WsSqlCrudResultModel crudResult = ExecuteSelectCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            items = criteria.List<T>().ToArray();
            foreach (T item in items)
            {
                FillReferences(item, sqlCrudConfig.IsFillReferences);
            }
        });
        if (!crudResult.IsOk) items = null;
        return items;
    }

    public T[]? GetNativeArrayNullable<T>(string query, List<SqlParameter> parameters) where T : WsSqlTableBase, new()
    {
        T[]? result = null;
        WsSqlCrudResultModel crudResult = ExecuteSelectCore(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
            if (sqlQuery is not null)
            {
                //sqlQuery.AddEntity(typeof(T));
                result = sqlQuery.List<T>().ToArray();
            }
        });
        if (!crudResult.IsOk) result = null;
        return result;
    }

    public T? GetNativeItemNullable<T>(string query, List<SqlParameter> parameters) where T : WsSqlTableBase, new()
    {
        T? result = null;
        WsSqlCrudResultModel crudResult = ExecuteSelectCore(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
            if (sqlQuery is not null)
            {
                //sqlQuery.AddEntity(typeof(T));
                IList<T>? list = sqlQuery.List<T>();
                result = list.First();
            }
        });
        if (!crudResult.IsOk) result = null;
        return result;
    }

    private object[]? GetNativeArrayObjectsNullable(string query, List<SqlParameter> parameters)
    {
        object[]? result = null;
        WsSqlCrudResultModel crudResult = ExecuteSelectCore(session =>
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
        if (!crudResult.IsOk) result = null;
        return result;
    }

    public object[] GetArrayObjectsNotNullable(string query) => 
        GetArrayObjectsNotNullable(query, new());

    public object[] GetArrayObjectsNotNullable(string query, List<SqlParameter> parameters) =>
        GetNativeArrayObjectsNullable(query, parameters) ?? Array.Empty<object>();

    public object[] GetArrayObjectsNotNullable(SqlCrudConfigModel sqlCrudConfig) =>
        GetNativeArrayObjectsNullable(sqlCrudConfig.NativeQuery, sqlCrudConfig.NativeParameters) ?? Array.Empty<object>();

    #endregion

    #region Public and private methods - GetList

    public List<T> GetListNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        List<T> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(GetItemNewEmpty<T>());

        List<T> list = new();
        T[]? items = GetArrayNullable<T>(sqlCrudConfig);
        if (items is not null && items.Length > 0)
            list = items.ToList();

        result.AddRange(list);
        return result;
    }

    #endregion

    #region Public and private methods - Fill references

    /// <summary>
    /// Заполнить ссылочные данные.
    /// Следует перенести в клиентский слой доступа к данным!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <param name="isFillReferences"></param>
    private void FillReferences<T>(T? item, bool isFillReferences) where T : WsSqlTableBase, new()
    {
        switch (item)
        {
            case XmlDeviceModel xmlDevice:
                xmlDevice.Scale = GetItemNotNullable<ScaleModel>(xmlDevice.Scale.IdentityValueId);
                break;
            case LogModel log:
                log.App = GetItemNotNullable<WsSqlAppModel>(log.App?.IdentityValueUid);
                log.Device = GetItemNullable<DeviceModel>(log.Device?.IdentityValueUid);
                log.LogType = GetItemNotNullable<LogTypeModel>(log.LogType?.IdentityValueUid);
                break;
            // Scales.
            case BarCodeModel barcode:
                barcode.PluLabel = GetItemNotNullable<PluLabelModel>(barcode.PluLabel.IdentityValueUid);
                break;
            case DeviceTypeFkModel deviceTypeFk:
                deviceTypeFk.Device = GetItemNotNullable<DeviceModel>(deviceTypeFk.Device.IdentityValueUid);
                deviceTypeFk.Type = GetItemNotNullable<DeviceTypeModel>(deviceTypeFk.Type.IdentityValueUid);
                break;
            case DeviceScaleFkModel deviceScaleFk:
                deviceScaleFk.Device = GetItemNotNullable<DeviceModel>(deviceScaleFk.Device.IdentityValueUid);
                deviceScaleFk.Scale = GetItemNotNullable<ScaleModel>(deviceScaleFk.Scale.IdentityValueId);
                break;
            case LogMemoryModel logMemory:
                logMemory.App = GetItemNotNullable<WsSqlAppModel>(logMemory.App.IdentityValueUid);
                logMemory.Device = GetItemNotNullable<DeviceModel>(logMemory.Device.IdentityValueUid);
                break;
            case LogWebFkModel logWebFk:
                logWebFk.LogWebRequest = GetItemNotNullable<LogWebModel>(logWebFk.LogWebRequest.IdentityValueUid);
                logWebFk.LogWebResponse = GetItemNotNullable<LogWebModel>(logWebFk.LogWebResponse.IdentityValueUid);
                logWebFk.App = GetItemNotNullable<WsSqlAppModel>(logWebFk.App.IdentityValueUid);
                logWebFk.LogType = GetItemNotNullable<LogTypeModel>(logWebFk.LogType.IdentityValueUid);
                logWebFk.Device = GetItemNotNullable<DeviceModel>(logWebFk.Device.IdentityValueUid);
                break;
            case PluFkModel pluFk:
                pluFk.Plu = GetItemNotNullable<PluModel>(pluFk.Plu.IdentityValueUid);
                pluFk.Parent = GetItemNotNullable<PluModel>(pluFk.Parent.IdentityValueUid);
                pluFk.Category = GetItemNotNullable<PluModel>(pluFk.Category?.IdentityValueUid);
                break;
            case PluBrandFkModel pluBrandFk:
                pluBrandFk.Plu = GetItemNotNullable<PluModel>(pluBrandFk.Plu.IdentityValueUid);
                pluBrandFk.Brand = GetItemNotNullable<BrandModel>(pluBrandFk.Brand.IdentityValueUid);
                break;
            case PluBundleFkModel pluBundle:
                pluBundle.Plu = GetItemNotNullable<PluModel>(pluBundle.Plu.IdentityValueUid);
                pluBundle.Bundle = GetItemNotNullable<BundleModel>(pluBundle.Bundle.IdentityValueUid);
                break;
            case PluClipFkModel pluClip:
                pluClip.Clip = GetItemNotNullable<ClipModel>(pluClip.Clip.IdentityValueUid);
                pluClip.Plu = GetItemNotNullable<PluModel>(pluClip.Plu.IdentityValueUid);
                break;
            case PluLabelModel pluLabel:
                pluLabel.PluWeighing = GetItemNullable<PluWeighingModel>(pluLabel.PluWeighing?.IdentityValueUid);
                pluLabel.PluScale = GetItemNotNullable<PluScaleModel>(pluLabel.PluScale.IdentityValueUid);
                break;
            case PluScaleModel pluScale:
                pluScale.Plu = GetItemNotNullable<PluModel>(pluScale.Plu.IdentityValueUid);
                pluScale.Scale = GetItemNotNullable<ScaleModel>(pluScale.Scale.IdentityValueId);
                break;
            case PluTemplateFkModel pluTemplateFk:
                pluTemplateFk.Plu = GetItemNotNullable<PluModel>(pluTemplateFk.Plu.IdentityValueUid);
                pluTemplateFk.Template = GetItemNotNullable<TemplateModel>(pluTemplateFk.Template.IdentityValueId);
                break;
            case PluWeighingModel pluWeighing:
                pluWeighing.PluScale = GetItemNotNullable<PluScaleModel>(pluWeighing.PluScale.IdentityValueUid);
                pluWeighing.Series = GetItemNullable<ProductSeriesModel>(pluWeighing.Series?.IdentityValueId);
                break;
            case PluNestingFkModel pluNestingFk:
                pluNestingFk.PluBundle = GetItemNotNullable<PluBundleFkModel>(pluNestingFk.PluBundle.IdentityValueUid);
                pluNestingFk.Box = GetItemNotNullable<BoxModel>(pluNestingFk.Box.IdentityValueUid);
                break;
            case PluGroupFkModel pluGroupFk:
                pluGroupFk.PluGroup = GetItemNotNullable<PluGroupModel>(pluGroupFk.PluGroup.IdentityValueUid);
                pluGroupFk.Parent = GetItemNotNullable<PluGroupModel>(pluGroupFk.Parent.IdentityValueUid);
                break;
            case PluCharacteristicsFkModel pluCharacteristicsFk:
                pluCharacteristicsFk.Plu = GetItemNotNullable<PluModel>(pluCharacteristicsFk.Plu.IdentityValueUid);
                pluCharacteristicsFk.Characteristic = GetItemNotNullable<PluCharacteristicModel>(pluCharacteristicsFk.Characteristic.IdentityValueUid);
                break;
            case PluStorageMethodFkModel pluStorageMethod:
                if (isFillReferences)
                {
                    pluStorageMethod.Plu = GetItemNotNullable<PluModel>(pluStorageMethod.Plu.IdentityValueUid);
                    pluStorageMethod.Method = GetItemNotNullable<PluStorageMethodModel>(pluStorageMethod.Method.IdentityValueUid);
                    pluStorageMethod.Resource = GetItemNotNullable<TemplateResourceModel>(pluStorageMethod.Resource.IdentityValueUid);
                }
                else
                {
                    //pluStorageMethod.FillProperties();
                    pluStorageMethod.Plu = new();
                    pluStorageMethod.Method = new();
                    pluStorageMethod.Resource = new();
                }
                break;
            case OrderWeighingModel orderWeighing:
                orderWeighing.Order = GetItemNotNullable<OrderModel>(orderWeighing.Order.IdentityValueUid);
                orderWeighing.PluWeighing = GetItemNotNullable<PluWeighingModel>(orderWeighing.PluWeighing.IdentityValueUid);
                break;
            case PrinterModel printer:
                printer.PrinterType = GetItemNotNullable<PrinterTypeModel>(printer.PrinterType.IdentityValueId);
                break;
            case PrinterResourceFkModel printerResource:
                printerResource.Printer = GetItemNotNullable<PrinterModel>(printerResource.Printer.IdentityValueId);
                printerResource.TemplateResource = GetItemNotNullable<TemplateResourceModel>(printerResource.TemplateResource.IdentityValueUid);
                if (string.IsNullOrEmpty(printerResource.TemplateResource.Description))
                    printerResource.TemplateResource.Description = printerResource.TemplateResource.Name;
                break;
            case ProductSeriesModel product:
                product.Scale = GetItemNotNullable<ScaleModel>(product.Scale.IdentityValueId);
                break;
            case ScaleModel scale:
                scale.PrinterMain = GetItemNullable<PrinterModel>(scale.PrinterMain?.IdentityValueId);
                scale.PrinterShipping = GetItemNullable<PrinterModel>(scale.PrinterShipping?.IdentityValueId);
                scale.WorkShop = GetItemNullable<WorkShopModel>(scale.WorkShop?.IdentityValueId);
                break;
            case ScaleScreenShotModel scaleScreenShot:
                scaleScreenShot.Scale = GetItemNotNullable<ScaleModel>(scaleScreenShot.Scale.IdentityValueId);
                break;
            case TaskModel task:
                task.TaskType = GetItemNotNullable<TaskTypeModel>(task.TaskType.IdentityValueUid);
                task.Scale = GetItemNotNullable<ScaleModel>(task.Scale.IdentityValueId);
                break;
            case WorkShopModel workshop:
                workshop.ProductionFacility = GetItemNotNullable<ProductionFacilityModel>(workshop.ProductionFacility.IdentityValueId);
                break;
            case WsSqlPlu1CFkModel plu1cFk:
                plu1cFk.Plu = GetItemNotNullable<PluModel>(plu1cFk.Plu.IdentityValueUid);
                break;
        }
    }

    #endregion
}