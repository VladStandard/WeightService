// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Interfaces;
using DataCore.Sql.Core.Utils;
using NHibernate;
using System.Threading.Tasks;

namespace DataCore.Sql.Core.Helpers;

public partial class DataAccessHelper
{
    #region Public and private methods

    private ICriteria GetCriteria<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        ICriteria criteria = session.CreateCriteria(typeof(T));
        if (sqlCrudConfig.ResultMaxCount > 0)
        {
            if (sqlCrudConfig.IsResultShowOnlyTop)
                criteria.SetMaxResults(sqlCrudConfig.ResultMaxCount);
            else if (sqlCrudConfig.ResultMaxCount == 1)
                criteria.SetMaxResults(sqlCrudConfig.ResultMaxCount);
        }
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

    private (bool IsOk, Exception? Exception) ExecuteCore(ExecCallback callback, bool isTransaction)
    {
        ISession? session = null;
        Exception? exception = null;
        ITransaction? transaction = null;

        try
        {
            session = SessionFactory.OpenSession();
            if (isTransaction)
                transaction = session.BeginTransaction();
            callback.Invoke(session);
            session.Flush();
            if (isTransaction)
                transaction?.Commit();
        }
        catch (Exception ex)
        {
            if (isTransaction)
                transaction?.Rollback();
            exception = ex;
            //throw;
        }
        finally
        {
            if (isTransaction)
                transaction?.Dispose();
            if (session is not null)
            {
                session.Disconnect();
                session.Close();
                session.Dispose();
            }
        }

        if (exception is not null)
        {
            LogError(exception, "", nameof(DataCore));
            return (false, exception);
        }
        return (true, null);
    }

    private (bool IsOk, Exception? Exception) ExecuteTransaction(ExecCallback callback) => ExecuteCore(callback, true);

    private (bool IsOk, Exception? Exception) ExecuteSelect(ExecCallback callback) => ExecuteCore(callback, false);

    public bool IsConnected()
    {
        bool result = false;
        ExecuteSelect(session =>
        {
            result = session.IsConnected;
        });
        return result;
    }

    [Obsolete(@"Use GetSqlQuery(ISession session, string query, List<SqlParameter> parameters)")]
    private ISQLQuery? GetSqlQuery(ISession session, string query)
    {
        if (string.IsNullOrEmpty(query)) return null;

        return session.CreateSQLQuery(query);
    }

    private ISQLQuery? GetSqlQuery(ISession session, string query, List<SqlParameter> parameters)
    {
        if (string.IsNullOrEmpty(query)) return null;

        ISQLQuery sqlQuery = session.CreateSQLQuery(query);
        foreach (SqlParameter parameter in parameters)
        {
            sqlQuery.SetParameter(parameter.ParameterName, parameter.Value);
        }
        return sqlQuery;
    }

    public int ExecQueryNative(string query, Dictionary<string, object>? parameters)
    {
        int result = 0;
        ExecuteTransaction(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query);
            if (sqlQuery is not null && parameters is not null)
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

    public (bool IsOk, Exception? Exception) Save<T>(T? item) where T : ISqlTable
    {
        if (item is null) return (false, null);

        item.ClearNullProperties();
        item.CreateDt = DateTime.Now;
        item.ChangeDt = DateTime.Now;
        return ExecuteTransaction(session => { session.Save(item); });
    }

    public async Task<(bool IsOk, Exception? Exception)> SaveAsync<T>(T? item) where T : ISqlTable
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        return Save(item);
    }

    public (bool IsOk, Exception? Exception) Save<T>(T? item, SqlFieldIdentityModel? identity) where T : ISqlTable
    {
        if (item is null) return (false, null);

        item.ClearNullProperties();
        item.CreateDt = DateTime.Now;
        item.ChangeDt = DateTime.Now;
        object? id = identity?.GetValueAsObjectNullable();
        if (id is null)
            return ExecuteTransaction(session => { session.Save(item); });
        else
            return ExecuteTransaction(session => { session.Save(item, id); });
    }

    [Obsolete(@"Use SaveOrUpdate or UpdateForce")]
    public (bool IsOk, Exception? Exception) Update<T>(T? item) where T : ISqlTable
    {
        if (item is null) return (false, null);

        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
        return ExecuteTransaction(session => { session.SaveOrUpdate(item); });
    }

    public (bool IsOk, Exception? Exception) SaveOrUpdate<T>(T? item) where T : ISqlTable
    {
        if (item is null) return (false, null);

        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
        return ExecuteTransaction(session => { session.SaveOrUpdate(item); });
    }

    public (bool IsOk, Exception? Exception) UpdateForce<T>(T? item) where T : ISqlTable
    {
        if (item is null) return (false, null);

        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
        return ExecuteTransaction(session => { session.Update(item); });
    }

    public (bool IsOk, Exception? Exception) Delete<T>(T? item) where T : ISqlTable
    {
        if (item is null) return (false, null);

        return ExecuteTransaction(session => { session.Delete(item); });
    }

    public (bool IsOk, Exception? Exception) Mark<T>(T? item) where T : ISqlTable
    {
        if (item is null) return (false, null);

        item.IsMarked = !item.IsMarked;
        return ExecuteTransaction(session => { session.SaveOrUpdate(item); });
    }

    #endregion
}
