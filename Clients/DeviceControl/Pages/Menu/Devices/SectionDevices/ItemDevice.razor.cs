// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.DeviceTypesFks;
using WsStorageCore.TableScaleModels.Devices;
using WsStorageCore.TableScaleModels.DeviceTypes;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionDevices;

public sealed partial class ItemDevice : RazorComponentItemBase<WsSqlDeviceModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlDeviceTypeModel _deviceType;
    private List<WsSqlDeviceTypeModel> DeviceTypeFkModels { get; set; }
    private WsSqlDeviceTypeModel DeviceType { get => _deviceType; set { _deviceType = value; SqlLinkedItems = new() { DeviceType }; } }
    private WsSqlDeviceTypeFkModel DeviceTypeFk { get; set; }

    #endregion

    public ItemDevice() : base()
    {
        _deviceType = new();
        DeviceTypeFk = new();
    }

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                DeviceTypeFkModels = ContextManager.ContextList.GetListNotNullable<WsSqlDeviceTypeModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullableByUid<WsSqlDeviceModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                    SqlItemCast = SqlItemNew<WsSqlDeviceModel>();
                DeviceTypeFk = ContextManager.ContextItem.GetItemDeviceTypeFkNotNullable(SqlItemCast);
                DeviceType = DeviceTypeFk.Type.IsNotNew ? DeviceTypeFk.Type : ContextManager.AccessManager.AccessItem.GetItemNewEmpty<WsSqlDeviceTypeModel>();
            }
        });
    }

    #endregion
}