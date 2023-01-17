// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Tables;
using FluentNHibernate.Conventions;
using NHibernate;
using System;

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
	private T? GetItemCoreNullable<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : class, new()
	{
		sqlCrudConfig.ResultMaxCount = 1;
		ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
		return criteria.UniqueResult<T>();
	}

	/// <summary>
	/// Get table item.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="sqlCrudConfig"></param>
	/// <returns></returns>
	private T? GetItemNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
	{
		T? item = null;
		ExecuteSelect(session =>
		{
			item = GetItemCoreNullable<T>(session, sqlCrudConfig);
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
	public T? GetItemNullable<T>(object? value) where T : class, new()
	{
		SqlCrudConfigModel? sqlCrudConfig = value switch
		{
			Guid uid => new(new List<SqlFieldFilterModel>
				{ new(nameof(SqlTableBase.IdentityValueUid), SqlFieldComparerEnum.Equal, uid) }, 
				true, false, false,  false),
			long id => new(new List<SqlFieldFilterModel> 
				{ new(nameof(SqlTableBase.IdentityValueId), SqlFieldComparerEnum.Equal, id) }, 
				true, false, false,  false),
			_ => null,
		};
		return sqlCrudConfig is not null ? GetItemNullable<T>(sqlCrudConfig) : null;
	}

	/// <summary>
	/// Get table not null item.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="sqlCrudConfig"></param>
	/// <returns></returns>
	public T GetItemNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
	{
		T? item = GetItemNullable<T>(sqlCrudConfig);
		return item ?? new();
	}

	/// <summary>
	/// Get table not null item.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="value"></param>
	/// <returns></returns>
	public T GetItemNotNullable<T>(object? value) where T : class, new()
	{
		T? item = value switch
		{
			Guid uid => GetItemNullable<T>(uid),
			long id => GetItemNullable<T>(id),
			_ => new(),
		};
		return item ?? new();
	}

	public bool IsItemExists<T>(T? item) where T : class, new()
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

	public bool IsItemExists<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
	{
		bool result = false;
		sqlCrudConfig.ResultMaxCount = 1;
		ExecuteSelect(session =>
		{
			result = GetCriteria<T>(session, sqlCrudConfig).List<T>().Any();
		});
		return result;
	}

    public T GetItemNewEmpty<T>() where T : SqlTableBase, new() =>
        new() { Name = LocaleCore.Table.FieldEmpty, Description = LocaleCore.Table.FieldEmpty };

    #endregion

    #region Public and private methods - GetArray

    private T[] GetArrayCore<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : class, new()
	{
		ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
		return criteria.List<T>().ToArray();
	}

	public T[]? GetArrayNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
	{
		T[]? items = null;
		ExecuteSelect(session =>
		{
			items = GetArrayCore<T>(session, sqlCrudConfig);
			if (sqlCrudConfig.IsFillReferences)
			    foreach (T item in items)
			    {
				    FillReferences(item);
			    }
		});
		return items;
	}

	private T[]? GetArrayNullable<T>(string query) where T : class, new()
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

	[Obsolete(@"Use GetArrayObjectsNullable(string query, List<SqlParameter> parameters)")]
	private object[]? GetArrayObjectsNullable(string query) => 
        GetArrayObjectsNullable(query, new());

    private object[]? GetArrayObjectsNullable(string query, List<SqlParameter> parameters)
	{
		object[]? result = null;
		ExecuteSelect(session =>
		{
			ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
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

	[Obsolete(@"Use GetArrayObjectsNotNullable(string query, List<SqlParameter> parameters)")]
	public object[] GetArrayObjectsNotNullable(string query) =>
        GetArrayObjectsNotNullable(query, new());

	public object[] GetArrayObjectsNotNullable(string query, List<SqlParameter> parameters) => 
		GetArrayObjectsNullable(query, parameters) ?? Array.Empty<object>();

	public object[] GetArrayObjectsNotNullable(SqlCrudConfigModel sqlCrudConfig) => 
		GetArrayObjectsNullable(sqlCrudConfig.NativeQuery, sqlCrudConfig.NativeParameters) ?? Array.Empty<object>();

	#endregion

	#region Public and private methods - GetList

	public List<T> GetListNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
        List<T> result = new();
		if (sqlCrudConfig.IsResultAddFieldEmpty)
		{
            result.Add(GetItemNewEmpty<T>());
		}

        List<T> list = new();
		T[]? items = GetArrayNullable<T>(sqlCrudConfig);
		if (items is not null && items.Length > 0)
			list = items.ToList();

        result.AddRange(list);
		return result;
	}

	#endregion
}