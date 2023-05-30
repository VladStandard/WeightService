// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

namespace WsLabelCore.Pages;

/// <summary>
/// Interaction logic for WsPinCodePage.xaml
/// </summary>
#nullable enable
public partial class WsPinCodePage : WsXamlBasePage
{
    #region Public and private fields, properties, constructor

    public WsPinCodePage(WsXamlBaseViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        //borderMain.Child = GridMain;

        // Очистить.
        labelClear.SetBinding(ContentProperty,
            new Binding(nameof(LocaleCore.Buttons.Clear)) { Mode = BindingMode.OneWay, Source = LocaleCore.Buttons });
        // Вввод.
        labelEnter.SetBinding(ContentProperty,
            new Binding(nameof(LocaleCore.Buttons.Enter)) { Mode = BindingMode.OneWay, Source = LocaleCore.Buttons });
    }

    #endregion
}