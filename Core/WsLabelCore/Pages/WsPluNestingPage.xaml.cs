// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Data;

namespace WsLabelCore.Pages;

/// <summary>
/// Interaction logic for WsPluNestingPage.xaml
/// </summary>
public partial class WsPluNestingPage : WsBasePage, INavigableView<WsPluNestingViewModel>
{
    #region Public and private fields, properties, constructor
    
    public WsPluNestingViewModel ViewModel { get; }

    public WsPluNestingPage(WsPluNestingViewModel viewModel)
    {
		InitializeComponent();
        DataContext = ViewModel = viewModel;

        // Линия.
        System.Windows.Data.Binding bindingPlusNestings = new(nameof(ViewModel.PlusNestings)) { Mode = BindingMode.OneWay, Source = ViewModel };
        comboBoxPluNesting.SetBinding(ItemsControl.ItemsSourceProperty, bindingPlusNestings);
        System.Windows.Data.Binding bindingPluNesting = new(nameof(ViewModel.PluNesting)) { Mode = BindingMode.TwoWay, Source = ViewModel };
        comboBoxPluNesting.SetBinding(Selector.SelectedItemProperty, bindingPluNesting);
        System.Windows.Data.Binding bindingPluNestingValue = new(nameof(ViewModel.PluNesting.PrettyName)) { Mode = BindingMode.OneWay, Source = ViewModel };
        comboBoxPluNesting.SetBinding(Selector.SelectedValueProperty, bindingPluNestingValue);
    }

    #endregion

    #region Public and private methods

    private void ButtonApply_OnClick(object sender, RoutedEventArgs e)
	{
        // Didn't work! Go here: MainForm -> ActionSwitchPluNesting
        ViewModel.ActionReturnOk();
    }

	private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
	{
        // Didn't work! Go here: MainForm -> ActionSwitchPluNesting
        ViewModel.ActionReturnCancel();
}

	#endregion
}