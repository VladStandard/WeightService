// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusBundlesFks;

/// <summary>
/// SQL-контроллер таблицы PLUS_BUNDLES_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluBundleFkRepository : WsSqlTableRepositoryBase<WsSqlPluBundleFkModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluBundleFkRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluBundleFkRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlBundleRepository ContextBundle => WsSqlBundleRepository.Instance;
    private WsSqlPluRepository ContextPlu => WsSqlPluRepository.Instance;

    #endregion

    #region Public and private methods

    public WsSqlPluBundleFkModel GetNewItem()
    {
        WsSqlPluBundleFkModel item = SqlCore.GetItemNewEmpty<WsSqlPluBundleFkModel>();
        item.Plu = ContextPlu.GetNewItem();
        item.Bundle = ContextBundle.GetNewItem();
        return item;
    }

    public List<WsSqlPluBundleFkModel> GetList() => ContextList.GetListNotNullablePlusBundlesFks(SqlCrudConfig);

    #endregion
}