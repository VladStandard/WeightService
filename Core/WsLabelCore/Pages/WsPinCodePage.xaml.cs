// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

namespace WsLabelCore.Pages;

/// <summary>
/// Interaction logic for WsPinCodePage.xaml
/// </summary>
public partial class WsPinCodePage : INavigableView<WsPinCodeViewModel>
{
    #region Public and private fields, properties, constructor

    public WsPinCodeViewModel ViewModel { get; }

    public WsPinCodePage(WsPinCodeViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
    }

    #endregion

    #region Private methods

    private void PagePin_OnLoaded(object sender, RoutedEventArgs e)
    {
        ButtonClear_Click(sender, e);
    }

    private void ButtonNum_Click(object sender, EventArgs e)
    {
        //
    }

    private void ButtonClear_Click(object sender, RoutedEventArgs e)
    {
        //
    }

    private void ButtonEnter_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnOk();
    }

    #endregion
}