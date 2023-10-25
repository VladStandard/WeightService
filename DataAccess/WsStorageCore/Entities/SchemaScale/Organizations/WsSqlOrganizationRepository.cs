using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.Organizations;

public class WsSqlOrganizationRepository : WsSqlTableRepositoryBase<WsSqlOrganizationEntity>
{
    public List<WsSqlOrganizationEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlOrganizationEntity>(sqlCrudConfig).ToList();
    }
}