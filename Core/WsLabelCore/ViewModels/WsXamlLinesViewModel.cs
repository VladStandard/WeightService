namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления линий.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlLinesViewModel : WsXamlBaseViewModel, IWsViewModel
{
    #region Public and private fields, properties, constructor

    public WsSqlProductionFacilityModel Area { get; set; } = new();
    public WsSqlScaleModel Line { get; set; } = new();
    public List<WsSqlProductionFacilityModel> Areas { get; set; } = new();
    public List<WsSqlScaleModel> Lines { get; set; } = new();
    public WsSqlDeviceModel Device => WsSqlContextManagerHelper.Instance.DeviceRepository.GetItemByLine(Line);

    public WsXamlLinesViewModel()
    {
        FormUserControl = WsEnumNavigationPage.Line;
    }

    #endregion
}