// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

namespace WsLabelCore.Pages;

/// <summary>
/// Interaction logic for WsPinCodePage.xaml
/// </summary>
public partial class WsPinCodePage //: INavigableView<WsPinCodeViewModel>
{
    #region Public and private fields, properties, constructor

    private WsPinCodeViewModel CastViewModel { get; }

    public WsPinCodePage(WsBaseViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        if (viewModel is not WsPinCodeViewModel pinCodeViewModel) return;
        CastViewModel = pinCodeViewModel;

        // Очистить.
        labelClear.SetBinding(ContentProperty,
            new Binding(nameof(LocaleCore.Buttons.Clear)) { Mode = BindingMode.OneWay, Source = LocaleCore.Buttons });
        // Вввод.
        labelEnter.SetBinding(ContentProperty,
            new Binding(nameof(LocaleCore.Buttons.Enter)) { Mode = BindingMode.OneWay, Source = LocaleCore.Buttons });
    }

    #endregion
}