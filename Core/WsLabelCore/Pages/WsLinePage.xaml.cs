// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace WsLabelCore.Pages;

/// <summary>
/// Interaction logic for WsLinePage.xaml
/// </summary>
public partial class WsLinePage : WsBasePage, INavigableView<WsLineViewModel>
{
    #region Public and private fields, properties, constructor

    public WsLineViewModel ViewModel { get; }

    public WsLinePage(WsLineViewModel viewModel)
    {
        InitializeComponent();
        DataContext = ViewModel = viewModel;

        //// New binding object using the path of 'Name' for whatever source object is used
        //System.Windows.Data.Binding nameBindingObject = new("Area.Name");
        //// Configure the binding
        //nameBindingObject.Mode = BindingMode.OneWay;
        //nameBindingObject.Source = ViewModel;
        ////nameBindingObject.Converter = NameConverter.Instance;
        ////nameBindingObject.ConverterCulture = new CultureInfo("en-US");
        //// Set the binding to a target object. The TextBlock.Name property on the NameBlock UI element
        //BindingOperations.SetBinding(labelArea, ContentProperty, nameBindingObject);

        // Площадка.
        System.Windows.Data.Binding bindingAreas = new(nameof(ViewModel.Areas)) { Mode = BindingMode.OneWay, Source = ViewModel };
        comboBoxArea.SetBinding(ItemsControl.ItemsSourceProperty, bindingAreas);
        System.Windows.Data.Binding bindingArea = new(nameof(ViewModel.Area)) { Mode = BindingMode.TwoWay, Source = ViewModel };
        comboBoxArea.SetBinding(Selector.SelectedItemProperty, bindingArea);
        System.Windows.Data.Binding bindingAreaValue = new(nameof(ViewModel.Area.Name)) { Mode = BindingMode.OneWay, Source = ViewModel };
        comboBoxArea.SetBinding(Selector.SelectedValueProperty, bindingAreaValue);
        
        // Линия.
        System.Windows.Data.Binding bindingLines = new(nameof(ViewModel.Lines)) { Mode = BindingMode.OneWay, Source = ViewModel };
        comboBoxLine.SetBinding(ItemsControl.ItemsSourceProperty, bindingLines);
        System.Windows.Data.Binding bindingLine = new(nameof(ViewModel.Line)) { Mode = BindingMode.TwoWay, Source = ViewModel };
        comboBoxLine.SetBinding(Selector.SelectedItemProperty, bindingLine);
        System.Windows.Data.Binding bindingLineValue = new(nameof(ViewModel.Line.NumberWithDescription)) { Mode = BindingMode.OneWay, Source = ViewModel };
        comboBoxLine.SetBinding(Selector.SelectedValueProperty, bindingLineValue);
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