// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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

    public WsXamlLinesViewModel()
    {
        FormUserControl = WsEnumNavigationPage.Line;
    }

    #endregion
}