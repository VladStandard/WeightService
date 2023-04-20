// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Controls;

public sealed partial class WaitUserControl : UserControlBase
{
    #region Public and private fields, properties, constructor

    public WaitUserControl()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override void RefreshAction()
    {
        base.RefreshAction();
        WsActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
        {
            labelMessage.Text = Message;
        });
    }

    #endregion
}