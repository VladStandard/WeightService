using System.Reflection;
using Microsoft.Extensions.Configuration;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using Ws.StorageCore.Listeners;

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
    private static SqlSettingsModels SqlSettingsModels { get; set; } = new();
    private ISessionFactory SessionFactory { get; set; } = null!;
    private Configuration SqlConfiguration { get; set; } = new();

    public void SetSessionFactory()
    {
        SetSqlConfiguration();
        AddConfigurationMappings();
        SessionFactory = SqlConfiguration.BuildSessionFactory();
    }
    
    private static SqlSettingsModels LoadJsonConfig()
    {
        IConfigurationRoot sqlConfiguration = new ConfigurationBuilder()
            .AddJsonFile("sqlconfig.json", optional: false, reloadOnChange: false)
            .Build();
        
        SqlSettingsModels sqlSettingsModels = new();
        sqlConfiguration.GetSection("SqlSettings").Bind(sqlSettingsModels);
        return sqlSettingsModels;
    }
    
    private void SetSqlConfiguration()
    {
        SqlSettingsModels = LoadJsonConfig();
        SqlConfiguration = new();
        SqlConfiguration.DataBaseIntegration(db => {
            db.ConnectionString = SqlSettingsModels.GetConnectionString();
            db.Dialect<MsSql2012Dialect>();
            db.Driver<SqlClientDriver>();
            db.LogSqlInConsole = SqlSettingsModels.IsShowSql;
        });
        SqlConfiguration.EventListeners.PreInsertEventListeners =
            [new SqlCreateDtListener()];
        SqlConfiguration.EventListeners.PreUpdateEventListeners =
            [new SqlChangeDtListener()];
    }
    
    #endregion

    #region Public and private methods - Base

    private void AddConfigurationMappings()
    {
        ModelMapper mapper = new();
        mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
        HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
        SqlConfiguration.AddMapping(mapping);
    }
    
    #endregion
    
    #region Public and private methods - Base

    private static ICriteria GetCriteria<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : class, new()
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
    
    private void ExecuteSelectCore(Action<ISession> action)
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
        }
    }

    private void ExecuteTransactionCore(Action<ISession> action)
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
            session.Disconnect();
            session.Close();
        }
    }
    
    private ISQLQuery? GetSqlQuery(ISession session, string query, IEnumerable<SqlParameter> parameters)
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

    public void SaveOrUpdate<T>(T item) where T : SqlEntityBase
    {
        if (item.IsNew) 
        {
            Save(item);
            return;
        }
        Update(item);
    }
    
    public void Save<T>(T item) where T : SqlEntityBase
    {
        ExecuteTransactionCore(session => session.Save(item));
    }

    public void Update<T>(T item) where T : SqlEntityBase
    {
        ExecuteTransactionCore(session => session.Update(item));
    }

    public void Delete<T>(T item) where T : SqlEntityBase
    {
        ExecuteTransactionCore(session => session.Delete(item));
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
        return item ?? new();
    }
    
    public T GetItemByUid<T>(Guid uid) where T : SqlEntityBase, new()
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlEntityBase.IdentityValueUid),  uid));
        return GetItemByCrud<T>(sqlCrudConfig);
    }

    public T GetItemById<T>(long id) where T : SqlEntityBase, new() {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlEntityBase.IdentityValueId),  id));
        return GetItemByCrud<T>(sqlCrudConfig);
    }

    #endregion

    #region Public and private methods - GetList

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
    
    public IEnumerable<Object> GetArrayObjects(string query, List<SqlParameter>? parameters = null)
    {
        parameters ??= [];
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