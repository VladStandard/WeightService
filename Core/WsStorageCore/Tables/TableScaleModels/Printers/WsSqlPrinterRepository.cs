namespace WsStorageCore.Tables.TableScaleModels.Printers;

public class WsSqlPrinterRepository : WsSqlTableRepositoryBase<WsSqlPrinterModel>
{
    public IEnumerable<WsSqlPrinterModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Name));
        return SqlCore.GetEnumerableNotNullable<WsSqlPrinterModel>(sqlCrudConfig);
    }
}