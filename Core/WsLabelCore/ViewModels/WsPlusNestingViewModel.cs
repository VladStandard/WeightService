// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

#nullable enable
public sealed class WsPluNestingViewModel : WsWpfBaseViewModel
{
    #region Public and private fields, properties, constructor

    public WsSqlViewPluNestingModel PluNesting { get; set; }
    public List<WsSqlViewPluNestingModel> PlusNestings { get; set; }

    public WsPluNestingViewModel()
    {
        PluNesting = new();
        PlusNestings = ContextCache.LocalViewPlusNesting;
    }

    #endregion
}