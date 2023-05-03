// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Helpers;

/// <summary>
/// Помощник веб-сервисов.
/// </summary>
public sealed class WsServiceControllerHelper : WsServiceControllerBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsServiceControllerHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsServiceControllerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public WsServiceBrandsController BrandsController { get; }
    public WsServicePlusCharacteristicsController PlusCharacteristicsController { get; }
    public WsServicePlusController PlusController { get; }
    public WsServicePlusGroupsController PlusGroupsController { get; }

    public WsServiceControllerHelper(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        BrandsController = new(sessionFactory);
        PlusCharacteristicsController = new(sessionFactory);
        PlusController = new(sessionFactory);
        PlusGroupsController = new(sessionFactory);
    }

    #endregion
}