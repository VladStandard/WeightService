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

    internal WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    internal WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    internal WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    public string Message { get; set; } = string.Empty;

    #endregion

    #region Public and private methods

    public virtual void RefreshAction() { }

    #endregion
}