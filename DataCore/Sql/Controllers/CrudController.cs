// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using FluentNHibernate.Conventions;
using NHibernate;

namespace DataCore.Sql.Controllers;

public partial class CrudController
{
    #region Public and private fields, properties, constructor

    private DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
    private DataConfigurationModel DataConfig { get; }

    private delegate void ExecCallback(ISession session);

    #endregion

    #region Constructor and destructor

    public CrudController()
    {
        DataConfig = new();
    }

    #endregion

    #region Public and private methods

    public T[]? GetItemsWithConfig<T>() where T : TableModel, new()
    {
        T[]? result = null;
        ExecuteTransaction(session =>
        {
            if (DataConfig != null)
            {
                result = DataConfig.OrderAsc
                    ? session.Query<T>()
                    .OrderBy(ent => ent)
                    .Skip(DataConfig.PageNo * DataConfig.PageSize)
                    .Take(DataConfig.PageSize)
                    .ToArray()
                    : session.Query<T>()
                        .OrderByDescending(ent => ent)
                        .Skip(DataConfig.PageNo * DataConfig.PageSize)
                        .Take(DataConfig.PageSize)
                        .ToArray()
                    ;
            }
        });
        return result;
    }

    private ICriteria GetCriteria<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : TableModel, new()
    {
        ICriteria criteria = session.CreateCriteria(typeof(T));
        if (sqlCrudConfig.MaxResults > 0)
            criteria.SetMaxResults(sqlCrudConfig.MaxResults);
        criteria.SetCriteriaFilters(sqlCrudConfig.Filters);
        criteria.SetCriteriaOrder(sqlCrudConfig.Order);
        return criteria;
    }

    private void ExecuteTransaction(ExecCallback callback)
    {
        using ISession? session = DataAccess.SessionFactory.OpenSession();
        Exception? exception = null;
        if (session != null)
        {
            using ITransaction? transaction = session.BeginTransaction();
            try
            {
                callback.Invoke(session);
                session.Flush();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                exception = ex;
                //throw;
            }
            finally
            {
                transaction.Dispose();
                session.Disconnect();
                session.Close();
                session.Dispose();
            }
        }
        if (exception != null)
        {
            DataAccess.Log.LogError(exception);
        }
    }

    public ISQLQuery? GetSqlQuery(ISession session, string query)
    {
        if (string.IsNullOrEmpty(query))
            return null;

        return session.CreateSQLQuery(query);
    }

    public int ExecQueryNative(string query, Dictionary<string, object>? parameters)
    {
        int result = 0;
        ExecuteTransaction(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query);
            if (sqlQuery != null && parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    if (parameter.Value is byte[] imagedata)
                        sqlQuery.SetParameter(parameter.Key, imagedata);
                    else
                        sqlQuery.SetParameter(parameter.Key, parameter.Value);
                }
                result = sqlQuery.ExecuteUpdate();
            }
        });
        return result;
    }

    public void Save<T>(T item) where T : TableModel, new() => ExecuteTransaction(session => { session.Save(item); });

    public void Update<T>(T item) where T : TableModel, new()
    {
        item.ChangeDt = DateTime.Now;
        ExecuteTransaction(session => { session.SaveOrUpdate(item); });
    }

    public void Delete<T>(T item) where T : TableModel, new() => ExecuteTransaction(session => { session.Delete(item); });

    public void Mark<T>(T item) where T : TableModel, new()
    {
        item.IsMarked = true;
        ExecuteTransaction(session => { session.SaveOrUpdate(item); });
    }

    private bool IsExistsItem<T>(T item) where T : TableModel, new()
    {
        bool result = false;
        ExecuteTransaction(session =>
        {
            result = session.Query<T>().Any(x => x.IsAny(item));
        });
        return result;
    }

    private bool IsExistsItem<T>(SqlCrudConfigModel sqlCrudConfig) where T : TableModel, new()
    {
        bool result = false;
        sqlCrudConfig.MaxResults = 1;
		ExecuteTransaction(session =>
        {
            result = GetCriteria<T>(session, sqlCrudConfig).List<T>().Count > 0;
        });
        return result;
    }

    #endregion
}
