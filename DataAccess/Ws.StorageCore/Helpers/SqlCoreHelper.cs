using Microsoft.Extensions.Configuration;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Enums;

namespace Ws.StorageCore.Helpers;

public sealed class SqlCoreHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static SqlCoreHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static SqlCoreHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private object LockerSessionFactory { get; } = new();
    private object LockerSelect { get; } = new();
    private object LockerExecute { get; } = new();

    public static SqlSettings SqlSettings { get; set; } = new();
    
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
    
    private static SqlSettings LoadJsonConfig()
    {
        IConfigurationRoot sqlConfiguration = new ConfigurationBuilder()
            .AddJsonFile("sqlconfig.json", optional: false, reloadOnChange: false)
            .Build();
        
        SqlSettings sqlSettings = new();
        sqlConfiguration.GetSection("SqlSettings").Bind(sqlSettings);
        return sqlSettings;
    }
    
    private void SetSqlConfiguration(bool isShowSql)
    {
        SqlSettings = LoadJsonConfig();
        SqlConfiguration = new();
        SqlConfiguration.DataBaseIntegration(db => {
            db.ConnectionString = SqlSettings.GetConnectionString();
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

    ~SqlCoreHelper() => Close();

    #endregion

    #region Public and private methods - Base

    private void AddConfigurationMappings()
    {
        ModelMapper mapper = new();
        
        mapper.AddMapping<SqlAccessMap>();
        mapper.AddMapping<SqlBoxMap>();
        mapper.AddMapping<SqlClipMap>();
        mapper.AddMapping<SqlPluClipFkMap>();
        mapper.AddMapping<SqlBundleMap>();
        mapper.AddMapping<SqlBrandMap>();
        mapper.AddMapping<SqlProductionSiteMap>();
        mapper.AddMapping<SqlWorkshopMap>();
        mapper.AddMapping<SqlAppMap>();
        mapper.AddMapping<SqlTemplateMap>();
        mapper.AddMapping<SqlTemplateResourceMap>();
        mapper.AddMapping<SqlVersionMap>();
        mapper.AddMapping<SqlPrinterMap>();
        mapper.AddMapping<SqlHostMap>();
        mapper.AddMapping<SqlLineMap>();
        mapper.AddMapping<SqlPluMap>();
        mapper.AddMapping<SqlPluScaleMap>();
        mapper.AddMapping<SqlPluWeighingMap>();
        mapper.AddMapping<SqlPluLabelMap>();
        mapper.AddMapping<SqlBarCodeMap>();
        mapper.AddMapping<SqlPluStorageMethodMap>();
        mapper.AddMapping<WsSqlLogWebMap>();
        mapper.AddMapping<SqlPluFkMap>();
        mapper.AddMapping<SqlPluNestingFkMap>();
        mapper.AddMapping<SqlPluStorageMethodFkMap>();
        mapper.AddMapping<SqlPluTemplateFkMap>();
        mapper.AddMapping<SqlLogMap>();
        
        HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
        SqlConfiguration.AddMapping(mapping);
    }
    
    #endregion

    #region Public and private methods - Base

    private ICriteria GetCriteria<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : class, new()
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
            catch (Exception)
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
    private void ExecuteTransactionCore(Action<ISession> action)
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
                transaction.Commit();
                session.Clear();
            }
            catch (Exception)
            {
                transaction.Rollback();
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
    
    #endregion
    
    #region CRUD

    public void Save<T>(T? item, SqlEnumSessionType sessionType = SqlEnumSessionType.Isolated) where T : SqlEntityBase
{
    if (item is null) throw new ArgumentException();
    item.CreateDt = DateTime.Now;
    item.ChangeDt = DateTime.Now;
    
    switch (sessionType)
    {
        case SqlEnumSessionType.Isolated:
            ExecuteTransactionCore(session => session.Save(item));
            break;
        case SqlEnumSessionType.IsolatedAsync:
            ExecuteTransactionCore(session => session.SaveAsync(item));
            break;
        default:
            throw new ArgumentOutOfRangeException(nameof(sessionType), sessionType, null);
    }
}

public void Update<T>(T? item, SqlEnumSessionType sessionType = SqlEnumSessionType.Isolated) where T : SqlEntityBase
{
    if (item is null) throw new ArgumentException();
    item.ChangeDt = DateTime.Now;

    switch (sessionType)
    {
        case SqlEnumSessionType.Isolated:
            ExecuteTransactionCore(session => session.Update(item));
            break;
        case SqlEnumSessionType.IsolatedAsync:
            ExecuteTransactionCore(session => session.UpdateAsync(item));
            break;
        default:
            throw new ArgumentOutOfRangeException(nameof(sessionType), sessionType, null);
    }
}

public void Delete<T>(T? item, SqlEnumSessionType sessionType = SqlEnumSessionType.Isolated) where T : SqlEntityBase
{
    if (item is null) throw new ArgumentException();

    switch (sessionType)
    {
        case SqlEnumSessionType.Isolated:
            ExecuteTransactionCore(session => session.Delete(item));
            break;
        case SqlEnumSessionType.IsolatedAsync:
            ExecuteTransactionCore(session => session.DeleteAsync(item));
            break;
        default:
            throw new ArgumentOutOfRangeException(nameof(sessionType), sessionType, null);
    }
    
}

public void Mark<T>(T? item, SqlEnumSessionType sessionType = SqlEnumSessionType.Isolated) where T : SqlEntityBase
{
    if (item is null) throw new ArgumentException();

    item.IsMarked = !item.IsMarked;

    switch (sessionType)
    {
        case SqlEnumSessionType.Isolated:
            ExecuteTransactionCore(session => session.Update(item));
            break;
        case SqlEnumSessionType.IsolatedAsync:
            ExecuteTransactionCore(session => session.UpdateAsync(item));
            break;
        default:
            throw new ArgumentOutOfRangeException(nameof(sessionType), sessionType, null);
    }
}

#endregion
    
    #region Public and private methods - GetItem

    public T GetItemByCrud<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlEntityBase, new()
    {
        T? item = null;
        ExecuteSelectCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            item = criteria.UniqueResult<T>();
        });
        return item ?? GetItemNewEmpty<T>();;
    }
    
    public T GetItemByUid<T>(Guid uid) where T : SqlEntityBase, new()
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlEntityBase.IdentityValueUid),  uid));
        return GetItemByCrud<T>(sqlCrudConfig);
    }

    public T GetItemById<T>(long id) where T : SqlEntityBase, new() {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlEntityBase.IdentityValueId),  id));
        return GetItemByCrud<T>(sqlCrudConfig);
    }

    public T GetItemByIdentity<T>(SqlFieldIdentityModel? identity) where T : SqlEntityBase, new() {
        return identity?.Name switch
        {
            SqlEnumFieldIdentity.Uid => GetItemByUid<T>(identity.Uid),
            SqlEnumFieldIdentity.Id => GetItemById<T>(identity.Id),
            _ => GetItemNewEmpty<T>()
        };
    }

    public T GetItemFirst<T>() where T : SqlEntityBase, new()
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.SelectTopRowsCount = 1;
        T result = GetItemByCrud<T>(sqlCrudConfig);
        result.FillProperties();
        return result;
    }

    public T GetItemNewEmpty<T>() where T : SqlEntityBase, new()
    {
        T result = new();
        result.FillProperties();
        return result;
    }

    #endregion

    #region Public and private methods - GetList

    public List<T> GetList<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlEntityBase, new()
    {
        List<T> items = new();
        ExecuteSelectCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            items.AddRange(criteria.List<T>());
        });
        return items;
    }

    public IEnumerable<T> GetEnumerable<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlEntityBase, new()
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