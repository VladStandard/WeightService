// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

using System.Windows.Controls;

namespace WsLabelCore.Pages;

/// <summary>
/// Interaction logic for WsMessageBoxPage.xaml
/// </summary>
public partial class WsMessageBoxPage : INavigableView<WsMessageBoxViewModel>
{
    #region Public and private fields, properties, constructor

    public WsMessageBoxViewModel ViewModel { get; }

    public WsMessageBoxPage(WsMessageBoxViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        
        // Сообщение.
        fieldMessage.SetBinding(TextBlock.TextProperty,
            new Binding(nameof(ViewModel.Message)) { Mode = BindingMode.OneWay, Source = ViewModel });
        fieldMessage.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.MessageVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel });
        fieldMessage.FontSize = ViewModel.FontSizeMessage;

        // Кнопка Abort.
        buttonAbort.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ActionAbort.Content)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionAbort });
        buttonAbort.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ActionAbort.Visibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionAbort });
        buttonAbort.FontSize = ViewModel.FontSizeButton;
        buttonAbort.SetBinding(ButtonBase.CommandProperty,
            new Binding($"{nameof(ViewModel.RelayAbort)}Command") { Mode = BindingMode.OneWay, Source = ViewModel });

        // Кнопка Cancel.
        buttonCancel.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ActionCancel.Content)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionCancel });
        buttonCancel.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ActionCancel.Visibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionCancel });
        buttonCancel.FontSize = ViewModel.FontSizeButton;
        buttonCancel.SetBinding(ButtonBase.CommandProperty,
            new Binding($"{nameof(ViewModel.RelayCancel)}Command") { Mode = BindingMode.OneWay, Source = ViewModel });

        // Кнопка Custom.
        buttonCustom.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ActionCustom.Content)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionCustom });
        buttonCustom.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ActionCustom.Visibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionCustom });
        buttonCustom.FontSize = ViewModel.FontSizeButton;
        buttonCustom.SetBinding(ButtonBase.CommandProperty,
            new Binding($"{nameof(ViewModel.RelayCustom)}Command") { Mode = BindingMode.OneWay, Source = ViewModel });

        // Кнопка Ignore.
        buttonIgnore.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ActionIgnore.Content)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionIgnore });
        buttonIgnore.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ActionIgnore.Visibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionIgnore });
        buttonIgnore.FontSize = ViewModel.FontSizeButton;
        buttonIgnore.SetBinding(ButtonBase.CommandProperty,
            new Binding($"{nameof(ViewModel.RelayIgnore)}Command") { Mode = BindingMode.OneWay, Source = ViewModel });

        // Кнопка No.
        buttonNo.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ActionNo.Content)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionNo });
        buttonNo.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ActionNo.Visibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionNo });
        buttonNo.FontSize = ViewModel.FontSizeButton;
        buttonNo.SetBinding(ButtonBase.CommandProperty,
            new Binding($"{nameof(ViewModel.RelayNo)}Command") { Mode = BindingMode.OneWay, Source = ViewModel });

        // Кнопка Ok.
        buttonOk.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ActionOk.Content)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionOk });
        buttonOk.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ActionOk.Visibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionOk });
        buttonOk.FontSize = ViewModel.FontSizeButton;
        buttonOk.SetBinding(ButtonBase.CommandProperty,
            new Binding($"{nameof(ViewModel.RelayOk)}Command") { Mode = BindingMode.OneWay, Source = ViewModel });

        // Кнопка Retry.
        buttonRetry.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ActionRetry.Content)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionRetry });
        buttonRetry.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ActionRetry.Visibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionRetry });
        buttonRetry.FontSize = ViewModel.FontSizeButton;
        buttonRetry.SetBinding(ButtonBase.CommandProperty,
            new Binding($"{nameof(ViewModel.RelayRetry)}Command") { Mode = BindingMode.OneWay, Source = ViewModel });

        // Кнопка Yes.
        buttonYes.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ActionYes.Content)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionYes });
        buttonYes.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ActionYes.Visibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ActionYes });
        buttonYes.FontSize = ViewModel.FontSizeButton;
        buttonYes.SetBinding(ButtonBase.CommandProperty,
            new Binding($"{nameof(ViewModel.RelayYes)}Command") { Mode = BindingMode.OneWay, Source = ViewModel });
    }

    #endregion

    #region Public and private methods

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        //FocusManager.SetIsFocusScope(gridMain, true);
        //foreach (object child in gridMain.Children)
        //{
        //    if (child is Button button)
        //    {
        //        button.Focusable = true;
        //        Keyboard.Focus(button);
        //        FocusManager.SetFocusedElement(gridMain, button);
        //    }
        //}
    }

    #endregion

    #region Public and private methods - Actions

    private void Button_KeyUp(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Escape:
                ViewModel.RelayCancel();
                break;
            case Key.Enter:
                ViewModel.RelayOk();
                break;
        }
    }

    #endregion
}