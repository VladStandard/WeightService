using System.Windows.Controls;

namespace Ws.LabelCore.Pages;

/// <summary>
/// Страница смены линии.
/// </summary>
[DebuggerDisplay("{ToString()}")]
#nullable enable
public partial class XamlLinesPage
{
    #region Public and private fields, properties, constructor

    public XamlLinesPage()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public void SetupViewModel(XamlLinesViewModel viewModel)
    {
        SetupViewModel(viewModel, gridLocal);

        FormNavigationUtils.ActionTryCatch(() =>
        {
            // Площадки.
            comboBoxArea.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding(nameof(viewModel.ProductionSites)) { Mode = BindingMode.OneWay, Source = viewModel });
            comboBoxArea.SetBinding(Selector.SelectedItemProperty,
                new Binding(nameof(viewModel.ProductionSite)) { Mode = BindingMode.TwoWay, Source = viewModel });
            comboBoxArea.SetBinding(Selector.SelectedValueProperty,
                new Binding(nameof(viewModel.ProductionSite.Name))
                {
                    Mode = BindingMode.OneWay,
                    Source = viewModel.ProductionSite
                });
            comboBoxArea.DisplayMemberPath = nameof(viewModel.ProductionSite.Name);
            comboBoxArea.SelectedValuePath = nameof(viewModel.ProductionSite.Name);
            labelArea.SetBinding(ContentProperty,
                new Binding(nameof(LocaleCore.Table.Area)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });

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
                new Binding(nameof(LocaleCore.Table.Line)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });

            // Настроить список кнопок.
            SetupListButtons(gridLocal, 2, 0, 1, 2);
        });
    }

    #endregion
}