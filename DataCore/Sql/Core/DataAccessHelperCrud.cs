// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using FluentNHibernate.Conventions;
using NHibernate;

namespace DataCore.Sql.Core;

public static class DataAccessHelperCrud
{
	#region Public and private methods

	public static ICriteria GetCriteria<T>(this ISession session, SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		ICriteria criteria = session.CreateCriteria(typeof(T));
		if (sqlCrudConfig.MaxResults > 0)
			criteria.SetMaxResults(sqlCrudConfig.MaxResults);
		criteria.SetCriteriaFilters(sqlCrudConfig.Filters);
		criteria.SetCriteriaOrder(sqlCrudConfig.Orders);
		return criteria;
	}

	public static void ExecuteCore(this DataAccessHelper dataAccess, DataAccessHelper.ExecCallback callback, bool isTransaction)
	{
		ISession? session = null;
		Exception? exception = null;
		ITransaction? transaction = null;

		try
		{
			if (dataAccess.SessionFactory is not null)
			{
				session = dataAccess.SessionFactory.OpenSession();
				if (isTransaction)
				    transaction = session.BeginTransaction();
				callback.Invoke(session);
				session.Flush();
                if (isTransaction)
                    transaction?.Commit();
			}
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
			dataAccess.LogError(exception);
		}
	}

    private static void ExecuteTransaction(this DataAccessHelper dataAccess, DataAccessHelper.ExecCallback callback) => 
        ExecuteCore(dataAccess, callback, true);

    public static void ExecuteSelect(this DataAccessHelper dataAccess, DataAccessHelper.ExecCallback callback) => 
        ExecuteCore(dataAccess, callback, false);

    public static bool IsConnected(this DataAccessHelper dataAccess)
	{
		bool result = false;
		ExecuteSelect(dataAccess, session =>
		{
			result = session.IsConnected;
        });
		return result;
	}

	public static ISQLQuery? GetSqlQuery(this DataAccessHelper dataAccess, ISession session, string query)
	{
		if (string.IsNullOrEmpty(query))
			return null;

		return session.CreateSQLQuery(query);
	}

	public static int ExecQueryNative(this DataAccessHelper dataAccess, string query, Dictionary<string, object>? parameters)
	{
		int result = 0;
		ExecuteTransaction(dataAccess, session =>
		{
			ISQLQuery? sqlQuery = GetSqlQuery(dataAccess, session, query);
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

	public static void Save<T>(this DataAccessHelper dataAccess, T? item) where T : SqlTableBase, new()
	{
		if (item is null) return;

        item.ClearNullProperties();
        item.CreateDt = DateTime.Now;
		item.ChangeDt = DateTime.Now;
		ExecuteTransaction(dataAccess, session => { session.Save(item); });
	}

	public static void Update<T>(this DataAccessHelper dataAccess, T? item) where T : SqlTableBase, new()
	{
		if (item is null) return;
        
        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
		ExecuteTransaction(dataAccess, session => { session.SaveOrUpdate(item); });
	}

	public static void Delete<T>(this DataAccessHelper dataAccess, T? item) where T : SqlTableBase, new()
	{
		if (item is null)
			return;

		ExecuteTransaction(dataAccess, session => { session.Delete(item); });
	}

	public static void Mark<T>(this DataAccessHelper dataAccess, T? item) where T : SqlTableBase, new()
	{
		if (item is null)
			return;

		item.IsMarked = !item.IsMarked;
		ExecuteTransaction(dataAccess, session => { session.SaveOrUpdate(item); });
	}

	public static bool IsExistsItem<T>(this DataAccessHelper dataAccess, T? item) where T : SqlTableBase, new()
	{
		if (item is null)
			return false;

		bool result = false;
        ExecuteSelect(dataAccess, session =>
		{
			result = session.Query<T>().Any(x => x.IsAny(item));
		});
		return result;
	}

	public static bool IsExistsItem<T>(this DataAccessHelper dataAccess, SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		bool result = false;
		sqlCrudConfig.MaxResults = 1;
        ExecuteSelect(dataAccess, session =>
		{
			result = session.GetCriteria<T>(sqlCrudConfig).List<T>().Any();
		});
		return result;
	}

	#endregion
}
