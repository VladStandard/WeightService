using System.Windows.Controls;

namespace WsLabelCore.Common;

/// <summary>
/// Базовый класс XAML-страницы.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public class WsXamlBasePage : UserControl//, IWsXamlPage
{
    #region Public and private fields, properties, constructor

    public WsXamlBaseViewModel ViewModel { get; private set; }
    private ItemsControl ItemsControlMain { get; set; }
    private FrameworkElementFactory ButtonFactoryMain { get; set; }
    private FrameworkElementFactory StackPanelFactory { get; set; }
    private ScrollViewer ScrollViewer { get; }

    public WsXamlBasePage()
    {
        ItemsControlMain = new() { Margin = new(2) };
        ViewModel = new();
        ButtonFactoryMain = new(typeof(Button));
        StackPanelFactory = new(typeof(StackPanel));
        ScrollViewer = new() { VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public void SetupViewModel(IWsViewModel viewModel, Grid? grid = null)
    {
        if (viewModel is not WsXamlBaseViewModel baseViewModel) return;
        // Задать команды.
        viewModel.UpdateCommandsFromActions();
        ObservableCollection<WsActionCommandModel> commands = ViewModel.Commands;
        ViewModel = baseViewModel;
        if (commands.Any())
            viewModel.SetCommands(commands);

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // Настроить прокрутку.
            SetupScrollViewer(grid);
            // Настроить сообщение.
            SetupTextBlockMessage();
        });
    }

    private void SetupScrollViewer(Grid? grid)
    {
        if (ViewModel.MessageVisibility.Equals(Visibility.Hidden)) return;
        if (grid is not null && grid.Children.Contains(ScrollViewer))
        {
            grid.Children.Remove(ScrollViewer);
        }
        Grid.SetRow(ScrollViewer, 0);
        Grid.SetColumn(ScrollViewer, 0);
        grid?.Children.Add(ScrollViewer);
    }

    /// <summary>
    /// Настроить сообщение.
    /// </summary>
    /// <returns></returns>
    private void SetupTextBlockMessage()
    {
        if (ViewModel.MessageVisibility.Equals(Visibility.Hidden)) return;
        TextBlock textBlockMessage = new()
        {
            Margin = new(2),
            FontStretch = FontStretches.Expanded,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
            TextWrapping = TextWrapping.Wrap,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };
        textBlockMessage.SetBinding(TextBlock.TextProperty,
            new Binding(nameof(ViewModel.Message)) { Mode = BindingMode.OneWay, Source = ViewModel });
        textBlockMessage.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.MessageVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel });
        textBlockMessage.FontSize = ViewModel.FontSizeMessage;
        ScrollViewer.Content = textBlockMessage;
    }

    /// <summary>
    /// Настроить список кнопок.
    /// </summary>
    protected void SetupListButtons(Grid grid, int row, int column, int rowSpan = 1, int columnSpan = 1)
    {
        // Проверки и пересоздание.
        if (grid.Children.Contains(ItemsControlMain))
        {
            grid.Children.Remove(ItemsControlMain);
            ItemsControlMain = new() { Margin = new(5) };
            ButtonFactoryMain = new(typeof(Button));
            StackPanelFactory = new(typeof(StackPanel));
        }

        // Привязки.
        ItemsControlMain.SetBinding(ItemsControl.ItemsSourceProperty,
            new Binding(nameof(ViewModel.CommandsSmart)) { Mode = BindingMode.OneWay, Source = ViewModel });
        ButtonFactoryMain.SetBinding(WidthProperty,
            new Binding(nameof(ViewModel.ButtonWidth)) { Mode = BindingMode.OneWay, Source = ViewModel });
        ButtonFactoryMain.SetBinding(ButtonBase.CommandProperty, new Binding(nameof(WsActionCommandModel.Cmd)));
        ButtonFactoryMain.SetBinding(ContentProperty, new Binding(nameof(WsActionCommandModel.Content)));
        // Конвертер типов Visibility/WsEnumVisibility.
        ButtonFactoryMain.SetBinding(VisibilityProperty, 
            new Binding(nameof(WsActionCommandModel.Visibility)) { Converter = new WsXamlVisibilityConverter() });

        Grid.SetRow(ItemsControlMain, row);
        Grid.SetColumn(ItemsControlMain, column);
        Grid.SetRowSpan(ItemsControlMain, rowSpan);
        Grid.SetColumnSpan(ItemsControlMain, columnSpan);
        grid.Children.Add(ItemsControlMain);

        // Настроить ItemsControlMain.
        DataTemplate itemTemplate = new();
        ButtonFactoryMain.SetValue(MarginProperty, new Thickness(2));
        ButtonFactoryMain.SetValue(FontWeightProperty, FontWeights.Bold);
        ButtonFactoryMain.SetValue(FontSizeProperty, ViewModel.FontSizeButton);
        ButtonFactoryMain.AddHandler(KeyUpEvent, new KeyEventHandler(ViewModel.Button_KeyUp));
        itemTemplate.VisualTree = ButtonFactoryMain;
        ItemsControlMain.ItemTemplate = itemTemplate;
        // Добавить stackPanel.
        StackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
        StackPanelFactory.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
        ItemsPanelTemplate itemsPanelTemplate = new(StackPanelFactory);
        ItemsControlMain.ItemsPanel = itemsPanelTemplate;
    }

    #endregion
}
