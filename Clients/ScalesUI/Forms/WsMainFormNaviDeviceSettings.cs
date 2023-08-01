// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Forms;

public partial class WsMainForm
{
    #region Public and private methods - Настройки устройства

    private void ActionSwitchDeviceSettings(object sender, EventArgs e)
    {
        // Загрузить WinForms-контрол настроек устройства.
        LoadNavigationDeviceSettings();
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Обновить кэш.
            ContextCache.Load(WsSqlEnumTableName.DeviceSettings);
            ContextCache.Load(WsSqlEnumTableName.DeviceSettingsFks);
            // Навигация в контрол линии.
            WsFormNavigationUtils.NavigateToExistsDeviceSettings(ShowFormUserControl);
        });
    }

    /// <summary>
    /// Загрузить WinForms-контрол настроек устройства.
    /// </summary>
    private void LoadNavigationDeviceSettings()
    {
        if (WsFormNavigationUtils.IsLoadDeviceSettings) return;
        WsFormNavigationUtils.IsLoadDeviceSettings = true;

        WsFormNavigationUtils.DeviceSettingsUserControl.SetupUserControl();
        WsFormNavigationUtils.DeviceSettingsUserControl.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DeviceSettingsUserControl.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.DeviceSettingsUserControl.ViewModel.CmdYes.AddAction(ReturnOkFromDeviceSettings);
        WsFormNavigationUtils.DeviceSettingsUserControl.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DeviceSettingsUserControl.ViewModel.CmdYes.AddAction(ActionFinally);
    }

    /// <summary>
    /// Возврат ОК из контрола настроек устройства.
    /// </summary>
    private void ReturnOkFromDeviceSettings()
    {
        // Настроить сессию для ПО `Печать этикеток`.
        //LabelSession.SetSessionForLabelPrint(ShowFormUserControl,
        //    WsFormNavigationUtils.DeviceSettingsUserControl.ViewModel.Line.IdentityValueId,
        //    WsFormNavigationUtils.DeviceSettingsUserControl.ViewModel.Area);
    }

    #endregion
}