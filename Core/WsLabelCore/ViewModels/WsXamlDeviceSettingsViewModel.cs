using WsStorageCore.Entities.SchemaConf.DeviceSettingsFks;
using WsStorageCore.Entities.SchemaScale.Devices;
using WsStorageCore.Entities.SchemaScale.Scales;
namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления настроек устройства.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlDeviceSettingsViewModel : WsXamlBaseViewModel
{
    #region Public and private fields, properties, constructor

    public WsSqlScaleEntity Line { get; set; } = new();
    public WsSqlDeviceEntity Device { get; set; } = new();
    public List<WsSqlDeviceEntity> Devices { get; set; } = new();
    public ObservableCollection<WsSqlDeviceSettingsFkEntity> DeviceSettingsFks { get; set; } = new();

    public WsXamlDeviceSettingsViewModel()
    {
        FormUserControl = WsEnumNavigationPage.Line;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        $"{FormUserControl} | " +
        (Commands.Any() ? $"{string.Join(" | ", Commands.Select(item => item.ToString()))}" : $"{nameof(Commands)} is <Empty>") +
        $"{Line} | {Device}";

    #endregion
}