namespace WsLabelCore.ViewModels;

/// <summary>
/// XAML модель представления диалога.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class XamlDialogViewModel : XamlBaseViewModel
{
    #region Public and private fields, properties, constructor

    public XamlDialogViewModel()
    {
        FormUserControl = EnumNavigationPage.Dialog;
    }

    #endregion
}