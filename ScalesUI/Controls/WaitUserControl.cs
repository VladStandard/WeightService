// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Controls;

public partial class WaitUserControl : UserControlBase
{
    #region Public and private fields, properties, constructor

    public WaitUserControl()
    {
        InitializeComponent();
        RefreshAction = WaitUserControl_Refresh;
    }

    #endregion

    #region Public and private methods

    private void WaitUserControl_Refresh()
    {
        ActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
        {
            labelMessage.Text = Message;
        });
    }

    #endregion
}