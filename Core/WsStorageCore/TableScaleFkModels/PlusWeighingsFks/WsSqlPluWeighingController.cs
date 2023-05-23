// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusWeighingsFks;

/// <summary>
/// SQL-помощник табличных записей таблицы PLUS_WEIGHINGS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluWeighingController
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluWeighingController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluWeighingController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;
    private WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;

    #endregion

    #region Public and private methods

    public WsSqlPluWeighingModel GetNewItem() => AccessItem.GetItemNewEmpty<WsSqlPluWeighingModel>();

    public WsSqlPluWeighingModel GetItem(Guid? uid) => AccessItem.GetItemNotNullableByUid<WsSqlPluWeighingModel>(uid);

    public List<WsSqlPluWeighingModel> GetList() => ContextList.GetListNotNullablePlusWeighings(new());

    #endregion
}