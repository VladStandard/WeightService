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
    public void SetupViewModel(WsXamlLinesViewModel viewModel)
    {
        SetupViewModel(viewModel, gridLocal);
        
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // Устройство.
            //labelLine.SetBinding(ContentProperty,
            //    new Binding(nameof(WsLocaleCore.Table.Line)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });
            labelLineValue.SetBinding(ContentProperty,
                new Binding(nameof(viewModel.Device)) { Mode = BindingMode.OneWay, Source = viewModel });

            // Линии.
            comboBoxLine.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding(nameof(viewModel.Lines)) { Mode = BindingMode.OneWay, Source = viewModel });
            comboBoxLine.SetBinding(Selector.SelectedItemProperty,
                new Binding(nameof(viewModel.Line)) { Mode = BindingMode.TwoWay, Source = viewModel });
            comboBoxLine.SetBinding(Selector.SelectedValueProperty,
                new Binding($"{nameof(viewModel.Line.NumberWithDescription)}")
                {
                    Mode = BindingMode.OneWay,
                    Source = viewModel.Line
                });
            comboBoxLine.DisplayMemberPath = nameof(viewModel.Line.NumberWithDescription);
            comboBoxLine.SelectedValuePath = nameof(viewModel.Line.NumberWithDescription);

            // Настроить список кнопок.
            SetupListButtons(gridLocal, 2, 0, 1, 2);
        });
    }

    #endregion
}