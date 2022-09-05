// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using NHibernate;

namespace DataCore.Sql.Core;

public static class DataAccessHelperItems
{
	#region Public and private methods

	private static T[] GetItemsCore<T>(this DataAccessHelper dataAccess, ISession session, SqlCrudConfigModel sqlCrudConfig) where T : TableModel, new()
	{
		ICriteria criteria = dataAccess.GetCriteria<T>(session, sqlCrudConfig);
		return criteria.List<T>().ToArray();
	}

	public static T[]? GetItems<T>(this DataAccessHelper dataAccess, SqlCrudConfigModel sqlCrudConfig) where T : TableModel, new()
	{
		T[]? items = null;
		dataAccess.ExecuteTransaction((session) =>
		{
			items = GetItemsCore<T>(dataAccess, session, sqlCrudConfig);
			foreach (T item in items)
			{
				dataAccess.FillReferences(item);
			}
		});
		return items;
	}

	public static T[]? GetItems<T>(this DataAccessHelper dataAccess, string query) where T : TableModel, new()
	{
		T[]? result = null;
		dataAccess.ExecuteTransaction((session) =>
		{
			ISQLQuery? sqlQuery = dataAccess.GetSqlQuery(session, query);
			if (sqlQuery != null)
			{
				sqlQuery.AddEntity(typeof(T));
				result = sqlQuery.List<T>().ToArray();
			}
		});
		return result;
	}

	public static List<T> GetList<T>(this DataAccessHelper dataAccess, SqlCrudConfigModel sqlCrudConfig) where T : TableModel, new()
	{
		T[]? items = GetItems<T>(dataAccess, sqlCrudConfig);
		if (items is not null && items.Length > 0)
			return items.ToList();
		return new();
	}

	public static object[] GetObjects(this DataAccessHelper dataAccess, string query)
	{
		object[] result = Array.Empty<object>();
		dataAccess.ExecuteTransaction((session) =>
		{
			ISQLQuery? sqlQuery = dataAccess.GetSqlQuery(session, query);
			if (sqlQuery != null)
			{
				System.Collections.IList? listEntities = sqlQuery.List();
				result = new object[listEntities.Count];
				for (int i = 0; i < result.Length; i++)
				{
					if (listEntities[i] is object[] records)
						result[i] = records;
					else
						result[i] = listEntities[i];
				}
			}
		});
		return result;
	}

	#endregion
}
