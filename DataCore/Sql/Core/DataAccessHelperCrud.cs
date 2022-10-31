// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using FluentNHibernate.Conventions;
using NHibernate;

namespace DataCore.Sql.Core;

public partial class DataAccessHelper
{
	#region Public and private methods

	public ICriteria GetCriteria<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		ICriteria criteria = session.CreateCriteria(typeof(T));
		if (sqlCrudConfig.MaxResults > 0)
			criteria.SetMaxResults(sqlCrudConfig.MaxResults);
		criteria.SetCriteriaFilters(sqlCrudConfig.Filters);
		criteria.SetCriteriaOrder(sqlCrudConfig.Orders);
		return criteria;
	}

	public void ExecuteCore(ExecCallback callback, bool isTransaction)
	{
		ISession? session = null;
		Exception? exception = null;
		ITransaction? transaction = null;

		try
		{
			if (SessionFactory is not null)
			{
				session = SessionFactory.OpenSession();
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
			LogError(exception);
		}
	}

    private void ExecuteTransaction(DataAccessHelper.ExecCallback callback) => 
        ExecuteCore(callback, true);

    public void ExecuteSelect(DataAccessHelper.ExecCallback callback) => 
        ExecuteCore(callback, false);

    public bool IsConnected()
	{
		bool result = false;
		ExecuteSelect(session =>
		{
			result = session.IsConnected;
        });
		return result;
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

	public void Save<T>(T? item) where T : SqlTableBase, new()
	{
		if (item is null) return;

        item.ClearNullProperties();
        item.CreateDt = DateTime.Now;
		item.ChangeDt = DateTime.Now;
		ExecuteTransaction(session => { session.Save(item); });
	}

	public void Update<T>(T? item) where T : SqlTableBase, new()
	{
		if (item is null) return;
        
        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
		ExecuteTransaction(session => { session.SaveOrUpdate(item); });
	}

	public void Delete<T>(T? item) where T : SqlTableBase, new()
	{
		if (item is null)
			return;

		ExecuteTransaction(session => { session.Delete(item); });
	}

	public void Mark<T>(T? item) where T : SqlTableBase, new()
	{
		if (item is null)
			return;

		item.IsMarked = !item.IsMarked;
		ExecuteTransaction(session => { session.SaveOrUpdate(item); });
	}

	public bool IsExistsItem<T>(T? item) where T : SqlTableBase, new()
	{
		if (item is null)
			return false;

		bool result = false;
        ExecuteSelect(session =>
		{
			result = session.Query<T>().Any(x => x.IsAny(item));
		});
		return result;
	}

	public bool IsExistsItem<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		bool result = false;
		sqlCrudConfig.MaxResults = 1;
        ExecuteSelect(session =>
		{
			result = GetCriteria<T>(session, sqlCrudConfig).List<T>().Any();
		});
		return result;
	}

	#endregion
}
