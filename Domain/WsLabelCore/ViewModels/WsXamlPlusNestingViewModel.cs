namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления вложенности ПЛУ.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlPlusNestingViewModel : WsXamlBaseViewModel, IWsViewModel
{
    #region Public and private fields, properties, constructor

    public WsSqlViewPluNestingModel PluNesting { get; set; } = new();
    public List<WsSqlViewPluNestingModel> PlusNestings { get; set; } = new();

    public WsXamlPlusNestingViewModel()
    {
        FormUserControl = WsEnumNavigationPage.PlusNesting;
    }

    #endregion
}