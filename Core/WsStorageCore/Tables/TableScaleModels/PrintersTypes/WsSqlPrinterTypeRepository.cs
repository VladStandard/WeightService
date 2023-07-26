namespace WsStorageCore.Tables.TableScaleModels.PrintersTypes;

public class WsSqlPrinterTypeRepository : WsSqlTableRepositoryBase<WsSqlPrinterTypeModel>
{
    public List<WsSqlPrinterTypeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        return SqlCore.GetListNotNullable<WsSqlPrinterTypeModel>(sqlCrudConfig);
    }
}