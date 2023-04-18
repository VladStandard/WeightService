// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.WinForms.Models;

public class PluginHelperBase : HelperBase
{
    #region Public and private fields and properties

    protected PluginModel ReopenItem { get; }
    protected PluginModel RequestItem { get; }
    protected PluginModel ResponseItem { get; }
    protected TaskType TskType { get; set; }
    protected int ReopenCounter => ReopenItem.Counter;
    protected int RequestCounter => RequestItem.Counter;
    protected int ResponseCounter => ResponseItem.Counter;

    #endregion

    #region Constructor and destructor

    protected PluginHelperBase()
    {
        TskType = TaskType.Default;
        ReopenItem = new() { Config = new(waitExecute: 0_250, waitClose: 0_250) };
        RequestItem = new() { Config = new(waitExecute: 0_250, waitClose: 0_250) };
        ResponseItem = new() { Config = new(waitExecute: 0_250, waitClose: 0_250) };
    }

    #endregion
}