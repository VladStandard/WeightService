namespace WsLabelCore.ViewModels;

/// <summary>
/// XAML модель представления диалога.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlDialogViewModel : WsXamlBaseViewModel
{
    #region Public and private fields, properties, constructor

    public WsXamlDialogViewModel()
    {
        FormUserControl = WsEnumNavigationPage.Dialog;
    }

    #endregion
}