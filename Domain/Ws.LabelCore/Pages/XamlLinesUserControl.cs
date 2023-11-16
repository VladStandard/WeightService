using Ws.LabelCore.Common;
using Ws.LabelCore.ViewModels;
namespace Ws.LabelCore.Pages;

/// <summary>
/// WinForms-контрол смены линии.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class XamlLinesUserControl : FormBaseUserControl, IFormUserControl
{
    #region Public and private fields, properties, constructor

    public XamlLinesViewModel ViewModel => Page.ViewModel as XamlLinesViewModel ?? new();

    public XamlLinesUserControl() : base(EnumNavigationPage.Line)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public void SetupUserControl() => ((XamlLinesPage)Page).SetupViewModel(ViewModel);

    #endregion
}