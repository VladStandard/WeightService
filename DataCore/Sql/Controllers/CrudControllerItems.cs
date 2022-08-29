// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Fields;
using DataCore.Sql.Tables;
using NHibernate;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers;

public partial class CrudController
{
	#region Public and private methods

	public T[]? GetItems<T>(FieldFilterModel filter, FieldOrderModel? order = null, int maxResults = 0) where T : TableModel, new() =>
		GetItems<T>(new List<FieldFilterModel> { filter }, order, maxResults);

	public T[]? GetItems<T>(List<FieldFilterModel> filters, FieldOrderModel? order, int maxResults = 0) where T : TableModel, new()
	{
		T[]? items = null;
		ExecuteTransaction((session) =>
		{
			items = GetItemsAction<T>(session, filters, order, maxResults);
		});
		if (items is not null)
		{
			foreach (T item in items)
			{
				FillReferences(item);
			}
		}
		return items;
	}

	public T[]? GetItems<T>(FieldOrderModel? order, int maxResults = 0) where T : TableModel, new()
	{
		T[]? items = null;
		ExecuteTransaction((session) =>
		{
			items = GetItemsAction<T>(session, null, order, maxResults);
		});
		if (items is not null)
		{
			foreach (T item in items)
			{
				FillReferences(item);
			}
		}
		return items;
	}

	private T[]? GetItemsAction<T>(ISession session, List<FieldFilterModel>? filters, FieldOrderModel? order, int maxResults = 0) where T : TableModel, new()
	{
		ICriteria criteria = GetCriteria<T>(session, filters, order, maxResults);
		IList<T>? list = criteria.List<T>();
		if (list is not null && list.Count > 0)
			return list.ToArray();
		return null;
	}

	public List<T> GetItemsListNotNull<T>(bool isShowMarked, bool isShowOnlyTop, FieldOrderModel? order = null) where T : TableModel, new()
	{
		List<FieldFilterModel> filtersMarked = new() { new(DbField.IsMarked, DbComparer.Equal, false) };
		return isShowMarked
			? GetItemsListNotNull(isShowOnlyTop, order)
			: GetItemsListNotNull(isShowOnlyTop, filtersMarked, order);
	}

	public List<T> GetItemsListNotNull<T>(bool isShowOnlyTop, FieldOrderModel? order = null) where T : TableModel, new()
	{
		int maxResults = isShowOnlyTop ? DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0;
		T[]? items = GetItems<T>(order, maxResults);
		if (items is not null && items.Length > 0)
			return items.ToList();
		return new();
	}

	public List<T> GetItemsListNotNull<T>(bool isShowOnlyTop, List<FieldFilterModel> filters, FieldOrderModel? order = null) where T : TableModel, new()
	{
		int maxResults = isShowOnlyTop ? DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0;
		T[]? items = GetItems<T>(filters, order, maxResults);
		if (items is not null && items.Length > 0)
			return items.ToList();
		return new();
	}

	public T[] GetItemsNativeMappingInside<T>(string query) where T : TableModel, new()
	{
		T[] result = Array.Empty<T>();
		ExecuteTransaction((session) =>
		{
			ISQLQuery? sqlQuery = GetSqlQuery(session, query);
			if (sqlQuery != null)
			{
				sqlQuery.AddEntity(typeof(T));
				System.Collections.IList? listEntities = sqlQuery.List();
				result = new T[listEntities.Count];
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = (T)listEntities[i];
				}
			}
		});
		return result;
	}

	public T[] GetItemsNativeMapping<T>(string query) where T : TableModel, new()
		=> GetItemsNativeMappingInside<T>(query);

	public object[] GetItemsNativeObject(string query)
	{
		object[] result = Array.Empty<object>();
		ExecuteTransaction((session) =>
		{
			ISQLQuery? sqlQuery = GetSqlQuery(session, query);
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

	//public T[] GetItemsNativeNotNull(string[] fieldsSelect, string from, object[] valuesParams,
	//    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	//{
	//    return DataAccess.GetEntitiesNative<T>(fieldsSelect, from, valuesParams, filePath, lineNumber, memberName);
	//}

	//public T[] GetItemsNative<T>(string[] fieldsSelect, string from, object[] valuesParams,
	//    string filePath, int lineNumber, string memberName) where T : class
	//{
	//    var result = new T[0];
	//    using var session = GetSession();
	//    if (session != null)
	//    {
	//        using var transaction = session.BeginTransaction();
	//        try
	//        {
	//            var query = GetSqlQuery<T>(session, from, fieldsSelect, valuesParams);
	//            query.AddEntity(typeof(TU));
	//            if (items != null)
	//            {
	//                List<T> listEntities = items.List<T>();
	//                result = new T[listEntities.Count];
	//                for (int i = 0; i < result.Length; i++)
	//                {
	//                    result[i] = (T)listEntities[i];
	//                }
	//            }

	//            session.Flush();
	//            transaction.Commit();
	//        }
	//        catch (Exception ex)
	//        {
	//            transaction.Rollback();
	//            LogException(ex, filePath, lineNumber, memberName);
	//            throw;
	//        }
	//        finally
	//        {
	//            session.Disconnect();
	//        }
	//    }
	//    return result;
	//}

	#endregion
}
