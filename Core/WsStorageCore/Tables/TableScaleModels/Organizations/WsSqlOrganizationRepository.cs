namespace WsStorageCore.Tables.TableScaleModels.Organizations;

public class WsSqlOrganizationRepository : WsSqlTableRepositoryBase<WsSqlOrganizationModel>
{
    public List<WsSqlOrganizationModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlOrganizationModel> list = SqlCore.GetListNotNullable<WsSqlOrganizationModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }
}