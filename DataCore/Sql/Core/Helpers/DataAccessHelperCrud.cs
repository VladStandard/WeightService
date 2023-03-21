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

    private SqlCrudResultModel ExecuteCore(Action<ISession> action, bool isTransaction)
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
            LogError(exception);
            return new() { IsOk = false, Exception = exception };
        }
        return new() { IsOk = true, Exception = null };
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

    public SqlCrudResultModel ExecQueryNative(string query, List<SqlParameter> parameters)
    {
        if (string.IsNullOrEmpty(query)) return new() { IsOk = false, Exception = null };
        return ExecuteCore(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
            if (sqlQuery is not null)
            {
                _ = sqlQuery.ExecuteUpdate();
            }
        }, true);
    }

    public SqlCrudResultModel ExecQueryNative(string query, SqlParameter parameter) =>
        ExecQueryNative(query, new List<SqlParameter> { parameter });

    public SqlCrudResultModel Save<T>(T? item) where T : ISqlTable
    {
        if (item is null) return new() { IsOk = false, Exception = null };

        item.ClearNullProperties();
        item.CreateDt = DateTime.Now;
        item.ChangeDt = DateTime.Now;
        return ExecuteCore(session => session.Save(item), true);
    }

    public async Task<SqlCrudResultModel> SaveAsync<T>(T? item) where T : ISqlTable
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        return Save(item);
    }

    public SqlCrudResultModel Save<T>(T? item, SqlFieldIdentityModel? identity) where T : ISqlTable
    {
        if (item is null) return new() { IsOk = false, Exception = null };

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

    public SqlCrudResultModel SaveOrUpdate<T>(T? item) where T : ISqlTable
    {
        if (item is null) return new() { IsOk = false, Exception = null};

        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
        return ExecuteCore(session => session.SaveOrUpdate(item), true);
    }

    public SqlCrudResultModel UpdateForce<T>(T? item) where T : ISqlTable
    {
        if (item is null) return new() { IsOk = false, Exception = null };

        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
        return ExecuteCore(session => session.Update(item), true);
    }

    public SqlCrudResultModel Delete<T>(T? item) where T : ISqlTable
    {
        if (item is null) return new() { IsOk = false, Exception = null };

        return ExecuteCore(session => session.Delete(item), true);
    }

    public SqlCrudResultModel Mark<T>(T? item) where T : ISqlTable
    {
        if (item is null) return new() { IsOk = false, Exception = null };

        item.IsMarked = !item.IsMarked;
        return ExecuteCore(session => session.SaveOrUpdate(item), true);
    }

    #endregion
}