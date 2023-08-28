using System.Windows.Controls;

namespace WsLabelCore.Pages;

/// <summary>
/// Страница смены линии.
/// </summary>
[DebuggerDisplay("{ToString()}")]
#nullable enable
public partial class WsXamlLinesPage
{
    #region Public and private fields, properties, constructor

    public WsXamlLinesPage()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public void SetupViewModel(WsXamlLinesViewModel viewModel)
    {
        SetupViewModel(viewModel, gridLocal);

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // Площадки.
            comboBoxArea.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding(nameof(viewModel.Areas)) { Mode = BindingMode.OneWay, Source = viewModel });
            comboBoxArea.SetBinding(Selector.SelectedItemProperty,
                new Binding(nameof(viewModel.Area)) { Mode = BindingMode.TwoWay, Source = viewModel });
            comboBoxArea.SetBinding(Selector.SelectedValueProperty,
                new Binding(nameof(viewModel.Area.Name))
                {
                    Mode = BindingMode.OneWay,
                    Source = viewModel.Area
                });
            comboBoxArea.DisplayMemberPath = nameof(viewModel.Area.Name);
            comboBoxArea.SelectedValuePath = nameof(viewModel.Area.Name);
            labelArea.SetBinding(ContentProperty,
                new Binding(nameof(WsLocaleCore.Table.Area)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });

            // Линии.
            comboBoxLine.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding(nameof(viewModel.Lines)) { Mode = BindingMode.OneWay, Source = viewModel });
            comboBoxLine.SetBinding(Selector.SelectedItemProperty,
                new Binding(nameof(viewModel.Line)) { Mode = BindingMode.TwoWay, Source = viewModel });
            comboBoxLine.SetBinding(Selector.SelectedValueProperty,
                new Binding($"{nameof(viewModel.Line.NumberWithDescription)}")
                {
                    Mode = BindingMode.OneWay,
                    Source = viewModel.Line
                });
            comboBoxLine.DisplayMemberPath = nameof(viewModel.Line.NumberWithDescription);
            comboBoxLine.SelectedValuePath = nameof(viewModel.Line.NumberWithDescription);
            labelLine.SetBinding(ContentProperty,
                new Binding(nameof(WsLocaleCore.Table.Line)) { Mode = BindingMode.OneWay, Source = WsLocaleCore.Table });

            // Настроить список кнопок.
            SetupListButtons(gridLocal, 2, 0, 1, 2);
        });
    }

    #endregion
}