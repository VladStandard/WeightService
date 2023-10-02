using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.Devices;

public sealed class WsSqlDeviceRepository : WsSqlTableRepositoryBase<WsSqlDeviceModel>
{
    #region Public and private methods

    public WsSqlDeviceModel GetItemByName(string name)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlTableBase.Name), name));
        return SqlCore.GetItemByCrud<WsSqlDeviceModel>(sqlCrudConfig);
    }
    
    public WsSqlDeviceModel SaveOrUpdate (WsSqlDeviceModel deviceModel)
    {
        deviceModel.LoginDt = DateTime.Now;
        if (!deviceModel.IsNew)
            SqlCore.Update(deviceModel);
        else
        {
            deviceModel.LoginDt = DateTime.Now;
            deviceModel.LogoutDt = DateTime.Now;
            SqlCore.Save(deviceModel);
        }
        return deviceModel;
    }
    
    public WsSqlDeviceModel GetItemByNameOrCreate(string name)
    {
        WsSqlDeviceModel device = GetItemByName(name);
        if (device.IsNew)
        {
            device.Name = name;
            device.Ipv4 = MdNetUtils.GetLocalIpAddress();
        }
        return SaveOrUpdate(device);
    }
    
    public WsSqlDeviceModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceModel>();

    public WsSqlDeviceModel GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlDeviceModel>(uid);
    
    public IEnumerable<WsSqlDeviceModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerableNotNullable<WsSqlDeviceModel>(sqlCrudConfig);
    }

    public WsSqlDeviceModel GetCurrentDevice() => GetItemByName(MdNetUtils.GetLocalDeviceName(false));

    #endregion
}