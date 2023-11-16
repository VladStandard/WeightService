using Ws.LabelCore.Common;
using Ws.LabelCore.ViewModels;
namespace Ws.LabelCore.Pages;

/// <summary>
/// WinForms-контрол диалога.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class XamlDialogUserControl : FormBaseUserControl, IFormUserControl
{
    #region Public and private fields, properties, constructor

    public XamlDialogViewModel ViewModel => Page.ViewModel as XamlDialogViewModel ?? new();
    public XamlDialogUserControl() : base(EnumNavigationPage.Dialog)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Настроить WinForms-контрол.
    /// </summary>
    public void SetupUserControl() => ((XamlDialogPage)Page).SetupViewModel(ViewModel);

    #endregion
}