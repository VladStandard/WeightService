// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace WsLabelCore.Common;

/// <summary>
/// Базовый класс Windows.Forms.UserControl.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public partial class WsFormBaseUserControl : UserControl
{
    #region Public and private fields, properties, constructor

    internal WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    internal WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    internal WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    private ElementHost ElementHost { get; }
    public WsXamlBasePage Page { get; }

    public WsFormBaseUserControl()
    {
        InitializeComponent();
        Page = new(new());
        ElementHost = new() { Dock = DockStyle.Fill };
        SetupElementHost();
    }

    protected WsFormBaseUserControl(WsDialogViewModel viewModel)
    {
        InitializeComponent();
        Page = new WsXamlDialogPage(viewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        SetupElementHost();
    }

    protected WsFormBaseUserControl(WsLinesViewModel viewModel)
    {
        InitializeComponent();
        Page = new WsXamlLinesPage(viewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        SetupElementHost();
    }

    protected WsFormBaseUserControl(WsMoreViewModel viewModel)
    {
        InitializeComponent();
        Page = new(viewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        SetupElementHost();
    }

    protected WsFormBaseUserControl(WsPinCodeViewModel viewModel)
    {
        InitializeComponent();
        Page = new WsPinCodePage(viewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        SetupElementHost();
    }

    protected WsFormBaseUserControl(WsPlusViewModel viewModel)
    {
        InitializeComponent();
        Page = new WsXamlLinesPage(viewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        SetupElementHost();
    }

    protected WsFormBaseUserControl(WsPlusNestingViewModel viewModel)
    {
        InitializeComponent();
        Page = new WsPlusNestingPage(viewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        SetupElementHost();
    }

    protected WsFormBaseUserControl(WsWaitViewModel viewModel)
    {
        InitializeComponent();
        Page = new WsXamlWaitPage(viewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        SetupElementHost();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{Name} | " + Page.ViewModel;

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public virtual void RefreshUserConrol()
    {
        Page.RefreshViewModel();
    }

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
