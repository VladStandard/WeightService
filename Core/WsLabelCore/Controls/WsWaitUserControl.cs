// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

#nullable enable
public sealed partial class WsWaitUserControl : WsBaseUserControl
{
    #region Public and private fields, properties, constructor

    public WsWaitViewModel ViewModel { get; }

    public WsWaitUserControl()
    {
        InitializeComponent();
        ViewModel = new();
    }

    #endregion

    #region Public and private methods

    public override void RefreshAction()
    {
        labelMessage.Text = Message;
    }

    #endregion
}