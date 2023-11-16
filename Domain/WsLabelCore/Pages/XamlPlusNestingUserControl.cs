namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол смены вложенности ПЛУ.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class XamlPlusNestingUserControl : FormBaseUserControl, IFormUserControl
{
    #region Public and private fields, properties, constructor

    public XamlPlusNestingViewModel ViewModel => Page.ViewModel as XamlPlusNestingViewModel ?? new();

    public XamlPlusNestingUserControl() : base(EnumNavigationPage.PlusNesting)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public void SetupUserControl() => ((XamlPlusNestingPage)Page).SetupViewModel(ViewModel);

    #endregion
}