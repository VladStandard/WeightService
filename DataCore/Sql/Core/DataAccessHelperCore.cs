// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using FluentNHibernate.Conventions;
using NHibernate;

namespace DataCore.Sql.Core;

public partial class DataAccessHelper
{
	#region Public and private methods - GetItem

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
	/// Get table item.
	/// </summary>
	/// <param name="value"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public T? GetItem<T>(object? value) where T : SqlTableBase, new()
	{
		SqlCrudConfigModel? sqlCrudConfig = value switch
		{
			Guid uid => new(new() { new(nameof(SqlTableBase.IdentityValueUid), SqlFieldComparerEnum.Equal, uid) }, 
			new() { new(nameof(SqlTableBase.IdentityValueUid), SqlFieldOrderEnum.Desc) }, 
				true, false, false,  false, 0),
			long id => new(new() { new(nameof(SqlTableBase.IdentityValueId), SqlFieldComparerEnum.Equal, id) }, 
			new() { new(nameof(SqlTableBase.IdentityValueId), SqlFieldOrderEnum.Desc) }, 
				true, false, false,  false, 0),
			_ => null,
		};
		return sqlCrudConfig is not null ? GetItem<T>(sqlCrudConfig) : null;
	}

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
	/// Get table not null item.
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

	public bool IsItemExists<T>(T? item) where T : SqlTableBase, new()
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

	public bool IsItemExists<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		bool result = false;
		sqlCrudConfig.MaxResults = 1;
		ExecuteSelect(session =>
		{
			result = GetCriteria<T>(session, sqlCrudConfig).List<T>().Any();
		});
		return result;
	}

	public T GetItemNew<T>() where T : SqlTableBase, new() =>
		new() { Name = LocaleCore.Table.FieldNull, Description = LocaleCore.Table.FieldNull };

	#endregion

	#region Public and private methods - GetArray

	private T[] GetArrayCore<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
		return criteria.List<T>().ToArray();
	}

	public T[]? GetArray<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		T[]? items = null;
		ExecuteSelect(session =>
		{
			items = GetArrayCore<T>(session, sqlCrudConfig);
			foreach (T item in items)
			{
				FillReferences(item);
			}
		});
		return items;
	}

	public T[]? GetArray<T>(string query) where T : SqlTableBase, new()
	{
		T[]? result = null;
		ExecuteSelect(session =>
		{
			ISQLQuery? sqlQuery = GetSqlQuery(session, query);
			if (sqlQuery is not null)
			{
				sqlQuery.AddEntity(typeof(T));
				result = sqlQuery.List<T>().ToArray();
			}
		});
		return result;
	}

	public object[]? GetArrayObjects(string query)
	{
		object[]? result = null;
		ExecuteSelect(session =>
		{
			ISQLQuery? sqlQuery = GetSqlQuery(session, query);
			if (sqlQuery is not null)
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

	public object[] GetArrayObjectsNotNull(string query) => 
		GetArrayObjects(query) ?? Array.Empty<object>();

	#endregion

	#region Public and private methods - GetList

	public List<T> GetListNotNull<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		List<T> result = new();
		if (sqlCrudConfig.IsAddFieldNull)
			result.Add(GetItemNew<T>());
		
		List<T> list = new();
		T[]? items = GetArray<T>(sqlCrudConfig);
		if (items is not null && items.Length > 0)
			list = items.ToList();
		
		result.AddRange(list);
		return result;
	}

	#endregion
}
