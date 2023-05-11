// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Controls;

/// <summary>
/// Базовый класс контрола.
/// </summary>
public class UserControlBase : UserControl
{
    #region Public and private fields, properties, constructor

    internal WsFontsSettingsHelper FontsSettings => WsFontsSettingsHelper.Instance;
    internal WsUserSessionHelper UserSession => WsUserSessionHelper.Instance;
    internal Action ReturnBackAction { get; set; }
    internal DialogResult Result { get; set; }
    internal string Message { get; set; }

    protected UserControlBase()
    {
        SuspendLayout();
        Result = DialogResult.None;
        ReturnBackAction = () => { };
        ResumeLayout(false);
    }

    #endregion

    #region Public and private methods

    public virtual void RefreshAction() { }

    #endregion
}