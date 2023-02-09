// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Tables;
using FluentNHibernate.Conventions;
using NHibernate;

namespace DataCore.Sql.Core.Helpers;

public partial class DataAccessHelper
{
	#region Public and private methods - GetItem

	private T? GetItemCoreNullable<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		sqlCrudConfig.ResultMaxCount = 1;
		ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
		return criteria.UniqueResult<T>();
	}

	private T? GetItemNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		T? item = null;
        ExecuteCore(session =>
        {
			item = GetItemCoreNullable<T>(session, sqlCrudConfig);
		}, false);
		FillReferences(item);
		return item;
	}

	public T? GetItemNullable<T>(object? value) where T : SqlTableBase, new()
	{
		SqlCrudConfigModel? sqlCrudConfig = value switch
		{
			Guid uid => new(new List<SqlFieldFilterModel>
				{ new(nameof(SqlTableBase.IdentityValueUid), SqlFieldComparerEnum.Equal, uid) }, 
				true, false, false,  false),
			long id => new(new List<SqlFieldFilterModel> 
				{ new(nameof(SqlTableBase.IdentityValueId), SqlFieldComparerEnum.Equal, id) }, 
				true, false, false,  false),
			_ => null
        };
		return sqlCrudConfig is not null ? GetItemNullable<T>(sqlCrudConfig) : null;
	}

	public T GetItemNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		T? item = GetItemNullable<T>(sqlCrudConfig);
		return item ?? new();
	}

	public T GetItemNotNullable<T>(object? value) where T : SqlTableBase, new()
	{
		T? item = value switch
		{
			Guid uid => GetItemNullable<T>(uid),
			long id => GetItemNullable<T>(id),
			_ => new()
        };
		return item ?? new();
	}

    public T? GetItemNullable<T>(SqlFieldIdentityModel identity) where T : SqlTableBase, new() =>
        identity.Name switch
        {
            SqlFieldIdentity.Uid => GetItemNullable<T>(identity.Uid),
            SqlFieldIdentity.Id => GetItemNullable<T>(identity.Id),
            _ => new()
        };

    public T GetItemNotNullable<T>(SqlFieldIdentityModel identity) where T : SqlTableBase, new() =>
        GetItemNullable<T>(identity) ?? new();

    public T? GetItemNullableByUid<T>(Guid? uid) where T : SqlTableBase, new() => 
        GetItemNullable<T>(uid);

    public T GetItemNotNullableByUid<T>(Guid? uid) where T : SqlTableBase, new() =>
        GetItemNullableByUid<T>(uid) ?? new();

    public T? GetItemNullableById<T>(long? id) where T : SqlTableBase, new() => 
        GetItemNullable<T>(id);

    public T GetItemNotNullableById<T>(long? id) where T : SqlTableBase, new() =>
        GetItemNullableById<T>(id) ?? new();

    public bool IsItemExists<T>(T? item) where T : SqlTableBase, new()
	{
		if (item is null)
			return false;

		bool result = false;
        ExecuteCore(session =>
        {
			result = session.Query<T>().Any(x => x.IsAny(item));
		}, false);
		return result;
	}

	public bool IsItemExists<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		bool result = false;
		sqlCrudConfig.ResultMaxCount = 1;
        ExecuteCore(session =>
        {
			result = GetCriteria<T>(session, sqlCrudConfig).List<T>().Any();
		}, false);
		return result;
	}

    public T GetItemNewEmpty<T>() where T : SqlTableBase, new() =>
        new() { Name = LocaleCore.Table.FieldEmpty, Description = LocaleCore.Table.FieldEmpty };

    #endregion

    #region Public and private methods - GetArray

    private T[] GetArrayCore<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
		return criteria.List<T>().ToArray();
	}

	public T[]? GetArrayNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new()
	{
		T[]? items = null;
        ExecuteCore(session =>
        {
			items = GetArrayCore<T>(session, sqlCrudConfig);
			if (sqlCrudConfig.IsFillReferences)
			    foreach (T item in items)
			    {
				    FillReferences(item);
			    }
		}, false);
		return items;
	}

	private T[]? GetArrayNullable<T>(string query) where T : SqlTableBase, new()
	{
		T[]? result = null;
        ExecuteCore(session =>
        {
			ISQLQuery? sqlQuery = GetSqlQuery(session, query);
			if (sqlQuery is not null)
			{
				sqlQuery.AddEntity(typeof(T));
				result = sqlQuery.List<T>().ToArray();
			}
		}, false);
		return result;
	}

	[Obsolete(@"Use GetArrayObjectsNullable(string query, List<SqlParameter> parameters)")]
	private object[]? GetArrayObjectsNullable(string query) => 
        GetArrayObjectsNullable(query, new());

    private object[]? GetArrayObjectsNullable(string query, List<SqlParameter> parameters)
	{
		object[]? result = null;
        ExecuteCore(session =>
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
		}, false);
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