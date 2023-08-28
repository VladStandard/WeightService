namespace WsLabelCore.Pages;

/// <summary>
/// Страница пин-кода.
/// </summary>
[DebuggerDisplay("{ToString()}")]
#nullable enable
public partial class WsXamlDigitsPage
{
    #region Public and private fields, properties, constructor

    public WsXamlDigitsPage()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public void SetupViewModel(WsXamlDigitsViewModel viewModel)
    {
        SetupViewModel(viewModel, gridLocal);

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // Очистить.
            labelClear.SetBinding(ContentProperty,
                new Binding(nameof(WsLocaleCore.Buttons.Clear)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Buttons });
            // Вввод.
            labelEnter.SetBinding(ContentProperty,
                new Binding(nameof(WsLocaleCore.Buttons.Enter)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Buttons });
            // Настроить список кнопок.
            SetupListButtons(gridLocal, 1, 0);
        });
    }

    #endregion
}