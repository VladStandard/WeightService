// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-менеджер прямого доступа к данным БД (используется ядром фреймворка).
/// Базовый слой доступа к БД.
/// </summary>
public sealed class WsSqlAccessManagerHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlAccessManagerHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlAccessManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessCoreHelper AccessCore => WsSqlAccessCoreHelper.Instance;
    public WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    public WsSqlAccessListHelper AccessList => WsSqlAccessListHelper.Instance;
    public ISessionFactory SessionFactory => AccessCore.SessionFactory;

    #endregion

    public bool IsConnected() => AccessCore.IsConnected();

    public void AddConfigurationMappings(FluentConfiguration fluentConfiguration) =>
        AccessCore.AddConfigurationMappings(fluentConfiguration);
}