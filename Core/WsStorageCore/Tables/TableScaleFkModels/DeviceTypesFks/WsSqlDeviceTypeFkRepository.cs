namespace WsStorageCore.Tables.TableScaleFkModels.DeviceTypesFks;

public class WsSqlDeviceTypeFkRepository : WsSqlTableRepositoryBase<WsSqlDeviceTypeFkModel>
{
    public WsSqlDeviceTypeFkModel GetItemByDevice(WsSqlDeviceModel device)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFkIdentityFilter(nameof(WsSqlDeviceTypeFkModel.Device), device);
        return SqlCore.GetItemByCrud<WsSqlDeviceTypeFkModel>(sqlCrudConfig);
    }

    public List<WsSqlDeviceTypeFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlDeviceTypeFkModel> items = SqlCore.GetEnumerableNotNullable<WsSqlDeviceTypeFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items
                .OrderBy(item => item.Type.Name)
                .ThenBy(item => item.Device.Name);
        return items.ToList();
    }
}