namespace WsStorageCore.Tables.TableScaleModels.Printers;

public class WsSqlPrinterRepository : WsSqlTableRepositoryBase<WsSqlPrinterModel>
{
    public List<WsSqlPrinterModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlPrinterModel> list = SqlCore.GetListNotNullable<WsSqlPrinterModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }
}