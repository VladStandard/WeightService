// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления настроек устройства.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlDeviceSettingsViewModel : WsXamlBaseViewModel, IWsViewModel
{
    #region Public and private fields, properties, constructor

    public WsSqlScaleModel Line { get; set; } = new();
    public WsSqlDeviceModel Device { get; set; } = new();
    public List<WsSqlDeviceModel> Devices { get; set; } = new();
    public List<WsSqlDeviceSettingsFkModel> DeviceSettingsFks { get; set; } = new();

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