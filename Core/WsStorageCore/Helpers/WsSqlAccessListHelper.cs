// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник методов доступа к табличным спискам.
/// Базовый слой доступа к БД.
/// </summary>
public sealed class WsSqlAccessListHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlAccessListHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlAccessListHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private static WsSqlAccessCoreHelper AccessCore => WsSqlAccessCoreHelper.Instance;

    #endregion

    #region Public and private methods

    public T[]? GetArrayNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new() =>
        AccessCore.GetArrayNullable<T>(sqlCrudConfig);

    public List<T> GetListNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new() =>
        AccessCore.GetListNotNullable<T>(sqlCrudConfig);

    public object[] GetArrayObjectsNotNullable(string query) =>
        AccessCore.GetArrayObjectsNotNullable(query);

    public object[] GetArrayObjectsNotNullable(string query, List<SqlParameter> parameters) =>
        AccessCore.GetArrayObjectsNotNullable(query, parameters);

    public object[] GetArrayObjectsNotNullable(SqlCrudConfigModel sqlCrudConfig) =>
        AccessCore.GetArrayObjectsNotNullable(sqlCrudConfig);

    #endregion
}