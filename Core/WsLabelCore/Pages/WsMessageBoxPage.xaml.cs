// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

using System.Windows.Controls;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Controls.Button;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

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
        
        // Заголовок.
        //fieldCaption.SetBinding(TextBlock.TextProperty,
        //    new Binding(nameof(ViewModel.Caption)) { Mode = BindingMode.OneWay, Source = ViewModel });
        //fieldCaption.SetBinding(VisibilityProperty,
        //    new Binding(nameof(ViewModel.CaptionVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel });
        //fieldCaption.FontSize = ViewModel.FontSizeCaption;
        // Сообщение.
        fieldMessage.SetBinding(TextBlock.TextProperty,
            new Binding(nameof(ViewModel.Message)) { Mode = BindingMode.OneWay, Source = ViewModel });
        fieldMessage.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.MessageVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel });
        fieldMessage.FontSize = ViewModel.FontSizeMessage;

        // Кнопка Custom.
        buttonCustom.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonCustomContent)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonCustom.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonCustomVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonCustom.FontSize = ViewModel.FontSizeButton;
        // Кнопка Yes.
        buttonYes.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonYesContent)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonYes.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonYesVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonYes.FontSize = ViewModel.FontSizeButton;
        // Кнопка Retry.
        buttonRetry.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonRetryContent)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonRetry.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonRetryVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonRetry.FontSize = ViewModel.FontSizeButton;
        // Кнопка No.
        buttonNo.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonNoContent)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonNo.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonNoVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonNo.FontSize = ViewModel.FontSizeButton;
        // Кнопка Ignore.
        buttonIgnore.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonIgnoreContent)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonIgnore.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonIgnoreVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonIgnore.FontSize = ViewModel.FontSizeButton;
        // Кнопка Cancel.
        buttonCancel.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonCancelContent)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonCancel.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonCancelVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonCancel.FontSize = ViewModel.FontSizeButton;
        // Кнопка Abort.
        buttonAbort.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonAbortContent)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonAbort.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonAbortVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonAbort.FontSize = ViewModel.FontSizeButton;
        // Кнопка Ok.
        buttonOk.SetBinding(ContentProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonOkContent)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonOk.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.ButtonVisibility.ButtonOkVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel.ButtonVisibility });
        buttonOk.FontSize = ViewModel.FontSizeButton;
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

    private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnOk();
    }

    private void ButtonYes_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnOk();
    }

    private void ButtonCustom_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnCancel();
    }

    private void ButtonRetry_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnCancel();
    }

    private void ButtonNo_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnCancel();
    }

    private void ButtonIgnore_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnCancel();
    }

    private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnCancel();
    }

    private void ButtonAbort_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnCancel();
    }

    private void Button_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key.Equals(Key.Escape)) 
            ViewModel.ActionReturnCancel();
    }

    #endregion
}