using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace WsLabelCore.Common;

/// <summary>
/// Базовый класс WinForms-контрола.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public partial class WsFormBaseUserControl : UserControl//, IWsFormUserControl
{
    #region Public and private fields, properties, constructor

    internal WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    internal WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    internal WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    private ElementHost ElementHost { get; }
    protected WsXamlBasePage Page { get; }

    /// <summary>
    /// Для корректного отображения наследуемых классов UserControl.
    /// </summary>
    public WsFormBaseUserControl()
    {
        InitializeComponent();
        ElementHost = new() { Dock = DockStyle.Fill };
        Page = new();
    }

    protected WsFormBaseUserControl(WsEnumNavigationPage formUserControl)
    {
        InitializeComponent();
        ElementHost = new() { Dock = DockStyle.Fill };
        switch (formUserControl)
        {
            case WsEnumNavigationPage.DeviceSettings:
                Page = new WsXamlDeviceSettingsPage();
                SetupElementHost();
                break;
            case WsEnumNavigationPage.Dialog:
                Page = new WsXamlDialogPage();
                SetupElementHost();
                break;
            case WsEnumNavigationPage.Digit:
                Page = new WsXamlDigitsPage();
                SetupElementHost();
                break;
            case WsEnumNavigationPage.Line:
                Page = new WsXamlLinesPage();
                SetupElementHost();
                break;
            case WsEnumNavigationPage.Kneading:
                Page = new();
                break;
            case WsEnumNavigationPage.PlusLine:
                Page = new();
                break;
            case WsEnumNavigationPage.PlusNesting:
                Page = new WsXamlPlusNestingPage();
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
    public void SetupActions(WsEnumDialogType dialogType, List<Action> actions)
    {
        switch (dialogType)
        {
            case WsEnumDialogType.CancelYes:
                if (!actions.Count.Equals(2))
                    throw new ArgumentOutOfRangeException(nameof(actions), actions, actions.ToString());
                Page.ViewModel.CmdCancel.AddAction(actions.First());
                Page.ViewModel.CmdYes.AddAction(actions.Last());
                break;
            case WsEnumDialogType.NoYes:
                if (!actions.Count.Equals(2))
                    throw new ArgumentOutOfRangeException(nameof(actions), actions, actions.ToString());
                Page.ViewModel.CmdNo.AddAction(actions.First());
                Page.ViewModel.CmdYes.AddAction(actions.Last());
                break;
            case WsEnumDialogType.Ok:
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
    public void SetupButtons(WsEnumDialogType dialogType, List<Action> actions, string message, int width)
    {
        switch (dialogType)
        {
            case WsEnumDialogType.CancelYes:
                if (!actions.Count.Equals(2))
                    throw new ArgumentOutOfRangeException(nameof(actions), actions, actions.ToString());
                Page.ViewModel.SetupButtonsCancelYes(message, actions.First(), actions.Last(), width);
                break;
            case WsEnumDialogType.NoYes:
                if (!actions.Count.Equals(2))
                    throw new ArgumentOutOfRangeException(nameof(actions), actions, actions.ToString());
                Page.ViewModel.SetupButtonsNoYes(message, actions.First(), actions.Last(), width);
                break;
            case WsEnumDialogType.Ok:
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