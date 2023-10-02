using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.PrintersTypes;

public class WsSqlPrinterTypeRepository : WsSqlTableRepositoryBase<WsSqlPrinterTypeModel>
{
    public List<WsSqlPrinterTypeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerableNotNullable<WsSqlPrinterTypeModel>(sqlCrudConfig).ToList();
    }
}