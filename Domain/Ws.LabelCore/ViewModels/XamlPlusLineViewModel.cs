using Ws.StorageCore.Entities.SchemaScale.PlusScales;

namespace Ws.LabelCore.ViewModels;

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