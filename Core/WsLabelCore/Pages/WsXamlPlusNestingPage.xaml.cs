// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

using System.Windows.Controls;

namespace WsLabelCore.Pages;
#nullable enable
/// <summary>
/// Interaction logic for WsPlusNestingPage.xaml
/// </summary>
public partial class WsPlusNestingPage : WsXamlBasePage//: INavigableView<WsPlusNestingViewModel>
{
    #region Public and private fields, properties, constructor

    public WsPlusNestingPage(WsXamlBaseViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        //borderMain.Child = GridMain;

        if (ViewModel is not WsPlusNestingViewModel plusNestingViewModel) return;
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
        labelPluValue.DataContext = ViewModel;
        labelPluValue.SetBinding(ContentProperty,
            new Binding(nameof(plusNestingViewModel.PluNesting.PluName))
            {
                Mode = BindingMode.OneWay,
                Source = plusNestingViewModel.PluNesting
            });

        // Вес тары ПЛУ.
        labelTareWeight.SetBinding(ContentProperty,
            new Binding(nameof(LocaleCore.Table.WeightTare)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });
        labelTareWeightValue.DataContext = ViewModel;
        labelTareWeightValue.SetBinding(ContentProperty,
            new Binding(nameof(plusNestingViewModel.PluNesting.TareWeightWithKg))
            {
                Mode = BindingMode.OneWay,
                Source = plusNestingViewModel.PluNesting
            });
    }

    #endregion
}