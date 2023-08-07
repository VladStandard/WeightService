// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Devices;

public sealed class WsSqlDeviceRepository : WsSqlTableRepositoryBase<WsSqlDeviceModel>
{
    #region Public and private methods

    public WsSqlDeviceModel GetItemByName(string name)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(new() { Name = nameof(WsSqlTableBase.Name), Value = name});
        return SqlCore.GetItemByCrud<WsSqlDeviceModel>(sqlCrudConfig);
    }
    
    public WsSqlDeviceModel GetItemByLine(WsSqlScaleModel line)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFkIdentityFilter(nameof(WsSqlDeviceScaleFkModel.Scale), line);
        return SqlCore.GetItemByCrud<WsSqlDeviceScaleFkModel>(sqlCrudConfig).Device;
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
            device.PrettyName = name;
            device.Ipv4 = MdNetUtils.GetLocalIpAddress();
        }
        return SaveOrUpdate(device);
    }
    
    public WsSqlDeviceModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceModel>();

    public WsSqlDeviceModel GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlDeviceModel>(uid);
    
    public List<WsSqlDeviceModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Name));
        return SqlCore.GetListNotNullable<WsSqlDeviceModel>(sqlCrudConfig);
    }

    public WsSqlDeviceModel GetCurrentDevice() => GetItemByName(MdNetUtils.GetLocalDeviceName(false));

    #endregion
}