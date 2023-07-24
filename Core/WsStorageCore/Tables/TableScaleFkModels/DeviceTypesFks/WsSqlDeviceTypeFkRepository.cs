namespace WsStorageCore.Tables.TableScaleFkModels.DeviceTypesFks;

public class WsSqlDeviceTypeFkRepository : WsSqlTableRepositoryBase<WsSqlDeviceTypeFkModel>
{
    public WsSqlDeviceTypeFkModel GetItemByDevice(WsSqlDeviceModel device)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            WsSqlCrudConfigModel.GetFiltersIdentity(nameof(WsSqlDeviceTypeFkModel.Device), device.IdentityValueUid), WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNullable<WsSqlDeviceTypeFkModel>(sqlCrudConfig) ?? new();
    }

    public List<WsSqlDeviceTypeFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlDeviceTypeFkModel> result = SqlCore.GetListNotNullable<WsSqlDeviceTypeFkModel>(sqlCrudConfig);
        result = result
            .OrderBy(item => item.Type.Name)
            .ThenBy(item => item.Device.Name).ToList();
        return result;
    }
}