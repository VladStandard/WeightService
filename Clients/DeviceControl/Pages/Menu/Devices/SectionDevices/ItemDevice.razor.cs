// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.DeviceTypesFks;
using WsStorageCore.TableScaleModels.Devices;
using WsStorageCore.TableScaleModels.DeviceTypes;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionDevices;

public sealed partial class ItemDevice : RazorComponentItemBase<WsSqlDeviceModel>
{
    #region Public and private fields, properties, constructor
    private List<WsSqlDeviceTypeModel> DeviceTypeFkModels { get; set; }
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
        DeviceTypeFkModels = ContextManager.ContextList.GetListNotNullable<WsSqlDeviceTypeModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
        DeviceTypeFk = ContextManager.ContextItem.GetItemDeviceTypeFkNotNullable(SqlItemCast);
        DeviceType = DeviceTypeFk.Type.IsNotNew ? DeviceTypeFk.Type : ContextManager.AccessManager.AccessItem.GetItemNewEmpty<WsSqlDeviceTypeModel>();
    }

    protected override void SqlItemSaveAdditional()
    {
        if (DeviceType.IsNotNew)
        {
            DeviceTypeFk.Type = DeviceType;
            DeviceTypeFk.Device = SqlItemCast;
            SqlItemSave(DeviceTypeFk);
            return;
        }
        ContextManager.AccessManager.AccessItem.Delete(DeviceTypeFk);
    }
    
    #endregion
}
