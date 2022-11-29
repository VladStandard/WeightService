// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using NHibernate;
using System;

namespace DataCore.Sql.Core;

public partial class DataAccessHelper
{
	#region Public and private methods

	private ICriteria GetCriteria<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : class, new()
	{
		ICriteria criteria = session.CreateCriteria(typeof(T));
		if (sqlCrudConfig.ResultMaxCount > 0)
			criteria.SetMaxResults(sqlCrudConfig.ResultMaxCount);
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

	private (bool isOk, Exception? exception) ExecuteCore(ExecCallback callback, bool isTransaction)
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

    private (bool isOk, Exception? exception) ExecuteTransaction(ExecCallback callback) => ExecuteCore(callback, true);

    private (bool isOk, Exception? exception) ExecuteSelect(ExecCallback callback) => ExecuteCore(callback, false);

    public bool IsConnected()
	{
		bool result = false;
		ExecuteSelect(session =>
		{
			result = session.IsConnected;
        });
		return result;
	}

    private ISQLQuery? GetSqlQuery(ISession session, string query)
	{
		if (string.IsNullOrEmpty(query)) return null;

		return session.CreateSQLQuery(query);
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

	public (bool isOk, Exception? exception) Save<T>(T? item) where T : SqlTableBase, new()
	{
		if (item is null) return (false, null);

        item.ClearNullProperties();
        item.CreateDt = DateTime.Now;
		item.ChangeDt = DateTime.Now;
		return ExecuteTransaction(session => { session.Save(item); });
	}

	public (bool isOk, Exception? exception) Update<T>(T? item) where T : SqlTableBase, new()
	{
		if (item is null) return (false, null);
        
        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
		return ExecuteTransaction(session => { session.SaveOrUpdate(item); });
	}

	public void Delete<T>(T? item) where T : SqlTableBase, new()
	{
		if (item is null) return;

		ExecuteTransaction(session => { session.Delete(item); });
	}

	public void Mark<T>(T? item) where T : SqlTableBase, new()
	{
		if (item is null) return;

		item.IsMarked = !item.IsMarked;
		ExecuteTransaction(session => { session.SaveOrUpdate(item); });
	}

	#endregion
}
