// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.DeviceTypesFks;
using WsStorageCore.TableScaleModels.Devices;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionDevices;

public sealed partial class Devices : RazorComponentSectionBase<DeviceModel>
{
    #region Public and private fields, properties, constructor
    
    private List<DeviceTypeFkModel> DeviceTypesFk { get; set; }
    
    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        base.SetSqlSectionCast();
        DeviceTypesFk = ContextManager.ContextList.GetListNotNullable<DeviceTypeFkModel>(new());
        // DataAccessHelper.Instance.GetItemDeviceTypeFkNotNullable(device).Type.PrettyName
    }

    private string GetDevicePrettyName(DeviceModel device)
    {
        DeviceTypeFkModel? deviceTypeFk = DeviceTypesFk.Find((x) => x.Device.Equals(device));
        return deviceTypeFk != null ? deviceTypeFk.Type.PrettyName : "";
    }

    #endregion
}
