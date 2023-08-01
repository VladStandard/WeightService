// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Controls;

namespace WsLabelCore.Pages;

/// <summary>
/// Страница смены вложенности ПЛУ.
/// </summary>
[DebuggerDisplay("{ToString()}")]
#nullable enable
public partial class WsXamlPlusNestingPage
{
    #region Public and private fields, properties, constructor

    public WsXamlPlusNestingPage()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    /// <param name="viewModel"></param>
    public void SetupViewModel(WsXamlPlusNestingViewModel viewModel)
    {
        SetupViewModel(viewModel, gridLocal);

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // ПЛУ.
            labelPlu.SetBinding(ContentProperty,
                new Binding(nameof(WsLocaleCore.Table.Plu)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });
            labelPluValue.DataContext = viewModel;
            labelPluValue.SetBinding(ContentProperty,
                new Binding(nameof(viewModel.PluNesting.PluNumberName)) { Mode = BindingMode.OneWay, Source = viewModel.PluNesting });

            // Вложенности ПЛУ.
            labelPluNesting.SetBinding(ContentProperty,
                new Binding(nameof(WsLocaleCore.Table.PluNesting)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });
            comboBoxPlusNesting.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding(nameof(viewModel.PlusNestings)) { Mode = BindingMode.OneWay, Source = viewModel });
            comboBoxPlusNesting.SetBinding(Selector.SelectedItemProperty,
                new Binding(nameof(viewModel.PluNesting)) { Mode = BindingMode.TwoWay, Source = viewModel });
            comboBoxPlusNesting.SetBinding(Selector.SelectedValueProperty,
                new Binding(nameof(viewModel.PluNesting.TareWeightDescription)) { Mode = BindingMode.OneWay, Source = viewModel.PluNesting });
            comboBoxPlusNesting.DisplayMemberPath = nameof(viewModel.PluNesting.TareWeightDescription);
            comboBoxPlusNesting.SelectedValuePath = nameof(viewModel.PluNesting.TareWeightDescription);

            // Вес тары ПЛУ.
            labelTareWeight.SetBinding(ContentProperty,
                new Binding(nameof(WsLocaleCore.Table.WeightTare)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });
            labelTareWeightValue.DataContext = viewModel;
            labelTareWeightValue.SetBinding(ContentProperty,
                new Binding(nameof(viewModel.PluNesting.TareWeightWithKg)) { Mode = BindingMode.OneWay, Source = viewModel.PluNesting });

            // Обмен между 1С и веб-сервисом.
            labelIsEnabled.SetBinding(ContentProperty,
                new Binding(nameof(WsLocaleCore.Table.IsEnabled)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });
            checkBoxIsEnabled.DataContext = viewModel;
            checkBoxIsEnabled.SetBinding(ContentProperty,
                new Binding(nameof(WsLocaleCore.Table.WebServiceExchange)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });
            checkBoxIsEnabled.SetBinding(ToggleButton.IsCheckedProperty,
                new Binding(nameof(viewModel.PluNesting.WebServiceIsEnabled)) { Mode = BindingMode.OneWay, Source = viewModel.PluNesting });

            // 1C.
            label1C.SetBinding(ContentProperty,
                new Binding(nameof(WsLocaleCore.Table.ImportFrom1C)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });
            textBox1CValue.DataContext = viewModel;
            textBox1CValue.SetBinding(TextBox.TextProperty,
                new Binding(nameof(viewModel.PluNesting.WebServiceXml)) { Mode = BindingMode.OneWay, Source = viewModel.PluNesting });

            // Настроить список кнопок.
            SetupListButtons(gridLocal, 5, 0, 1, 2);
        });
    }

    #endregion
}