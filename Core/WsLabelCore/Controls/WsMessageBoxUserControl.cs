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
        double fontSizeCaption = 30;
        double fontSizeMessage = 26;
        double fontSizeButton = 22;
        ushort sizeCaption = 1;
        ushort sizeMessage = 5;
        ushort sizeButton = 1;
        ViewModel.FontSizeCaption = fontSizeCaption;
        ViewModel.FontSizeMessage = fontSizeMessage;
        ViewModel.FontSizeButton = fontSizeButton;
        ViewModel.SizeCaption = sizeCaption;
        ViewModel.SizeMessage = sizeMessage;
        ViewModel.SizeButton = sizeButton;
        Page = new(ViewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        ElementHost.Child = Page;
        Controls.Add(ElementHost);
    }

    #endregion

    #region Public and private methods

    public override void RefreshAction()
    {
        WsActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
        {
            //
        });
    }

    #endregion
}