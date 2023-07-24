// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleFkModels.DeviceTypesFks;
using WsStorageCore.Tables.TableScaleModels.Devices;
using WsStorageCore.Tables.TableScaleModels.DeviceTypes;

namespace DeviceControl.Pages.Menu.Devices.Hosts;

public sealed partial class ItemDevice : ItemBase<WsSqlDeviceModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlDeviceTypeModel> DeviceTypes { get; set; }
    private WsSqlDeviceTypeModel DeviceType { get; set; }
    private WsSqlDeviceTypeFkModel DeviceTypeFk { get; set; }

    #endregion

    public ItemDevice() : base()
    {
        DeviceType = new();
        DeviceTypeFk = new();
    }

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        DeviceTypes = new WsSqlDeviceTypeRepository().GetList(WsSqlCrudConfigUtils.GetCrudConfigComboBox());;
        DeviceTypeFk = new WsSqlDeviceTypeFkRepository().GetItemByDevice(SqlItemCast);
        DeviceType = DeviceTypeFk.Type.IsNotNew ? DeviceTypeFk.Type : ContextManager.SqlCore.GetItemNewEmpty<WsSqlDeviceTypeModel>();
    }

    protected override bool ValidateItemBeforeSave()
    { 
        DeviceTypeFk.Type = DeviceType;
        DeviceTypeFk.Device = SqlItemCast; 
        if (!SqlItemValidateWithMsg(DeviceTypeFk, !(DeviceTypeFk?.IsNew ?? false))) 
            return false;
        return base.ValidateItemBeforeSave();
    }

    protected override void ItemSave()
    {
        base.ItemSave();
        SqlItemSave(DeviceTypeFk);
    }
    
    #endregion
}