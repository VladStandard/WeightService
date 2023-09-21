using WsStorageCore.Tables.TableRefModels.ProductionSites;

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления линий.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlLinesViewModel : WsXamlBaseViewModel, IWsViewModel
{
    #region Public and private fields, properties, constructor

    public WsSqlProductionSiteModel ProductionSite { get; set; } = new();
    public WsSqlScaleModel Line { get; set; } = new();
    public List<WsSqlProductionSiteModel> ProductionSites { get; set; } = new();
    public List<WsSqlScaleModel> Lines { get; set; } = new();

    public WsXamlLinesViewModel()
    {
        FormUserControl = WsEnumNavigationPage.Line;
    }

    #endregion
}