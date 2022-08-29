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

	public T? GetItem<T>(FieldFilterModel filter, FieldOrderModel? order = null)
		where T : TableModel, new() =>
		GetItem<T>(new List<FieldFilterModel> { filter }, order);

	public T? GetItem<T>(List<FieldFilterModel> filters, FieldOrderModel? order = null) where T : TableModel, new()
	{
		T? item = null;
		ExecuteTransaction((session) =>
		{
			item = GetItemAction<T>(session, filters, order);
		});
		FillReferences(item);
		return item;
	}

	public T? GetItem<T>(SqlCrudConfigModel sqlCrudConfig) where T : TableModel, new()
	{
		T? item = null;
		ExecuteTransaction((session) =>
		{
			item = GetItemAction<T>(session, sqlCrudConfig);
		});
		FillReferences(item);
		return item;
	}

	private T? GetItemAction<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : TableModel, new()
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
	public T? GetItemById<T>(long? id, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
		[CallerMemberName] string memberName = "") where T : TableModel, new() =>
		GetItem<T>(new List<FieldFilterModel> { new(DbField.IdentityId, DbComparer.Equal, id) },
			new(DbField.IdentityId, DbOrderDirection.Desc), filePath, lineNumber, memberName);

	/// <summary>
	/// Get entity by UID.
	/// </summary>
	/// <param name="uid"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public T? GetItemByUid<T>(Guid? uid, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
		[CallerMemberName] string memberName = "") where T : TableModel, new() =>
		GetItem<T>(new List<FieldFilterModel> { new(DbField.IdentityUid, DbComparer.Equal, uid) },
			new(DbField.IdentityUid, DbOrderDirection.Desc));

	public T GetItemNotNull<T>(FieldFilterModel filter, FieldOrderModel? order = null,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
		where T : TableModel, new()
	{
		return GetItemNotNull<T>(new List<FieldFilterModel>() { filter }, order, filePath, lineNumber, memberName);
	}

	public T GetItemNotNull<T>(List<FieldFilterModel> filters, FieldOrderModel? order = null,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
		where T : TableModel, new()
	{
		T? item = GetItem<T>(filters, order, filePath, lineNumber, memberName);
		if (item is not null)
			return item;
		return new();
	}

	public T GetItemByIdNotNull<T>(long? id,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
		where T : TableModel, new()
	{
		T? item = GetItemById<T>(id, filePath, lineNumber, memberName);
		if (item is not null)
			return item;
		return new();
	}

	public T GetItemByUidNotNull<T>(Guid? uid,
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
		where T : TableModel, new()
	{
		T? item = GetItemByUid<T>(uid, filePath, lineNumber, memberName);
		if (item is not null)
			return item;
		return new();
	}

	#endregion
}
