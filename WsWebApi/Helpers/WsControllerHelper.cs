// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsWebApi.Helpers;

/// <summary>
/// Web API Controller helper.
/// </summary>
internal sealed class WsControllerHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsControllerHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsControllerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public WsBrandsController BrandsController => WsBrandsController.Instance;
    public WsPlusCharacteristicsController PlusCharacteristicsController => WsPlusCharacteristicsController.Instance;
    public WsPlusController PlusController => WsPlusController.Instance;
    public WsPlusGroupsController PlusGroupsController => WsPlusGroupsController.Instance;

    #endregion
}