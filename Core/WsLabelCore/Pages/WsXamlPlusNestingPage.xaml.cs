// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

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
    public void SetupViewModel(IWsXamlViewModel viewModel)
    {
        if (viewModel is not WsXamlPlusNestingViewModel plusNestingViewModel) return;
        base.SetupViewModel(plusNestingViewModel, gridLocal);

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // Вложенности ПЛУ.
            labelPluNesting.SetBinding(ContentProperty,
                new Binding(nameof(LocaleCore.Table.PluNesting)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });
            comboBoxPlusNesting.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding(nameof(plusNestingViewModel.PlusNestings)) { Mode = BindingMode.OneWay, Source = plusNestingViewModel });
            comboBoxPlusNesting.SetBinding(Selector.SelectedItemProperty,
                new Binding(nameof(plusNestingViewModel.PluNesting)) { Mode = BindingMode.TwoWay, Source = plusNestingViewModel });
            comboBoxPlusNesting.SetBinding(Selector.SelectedValueProperty,
                new Binding(nameof(plusNestingViewModel.PluNesting.TareWeightDescription))
                {
                    Mode = BindingMode.OneWay,
                    Source = plusNestingViewModel.PluNesting
                });
            comboBoxPlusNesting.DisplayMemberPath = nameof(plusNestingViewModel.PluNesting.TareWeightDescription);
            comboBoxPlusNesting.SelectedValuePath = nameof(plusNestingViewModel.PluNesting.TareWeightDescription);

            // ПЛУ.
            labelPlu.SetBinding(ContentProperty,
                new Binding(nameof(LocaleCore.Table.Plu)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });
            labelPluValue.DataContext = plusNestingViewModel;
            labelPluValue.SetBinding(ContentProperty,
                new Binding(nameof(plusNestingViewModel.PluNesting.PluName))
                {
                    Mode = BindingMode.OneWay,
                    Source = plusNestingViewModel.PluNesting
                });

            // Вес тары ПЛУ.
            labelTareWeight.SetBinding(ContentProperty,
                new Binding(nameof(LocaleCore.Table.WeightTare)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });
            labelTareWeightValue.DataContext = plusNestingViewModel;
            labelTareWeightValue.SetBinding(ContentProperty,
                new Binding(nameof(plusNestingViewModel.PluNesting.TareWeightWithKg))
                {
                    Mode = BindingMode.OneWay,
                    Source = plusNestingViewModel.PluNesting
                });

            // Настроить список кнопок.
            SetupListButtons(gridLocal, 3, 0, 1, 2);
        });
    }

    #endregion
}