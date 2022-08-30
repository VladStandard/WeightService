// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using NHibernate;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers;

public partial class CrudController
{
	#region Public and private methods

	public T? GetItem<T>(SqlCrudConfigModel sqlCrudConfig) where T : TableModel, new()
	{
		T? item = null;
		ExecuteTransaction((session) =>
		{
			item = GetItemCore<T>(session, sqlCrudConfig);
		});
		FillReferences(item);
		return item;
	}

	private T? GetItemCore<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : TableModel, new()
	{
		sqlCrudConfig.MaxResults = 1;
		ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
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
	public T? GetItemById<T>(long? id) where T : TableModel, new()
	{
		SqlCrudConfigModel sqlCrudConfig = new(new() { new(DbField.IdentityId, DbComparer.Equal, id) },
			new(DbField.IdentityId, DbOrderDirection.Desc), 0);
		return GetItem<T>(sqlCrudConfig);
	}

	/// <summary>
	/// Get entity by UID.
	/// </summary>
	/// <param name="uid"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public T? GetItemByUid<T>(Guid? uid) where T : TableModel, new()
	{
		SqlCrudConfigModel sqlCrudConfig = new(new() { new(DbField.IdentityUid, DbComparer.Equal, uid) },
			new(DbField.IdentityUid, DbOrderDirection.Desc), 0);
		return GetItem<T>(sqlCrudConfig);
	}

	public T GetItemNotNull<T>(SqlCrudConfigModel sqlCrudConfig) where T : TableModel, new()
	{
		T? item = GetItem<T>(sqlCrudConfig);
		if (item is not null)
			return item;
		return new();
	}

	public T GetItemByIdNotNull<T>(long? id) where T : TableModel, new()
	{
		T? item = GetItemById<T>(id);
		if (item is not null)
			return item;
		return new();
	}

	public T GetItemByUidNotNull<T>(Guid? uid) where T : TableModel, new()
	{
		T? item = GetItemByUid<T>(uid);
		if (item is not null)
			return item;
		return new();
	}

	#endregion
}
