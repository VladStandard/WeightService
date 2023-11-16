using System.Windows.Forms;

namespace Ws.LabelCore.Controls;

/// <summary>
/// WinForms-контрол навигации.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class FormNavigationUserControl : UserControl
{
    #region Public and private fields, properties, constructor

    public FormNavigationUserControl()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Перейти в контрол.
    /// </summary>
    public void SwitchUserControl(FormBaseUserControl formUserControl)
    {
        foreach (Control formControl in layoutPanelUser.Controls)
        {
            if (formControl is FormBaseUserControl getControl)
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