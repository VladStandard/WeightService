using Ws.LabelCore.Models;
namespace Ws.LabelCore.Common;

/// <summary>
/// Базовый класс помощника плагина.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public class PluginBaseHelper : IDisposable
{
    #region Public and private fields and properties

    /// <summary>
    /// Тип плагина.
    /// </summary>
    protected EnumPluginType PluginType { get; set; }
    protected PluginModel ReopenItem { get; }
    protected PluginModel RequestItem { get; }
    protected PluginModel ResponseItem { get; }

    #endregion

    #region Constructor and destructor

    protected PluginBaseHelper()
    {
        PluginType = EnumPluginType.Default;
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