// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using NHibernate;

namespace DataCore.Sql.Core;

public partial class DataAccessHelper
{
	#region Public and private methods

	/// <summary>
	/// Get table item.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="sqlCrudConfig"></param>
	/// <returns></returns>
	public T? GetItem<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		T? item = null;
		ExecuteSelect(session =>
		{
			item = GetItemCore<T>(session, sqlCrudConfig);
		});
		FillReferences(item);
		return item;
	}

	/// <summary>
	/// Get table item core.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="session"></param>
	/// <param name="sqlCrudConfig"></param>
	/// <returns></returns>
	private T? GetItemCore<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		sqlCrudConfig.MaxResults = 1;
		ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
		//IList<T>? list = criteria.List<T>();
		//if (list is not null && list.Count > 0)
		//	return list.FirstOrDefault();
		//return null;
		return criteria.UniqueResult<T>();
	}

	/// <summary>
	/// Get table item.
	/// </summary>
	/// <param name="value"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public T? GetItem<T>(object? value) where T : SqlTableBase, new()
	{
		SqlCrudConfigModel? sqlCrudConfig = value switch
		{
			Guid uid => new(new SqlFieldFilterModel(nameof(SqlTableBase.IdentityValueUid), SqlFieldComparerEnum.Equal, uid),
				new SqlFieldOrderModel(nameof(SqlTableBase.IdentityValueUid), SqlFieldOrderEnum.Desc), 0),
			long id => new(new SqlFieldFilterModel(nameof(SqlTableBase.IdentityValueId), SqlFieldComparerEnum.Equal, id),
				new SqlFieldOrderModel(nameof(SqlTableBase.IdentityValueId), SqlFieldOrderEnum.Desc), 0),
			_ => null,
		};
		return sqlCrudConfig is not null ? GetItem<T>(sqlCrudConfig) : null;
	}

	///// <summary>
	///// Get table item by UID.
	///// </summary>
	///// <param name="uid"></param>
	///// <typeparam name="T"></typeparam>
	///// <returns></returns>
	//public T? GetItem<T>(Guid? uid) where T : SqlTableBase, new()
	//{
	//	SqlCrudConfigModel sqlCrudConfig = new(
	//		new SqlFieldFilterModel(nameof(SqlTableBase.IdentityValueUid), SqlFieldComparerEnum.Equal, uid),
	//		new SqlFieldOrderModel(nameof(SqlTableBase.IdentityValueUid), SqlFieldOrderEnum.Desc), 0);
	//	return GetItem<T>(sqlCrudConfig);
	//}

	/// <summary>
	/// Get table not null item.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="sqlCrudConfig"></param>
	/// <returns></returns>
	public T GetItemNotNull<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		T? item = GetItem<T>(sqlCrudConfig);
		return item ?? new();
	}

	/// <summary>
	/// Get table not null item by ID.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="value"></param>
	/// <returns></returns>
	public T GetItemNotNull<T>(object? value) where T : SqlTableBase, new()
	{
		T? item = value switch
		{
			Guid uid => GetItem<T>(uid),
			long id => GetItem<T>(id),
			_ => new(),
		};
		return item ?? new();
	}

	public List<T> GetListNotNull<T>(SqlCrudConfigModel sqlCrudConfig, bool isAddFieldNull) where T : SqlTableBase, new()
	{
		List<T> result = new();
		if (isAddFieldNull)
			result.Add(GetNewItem<T>());
		List<T> list = GetList<T>(sqlCrudConfig);
		//if (sqlFieldOrder.Name.Equals(nameof(SqlTableBase.Name)))
		//	result = result.OrderBy(x => x.Name).ToList();
		result.AddRange(list);
		return result;
	}

	public List<T> GetListNotNull<T>(SqlFieldOrderModel sqlFieldOrder,
		int maxResults, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull) where T : SqlTableBase, new() =>
		GetListNotNull<T>(SqlUtils.GetCrudConfig(sqlFieldOrder, maxResults, isShowMarked, isShowOnlyTop), isAddFieldNull);

	public List<T> GetListNotNull<T>(SqlFieldOrderModel sqlFieldOrder, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull) where T : SqlTableBase, new() =>
		GetListNotNull<T>(sqlFieldOrder, 0, isShowMarked, isShowOnlyTop, isAddFieldNull);

	public List<T> GetListNotNull<T>(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull) where T : SqlTableBase, new() =>
		GetListNotNull<T>(new(nameof(AccessModel.Name), SqlFieldOrderEnum.Asc), 
			0, isShowMarked, isShowOnlyTop, isAddFieldNull);

	#endregion
}
