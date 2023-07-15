// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.ViewRefModels.PluNestings;

namespace WsLabelCore.ViewModels;

/// <summary>
/// Модель представления вложенности ПЛУ.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsXamlPlusNestingViewModel : WsXamlBaseViewModel, IWsViewModel
{
    #region Public and private fields, properties, constructor

    public WsSqlViewPluNestingModel PluNesting { get; set; } = new();
    public List<WsSqlViewPluNestingModel> PlusNestings { get; set; } = new();

    public WsXamlPlusNestingViewModel()
    {
        FormUserControl = WsEnumNavigationPage.PlusNesting;
    }

    #endregion
}