// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
    public WsXamlBasePage Page { get; }

    /// <summary>
    /// Для корректного отображения наследуемых классов UserControl.
    /// </summary>
    public WsFormBaseUserControl()
    {
        InitializeComponent();
        ElementHost = new() { Dock = DockStyle.Fill };
        Page = new WsXamlBasePage();
    }

    protected WsFormBaseUserControl(WsEnumNavigationPage formUserControl)
    {
        InitializeComponent();
        ElementHost = new() { Dock = DockStyle.Fill };
        switch (formUserControl)
        {
            case WsEnumNavigationPage.Dialog:
                Page = new WsXamlDialogPage();
                SetupElementHost();
                break;
            case WsEnumNavigationPage.Line:
                Page = new WsXamlLinesPage();
                SetupElementHost();
                break;
            case WsEnumNavigationPage.Kneading:
                Page = new WsXamlBasePage();
                break;
            case WsEnumNavigationPage.PinCode:
                Page = new WsXamlPinCodePage();
                SetupElementHost();
                break;
            case WsEnumNavigationPage.PlusLine:
                Page = new WsXamlBasePage();
                break;
            case WsEnumNavigationPage.PlusNesting:
                Page = new WsXamlPlusNestingPage();
                SetupElementHost();
                break;
            case WsEnumNavigationPage.Wait:
                Page = new WsXamlWaitPage();
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
    /// Настройка ElementHost.
    /// </summary>
    private void SetupElementHost()
    {
        ElementHost.Child = Page;
        Controls.Add(ElementHost);
    }

    #endregion
}