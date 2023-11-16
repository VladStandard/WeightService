using System.Windows.Controls;

namespace Ws.LabelCore.Pages;

/// <summary>
/// Страница смены вложенности ПЛУ.
/// </summary>
[DebuggerDisplay("{ToString()}")]
#nullable enable
public partial class XamlPlusNestingPage
{
    #region Public and private fields, properties, constructor

    public XamlPlusNestingPage()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public void SetupViewModel(XamlPlusNestingViewModel viewModel)
    {
        SetupViewModel(viewModel, gridLocal);

        FormNavigationUtils.ActionTryCatch(() =>
        {
            // ПЛУ.
            labelPlu.SetBinding(ContentProperty,
                new Binding(nameof(LocaleCore.Table.Plu)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });
            labelPluValue.DataContext = viewModel;
            labelPluValue.SetBinding(ContentProperty,
                new Binding(nameof(viewModel.PluNesting.PluNumberName)) { Mode = BindingMode.OneWay, Source = viewModel.PluNesting });

            // Вложенности ПЛУ.
            labelPluNesting.SetBinding(ContentProperty,
                new Binding(nameof(LocaleCore.Table.PluNesting)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });
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
                new Binding(nameof(LocaleCore.Table.WeightTare)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });
            labelTareWeightValue.DataContext = viewModel;
            labelTareWeightValue.SetBinding(ContentProperty,
                new Binding(nameof(viewModel.PluNesting.TareWeightWithKg)) { Mode = BindingMode.OneWay, Source = viewModel.PluNesting });

            // Настроить список кнопок.
            SetupListButtons(gridLocal, 5, 0, 1, 2);
        });
    }

    #endregion
}