namespace WsStorageCore.Tables.TableScaleFkModels.PlusBrandsFks;

/// <summary>
/// SQL-контроллер таблицы бренды ПЛУ.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluBrandFkRepository : WsSqlTableRepositoryBase<WsSqlPluBrandFkModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlBrandRepository ContextBrand { get; } = new();
    private WsSqlPluRepository ContextPlu { get; } = new();

    #endregion

    #region Public and private methods

    public WsSqlPluBrandFkModel GetNewItem()
    {
        WsSqlPluBrandFkModel item = SqlCore.GetItemNewEmpty<WsSqlPluBrandFkModel>();
        item.Plu = ContextPlu.GetNewItem();
        item.Brand = ContextBrand.GetNewItem();
        return item;
    }

    public IEnumerable<WsSqlPluBrandFkModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.ClearNullProperties), SqlOrderDirection.Asc));
        IEnumerable<WsSqlPluBrandFkModel> items = SqlCore.GetEnumerableNotNullable<WsSqlPluBrandFkModel>(sqlCrudConfig).ToList();
        if (items.Any())
        {
            WsSqlPluBrandFkModel bundleFk = items.First();
            if (bundleFk.Plu.IsNew)
                bundleFk.Plu = SqlCore.GetItemNewEmpty<WsSqlPluModel>();
            if (bundleFk.Brand.IsNew)
                bundleFk.Brand = SqlCore.GetItemNewEmpty<WsSqlBrandModel>();
        }
        if (sqlCrudConfig.IsResultOrder && items.Any())
            items = items.OrderBy(item => item.Brand.Name).ToList();
        return items;
    }
    
    #endregion
}