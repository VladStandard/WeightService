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
}