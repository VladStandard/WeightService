// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

namespace WsLabelCore.Pages;

/// <summary>
/// Страница пин-кода.
/// </summary>
[DebuggerDisplay("{ToString()}")]
#nullable enable
public partial class WsXamlPinCodePage
{
    #region Public and private fields, properties, constructor

    public WsXamlPinCodePage()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public void SetupViewModel(IWsViewModel viewModel)
    {
        if (viewModel is not WsXamlPinCodeViewModel pinCodeViewModel) return;
        base.SetupViewModel(pinCodeViewModel, gridLocal);

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // Очистить.
            labelClear.SetBinding(ContentProperty,
                new Binding(nameof(LocaleCore.Buttons.Clear)) { Mode = BindingMode.OneWay, Source = LocaleCore.Buttons });
            // Вввод.
            labelEnter.SetBinding(ContentProperty,
                new Binding(nameof(LocaleCore.Buttons.Enter)) { Mode = BindingMode.OneWay, Source = LocaleCore.Buttons });
            // Настроить список кнопок.
            SetupListButtons(gridLocal, 1, 0);
        });
    }

    #endregion
}