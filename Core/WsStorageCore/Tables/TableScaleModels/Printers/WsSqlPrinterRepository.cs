namespace WsStorageCore.Tables.TableScaleModels.Printers;

public class WsSqlPrinterRepository : WsSqlTableRepositoryBase<WsSqlPrinterModel>
{
    public List<WsSqlPrinterModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullablePrinters(sqlCrudConfig);
}