namespace WsStorageCore.Tables.TableScaleModels.PrintersTypes;

public class WsSqlPrinterTypeRepository : WsSqlTableRepositoryBase<WsSqlPrinterTypeModel>
{
    public List<WsSqlPrinterTypeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullablePrintersTypes(sqlCrudConfig);
}