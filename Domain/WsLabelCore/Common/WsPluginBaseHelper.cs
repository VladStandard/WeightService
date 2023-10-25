namespace WsLabelCore.Common;

/// <summary>
/// Базовый класс помощника плагина.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public class WsPluginBaseHelper : IDisposable
{
    #region Public and private fields and properties

    /// <summary>
    /// Тип плагина.
    /// </summary>
    protected WsEnumPluginType PluginType { get; init; }
    protected WsPluginModel ReopenItem { get; }
    protected WsPluginModel RequestItem { get; }
    protected WsPluginModel ResponseItem { get; }

    #endregion

    #region Constructor and destructor

    protected WsPluginBaseHelper()
    {
        PluginType = WsEnumPluginType.Default;
        ReopenItem = new() { Config = new(waitExecute: 0_250) };
        RequestItem = new() { Config = new(waitExecute: 0_250) };
        ResponseItem = new() { Config = new(waitExecute: 0_250) };
    }

    #endregion

    #region Public and private methods
    
    
    public override string ToString() => $"{PluginType} | {ReopenItem} | {RequestItem} | {ResponseItem}";
    
    public virtual void Dispose()
    {
    }

    public virtual void Execute()
    {
    }
    
    #endregion
}