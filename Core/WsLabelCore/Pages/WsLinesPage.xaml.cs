// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

using System.Windows.Controls;

namespace WsLabelCore.Pages;

/// <summary>
/// Interaction logic for WsLinesPage.xaml
/// </summary>
public partial class WsLinesPage //: INavigableView<WsLinesViewModel>
{
    #region Public and private fields, properties, constructor

    private WsLinesViewModel CastViewModel { get; }

    public WsLinesPage(WsBaseViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        if (viewModel is not WsLinesViewModel linesViewModel) return;
        CastViewModel = linesViewModel;
        
        // Площадки.
        comboBoxArea.SetBinding(ItemsControl.ItemsSourceProperty,
            new Binding(nameof(CastViewModel.Areas)) { Mode = BindingMode.OneWay, Source = CastViewModel });
        comboBoxArea.SetBinding(Selector.SelectedItemProperty,
            new Binding(nameof(CastViewModel.Area)) { Mode = BindingMode.TwoWay, Source = CastViewModel });
        comboBoxArea.SetBinding(Selector.SelectedValueProperty,
            new Binding(nameof(CastViewModel.Area.Name)) { Mode = BindingMode.OneWay, Source = CastViewModel.Area });
        comboBoxArea.DisplayMemberPath = nameof(CastViewModel.Area.Name);
        comboBoxArea.SelectedValuePath = nameof(CastViewModel.Area.Name);
        labelArea.SetBinding(ContentProperty,
            new Binding(nameof(LocaleCore.Table.Area)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });

        // Линии.
        comboBoxLine.SetBinding(ItemsControl.ItemsSourceProperty, 
            new Binding(nameof(CastViewModel.Lines)) { Mode = BindingMode.OneWay, Source = CastViewModel });
        comboBoxLine.SetBinding(Selector.SelectedItemProperty,
            new Binding(nameof(CastViewModel.Line)) { Mode = BindingMode.TwoWay, Source = CastViewModel });
        comboBoxLine.SetBinding(Selector.SelectedValueProperty,
            new Binding($"{nameof(CastViewModel.Line.NumberWithDescription)}") { Mode = BindingMode.OneWay, Source = CastViewModel.Line });
        comboBoxLine.DisplayMemberPath = nameof(CastViewModel.Line.NumberWithDescription);
        comboBoxLine.SelectedValuePath = nameof(CastViewModel.Line.NumberWithDescription);
        labelLine.SetBinding(ContentProperty,
            new Binding(nameof(LocaleCore.Table.Line)) { Mode = BindingMode.OneWay, Source = LocaleCore.Table });

        // Настроить кнопки.
        SetupButtons(CastViewModel);
    }

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public override void RefreshViewModel()
    {
        WsWinFormNavigationUtils.ActionTryCatchSimple(() =>
        {
            CastViewModel.Area = LabelSession.Area;
            CastViewModel.Line = LabelSession.Line;
        });
    }

    #endregion
}