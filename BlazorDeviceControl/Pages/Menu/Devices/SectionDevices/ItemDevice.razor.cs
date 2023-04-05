// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionDevices;

public sealed partial class ItemDevice : RazorComponentItemBase<DeviceModel>
{
    #region Public and private fields, properties, constructor

    private DeviceTypeModel _deviceType;
    private DeviceTypeModel DeviceType { get => _deviceType; set { _deviceType = value; SqlLinkedItems = new() { DeviceType }; } }
    private DeviceTypeFkModel DeviceTypeFk { get; set; }

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
                DataContext.GetListNotNullable<DeviceTypeModel>(SqlCrudConfigUtils.GetCrudConfigComboBox());

                SqlItemCast = DataContext.GetItemNotNullable<DeviceModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                    SqlItemCast = SqlItemNew<DeviceModel>();
                DeviceTypeFk = DataAccess.GetItemDeviceTypeFkNotNullable(SqlItemCast);
                DeviceType = DeviceTypeFk.Type.IsNotNew ? DeviceTypeFk.Type : DataAccess.GetItemNewEmpty<DeviceTypeModel>();
            }
        });
    }

    #endregion
}
