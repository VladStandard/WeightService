// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

#nullable enable
public sealed partial class WsMessageBoxUserControl : WsBaseUserControl
{
    #region Public and private fields, properties, constructor

    private ElementHost ElementHost { get; }
    public WsMessageBoxViewModel ViewModel { get; }
    private WsMessageBoxPage Page { get; }

    public WsMessageBoxUserControl()
    {
        InitializeComponent();
        
        ViewModel = new();
        SetupViewModel(ViewModel);
        Page = new(ViewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        ElementHost.Child = Page;
        Controls.Add(ElementHost);
    }

    #endregion

    #region Public and private methods

    public void SetupViewModel(WsMessageBoxViewModel viewModel)
    {
        viewModel.FontSizeCaption = 30;
        viewModel.FontSizeMessage = 26;
        viewModel.FontSizeButton = 22;
        viewModel.SizeCaption = 1;
        viewModel.SizeMessage = 5;
        viewModel.SizeButton = 1;
    }

    public override void RefreshAction()
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            //
        });
    }

    #endregion
}