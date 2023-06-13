// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

namespace ScalesUI.Forms;

public partial class WsMainForm
{
    #region Public and private methods - Смена линии

    /// <summary>
    /// Сменить линию.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionSwitchLine(object sender, EventArgs e)
    {
        // Загрузить WinForms-контрол смены линии.
        LoadNavigationLine();
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.Areas);
            ContextCache.Load(WsSqlEnumTableName.Lines);
            // Навигация в контрол линии.
            WsFormNavigationUtils.NavigateToExistsLines(ShowFormUserControl);
        });
    }

    /// <summary>
    /// Загрузить WinForms-контрол смены линии.
    /// </summary>
    private void LoadNavigationLine()
    {
        if (WsFormNavigationUtils.IsLoadLines) return;
        WsFormNavigationUtils.IsLoadLines = true;

        WsFormNavigationUtils.LinesUserControl.SetupUserConrol();
        WsFormNavigationUtils.LinesUserControl.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.LinesUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.LinesUserControl.ViewModel.CmdYes.AddAction(ReturnOkFromLines);
        WsFormNavigationUtils.LinesUserControl.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.LinesUserControl.ViewModel.CmdYes.AddAction(ActionFinally);
    }

    /// <summary>
    /// Возврат ОК из контрола смены линии.
    /// </summary>
    private void ReturnOkFromLines()
    {
        LabelSession.SetSessionForLabelPrint(ShowFormUserControl,
            WsFormNavigationUtils.LinesUserControl.ViewModel.Line.IdentityValueId,
            WsFormNavigationUtils.LinesUserControl.ViewModel.Area);
    }

    #endregion
}