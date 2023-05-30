// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace WsLabelCore.Bases;

/// <summary>
/// Базовый класс Windows.Forms.UserControl.
/// </summary>
#nullable enable
public partial class WsFormBaseUserControl : UserControl
{
    #region Public and private fields, properties, constructor

    internal WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    internal WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    internal WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    public ElementHost ElementHost { get; set; }
    public WsXamlBaseViewModel ViewModel { get; }
    public WsXamlBasePage Page { get; }

    public WsFormBaseUserControl()
    {
        InitializeComponent();
        ViewModel = new();
        Page = new(ViewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        //SetupElementHost();
    }

    protected WsFormBaseUserControl(WsLinesViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        Page = new(viewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        //SetupElementHost();
    }

    protected WsFormBaseUserControl(WsMoreViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        Page = new(viewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        //SetupElementHost();
    }

    protected WsFormBaseUserControl(WsPinCodeViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        Page = new WsPinCodePage(viewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        SetupElementHost();
    }

    protected WsFormBaseUserControl(WsPlusViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        Page = new WsLinesPage(viewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        SetupElementHost();
    }

    protected WsFormBaseUserControl(WsPlusNestingViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        Page = new WsPlusNestingPage(viewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        SetupElementHost();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    public virtual void RefreshViewModel() { }

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
