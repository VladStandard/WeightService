using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using WsStorageCore.Entities.SchemaRef.Hosts;
using WsStorageCore.Entities.SchemaRef.Printers;
using WsStorageCore.OrmUtils;
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

    private Configuration SqlConfiguration { get; set; } = new();

    public void SetSessionFactory(bool isShowSql)
    {
        lock (LockerSessionFactory)
        {
            SetSqlConfiguration(isShowSql);
            AddConfigurationMappings();
            SessionFactory = SqlConfiguration.BuildSessionFactory();
        }
    }
    
    private void SetSqlConfiguration(bool isShowSql)
    {
        SqlConfiguration = new();
        SqlConfiguration.DataBaseIntegration(db => {
            db.ConnectionString = GetConnectionString();
            db.Dialect<MsSql2012Dialect>();
            db.Driver<SqlClientDriver>();
            db.LogSqlInConsole = isShowSql;
        });
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

    private void AddConfigurationMappings()
    {
        ModelMapper mapper = new();
        
        mapper.AddMapping<WsSqlAccessMap>();
        mapper.AddMapping<WsSqlBoxMap>();
        mapper.AddMapping<WsSqlClipMap>();
        mapper.AddMapping<WsSqlPluClipFkMap>();
        mapper.AddMapping<WsSqlBundleMap>();
        mapper.AddMapping<WsSqlBrandMap>();
        mapper.AddMapping<WsSqlProductionSiteMap>();
        mapper.AddMapping<WsSqlWorkshopMap>();
        mapper.AddMapping<WsSqlAppMap>();
        mapper.AddMapping<WsSqlLogTypeMap>();
        mapper.AddMapping<WsSqlTemplateMap>();
        mapper.AddMapping<WsSqlTemplateResourceMap>();
        mapper.AddMapping<WsSqlVersionMap>();
        mapper.AddMapping<WsSqlPrinterMap>();
        mapper.AddMapping<WsSqlHostMap>();
        mapper.AddMapping<WsSqlScaleMap>();
        mapper.AddMapping<WsSqlPluMap>();
        mapper.AddMapping<WsSqlPluScaleMap>();
        mapper.AddMapping<WsSqlPluWeighingMap>();
        mapper.AddMapping<WsSqlPluLabelMap>();
        mapper.AddMapping<WsSqlBarCodeMap>();
        mapper.AddMapping<WsSqlOrganizationMap>();
        mapper.AddMapping<WsSqlPluStorageMethodMap>();
        mapper.AddMapping<WsSqlTaskTypeMap>();
        mapper.AddMapping<WsSqlTaskMap>();
        mapper.AddMapping<WsSqlLogWebMap>();
        mapper.AddMapping<WsSqlDeviceSettingsMap>();
        mapper.AddMapping<WsSqlDeviceSettingsFkMap>();
        mapper.AddMapping<WsSqlPluFkMap>();
        mapper.AddMapping<WsSqlPluNestingFkMap>();
        mapper.AddMapping<WsSqlPluStorageMethodFkMap>();
        mapper.AddMapping<WsSqlPluTemplateFkMap>();
        mapper.AddMapping<WsSqlLogMap>();
        
        HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
        SqlConfiguration.AddMapping(mapping);
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

    public void Save<T>(T? item, WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.Isolated) where T : WsSqlEntityBase
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

public void Update<T>(T? item, WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.Isolated) where T : WsSqlEntityBase
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

public void Delete<T>(T? item, WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.Isolated) where T : WsSqlEntityBase
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

public void Mark<T>(T? item, WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.Isolated) where T : WsSqlEntityBase
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

    public T GetItemByCrud<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlEntityBase, new()
    {
        T? item = null;
        ExecuteSelectCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            item = criteria.UniqueResult<T>();
        });
        return item ?? GetItemNewEmpty<T>();;
    }
    
    public T GetItemByUid<T>(Guid uid) where T : WsSqlEntityBase, new()
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlEntityBase.IdentityValueUid),  uid));
        return GetItemByCrud<T>(sqlCrudConfig);
    }

    public T GetItemById<T>(long id) where T : WsSqlEntityBase, new() {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlEntityBase.IdentityValueId),  id));
        return GetItemByCrud<T>(sqlCrudConfig);
    }

    public T GetItemByIdentity<T>(WsSqlFieldIdentityModel? identity) where T : WsSqlEntityBase, new() {
        return identity?.Name switch
        {
            WsSqlEnumFieldIdentity.Uid => GetItemByUid<T>(identity.Uid),
            WsSqlEnumFieldIdentity.Id => GetItemById<T>(identity.Id),
            _ => GetItemNewEmpty<T>()
        };
    }

    public T GetItemFirst<T>() where T : WsSqlEntityBase, new()
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.SelectTopRowsCount = 1;
        T result = GetItemByCrud<T>(sqlCrudConfig);
        result.FillProperties();
        return result;
    }

    public T GetItemNewEmpty<T>() where T : WsSqlEntityBase, new()
    {
        T result = new();
        result.FillProperties();
        return result;
    }

    #endregion

    #region Public and private methods - GetList

    public List<T> GetList<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlEntityBase, new()
    {
        List<T> items = new();
        ExecuteSelectCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            items.AddRange(criteria.List<T>());
        });
        return items;
    }

    public IEnumerable<T> GetEnumerable<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlEntityBase, new()
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