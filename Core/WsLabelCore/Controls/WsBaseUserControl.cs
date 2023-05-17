// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

#nullable enable
/// <summary>
/// Базовый класс контрола.
/// </summary>
public class WsBaseUserControl : UserControl
{
    #region Public and private fields, properties, constructor

    internal WsFontsSettingsHelper FontsSettings => WsFontsSettingsHelper.Instance;
    internal WsUserSessionHelper UserSession => WsUserSessionHelper.Instance;
    public string Message { get; set; }

    protected WsBaseUserControl()
    {
        Message = string.Empty;
    }

    #endregion

    #region Public and private methods

    public virtual void RefreshAction() { }

    #endregion
}