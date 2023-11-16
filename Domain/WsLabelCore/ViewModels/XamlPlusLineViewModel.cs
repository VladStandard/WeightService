using WsStorageCore.Entities.SchemaScale.PlusScales;

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления ПЛУ линии.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed class XamlPlusLineViewModel : XamlBaseViewModel
{
    #region Public and private fields, properties, constructor

    public SqlPluScaleEntity PluLine { get; set; } = new();

    public XamlPlusLineViewModel()
    {
        FormUserControl = EnumNavigationPage.PlusLine;
    }

    #endregion
}