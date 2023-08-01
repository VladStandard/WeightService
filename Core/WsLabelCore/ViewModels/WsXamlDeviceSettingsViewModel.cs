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

    public WsSqlProductionFacilityModel Area { get; set; } = new();
    public WsSqlScaleModel Line { get; set; } = new();
    public WsSqlDeviceModel Device => WsSqlContextManagerHelper.Instance.DevicesRepository.GetItemByLine(Line);

    public WsXamlDeviceSettingsViewModel()
    {
        FormUserControl = WsEnumNavigationPage.Line;
    }

    #endregion
}