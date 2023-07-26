// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.DeviceTypesFks;

public class WsSqlDeviceTypeFkRepository : WsSqlTableRepositoryBase<WsSqlDeviceTypeFkModel>
{
    public WsSqlDeviceTypeFkModel GetItemByDevice(WsSqlDeviceModel device)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudConfig(
            WsSqlCrudConfigModel.GetFiltersIdentity(nameof(WsSqlDeviceTypeFkModel.Device), device.IdentityValueUid), WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNullable<WsSqlDeviceTypeFkModel>(sqlCrudConfig) ?? new();
    }

    public List<WsSqlDeviceTypeFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlDeviceTypeFkModel> result = SqlCore.GetListNotNullable<WsSqlDeviceTypeFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            result = result
                .OrderBy(item => item.Type.Name)
                .ThenBy(item => item.Device.Name).ToList();
        return result;
    }
}