using WsStorageCore.Entities.SchemaRef.ProductionSites;
using WsStorageCore.Entities.SchemaScale.Scales;

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления линий.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class XamlLinesViewModel : XamlBaseViewModel
{
    #region Public and private fields, properties, constructor

    public SqlProductionSiteEntity ProductionSite { get; set; } = new();
    public SqlScaleEntity Line { get; set; } = new();
    public List<SqlProductionSiteEntity> ProductionSites { get; set; } = new();
    public List<SqlScaleEntity> Lines { get; set; } = new();

    public XamlLinesViewModel()
    {
        FormUserControl = EnumNavigationPage.Line;
    }

    #endregion
}