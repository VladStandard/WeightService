using WsStorageCore.Entities.SchemaScale.PlusScales;

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления ПЛУ линии.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlPlusLineViewModel : WsXamlBaseViewModel
{
    #region Public and private fields, properties, constructor

    public SqlPluScaleEntity PluLine { get; set; } = new();

    public WsXamlPlusLineViewModel()
    {
        FormUserControl = WsEnumNavigationPage.PlusLine;
    }

    #endregion
}