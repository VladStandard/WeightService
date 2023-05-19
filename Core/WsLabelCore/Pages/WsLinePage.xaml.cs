// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

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
        ViewModel = viewModel;

        //DataContext = ViewModel;
        //SetItemFromList(comboBoxArea, ViewModel.Areas, ViewModel.Area);
        //SetItemFromList(comboBoxLine, ViewModel.Lines, ViewModel.Line);
        // d:DataContext="{d:DesignInstance pages:WsLinePage}"
        //comboBoxArea.DataContext = $"{{DynamicResource ViewModel}}";
        //comboBoxArea.ItemsSource = $"{{Binding ViewModel.Areas}}";
        //comboBoxArea.SelectedItem = $"{{Binding ViewModel.Area}}";
        //comboBoxArea.SelectedValue = $"{{Binding ViewModel.Area.Name}}";

        //comboBoxLine.DataContext = $"{{DynamicResource {nameof(ViewModel)}}}";
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