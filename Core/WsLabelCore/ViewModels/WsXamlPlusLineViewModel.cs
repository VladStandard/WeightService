namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления ПЛУ линии.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlPlusLineViewModel : WsXamlBaseViewModel, IWsViewModel
{
    #region Public and private fields, properties, constructor

    public WsSqlPluScaleModel PluLine { get; set; } = new();

    public WsXamlPlusLineViewModel()
    {
        FormUserControl = WsEnumNavigationPage.PlusLine;
    }

    #endregion
}