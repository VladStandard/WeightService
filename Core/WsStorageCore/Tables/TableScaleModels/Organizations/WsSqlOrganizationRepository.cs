namespace WsStorageCore.Tables.TableScaleModels.Organizations;

public class WsSqlOrganizationRepository : WsSqlTableRepositoryBase<WsSqlOrganizationModel>
{
    public List<WsSqlOrganizationModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(new(nameof(WsSqlTableBase.Name), WsSqlEnumOrder.Asc));
        return SqlCore.GetListNotNullable<WsSqlOrganizationModel>(sqlCrudConfig);
    }
}