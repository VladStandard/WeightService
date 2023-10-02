using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.Organizations;

public class WsSqlOrganizationRepository : WsSqlTableRepositoryBase<WsSqlOrganizationModel>
{
    public List<WsSqlOrganizationModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerableNotNullable<WsSqlOrganizationModel>(sqlCrudConfig).ToList();
    }
}