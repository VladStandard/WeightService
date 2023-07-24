namespace WsStorageCore.Tables.TableScaleFkModels.PrintersResourcesFks;

public class WsSqlPrinterResourceFkRepository : WsSqlTableRepositoryBase<WsSqlPrinterResourceFkModel>
{
    public List<WsSqlPrinterResourceFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.Name), SqlOrderDirection.Asc));
        List<WsSqlPrinterResourceFkModel> list = SqlCore.GetListNotNullable<WsSqlPrinterResourceFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Printer.Name)
                .ThenBy(item => item.TemplateResource.Name).ToList();
        return list;
    }
}