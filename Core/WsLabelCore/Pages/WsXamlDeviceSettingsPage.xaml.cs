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
        SetupViewModel(viewModel, gridLocal);
        if (ViewModel is WsXamlDeviceSettingsViewModel deviceSettingsViewModel) 
            ViewModelCast = deviceSettingsViewModel;

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // Устройства.
            labelDeviceName.SetBinding(ContentProperty,
                new Binding(nameof(WsLocaleCore.Table.Devices)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });
            comboBoxDevice.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding(nameof(viewModel.Devices)) { Mode = BindingMode.OneWay, Source = viewModel });
            comboBoxDevice.SetBinding(Selector.SelectedItemProperty,
                new Binding(nameof(viewModel.Device)) { Mode = BindingMode.TwoWay, Source = viewModel });
            comboBoxDevice.SetBinding(Selector.SelectedValueProperty,
                new Binding($"{nameof(viewModel.Device.Name)}")
                {
                    Mode = BindingMode.OneWay,
                    Source = viewModel.Device
                });
            comboBoxDevice.DisplayMemberPath = nameof(viewModel.Device.Name);
            comboBoxDevice.SelectedValuePath = nameof(viewModel.Device.Name);

            // Таблица "Настройки".
            dataGridSettings.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding(nameof(viewModel.DeviceSettingsFks)) { Mode = BindingMode.OneWay, Source = viewModel });
            // Колонка "Настройка".
            headerSetting.SetBinding(ContentProperty,
                new Binding(nameof(WsLocaleCore.Table.Setting)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });
            // Колонка "Включено".
            headerIsEnabled.SetBinding(ContentProperty,
                new Binding(nameof(WsLocaleCore.Table.IsEnabled)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });

            // Настроить список кнопок.
            SetupListButtons(gridLocal, 2, 0, 1, 2);
        });
    }

    #endregion
}