// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Controls;

public class UserControlBase : UserControl
{
    #region Public and private fields, properties, constructor

    internal FontsSettingsHelper FontsSettings => FontsSettingsHelper.Instance;
    internal UserSessionHelper UserSession => UserSessionHelper.Instance;
    internal Action ReturnBackAction { get; set; }
    internal Action RefreshAction { get; set; }
    internal DialogResult Result { get; set; }
    internal string Message { get; set; }

    protected UserControlBase()
    {
        Result = DialogResult.None;
        ReturnBackAction = () => { };
        RefreshAction = () => { };
    }

    #endregion

    private void InitializeComponent()
    {
        SuspendLayout();
        // 
        // UserControlBase
        // 
        BackColor = Color.Transparent;
        Name = "UserControlBase";
        ResumeLayout(false);
    }
}