// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusBrandsFks;

/// <summary>
/// SQL-контроллер таблицы бренды ПЛУ.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluBrandFkRepository : WsSqlTableRepositoryBase<WsSqlPluBrandFkModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluBrandFkRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluBrandFkRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlBrandRepository ContextBrand => WsSqlBrandRepository.Instance;
    private WsSqlPluRepository ContextPlu => WsSqlPluRepository.Instance;

    #endregion

    #region Public and private methods

    public WsSqlPluBrandFkModel GetNewItem()
    {
        WsSqlPluBrandFkModel item = SqlCore.GetItemNewEmpty<WsSqlPluBrandFkModel>();
        item.Plu = ContextPlu.GetNewItem();
        item.Brand = ContextBrand.GetNewItem();
        return item;
    }

    public List<WsSqlPluBrandFkModel> GetList() => ContextList.GetListNotNullablePlusBrandsFks(SqlCrudConfig);

    #endregion
}