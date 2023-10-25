using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.PrintersTypes;

public class WsSqlPrinterTypeRepository : WsSqlTableRepositoryBase<WsSqlPrinterTypeEntity>
{
    public List<WsSqlPrinterTypeEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlPrinterTypeEntity>(sqlCrudConfig).ToList();
    }
}