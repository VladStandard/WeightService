namespace WsStorageCore.Tables.TableScaleModels.DeviceTypes;

public class WsSqlDeviceTypeRepository : WsSqlTableRepositoryBase<WsSqlDeviceTypeModel>
{
    #region Item
    
    public WsSqlDeviceTypeModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceTypeModel>();
    
    public WsSqlDeviceTypeModel GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlDeviceTypeModel>(uid);

    public WsSqlDeviceTypeModel GetItemByName(string name)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(new() { Name = nameof(WsSqlTableBase.Name), Value = name});
        return SqlCore.GetItemByCrud<WsSqlDeviceTypeModel>(sqlCrudConfig);
    }

    public WsSqlDeviceTypeModel GetItemByNameOrCreate(string name)
    {
        WsSqlDeviceTypeModel deviceType = GetItemByName(name);
        if (deviceType.IsNew)
        {
            deviceType.Name = name;
            deviceType.PrettyName = name;
        }
        return SaveOrUpdate(deviceType);
    }
    
    #endregion

    #region List

    public List<WsSqlDeviceTypeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Name));
        return SqlCore.GetListNotNullable<WsSqlDeviceTypeModel>(sqlCrudConfig);
    }

    #endregion

    #region Crud

    public WsSqlDeviceTypeModel SaveOrUpdate (WsSqlDeviceTypeModel deviceTypeModel)
    {
        if (!deviceTypeModel.IsNew)
            SqlCore.Update(deviceTypeModel);
        else
            SqlCore.Save(deviceTypeModel);
        return deviceTypeModel;
    }

    #endregion
}