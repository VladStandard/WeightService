// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

using System.Windows.Controls;

namespace WsLabelCore.Pages;
#nullable enable
/// <summary>
/// Interaction logic for WsPlusNestingPage.xaml
/// </summary>
public partial class WsPlusNestingPage //: INavigableView<WsPlusNestingViewModel>
{
    #region Public and private fields, properties, constructor

    private WsPlusNestingViewModel CastViewModel { get; }

    public WsPlusNestingPage(WsBaseViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        if (viewModel is not WsPlusNestingViewModel pluNestingViewModel) return;
        CastViewModel = pluNestingViewModel;

        // Вложенности ПЛУ.
        labelPluNesting.SetBinding(ContentProperty,
            new Binding(nameof(LocaleCore.Table.PluNesting)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });
        comboBoxPlusNesting.SetBinding(ItemsControl.ItemsSourceProperty, 
            new Binding(nameof(CastViewModel.PlusNestings)) { Mode = BindingMode.OneWay, Source = CastViewModel });
        comboBoxPlusNesting.SetBinding(Selector.SelectedItemProperty,
            new Binding(nameof(CastViewModel.PluNesting)) { Mode = BindingMode.TwoWay, Source = CastViewModel });
        comboBoxPlusNesting.SetBinding(Selector.SelectedValueProperty, 
            new Binding(nameof(CastViewModel.PluNesting.TareWeightDescription)) { Mode = BindingMode.OneWay, Source = CastViewModel.PluNesting });
        comboBoxPlusNesting.DisplayMemberPath = nameof(CastViewModel.PluNesting.TareWeightDescription);
        comboBoxPlusNesting.SelectedValuePath = nameof(CastViewModel.PluNesting.TareWeightDescription);

        // ПЛУ.
        labelPlu.SetBinding(ContentProperty,
            new Binding(nameof(LocaleCore.Table.Plu)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });
        labelPluValue.DataContext = CastViewModel;
        labelPluValue.SetBinding(ContentProperty, 
            new Binding(nameof(CastViewModel.PluNesting.PluName)) { Mode = BindingMode.OneWay, Source = CastViewModel.PluNesting });

        // Вес тары ПЛУ.
        labelTareWeight.SetBinding(ContentProperty,
            new Binding(nameof(LocaleCore.Table.WeightTare)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });
        labelTareWeightValue.DataContext = CastViewModel;
        labelTareWeightValue.SetBinding(ContentProperty,
            new Binding(nameof(CastViewModel.PluNesting.TareWeightWithKg)) { Mode = BindingMode.OneWay, Source = CastViewModel.PluNesting });

        // Настроить кнопки.
        SetupButtons(CastViewModel);
    }

    #endregion
}