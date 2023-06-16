// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник методов доступа к табличным записям.
/// Базовый слой доступа к БД.
/// </summary>
public sealed class WsSqlCoreItemHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlCoreItemHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlCoreItemHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private static WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    #endregion

    #region Public and public methods

    public T GetItemNewEmpty<T>() where T : WsSqlTableBase, new() => SqlCore.GetItemNewEmpty<T>();

    public T? GetItemNullable<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new() =>
        SqlCore.GetItemNullable<T>(sqlCrudConfig);

    public T GetItemNotNullable<T>(WsSqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new() =>
        SqlCore.GetItemNotNullable<T>(sqlCrudConfig);

    public T? GetItemNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        SqlCore.GetItemNullableByUid<T>(uid);

    public T GetItemNotNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        SqlCore.GetItemNotNullableByUid<T>(uid);

    public T? GetItemNullableById<T>(long? id) where T : WsSqlTableBase, new() =>
        SqlCore.GetItemNullableById<T>(id);

    public T GetItemNotNullableById<T>(long? id) where T : WsSqlTableBase, new() =>
        SqlCore.GetItemNotNullableById<T>(id) ?? new();

    private void IsItemExists<T>(T? item) where T : WsSqlTableBase => SqlCore.IsItemExists(item);

    public void ExecQueryNative(string query, List<SqlParameter> parameters) =>
        SqlCore.ExecQueryNative(query, parameters);

    public void ExecQueryNative(string query, SqlParameter parameter) =>
        SqlCore.ExecQueryNative(query, parameter);

    public void Save<T>(T? item, WsSqlFieldIdentityModel? identity, WsSqlEnumSessionType sessionType) 
        where T : WsSqlTableBase => SqlCore.Save(item, identity, sessionType);
    
    public void Save<T>(T? item, WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.Isolated) where T : WsSqlTableBase => 
        SqlCore.Save(item, sessionType);

    public void Update<T>(T? item, WsSqlEnumSessionType sessionType = WsSqlEnumSessionType.Isolated) 
        where T : WsSqlTableBase => SqlCore.Update(item, sessionType);

    #endregion
}