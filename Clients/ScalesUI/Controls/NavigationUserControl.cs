// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Controls;

#nullable enable
public sealed partial class NavigationUserControl : UserControlBase
{
    #region Public and private fields, properties, constructor

    private UserControl? _userControl;
    public UserControl? UserControl
    {
        get => _userControl;
        set
        {
            _userControl = value;
            if (_userControl is not null)
            {
                _userControl.Visible = false;
                layoutPanel.Controls.Add(_userControl, 1, 1);
                layoutPanel.SetColumnSpan(_userControl, 3);
                _userControl.Dock = DockStyle.Fill;
                _userControl.Visible = true;
            }
        }
    }

    public NavigationUserControl()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    private void pictureBoxReturn_Click(object sender, EventArgs e)
    {
        ReturnBackAction();
    }

    #endregion
}