// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

using System.Windows.Controls;

namespace WsLabelCore.Pages;
#nullable enable
/// <summary>
/// Interaction logic for WsPluNestingPage.xaml
/// </summary>
public partial class WsPluNestingPage : INavigableView<WsPluNestingViewModel>
{
    #region Public and private fields, properties, constructor
    
    public WsPluNestingViewModel ViewModel { get; }

    public WsPluNestingPage(WsPluNestingViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        
        // Вложенности ПЛУ.
        comboBoxPlusNesting.SetBinding(ItemsControl.ItemsSourceProperty, 
            new Binding(nameof(ViewModel.PlusNestings)) { Mode = BindingMode.OneWay, Source = ViewModel });
        comboBoxPlusNesting.SetBinding(Selector.SelectedItemProperty,
            new Binding(nameof(ViewModel.PluNesting)) { Mode = BindingMode.TwoWay, Source = ViewModel });
        comboBoxPlusNesting.SetBinding(Selector.SelectedValueProperty, 
            new Binding(nameof(ViewModel.PluNesting.TareWeightDescription)) { Mode = BindingMode.OneWay, Source = ViewModel.PluNesting });
        comboBoxPlusNesting.DisplayMemberPath = nameof(ViewModel.PluNesting.TareWeightDescription);
        comboBoxPlusNesting.SelectedValuePath = nameof(ViewModel.PluNesting.TareWeightDescription);

        // ПЛУ.
        labelPlu.DataContext = ViewModel;
        labelPlu.SetBinding(ContentProperty, 
            new Binding(nameof(ViewModel.PluNesting.PluName)) { Mode = BindingMode.OneWay, Source = ViewModel.PluNesting });

        // Вес тары ПЛУ.
        labelTareWeight.DataContext = ViewModel;
        labelTareWeight.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.PluNesting.TareWeightWithKg)) { Mode = BindingMode.OneWay, Source = ViewModel.PluNesting });
    }

    #endregion

    #region Public and private methods

    private void ButtonApply_OnClick(object sender, RoutedEventArgs e)
    {
        // Didn't work! Go here: MainForm -> ActionSwitchPluNesting
        ViewModel.RelayOk();
    }

    private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
    {
        // Didn't work! Go here: MainForm -> ActionSwitchPluNesting
        ViewModel.RelayCancel();
    }

    #endregion
}