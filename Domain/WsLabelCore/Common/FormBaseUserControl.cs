using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace WsLabelCore.Common;

/// <summary>
/// Базовый класс WinForms-контрола.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public partial class FormBaseUserControl : UserControl//, IFormUserControl
{
    #region Public and private fields, properties, constructor

    internal LabelSessionHelper LabelSession => LabelSessionHelper.Instance;
    internal SqlContextManagerHelper ContextManager => SqlContextManagerHelper.Instance;
    internal SqlContextCacheHelper ContextCache => SqlContextCacheHelper.Instance;
    private ElementHost ElementHost { get; }
    protected XamlBasePage Page { get; }

    /// <summary>
    /// Для корректного отображения наследуемых классов UserControl.
    /// </summary>
    public FormBaseUserControl()
    {
        InitializeComponent();
        ElementHost = new() { Dock = DockStyle.Fill };
        Page = new();
    }

    protected FormBaseUserControl(EnumNavigationPage formUserControl)
    {
        InitializeComponent();
        ElementHost = new() { Dock = DockStyle.Fill };
        switch (formUserControl)
        {
            case EnumNavigationPage.Dialog:
                Page = new XamlDialogPage();
                SetupElementHost();
                break;
            case EnumNavigationPage.Digit:
                Page = new XamlDigitsPage();
                SetupElementHost();
                break;
            case EnumNavigationPage.Line:
                Page = new XamlLinesPage();
                SetupElementHost();
                break;
            case EnumNavigationPage.Kneading:
                Page = new();
                break;
            case EnumNavigationPage.PlusLine:
                Page = new();
                break;
            case EnumNavigationPage.PlusNesting:
                Page = new XamlPlusNestingPage();
                SetupElementHost();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(formUserControl), formUserControl, formUserControl.ToString());
        }
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{Name} | " + Page.ViewModel;

    /// <summary>
    /// Настроить ElementHost.
    /// </summary>
    private void SetupElementHost()
    {
        ElementHost.Child = Page;
        Controls.Add(ElementHost);
    }

    /// <summary>
    /// Настроить действия диалога.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void SetupActions(EnumDialogType dialogType, List<Action> actions)
    {
        switch (dialogType)
        {
            case EnumDialogType.CancelYes:
                if (!actions.Count.Equals(2))
                    throw new ArgumentOutOfRangeException(nameof(actions), actions, actions.ToString());
                Page.ViewModel.CmdCancel.AddAction(actions.First());
                Page.ViewModel.CmdYes.AddAction(actions.Last());
                break;
            case EnumDialogType.NoYes:
                if (!actions.Count.Equals(2))
                    throw new ArgumentOutOfRangeException(nameof(actions), actions, actions.ToString());
                Page.ViewModel.CmdNo.AddAction(actions.First());
                Page.ViewModel.CmdYes.AddAction(actions.Last());
                break;
            case EnumDialogType.Ok:
                if (!actions.Count.Equals(1))
                    throw new ArgumentOutOfRangeException(nameof(actions), actions, actions.ToString());
                Page.ViewModel.CmdOk.AddAction(actions.First());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dialogType), dialogType, dialogType.ToString());
        }
    }

    /// <summary>
    /// Настроить кнопки.
    /// </summary>
    public void SetupButtons(EnumDialogType dialogType, List<Action> actions, string message, int width)
    {
        switch (dialogType)
        {
            case EnumDialogType.CancelYes:
                if (!actions.Count.Equals(2))
                    throw new ArgumentOutOfRangeException(nameof(actions), actions, actions.ToString());
                Page.ViewModel.SetupButtonsCancelYes(message, actions.First(), actions.Last(), width);
                break;
            case EnumDialogType.NoYes:
                if (!actions.Count.Equals(2))
                    throw new ArgumentOutOfRangeException(nameof(actions), actions, actions.ToString());
                Page.ViewModel.SetupButtonsNoYes(message, actions.First(), actions.Last(), width);
                break;
            case EnumDialogType.Ok:
                if (!actions.Count.Equals(1))
                    throw new ArgumentOutOfRangeException(nameof(actions), actions, actions.ToString());
                Page.ViewModel.SetupButtonsOk(message, actions.First(), width);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dialogType), dialogType, dialogType.ToString());
        }
    }

    #endregion
}