// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

#nullable enable
public sealed partial class WsLinesUserControl : WsBaseUserControl
{
    #region Public and private fields, properties, constructor

    private ElementHost ElementHost { get; }
    public WsLinesViewModel ViewModel { get; }
    private WsLinesPage Page { get; }

    public WsLinesUserControl()
    {
        InitializeComponent();
        
        ViewModel = new();
        Page = new(ViewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        ElementHost.Child = Page;
        Controls.Add(ElementHost);
    }

    #endregion

    #region Public and private methods

    public override void RefreshAction()
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            ViewModel.Area = UserSession.ProductionFacility;
            ViewModel.Line = UserSession.Scale;
        });
    }

    #endregion
}