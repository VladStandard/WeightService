using Ws.LabelCore.Utils;
using Ws.LabelCore.ViewModels;
namespace Ws.LabelCore.Pages;

/// <summary>
/// Страница пин-кода.
/// </summary>
[DebuggerDisplay("{ToString()}")]
#nullable enable
public partial class XamlDigitsPage
{
    #region Public and private fields, properties, constructor

    public XamlDigitsPage()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public void SetupViewModel(XamlDigitsViewModel viewModel)
    {
        SetupViewModel(viewModel, gridLocal);

        FormNavigationUtils.ActionTryCatch(() =>
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