using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.TasksTypes;

public class WsSqlTaskTypeRepository : WsSqlTableRepositoryBase<WsSqlTaskTypeEntity>
{
    public List<WsSqlTaskTypeEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlTaskTypeEntity>(sqlCrudConfig).ToList();
    }
}