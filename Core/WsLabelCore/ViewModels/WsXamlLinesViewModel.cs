using WsStorageCore.Entities.SchemaRef.ProductionSites;
using WsStorageCore.Entities.SchemaScale.Scales;

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления линий.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlLinesViewModel : WsXamlBaseViewModel
{
    #region Public and private fields, properties, constructor

    public WsSqlProductionSiteEntity ProductionSite { get; set; } = new();
    public WsSqlScaleEntity Line { get; set; } = new();
    public List<WsSqlProductionSiteEntity> ProductionSites { get; set; } = new();
    public List<WsSqlScaleEntity> Lines { get; set; } = new();

    public WsXamlLinesViewModel()
    {
        FormUserControl = WsEnumNavigationPage.Line;
    }

    #endregion
}