using System.Windows.Forms;
namespace WsLabelCore.Controls;

/// <summary>
/// WinForms-контрол навигации.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsFormNavigationUserControl : UserControl
{
    #region Public and private fields, properties, constructor

    public WsFormNavigationUserControl()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Перейти в контрол.
    /// </summary>
    public void SwitchUserControl(WsFormBaseUserControl formUserControl)
    {
        foreach (Control formControl in layoutPanelUser.Controls)
        {
            if (formControl is WsFormBaseUserControl getControl)
            {
                if (!getControl.Name.Equals(formUserControl.Name))
                    MdInvokeControl.SetVisible(formControl, false);
            }
            else if (formControl is not TableLayoutPanel)
                MdInvokeControl.SetVisible(formControl, false);
        }
        if (!layoutPanelUser.Controls.Contains(formUserControl))
            layoutPanelUser.Controls.Add(formUserControl, 1, 1);
        layoutPanelUser.SetRowSpan(formUserControl, 1);
        layoutPanelUser.SetColumnSpan(formUserControl, 1);
        formUserControl.ResumeLayout();
        formUserControl.Refresh();
    }

    /// <summary>
    /// Задать заголовок.
    /// </summary>
    public void SetTitle(string title) => MdInvokeControl.SetText(fieldTitle, title);

    #endregion
}