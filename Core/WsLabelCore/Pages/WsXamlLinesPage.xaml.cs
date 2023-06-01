// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

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
    public void SetupViewModel(IWsViewModel viewModel)
    {
        if (viewModel is not WsXamlLinesViewModel linesViewModel) return;
        base.SetupViewModel(linesViewModel, gridLocal);

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            //viewModel.Area = LabelSession.Area;
            //viewModel.Line = LabelSession.Line;

            // Площадки.
            comboBoxArea.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding(nameof(linesViewModel.Areas)) { Mode = BindingMode.OneWay, Source = linesViewModel });
            comboBoxArea.SetBinding(Selector.SelectedItemProperty,
                new Binding(nameof(linesViewModel.Area)) { Mode = BindingMode.TwoWay, Source = linesViewModel });
            comboBoxArea.SetBinding(Selector.SelectedValueProperty,
                new Binding(nameof(linesViewModel.Area.Name))
                {
                    Mode = BindingMode.OneWay,
                    Source = linesViewModel.Area
                });
            comboBoxArea.DisplayMemberPath = nameof(linesViewModel.Area.Name);
            comboBoxArea.SelectedValuePath = nameof(linesViewModel.Area.Name);
            labelArea.SetBinding(ContentProperty,
                new Binding(nameof(LocaleCore.Table.Area)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });

            // Линии.
            comboBoxLine.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding(nameof(linesViewModel.Lines)) { Mode = BindingMode.OneWay, Source = linesViewModel });
            comboBoxLine.SetBinding(Selector.SelectedItemProperty,
                new Binding(nameof(linesViewModel.Line)) { Mode = BindingMode.TwoWay, Source = linesViewModel });
            comboBoxLine.SetBinding(Selector.SelectedValueProperty,
                new Binding($"{nameof(linesViewModel.Line.NumberWithDescription)}")
                {
                    Mode = BindingMode.OneWay,
                    Source = linesViewModel
                });
            comboBoxLine.DisplayMemberPath = nameof(linesViewModel.Line.NumberWithDescription);
            comboBoxLine.SelectedValuePath = nameof(linesViewModel.Line.NumberWithDescription);
            labelLine.SetBinding(ContentProperty,
                new Binding(nameof(LocaleCore.Table.Line)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });

            // Настроить список кнопок.
            SetupListButtons(gridLocal, 2, 0, 1, 2);
        });
    }

    #endregion
}