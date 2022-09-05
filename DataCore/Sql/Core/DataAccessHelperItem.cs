// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using NHibernate;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Core;

public static class DataAccessHelperItem
{
	#region Public and private methods

	public static T? GetItem<T>(this DataAccessHelper dataAccess, SqlCrudConfigModel sqlCrudConfig) where T : TableModel, new()
	{
		T? item = null;
		dataAccess.ExecuteTransaction((session) =>
		{
			item = dataAccess.GetItemCore<T>(session, sqlCrudConfig);
		});
		dataAccess.FillReferences(item);
		return item;
	}

	private static T? GetItemCore<T>(this DataAccessHelper dataAccess, ISession session, SqlCrudConfigModel sqlCrudConfig) where T : TableModel, new()
	{
		sqlCrudConfig.MaxResults = 1;
		ICriteria criteria = dataAccess.GetCriteria<T>(session, sqlCrudConfig);
		IList<T>? list = criteria.List<T>();
		if (list is not null && list.Count > 0)
			return list.FirstOrDefault();
		return null;
	}

	/// <summary>
	/// Get entity by ID.
	/// </summary>
	/// <param name="id"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T? GetItemById<T>(this DataAccessHelper dataAccess, long? id) where T : TableModel, new()
	{
		SqlCrudConfigModel sqlCrudConfig = new(new() { new(DbField.IdentityValueId, DbComparer.Equal, id) },
			new(DbField.IdentityValueId, DbOrderDirection.Desc), 0);
		return GetItem<T>(dataAccess, sqlCrudConfig);
	}

	/// <summary>
	/// Get entity by UID.
	/// </summary>
	/// <param name="uid"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T? GetItemByUid<T>(this DataAccessHelper dataAccess, Guid? uid) where T : TableModel, new()
	{
		SqlCrudConfigModel sqlCrudConfig = new(new() { new(DbField.IdentityValueUid, DbComparer.Equal, uid) },
			new(DbField.IdentityValueUid, DbOrderDirection.Desc), 0);
		return GetItem<T>(dataAccess, sqlCrudConfig);
	}

	public static T GetItemNotNull<T>(this DataAccessHelper dataAccess, SqlCrudConfigModel sqlCrudConfig) where T : TableModel, new()
	{
		T? item = GetItem<T>(dataAccess, sqlCrudConfig);
		if (item is not null)
			return item;
		return new();
	}

	public static T GetItemByIdNotNull<T>(this DataAccessHelper dataAccess, long? id) where T : TableModel, new()
	{
		T? item = GetItemById<T>(dataAccess, id);
		if (item is not null)
			return item;
		return new();
	}

	public static T GetItemByUidNotNull<T>(this DataAccessHelper dataAccess, Guid? uid) where T : TableModel, new()
	{
		T? item = GetItemByUid<T>(dataAccess, uid);
		if (item is not null)
			return item;
		return new();
	}

	#endregion
}
