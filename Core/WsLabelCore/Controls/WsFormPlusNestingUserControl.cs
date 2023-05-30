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
public sealed partial class WsFormPlusNestingUserControl : WsFormBaseUserControl
{
    #region Public and private fields, properties, constructor

    public WsFormPlusNestingUserControl() : base(new WsPlusNestingViewModel())
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => Page.ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public override void RefreshUserConrol()
    {
        base.RefreshUserConrol();
        WsFormNavigationUtils.ActionTryCatchSimple(() =>
        {
            ((WsPlusNestingViewModel)Page.ViewModel).PluNesting = LabelSession.ViewPluNesting;
            ((WsPlusNestingViewModel)Page.ViewModel).PlusNestings = ContextManager.ContextView.GetListViewPlusNesting((ushort)LabelSession.PluLine.Plu.Number);
        });
    }

    #endregion
}