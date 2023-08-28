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
            //ContextCache.Load(WsSqlEnumTableName.DeviceSettings);
            //ContextCache.Load(WsSqlEnumTableName.DeviceSettingsFks);
            // Навигация в контрол линии.
            WsFormNavigationUtils.NavigateToExistsDeviceSettings(ShowFormUserControl);
        });
    }

    /// <summary>
    /// Загрузить WinForms-контрол настроек устройства.
    /// </summary>
    private void LoadNavigationDeviceSettings()
    {
        if (WsFormNavigationUtils.IsLoadDeviceSettings)
            return;
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
    // TODO: подумать об универсальном алгоритме для всех настроек
    private void ReturnOkFromDeviceSettings()
    {
        _ = Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            
            // Настроить устройство ПО `Печать этикеток`.
            //LabelSession.SetSessionForLabelPrint();
        }).ConfigureAwait(false);
        
        //this.SwitchResolution(Debug.IsDevelop ? WsEnumScreenResolution.Value1366x768 : WsEnumScreenResolution.FullScreen);
        bool isFormFullScreen = true;
        foreach (WsSqlDeviceSettingsFkModel deviceSettingsFk in ContextManager.DeviceSettingsFksRepository.GetEnumerableByLine(LabelSession.Line))
        {
            switch (deviceSettingsFk.Setting.Name)
            {
                // Отображать кнопку максимизации.
                case "IsShowMaximizeButton":
                    MaximizeBox = deviceSettingsFk.IsEnabled;
                    FormBorderStyle = deviceSettingsFk.IsEnabled ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
                    if (deviceSettingsFk.IsEnabled)
                        isFormFullScreen = false;
                    break;
                // Отображать кнопку минимизации.
                case "IsShowMinimizeButton":
                    MinimizeBox = deviceSettingsFk.IsEnabled;
                    FormBorderStyle = deviceSettingsFk.IsEnabled ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
                    if (deviceSettingsFk.IsEnabled)
                        isFormFullScreen = false;
                    break;
                // Отображать кнопку печати.
                case "IsShowPrintButton":
                    ButtonPrint.Visible = deviceSettingsFk.IsEnabled;
                    break;
            }
        }
        if (isFormFullScreen)
            this.SwitchResolution(WsEnumScreenResolution.FullScreen);
    }

    #endregion
}
