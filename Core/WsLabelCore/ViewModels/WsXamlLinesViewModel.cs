// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления линий.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlLinesViewModel : WsXamlBaseViewModel, IWsViewModel
{
    #region Public and private fields, properties, constructor

    public WsSqlProductionFacilityModel Area { get; set; } = new();
    public WsSqlScaleModel Line { get; set; } = new();
    public List<WsSqlProductionFacilityModel> Areas { get; set; }
    public List<WsSqlScaleModel> Lines { get; set; }

    public WsXamlLinesViewModel()
    {
        FormUserControl = WsEnumNavigationPage.Line;
        Areas = ContextCache.Areas;
        Lines = ContextCache.Lines;
    }

    //public WsLinesViewModel(WsActionCommandModel cmdAbort, WsActionCommandModel cmdCancel, WsActionCommandModel cmdCustom, WsActionCommandModel cmdIgnore,
    //    WsActionCommandModel cmdNo, WsActionCommandModel cmdOk, WsActionCommandModel cmdRetry, WsActionCommandModel cmdYes) :
    //    base(cmdAbort, cmdCancel, cmdCustom, cmdIgnore, cmdNo, cmdOk, cmdRetry, cmdYes)
    //{
    //    Areas = ContextCache.Areas;
    //    Lines = ContextCache.Lines;
    //}

    //public WsLinesViewModel(Action actionAbort, Action actionCancel, Action actionCustom, Action actionIgnore,
    //    Action actionNo, Action actionOk, Action actionRetry, Action actionYes) :
    //    base(actionAbort, actionCancel, actionCustom, actionIgnore, actionNo, actionOk, actionRetry, actionYes)
    //{
    //    Areas = ContextCache.Areas;
    //    Lines = ContextCache.Lines;
    //}

    #endregion
}