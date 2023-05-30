// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления вложенности ПЛУ.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsPlusNestingViewModel : WsXamlBaseViewModel, INotifyPropertyChanged
{
    #region Public and private fields, properties, constructor

    public WsSqlViewPluNestingModel PluNesting { get; set; }
    public List<WsSqlViewPluNestingModel> PlusNestings { get; set; }

    public WsPlusNestingViewModel()
    {
        PluNesting = new();
        PlusNestings = ContextCache.ViewPlusNesting;
    }

    #endregion
}