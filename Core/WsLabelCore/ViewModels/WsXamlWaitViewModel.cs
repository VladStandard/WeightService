namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления ожидания.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlWaitViewModel : WsXamlBaseViewModel, IWsViewModel
{
    #region Public and private fields, properties, constructor

    public WsXamlWaitViewModel()
    {
        FormUserControl = WsEnumNavigationPage.Wait;
    }

    #endregion
}