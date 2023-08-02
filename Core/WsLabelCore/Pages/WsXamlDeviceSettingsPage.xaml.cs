// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Controls;

namespace WsLabelCore.Pages;

/// <summary>
/// Страница настроек устройства.
/// </summary>
[DebuggerDisplay("{ToString()}")]
#nullable enable
public partial class WsXamlDeviceSettingsPage
{
    #region Public and private fields, properties, constructor

    public WsXamlDeviceSettingsViewModel ViewModelCast { get; private set; } = new();

    public WsXamlDeviceSettingsPage()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public void SetupViewModel(WsXamlDeviceSettingsViewModel viewModel)
    {
        ViewModelCast = viewModel;
        SetupViewModel(ViewModelCast, gridLocal);

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // Устройства.
            comboBoxDevice.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding(nameof(ViewModelCast.Devices)) { Mode = BindingMode.OneWay, Source = ViewModelCast });
            comboBoxDevice.SetBinding(Selector.SelectedItemProperty,
                new Binding(nameof(ViewModelCast.Device)) { Mode = BindingMode.TwoWay, Source = ViewModelCast });
            comboBoxDevice.SetBinding(Selector.SelectedValueProperty,
                new Binding($"{nameof(ViewModelCast.Device.Name)}")
                {
                    Mode = BindingMode.OneWay,
                    Source = ViewModelCast.Device
                });
            comboBoxDevice.DisplayMemberPath = nameof(ViewModelCast.Device.Name);
            comboBoxDevice.SelectedValuePath = nameof(ViewModelCast.Device.Name);

            // Таблица "Настройки".
            dataGridSettings.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding(nameof(ViewModelCast.DeviceSettingsFks)) { Mode = BindingMode.OneWay, Source = ViewModelCast });
            // Колонка "Настройка".
            //headerSetting.SetBinding(ContentProperty,
            //    new Binding(nameof(WsLocaleCore.Table.Setting)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });
            // Колонка "Включено".
            //headerIsEnabled.SetBinding(ContentProperty,
            //    new Binding(nameof(WsLocaleCore.Table.IsEnabled)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });

            // Настроить список кнопок.
            SetupListButtons(gridLocal, 2, 0, 1, 2);
        });
    }

    private void valueIsEnabled_Checked(object sender, RoutedEventArgs e)
    {
        //if (!ViewModel.IsLoaded) return;
        if (sender is not CheckBox checkBox) return;
        if (checkBox.DataContext is not WsSqlDeviceSettingsFkModel deviceSettingsFk) return;
        if (deviceSettingsFk.IsEnabled) return;
        deviceSettingsFk.IsEnabled = true;
        WsSqlContextManagerHelper.Instance.DeviceSettingsFksRepository.UpdateItemAsync(deviceSettingsFk);
    }

    private void valueIsEnabled_Unchecked(object sender, RoutedEventArgs e)
    {
        //if (!ViewModel.IsLoaded) return;
        if (sender is not CheckBox checkBox) return;
        if (checkBox.DataContext is not WsSqlDeviceSettingsFkModel deviceSettingsFk) return;
        if (!deviceSettingsFk.IsEnabled) return;
        deviceSettingsFk.IsEnabled = false;
        WsSqlContextManagerHelper.Instance.DeviceSettingsFksRepository.UpdateItemAsync(deviceSettingsFk);
    }

    #endregion

}