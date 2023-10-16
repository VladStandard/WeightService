using WsStorageCore.OrmUtils;
using WsStorageCore.Tables.TableRef1cModels.Brands;
using WsStorageCore.Tables.TableRefModels.ProductionSites;
using WsStorageCore.Tables.TableRefModels.WorkShops;

namespace WsStorageCore.Helpers;

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

    public MsSqlConfiguration? SqlConfiguration { get; private set; }

    private FluentConfiguration? FluentConfiguration { get; set; }

    private void SetFluentConfiguration()
    {
        if (SqlConfiguration is null)
            throw new ArgumentNullException(nameof(SqlConfiguration));

        FluentConfiguration = Fluently.Configure().Database(SqlConfiguration);
        AddConfigurationMappings(FluentConfiguration);
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
    
    private void SetSqlConfiguration(bool isShowSql)
    {
        string connectionString = GetConnectionString();
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException(nameof(connectionString));

        SqlConfiguration = MsSqlConfiguration.MsSql2012.ConnectionString(connectionString);
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

    ~WsSqlCoreHelper() => Close();

    #endregion

    #region Public and private methods - Base
    
    private string GetConnectionString() => JsonSettings.IsRemote
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
    
    public string GetConnectionServer() => JsonSettings.IsRemote
        ? $"Server = {JsonSettings.Remote.Sql.DataSource} | DB = {JsonSettings.Remote.Sql.InitialCatalog}"
        : $"Server = {JsonSettings.Local.Sql.DataSource} | DB = {JsonSettings.Local.Sql.InitialCatalog}";

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
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlDeviceSettingsMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlDeviceSettingsFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlDeviceTypeFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlDeviceTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlLogMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlLogMemoryMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlLogTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlLogWebMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlOrderMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlOrderWeighingMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlOrganizationMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluClipFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlPluFkMap>());
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
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlProductionSiteMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WsSqlScaleMap>());
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
        
        foreach (ICriterion filter in sqlCrudConfig.Filters)
            criteria.Add(filter);
        
        foreach (Order order in sqlCrudConfig.Orders)
            criteria.AddOrder(order);
        
        return criteria;
    }

    /// <summary>
    /// Select in isolated session.
    /// </summary>
    private void ExecuteSelectCore(Action<ISession> action)
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
                return;
            }
            catch (Exception ex)
            {
                return;
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
        ExecuteSelectCore(session => { result = session.IsConnected; });
        return result;
    }

    private ISQLQuery? GetSqlQuery(ISession session, string query, IList<SqlParameter> parameters)
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

    private WsSqlCrudResultModel ExecQueryNative(string query, IList<SqlParameter> parameters)
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

    #endregion
    
    #region CRUD

    public void Save<T>(T? item, WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.Isolated) where T : WsSqlTableBase
{
    if (item is null) throw new ArgumentException();
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

public void Update<T>(T? item, WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.Isolated) where T : WsSqlTableBase
{
    if (item is null) throw new ArgumentException();
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

    public T GetItemByCrud<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        T? item = null;
        ExecuteSelectCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            item = criteria.UniqueResult<T>();
        });
        return item ?? GetItemNewEmpty<T>();;
    }
    
    public T GetItemByUid<T>(Guid uid) where T : WsSqlTableBase, new()
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlTableBase.IdentityValueUid),  uid));
        return GetItemByCrud<T>(sqlCrudConfig);
    }

    public T GetItemById<T>(long id) where T : WsSqlTableBase, new() {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlTableBase.IdentityValueId),  id));
        return GetItemByCrud<T>(sqlCrudConfig);
    }

    public T GetItemByIdentity<T>(WsSqlFieldIdentityModel? identity) where T : WsSqlTableBase, new() {
        return identity?.Name switch
        {
            WsSqlEnumFieldIdentity.Uid => GetItemByUid<T>(identity.Uid),
            WsSqlEnumFieldIdentity.Id => GetItemById<T>(identity.Id),
            _ => GetItemNewEmpty<T>()
        };
    }

    public T GetItemFirst<T>() where T : WsSqlTableBase, new()
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.SelectTopRowsCount = 1;
        T result = GetItemByCrud<T>(sqlCrudConfig);
        result.FillProperties();
        return result;
    }

    public T GetItemNewEmpty<T>() where T : WsSqlTableBase, new()
    {
        T result = new();
        result.FillProperties();
        return result;
    }

    #endregion

    #region Public and private methods - GetList

    public List<T> GetList<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        List<T> items = new();
        ExecuteSelectCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            items.AddRange(criteria.List<T>());
        });
        return items;
    }

    public IEnumerable<T> GetEnumerable<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        IEnumerable<T> items = Enumerable.Empty<T>();
        ExecuteSelectCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            items = criteria.List<T>();
        });
        return items;
    }
    
    public object[] GetArrayObjects(string query, List<SqlParameter>? parameters = null)
    {
        parameters ??= new();
        object[] result = Array.Empty<object>();
        ExecuteSelectCore(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
            if (sqlQuery is null)
                return;
            
            IList? listEntities = sqlQuery.List();
            result = new object[listEntities.Count];
            for (int i = 0; i < result.Length; i++)
            {
                if (listEntities[i] is object[] records)
                    result[i] = records;
                else
                    result[i] = listEntities[i];
            }
        });
        return result;
    }
    
    #endregion
}