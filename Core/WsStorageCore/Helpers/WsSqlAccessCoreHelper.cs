// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://github.com/nhibernate/fluent-nhibernate/wiki/Database-configuration
// https://docs.microsoft.com/ru-ru/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring

using WsDataCore.Common;

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
    private readonly object _lockerSessionSelect = new();
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
    private readonly object _lockerSessionExecute = new();

    private void Close()
    {
        lock (_lockerSessionSelect)
        {
            SessionSelect.Disconnect();
            SessionSelect.Close();
            SessionSelect.Dispose();
        }

        lock (_lockerSessionExecute)
        {
            SessionExecute.Disconnect();
            SessionExecute.Close();
            SessionExecute.Dispose();
        }

        lock (_lockerSessionFactory)
        {
            SessionFactory.Close();
            SessionFactory.Dispose();
        }
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
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlBarCodeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlBoxMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlBrandMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlBundleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlClipMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlContragentMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlDeviceMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlDeviceScaleFkMap>());
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
        lock (_lockerSessionExecute)
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

    public WsSqlCrudResultModel Save<T>(T? item, WsSqlFieldIdentityModel? identity) where T : WsSqlTableBase
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
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), appName, WsSqlIsMarked.ShowAll, false);
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
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), appName, WsSqlIsMarked.ShowAll, false);
        return GetItemNullable<WsSqlAppModel>(sqlCrudConfig);
    }

    public WsSqlLogTypeModel? GetItemLogTypeNullable(WsEnumLogType logType)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>
                { new() { Name = nameof(WsSqlLogTypeModel.Number), Value = (byte)logType } },
            WsSqlIsMarked.ShowAll, true, false, false, false);
        return GetItemNullable<WsSqlLogTypeModel>(sqlCrudConfig);
    }

    public WsSqlLogTypeModel GetItemLogTypeNotNullable(WsEnumLogType logType) =>
        GetItemLogTypeNullable(logType) ?? new();

    public List<WsSqlLogTypeModel> GetListLogTypesNotNullable()
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>(),
            WsSqlIsMarked.ShowAll, false, false, true, false);
        sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlLogTypeModel.Number), Direction = WsSqlOrderDirection.Asc });
        return GetListNotNullable<WsSqlLogTypeModel>(sqlCrudConfig);
    }

    #endregion

    #region Public and private methods - GetItem

    public T? GetItemNullable<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        T? item = null;
        WsSqlCrudResultModel dbResult = ExecuteSelectCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            item = criteria.UniqueResult<T>();
        });
        if (!dbResult.IsOk) return null;
        FillReferences(item, sqlCrudConfig.IsFillReferences);
        return item;
    }

    public T? GetItemNullable<T>(object? value) where T : WsSqlTableBase, new()
    {
        WsSqlCrudConfigModel? sqlCrudConfig = value switch
        {
            Guid uid => new(new List<WsSqlFieldFilterModel> { new() { Name = nameof(WsSqlTableBase.IdentityValueUid), Value = uid } },
                WsSqlIsMarked.ShowAll, false, false, false, false),
            long id => new(new List<WsSqlFieldFilterModel> { new() { Name = nameof(WsSqlTableBase.IdentityValueId), Value = id } },
                WsSqlIsMarked.ShowAll, false, false, false, false),
            _ => null
        };
        return sqlCrudConfig is not null ? GetItemNullable<T>(sqlCrudConfig) : null;
    }

    public T GetItemNotNullable<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
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

    public T? GetItemNullable<T>(WsSqlFieldIdentityModel identity) where T : WsSqlTableBase, new() =>
        identity.Name switch
        {
            WsSqlFieldIdentity.Uid => GetItemNullable<T>(identity.Uid),
            WsSqlFieldIdentity.Id => GetItemNullable<T>(identity.Id),
            _ => new()
        };

    public T GetItemNotNullable<T>(WsSqlFieldIdentityModel identity) where T : WsSqlTableBase, new() =>
        GetItemNullable<T>(identity) ?? new();

    public T? GetItemNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        GetItemNullable<T>(uid);

    public T GetItemNotNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        GetItemNullableByUid<T>(uid) ?? new();

    public T? GetItemNullableById<T>(long? id) where T : WsSqlTableBase, new() =>
        GetItemNullable<T>(id);

    public T GetItemNotNullableById<T>(long? id) where T : WsSqlTableBase, new() =>
        GetItemNullableById<T>(id) ?? new();

    public WsSqlCrudResultModel IsItemExists<T>(T? item) where T : WsSqlTableBase
    {
        if (item is null) return new(false);
        bool result = false;
        WsSqlCrudResultModel dbResult = ExecuteSelectCore(session =>
        {
            result = session.Query<T>().Any(item => item.IsAny(item));

            //result = session.Query<T>().Any(item => item.Identity.Equals(item.Identity));

            //IQueryable<T> query = session.Query<T>().Where(item => item.Equals(item));
            //result = query.IsAny();
        });
        if (!result) 
            dbResult = dbResult with { IsOk = false };
        return dbResult;
    }

    public bool IsItemExists<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        throw new Exception("FIX HERE!");
        bool result = false;
        WsSqlCrudResultModel dbResult = ExecuteSelectCore(session =>
        {
            int saveCount = JsonSettings.Local.SelectTopRowsCount;
            JsonSettings.Local.SelectTopRowsCount = 1;
            ICriteria criteria = GetCriteriaFirst<T>(session, sqlCrudConfig);
            result = criteria.IsAny();
            JsonSettings.Local.SelectTopRowsCount = saveCount;
        });
        if (!dbResult.IsOk) result = false;
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

    public T[]? GetArrayNullable<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        T[]? items = null;
        WsSqlCrudResultModel dbResult = ExecuteSelectCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            items = criteria.List<T>().ToArray();
            foreach (T item in items)
            {
                FillReferences(item, sqlCrudConfig.IsFillReferences);
            }
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

    public object[] GetArrayObjectsNotNullable(string query) => 
        GetArrayObjectsNotNullable(query, new());

    public object[] GetArrayObjectsNotNullable(string query, List<SqlParameter> parameters) =>
        GetNativeArrayObjectsNullable(query, parameters) ?? Array.Empty<object>();

    public object[] GetArrayObjectsNotNullable(WsSqlCrudConfigModel sqlCrudConfig) =>
        GetNativeArrayObjectsNullable(sqlCrudConfig.NativeQuery, sqlCrudConfig.NativeParameters) ?? Array.Empty<object>();

    #endregion

    #region Public and private methods - GetList

    public List<T> GetListNotNullable<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
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
            case WsXmlDeviceModel xmlDevice:
                xmlDevice.Scale = GetItemNotNullable<WsSqlScaleModel>(xmlDevice.Scale.IdentityValueId);
                break;
            case WsSqlLogModel log:
                log.App = GetItemNotNullable<WsSqlAppModel>(log.App?.IdentityValueUid);
                log.Device = GetItemNullable<WsSqlDeviceModel>(log.Device?.IdentityValueUid);
                log.LogType = GetItemNotNullable<WsSqlLogTypeModel>(log.LogType?.IdentityValueUid);
                break;
            // Scales.
            case WsSqlBarCodeModel barcode:
                barcode.PluLabel = GetItemNotNullable<WsSqlPluLabelModel>(barcode.PluLabel.IdentityValueUid);
                break;
            case WsSqlDeviceTypeFkModel deviceTypeFk:
                deviceTypeFk.Device = GetItemNotNullable<WsSqlDeviceModel>(deviceTypeFk.Device.IdentityValueUid);
                deviceTypeFk.Type = GetItemNotNullable<WsSqlDeviceTypeModel>(deviceTypeFk.Type.IdentityValueUid);
                break;
            case WsSqlDeviceScaleFkModel deviceScaleFk:
                deviceScaleFk.Device = GetItemNotNullable<WsSqlDeviceModel>(deviceScaleFk.Device.IdentityValueUid);
                deviceScaleFk.Scale = GetItemNotNullable<WsSqlScaleModel>(deviceScaleFk.Scale.IdentityValueId);
                break;
            case WsSqlLogMemoryModel logMemory:
                logMemory.App = GetItemNotNullable<WsSqlAppModel>(logMemory.App.IdentityValueUid);
                logMemory.Device = GetItemNotNullable<WsSqlDeviceModel>(logMemory.Device.IdentityValueUid);
                break;
            case WsSqlLogWebFkModel logWebFk:
                logWebFk.LogWebRequest = GetItemNotNullable<WsSqlLogWebModel>(logWebFk.LogWebRequest.IdentityValueUid);
                logWebFk.LogWebResponse = GetItemNotNullable<WsSqlLogWebModel>(logWebFk.LogWebResponse.IdentityValueUid);
                logWebFk.App = GetItemNotNullable<WsSqlAppModel>(logWebFk.App.IdentityValueUid);
                logWebFk.LogType = GetItemNotNullable<WsSqlLogTypeModel>(logWebFk.LogType.IdentityValueUid);
                logWebFk.Device = GetItemNotNullable<WsSqlDeviceModel>(logWebFk.Device.IdentityValueUid);
                break;
            case WsSqlPluFkModel pluFk:
                pluFk.Plu = GetItemNotNullable<WsSqlPluModel>(pluFk.Plu.IdentityValueUid);
                pluFk.Parent = GetItemNotNullable<WsSqlPluModel>(pluFk.Parent.IdentityValueUid);
                pluFk.Category = GetItemNotNullable<WsSqlPluModel>(pluFk.Category?.IdentityValueUid);
                break;
            case WsSqlPluBrandFkModel pluBrandFk:
                pluBrandFk.Plu = GetItemNotNullable<WsSqlPluModel>(pluBrandFk.Plu.IdentityValueUid);
                pluBrandFk.Brand = GetItemNotNullable<WsSqlBrandModel>(pluBrandFk.Brand.IdentityValueUid);
                break;
            case WsSqlPluBundleFkModel pluBundle:
                pluBundle.Plu = GetItemNotNullable<WsSqlPluModel>(pluBundle.Plu.IdentityValueUid);
                pluBundle.Bundle = GetItemNotNullable<WsSqlBundleModel>(pluBundle.Bundle.IdentityValueUid);
                break;
            case WsSqlPluClipFkModel pluClip:
                pluClip.Clip = GetItemNotNullable<WsSqlClipModel>(pluClip.Clip.IdentityValueUid);
                pluClip.Plu = GetItemNotNullable<WsSqlPluModel>(pluClip.Plu.IdentityValueUid);
                break;
            case WsSqlPluLabelModel pluLabel:
                pluLabel.PluWeighing = GetItemNullable<WsSqlPluWeighingModel>(pluLabel.PluWeighing?.IdentityValueUid);
                pluLabel.PluScale = GetItemNotNullable<WsSqlPluScaleModel>(pluLabel.PluScale.IdentityValueUid);
                break;
            case WsSqlPluScaleModel pluScale:
                pluScale.Plu = GetItemNotNullable<WsSqlPluModel>(pluScale.Plu.IdentityValueUid);
                pluScale.Line = GetItemNotNullable<WsSqlScaleModel>(pluScale.Line.IdentityValueId);
                break;
            case WsSqlPluTemplateFkModel pluTemplateFk:
                pluTemplateFk.Plu = GetItemNotNullable<WsSqlPluModel>(pluTemplateFk.Plu.IdentityValueUid);
                pluTemplateFk.Template = GetItemNotNullable<WsSqlTemplateModel>(pluTemplateFk.Template.IdentityValueId);
                break;
            case WsSqlPluWeighingModel pluWeighing:
                pluWeighing.PluScale = GetItemNotNullable<WsSqlPluScaleModel>(pluWeighing.PluScale.IdentityValueUid);
                pluWeighing.Series = GetItemNullable<WsSqlProductSeriesModel>(pluWeighing.Series?.IdentityValueId);
                break;
            case WsSqlPluNestingFkModel pluNestingFk:
                pluNestingFk.PluBundle = GetItemNotNullable<WsSqlPluBundleFkModel>(pluNestingFk.PluBundle.IdentityValueUid);
                pluNestingFk.Box = GetItemNotNullable<WsSqlBoxModel>(pluNestingFk.Box.IdentityValueUid);
                break;
            case WsSqlPluGroupFkModel pluGroupFk:
                pluGroupFk.PluGroup = GetItemNotNullable<WsSqlPluGroupModel>(pluGroupFk.PluGroup.IdentityValueUid);
                pluGroupFk.Parent = GetItemNotNullable<WsSqlPluGroupModel>(pluGroupFk.Parent.IdentityValueUid);
                break;
            case WsSqlPluCharacteristicsFkModel pluCharacteristicsFk:
                pluCharacteristicsFk.Plu = GetItemNotNullable<WsSqlPluModel>(pluCharacteristicsFk.Plu.IdentityValueUid);
                pluCharacteristicsFk.Characteristic = GetItemNotNullable<WsSqlPluCharacteristicModel>(pluCharacteristicsFk.Characteristic.IdentityValueUid);
                break;
            case WsSqlPluStorageMethodFkModel pluStorageMethod:
                if (isFillReferences)
                {
                    pluStorageMethod.Plu = GetItemNotNullable<WsSqlPluModel>(pluStorageMethod.Plu.IdentityValueUid);
                    pluStorageMethod.Method = GetItemNotNullable<WsSqlPluStorageMethodModel>(pluStorageMethod.Method.IdentityValueUid);
                    pluStorageMethod.Resource = GetItemNotNullable<WsSqlTemplateResourceModel>(pluStorageMethod.Resource.IdentityValueUid);
                }
                else
                {
                    //pluStorageMethod.FillProperties();
                    pluStorageMethod.Plu = new();
                    pluStorageMethod.Method = new();
                    pluStorageMethod.Resource = new();
                }
                break;
            case WsSqlOrderWeighingModel orderWeighing:
                orderWeighing.Order = GetItemNotNullable<WsSqlOrderModel>(orderWeighing.Order.IdentityValueUid);
                orderWeighing.PluWeighing = GetItemNotNullable<WsSqlPluWeighingModel>(orderWeighing.PluWeighing.IdentityValueUid);
                break;
            case WsSqlPrinterModel printer:
                printer.PrinterType = GetItemNotNullable<WsSqlPrinterTypeModel>(printer.PrinterType.IdentityValueId);
                break;
            case WsSqlPrinterResourceFkModel printerResource:
                printerResource.Printer = GetItemNotNullable<WsSqlPrinterModel>(printerResource.Printer.IdentityValueId);
                printerResource.TemplateResource = GetItemNotNullable<WsSqlTemplateResourceModel>(printerResource.TemplateResource.IdentityValueUid);
                if (string.IsNullOrEmpty(printerResource.TemplateResource.Description))
                    printerResource.TemplateResource.Description = printerResource.TemplateResource.Name;
                break;
            case WsSqlProductSeriesModel product:
                product.Scale = GetItemNotNullable<WsSqlScaleModel>(product.Scale.IdentityValueId);
                break;
            case WsSqlScaleModel scale:
                scale.PrinterMain = GetItemNullable<WsSqlPrinterModel>(scale.PrinterMain?.IdentityValueId);
                scale.PrinterShipping = GetItemNullable<WsSqlPrinterModel>(scale.PrinterShipping?.IdentityValueId);
                scale.WorkShop = GetItemNullable<WsSqlWorkShopModel>(scale.WorkShop?.IdentityValueId);
                break;
            case WsSqlScaleScreenShotModel scaleScreenShot:
                scaleScreenShot.Scale = GetItemNotNullable<WsSqlScaleModel>(scaleScreenShot.Scale.IdentityValueId);
                break;
            case WsSqlTaskModel task:
                task.TaskType = GetItemNotNullable<WsSqlTaskTypeModel>(task.TaskType.IdentityValueUid);
                task.Scale = GetItemNotNullable<WsSqlScaleModel>(task.Scale.IdentityValueId);
                break;
            case WsSqlWorkShopModel workshop:
                workshop.ProductionFacility = GetItemNotNullable<WsSqlProductionFacilityModel>(workshop.ProductionFacility.IdentityValueId);
                break;
            case WsSqlPlu1CFkModel plu1cFk:
                plu1cFk.Plu = GetItemNotNullable<WsSqlPluModel>(plu1cFk.Plu.IdentityValueUid);
                break;
        }
    }

    #endregion
}