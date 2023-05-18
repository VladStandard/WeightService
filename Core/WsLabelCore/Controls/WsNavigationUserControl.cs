// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

#nullable enable
public sealed partial class WsNavigationUserControl : WsBaseUserControl
{
    #region Public and private fields, properties, constructor

    public WsNavigationViewModel ViewModel { get; }

    public WsNavigationUserControl()
    {
        InitializeComponent();
        ViewModel = new();
    }

    #endregion

    #region Public and private methods

    public void AddUserControl(UserControl userControl)
    {
        userControl.Visible = false;
        foreach (Control control in layoutPanel.Controls) control.Visible = false;
        if (!layoutPanel.Controls.Contains(userControl))
            layoutPanel.Controls.Add(userControl, 1, 1);
        //layoutPanel.SetColumnSpan(userControl, 3);
        userControl.Dock = DockStyle.Fill;
        userControl.Visible = true;
        Visible = true;
    }

    private void pictureBoxReturn_Click(object sender, EventArgs e)
    {
        ViewModel.ActionReturnCancel();
    }

    #endregion
}