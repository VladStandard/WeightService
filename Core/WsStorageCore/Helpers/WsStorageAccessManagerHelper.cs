// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-менеджер SQL-помощников методов доступа.
/// Базовый слой доступа к БД.
/// </summary>
public sealed class WsStorageAccessManagerHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsStorageAccessManagerHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsStorageAccessManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsStorageAccessCoreHelper AccessCore => WsStorageAccessCoreHelper.Instance;
    public WsStorageAccessItemHelper AccessItem => WsStorageAccessItemHelper.Instance;
    public WsStorageAccessListHelper AccessList => WsStorageAccessListHelper.Instance;
    public ISessionFactory SessionFactory => AccessCore.SessionFactory;

    #endregion

    public bool IsConnected() => AccessCore.IsConnected();
}