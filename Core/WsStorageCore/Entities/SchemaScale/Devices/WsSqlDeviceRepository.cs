using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.Devices;

public sealed class WsSqlDeviceRepository : WsSqlTableRepositoryBase<WsSqlDeviceEntity>
{
    #region Public and private methods

    public WsSqlDeviceEntity GetItemByName(string name)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlTableBase.Name), name));
        return SqlCore.GetItemByCrud<WsSqlDeviceEntity>(sqlCrudConfig);
    }
    
    public WsSqlDeviceEntity SaveOrUpdate (WsSqlDeviceEntity deviceEntity)
    {
        deviceEntity.LoginDt = DateTime.Now;
        if (!deviceEntity.IsNew)
            SqlCore.Update(deviceEntity);
        else
        {
            deviceEntity.LoginDt = DateTime.Now;
            deviceEntity.LogoutDt = DateTime.Now;
            SqlCore.Save(deviceEntity);
        }
        return deviceEntity;
    }
    
    public WsSqlDeviceEntity GetItemByNameOrCreate(string name)
    {
        WsSqlDeviceEntity device = GetItemByName(name);
        if (device.IsNew)
        {
            device.Name = name;
            device.Ipv4 = MdNetUtils.GetLocalIpAddress();
        }
        return SaveOrUpdate(device);
    }
    
    public WsSqlDeviceEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceEntity>();

    public WsSqlDeviceEntity GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlDeviceEntity>(uid);
    
    public IEnumerable<WsSqlDeviceEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlDeviceEntity>(sqlCrudConfig);
    }

    public WsSqlDeviceEntity GetCurrentDevice() => GetItemByName(MdNetUtils.GetLocalDeviceName(false));

    #endregion
}