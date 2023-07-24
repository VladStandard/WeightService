// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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

    public List<WsSqlPluBrandFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.ClearNullProperties), SqlOrderDirection.Asc));
        List<WsSqlPluBrandFkModel> list = SqlCore.GetListNotNullable<WsSqlPluBrandFkModel>(sqlCrudConfig);
        if (list.Any())
        {
            WsSqlPluBrandFkModel bundleFk = list.First();
            if (bundleFk.Plu.IsNew)
                bundleFk.Plu = SqlCore.GetItemNewEmpty<WsSqlPluModel>();
            if (bundleFk.Brand.IsNew)
                bundleFk.Brand = SqlCore.GetItemNewEmpty<WsSqlBrandModel>();
        }
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Brand.Name).ToList();
        return list;
    }
    
    #endregion
}