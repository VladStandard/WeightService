// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Devices;

public sealed class WsSqlDeviceRepository : WsSqlTableRepositoryBase<WsSqlDeviceModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlDeviceRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlDeviceRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Item
    
    public WsSqlDeviceModel GetItemByName(string name)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), name, WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNotNullable<WsSqlDeviceModel>(sqlCrudConfig);
    }
    
    public WsSqlDeviceModel GetItemByLine(WsSqlScaleModel scale)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            WsSqlCrudConfigModel.GetFiltersIdentity(nameof(WsSqlDeviceScaleFkModel.Scale), scale.IdentityValueId), WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNotNullable<WsSqlDeviceScaleFkModel>(sqlCrudConfig).Device;
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

    public WsSqlDeviceModel GetItemByUid(Guid uid) => SqlCore.GetItemNotNullableByUid<WsSqlDeviceModel>(uid);
    
    #endregion

    #region List
    
    public List<WsSqlDeviceModel> GetList() => ContextList.GetListNotNullableDevices(SqlCrudConfig);
    
    public List<WsSqlDeviceModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) =>
        ContextList.GetListNotNullableDevices(sqlCrudConfig);
    
    #endregion
}