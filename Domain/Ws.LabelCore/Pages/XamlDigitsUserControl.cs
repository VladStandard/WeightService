using Ws.LabelCore.Common;
using Ws.LabelCore.ViewModels;
namespace Ws.LabelCore.Pages;

/// <summary>
/// WinForms-контрол ввода цифр.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class XamlDigitsUserControl : FormBaseUserControl, IFormUserControl
{
    #region Public and private fields, properties, constructor

    public XamlDigitsViewModel ViewModel => Page.ViewModel as XamlDigitsViewModel ?? new();
    public XamlDigitsUserControl() : base(EnumNavigationPage.Digit)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public void SetupUserControl() => 
        ((XamlDigitsPage)Page).SetupViewModel(ViewModel);

    #endregion
}