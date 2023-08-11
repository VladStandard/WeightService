// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusBundlesFks;

/// <summary>
/// SQL-контроллер таблицы PLUS_BUNDLES_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluBundleFkRepository : WsSqlTableRepositoryBase<WsSqlPluBundleFkModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlBundleRepository ContextBundle { get; } = new();
    private WsSqlPluRepository ContextPlu { get; } = new();

    #endregion

    #region Public and private methods

    public WsSqlPluBundleFkModel GetNewItem()
    {
        WsSqlPluBundleFkModel item = SqlCore.GetItemNewEmpty<WsSqlPluBundleFkModel>();
        item.Plu = ContextPlu.GetNewItem();
        item.Bundle = ContextBundle.GetNewItem();
        return item;
    }

    public List<WsSqlPluBundleFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluBundleFkModel.Bundle)}.{nameof(BundleModel.Name)}", SqlOrderDirection.Asc));
        List<WsSqlPluBundleFkModel> list = SqlCore.GetEnumerableNotNullable<WsSqlPluBundleFkModel>(sqlCrudConfig).ToList();
        if (list.Any())
        {
            WsSqlPluBundleFkModel bundleFk = list.First();
            if (bundleFk.Plu.IsNew)
                bundleFk.Plu = SqlCore.GetItemNewEmpty<WsSqlPluModel>();
            if (bundleFk.Bundle.IsNew)
                bundleFk.Bundle = SqlCore.GetItemNewEmpty<WsSqlBundleModel>();
        }
        if (sqlCrudConfig.IsResultOrder)
            list = list.OrderBy(item => item.Bundle.Name).ToList();
        return list;
    }

    public List<WsSqlPluBundleFkModel> GetListByPlu(WsSqlPluModel plu)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFkIdentityFilter(nameof(WsSqlPluBundleFkModel.Plu), plu);
        return GetList(sqlCrudConfig);   
    }
    
    #endregion
}