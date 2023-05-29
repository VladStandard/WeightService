// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MvvmHelpers;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace WsLabelCore.Controls;

/// <summary>
/// Контрол смены вложенности ПЛУ.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsPlusNestingUserControl : WsBaseUserControl
{
    #region Public and private fields, properties, constructor

    public WsPlusNestingViewModel CastViewModel => (WsPlusNestingViewModel)ViewModel;

    public WsPlusNestingUserControl() : base(new WsPlusNestingViewModel())
    {
        InitializeComponent();
        
        RefreshViewModel();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => CastViewModel.ToString();

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public override void RefreshViewModel()
    {
        WsWinFormNavigationUtils.ActionTryCatchSimple(() =>
        {
            CastViewModel.PluNesting = LabelSession.ViewPluNesting;
            CastViewModel.PlusNestings = ContextManager.ContextView.GetListViewPlusNesting((ushort)LabelSession.PluLine.Plu.Number);
        });
    }

    #endregion
}