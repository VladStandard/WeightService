// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using NHibernate;

namespace DataCore.Sql.Core;

/// <summary>
/// 
/// </summary>
public static class DataAccessHelperItem
{
	#region Public and private methods

	/// <summary>
	/// Get table item.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="dataAccess"></param>
	/// <param name="sqlCrudConfig"></param>
	/// <returns></returns>
	public static T? GetItem<T>(this DataAccessHelper dataAccess, SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		T? item = null;
		dataAccess.ExecuteSelect(session =>
		{
			item = dataAccess.GetItemCore<T>(session, sqlCrudConfig);
		});
		dataAccess.FillReferences(item);
		return item;
	}

	/// <summary>
	/// Get table item core.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="dataAccess"></param>
	/// <param name="session"></param>
	/// <param name="sqlCrudConfig"></param>
	/// <returns></returns>
	private static T? GetItemCore<T>(this DataAccessHelper dataAccess, ISession session, SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		sqlCrudConfig.MaxResults = 1;
		ICriteria criteria = session.GetCriteria<T>(sqlCrudConfig);
		IList<T>? list = criteria.List<T>();
		if (list is not null && list.Count > 0)
			return list.FirstOrDefault();
		return null;
	}

	/// <summary>
	/// Get table item by ID.
	/// </summary>
	/// <param name="dataAccess"></param>
	/// <param name="id"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T? GetItemById<T>(this DataAccessHelper dataAccess, long? id) where T : SqlTableBase, new()
	{
		SqlCrudConfigModel sqlCrudConfig = new(
            new SqlFieldFilterModel(nameof(SqlTableBase.IdentityValueId), SqlFieldComparerEnum.Equal, id),
			new SqlFieldOrderModel(nameof(SqlTableBase.IdentityValueId), SqlFieldOrderEnum.Desc), 0);
		return dataAccess.GetItem<T>(sqlCrudConfig);
	}

	/// <summary>
	/// Get table item by UID.
	/// </summary>
	/// <param name="dataAccess"></param>
	/// <param name="uid"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T? GetItemByUid<T>(this DataAccessHelper dataAccess, Guid? uid) where T : SqlTableBase, new()
	{
		SqlCrudConfigModel sqlCrudConfig = new(
            new SqlFieldFilterModel(nameof(SqlTableBase.IdentityValueUid), SqlFieldComparerEnum.Equal, uid),
			new SqlFieldOrderModel(nameof(SqlTableBase.IdentityValueUid), SqlFieldOrderEnum.Desc), 0);
		return dataAccess.GetItem<T>(sqlCrudConfig);
	}

	/// <summary>
	/// Get table not null item.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="dataAccess"></param>
	/// <param name="sqlCrudConfig"></param>
	/// <returns></returns>
	public static T GetItemNotNull<T>(this DataAccessHelper dataAccess, SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		T? item = dataAccess.GetItem<T>(sqlCrudConfig);
		return item ?? new();
	}

	/// <summary>
	/// Get table not null item by ID.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="dataAccess"></param>
	/// <param name="id"></param>
	/// <returns></returns>
	public static T GetItemByIdNotNull<T>(this DataAccessHelper dataAccess, long? id) where T : SqlTableBase, new()
	{
		T? item = dataAccess.GetItemById<T>(id);
		return item ?? new();
    }

	/// <summary>
	/// Get table not null item by UID.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="dataAccess"></param>
	/// <param name="uid"></param>
	/// <returns></returns>
	public static T GetItemByUidNotNull<T>(this DataAccessHelper dataAccess, Guid? uid) where T : SqlTableBase, new()
	{
		T? item = dataAccess.GetItemByUid<T>(uid);
        return item ?? new();
    }

	#endregion
}
