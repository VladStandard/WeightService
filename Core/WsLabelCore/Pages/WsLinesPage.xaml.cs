// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WsLabelCore.Pages;

/// <summary>
/// Interaction logic for WsLinesPage.xaml
/// </summary>
public partial class WsLinesPage : INavigableView<WsLinesViewModel>
{
    #region Public and private fields, properties, constructor

    public WsLinesViewModel ViewModel { get; }

    public WsLinesPage(WsLinesViewModel viewModel)
    {
        InitializeComponent();
        DataContext = ViewModel = viewModel;
        // Площадки.
        comboBoxArea.SetBinding(ItemsControl.ItemsSourceProperty,
            new Binding(nameof(ViewModel.Areas)) { Mode = BindingMode.OneWay, Source = ViewModel });
        comboBoxArea.SetBinding(Selector.SelectedItemProperty,
            new Binding(nameof(ViewModel.Area)) { Mode = BindingMode.TwoWay, Source = ViewModel });
        comboBoxArea.SetBinding(Selector.SelectedValueProperty,
            new Binding(nameof(ViewModel.Area.Name)) { Mode = BindingMode.OneWay, Source = ViewModel.Area });
        comboBoxArea.DisplayMemberPath = nameof(ViewModel.Area.Name);
        comboBoxArea.SelectedValuePath = nameof(ViewModel.Area.Name);
        // Линии.
        comboBoxLine.SetBinding(ItemsControl.ItemsSourceProperty, 
            new Binding(nameof(ViewModel.Lines)) { Mode = BindingMode.OneWay, Source = ViewModel });
        comboBoxLine.SetBinding(Selector.SelectedItemProperty,
            new Binding(nameof(ViewModel.Line)) { Mode = BindingMode.TwoWay, Source = ViewModel });
        comboBoxLine.SetBinding(Selector.SelectedValueProperty,
            new Binding($"{nameof(ViewModel.Line.NumberWithDescription)}") { Mode = BindingMode.OneWay, Source = ViewModel.Line });
        comboBoxLine.DisplayMemberPath = nameof(ViewModel.Line.NumberWithDescription);
        comboBoxLine.SelectedValuePath = nameof(ViewModel.Line.NumberWithDescription);
    }

    #endregion

    private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ReturnOk();
    }

    private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ReturnCancel();
    }
}