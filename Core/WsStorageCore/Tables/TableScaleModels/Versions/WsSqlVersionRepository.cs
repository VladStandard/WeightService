namespace WsStorageCore.Tables.TableScaleModels.Versions;

public class WsSqlVersionRepository : WsSqlTableRepositoryBase<WsSqlVersionModel>
{
    public List<WsSqlVersionModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlVersionModel.Version), Direction = WsSqlEnumOrder.Desc });
        List<WsSqlVersionModel> list = SqlCore.GetListNotNullable<WsSqlVersionModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.Version).ToList();
        return list;
    }
}