﻿namespace WsStorageCore.Tables.TableScaleModels.DeviceTypes;

public class WsSqlDeviceTypeRepository : WsSqlTableRepositoryBase<WsSqlDeviceTypeModel>
{
    #region Item
    
    public WsSqlDeviceTypeModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceTypeModel>();
    
    public WsSqlDeviceTypeModel GetItemByUid(Guid uid) => SqlCore.GetItemNotNullableByUid<WsSqlDeviceTypeModel>(uid);

    public WsSqlDeviceTypeModel GetItemByName(string name)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), name, WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNotNullable<WsSqlDeviceTypeModel>(sqlCrudConfig);
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
        List<WsSqlDeviceTypeModel> result = SqlCore.GetListNotNullable<WsSqlDeviceTypeModel>(sqlCrudConfig);
        result = result.OrderBy(item => item.Name).ToList();
        return result;
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