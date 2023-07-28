namespace WsStorageCore.Tables.TableScaleModels.Printers;

public class WsSqlPrinterRepository : WsSqlTableRepositoryBase<WsSqlPrinterModel>
{
    public List<WsSqlPrinterModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Name));
        return SqlCore.GetListNotNullable<WsSqlPrinterModel>(sqlCrudConfig);
    }
}