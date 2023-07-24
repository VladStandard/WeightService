namespace WsStorageCore.Tables.TableScaleModels.PrintersTypes;

public class WsSqlPrinterTypeRepository : WsSqlTableRepositoryBase<WsSqlPrinterTypeModel>
{
    public List<WsSqlPrinterTypeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlPrinterTypeModel> list = SqlCore.GetListNotNullable<WsSqlPrinterTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }
}