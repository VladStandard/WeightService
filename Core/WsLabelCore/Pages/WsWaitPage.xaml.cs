// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

using System.Windows.Controls;

namespace WsLabelCore.Pages;

/// <summary>
/// Interaction logic for WsWaitPage.xaml
/// </summary>
public partial class WsWaitPage : INavigableView<WsMessageBoxViewModel>
{
    #region Public and private fields, properties, constructor

    public WsMessageBoxViewModel ViewModel { get; }

    public WsWaitPage(WsMessageBoxViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        
        // Сообщение.
        fieldMessage.SetBinding(TextBlock.TextProperty,
            new Binding(nameof(ViewModel.Message)) { Mode = BindingMode.OneWay, Source = ViewModel });
        fieldMessage.FontSize = ViewModel.FontSizeMessage;

        // Настроить кнопки.
        SetupButtons(ViewModel, itemsControl);
    }

    #endregion
}