namespace Ws.LabelCore.ViewModels;

/// <summary>
/// Модель представления вложенности ПЛУ.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class XamlPlusNestingViewModel : XamlBaseViewModel, IViewModel
{
    #region Public and private fields, properties, constructor

    public SqlViewPluNestingModel PluNesting { get; set; } = new();
    public List<SqlViewPluNestingModel> PlusNestings { get; set; } = new();

    public XamlPlusNestingViewModel()
    {
        FormUserControl = EnumNavigationPage.PlusNesting;
    }

    #endregion
}