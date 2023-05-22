// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

#nullable enable
/// <summary>
/// Корневой контрол навигации.
/// </summary>
public sealed partial class WsNavigationUserControl : WsBaseUserControl
{
    #region Public and private fields, properties, constructor

    public WsNavigationUserControl()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Перейти в контрол.
    /// </summary>
    /// <param name="userControl"></param>
    /// <param name="title"></param>
    /// <param name="message"></param>
    public void SwitchUserControl(WsBaseUserControl userControl, string title, string message)
    {
        if (!string.IsNullOrEmpty(title))
        {
            fieldTitle.Text = title;
            fieldTitle.Visible = true;
        }
        else
            fieldTitle.Visible = false;
        userControl.Message = message;
        userControl.Visible = false;
        foreach (Control control in layoutPanelUser.Controls) control.Visible = false;
        if (!layoutPanelUser.Controls.Contains(userControl))
            layoutPanelUser.Controls.Add(userControl, 1, 1);
        layoutPanelUser.SetRowSpan(userControl, 1);
        layoutPanelUser.SetColumnSpan(userControl, 1);
        userControl.Dock = DockStyle.Fill;
        userControl.Visible = true;
        userControl.RefreshAction();
    }

    #endregion
}