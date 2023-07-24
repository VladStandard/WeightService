namespace WsStorageCore.Tables.TableScaleModels.PlusStorageMethods;

public class WsSqlPluStorageMethodRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodModel>
{
    public List<WsSqlPluStorageMethodModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlPluStorageMethodModel> list = SqlCore.GetListNotNullable<WsSqlPluStorageMethodModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }
}