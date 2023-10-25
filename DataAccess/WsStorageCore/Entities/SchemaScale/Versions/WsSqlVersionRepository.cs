using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.Versions;

public class WsSqlVersionRepository : WsSqlTableRepositoryBase<WsSqlVersionEntity>
{
    public List<WsSqlVersionEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Desc(nameof(WsSqlVersionEntity.Version)));
        return SqlCore.GetEnumerable<WsSqlVersionEntity>(sqlCrudConfig).ToList();
    }
}