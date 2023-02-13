// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
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

    private (bool IsOk, Exception? Exception) ExecuteCore(Action<ISession> action, bool isTransaction)
    {
        ISession? session = null;
        Exception? exception = null;
        ITransaction? transaction = null;

        try
        {
            session = SessionFactory.OpenSession();
            if (isTransaction)
                transaction = session.BeginTransaction();
            session.FlushMode = isTransaction ? FlushMode.Commit : FlushMode.Manual;
            action.Invoke(session);
            if (isTransaction)
                session.Flush();
            if (isTransaction)
                transaction?.Commit();
            session.Clear();
        }
        catch (Exception ex)
        {
            if (isTransaction)
                transaction?.Rollback();
            exception = ex;
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

    public bool IsConnected()
    {
        bool result = false;
        ExecuteCore(session =>
        {
            result = session.IsConnected;
        }, false);
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
            if (parameter.Value is byte[] imagedata)
                sqlQuery.SetParameter(parameter.ParameterName, imagedata);
            else
                sqlQuery.SetParameter(parameter.ParameterName, parameter.Value);
        }
        return sqlQuery;
    }

    public (bool IsOk, Exception? Exception) ExecQueryNative(string query, List<SqlParameter> parameters)
    {
        if (string.IsNullOrEmpty(query)) return (false, null);
        return ExecuteCore(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
            if (sqlQuery is not null)
            {
                _ = sqlQuery.ExecuteUpdate();
            }
        }, true);
    }

    public (bool IsOk, Exception? Exception) ExecQueryNative(string query, SqlParameter parameter) =>
        ExecQueryNative(query, new List<SqlParameter> { parameter });

    public (bool IsOk, Exception? Exception) Save<T>(T? item) where T : ISqlTable
    {
        if (item is null) return (false, null);

        item.ClearNullProperties();
        item.CreateDt = DateTime.Now;
        item.ChangeDt = DateTime.Now;
        return ExecuteCore(session => session.Save(item), true);
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
        if (Equals(identity?.Name, SqlFieldIdentity.Uid) && Equals(id, Guid.Empty))
            id = Guid.NewGuid();
        return id is null 
            ? ExecuteCore(session => session.Save(item), true) 
            : ExecuteCore(session => session.Save(item, id), true);
    }

    [Obsolete(@"Use SaveOrUpdate or UpdateForce")]
    public (bool IsOk, Exception? Exception) Update<T>(T? item) where T : ISqlTable
    {
        if (item is null) return (false, null);

        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
        return ExecuteCore(session => session.SaveOrUpdate(item), true);
    }

    public (bool IsOk, Exception? Exception) SaveOrUpdate<T>(T? item) where T : ISqlTable
    {
        if (item is null) return (false, null);

        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
        return ExecuteCore(session => session.SaveOrUpdate(item), true);
    }

    public (bool IsOk, Exception? Exception) UpdateForce<T>(T? item) where T : ISqlTable
    {
        if (item is null) return (false, null);

        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
        return ExecuteCore(session => session.Update(item), true);
    }

    public (bool IsOk, Exception? Exception) Delete<T>(T? item) where T : ISqlTable
    {
        if (item is null) return (false, null);

        return ExecuteCore(session => session.Delete(item), true);
    }

    public (bool IsOk, Exception? Exception) Mark<T>(T? item) where T : ISqlTable
    {
        if (item is null) return (false, null);

        item.IsMarked = !item.IsMarked;
        return ExecuteCore(session => session.SaveOrUpdate(item), true);
    }

    #endregion
}