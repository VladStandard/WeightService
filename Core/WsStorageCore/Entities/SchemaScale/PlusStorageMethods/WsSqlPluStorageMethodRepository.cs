using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.PlusStorageMethods;

public class WsSqlPluStorageMethodRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodEntity>
{
    public List<WsSqlPluStorageMethodEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlPluStorageMethodEntity>(sqlCrudConfig).ToList();
    }
}