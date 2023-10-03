using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.Printers;

public class WsSqlPrinterRepository : WsSqlTableRepositoryBase<WsSqlPrinterModel>
{
    public IEnumerable<WsSqlPrinterModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlPrinterModel>(sqlCrudConfig);
    }
}