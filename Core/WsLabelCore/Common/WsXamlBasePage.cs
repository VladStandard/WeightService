// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Controls;

namespace WsLabelCore.Common;

/// <summary>
/// Базовый класс Controls.UserControl.
/// </summary>
#nullable enable
public class WsXamlBasePage : UserControl
{
    #region Public and private fields, properties, constructor

    internal WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    public WsXamlBaseViewModel ViewModel { get; protected set; }
    protected Grid GridMain { get; }
    private ItemsControl ItemsControlMain { get; }

    public WsXamlBasePage(WsXamlBaseViewModel viewModel)
    {
        ViewModel = viewModel;

        // Таблица.
        GridMain = new() { Margin = new(2) };
        GridMain.ColumnDefinitions.Add(new() { Width = new(1, GridUnitType.Star) });
        GridMain.RowDefinitions.Add(new() { Height = new(5, GridUnitType.Star) });
        GridMain.RowDefinitions.Add(new() { Height = new(1, GridUnitType.Star) });
        // ScrollViewer.
        ScrollViewer scrollViewer = new() { VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        Grid.SetRow(scrollViewer, 0);
        Grid.SetColumn(scrollViewer, 0);
        GridMain.Children.Add(scrollViewer);
        // Сообщение.
        TextBlock textBlockMessage = new() { Margin = new(2), FontStretch = FontStretches.Expanded,
            FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap, 
            HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center,
        };
        scrollViewer.Content = textBlockMessage;
        textBlockMessage.SetBinding(TextBlock.TextProperty,
            new Binding(nameof(ViewModel.Message)) { Mode = BindingMode.OneWay, Source = ViewModel });
        textBlockMessage.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.MessageVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel });
        textBlockMessage.FontSize = ViewModel.FontSizeMessage;

        // Список кнопок.
        ItemsControlMain = new() { Margin = new(2) };
        Grid.SetRow(ItemsControlMain, 1);
        Grid.SetColumn(ItemsControlMain, 0);
        GridMain.Children.Add(ItemsControlMain);
        ItemsControlMain.SetBinding(ItemsControl.ItemsSourceProperty,
            new Binding(nameof(ViewModel.Commands)) { Mode = BindingMode.OneWay, Source = ViewModel });

        // Настрить itemsControl.
        DataTemplate itemTemplate = new();
        FrameworkElementFactory buttonFactory = new(typeof(Button));
        buttonFactory.SetValue(MarginProperty, new Thickness(2));
        buttonFactory.SetValue(FontWeightProperty, FontWeights.Bold);
        buttonFactory.SetValue(FontSizeProperty, ViewModel.FontSizeButton);
        buttonFactory.AddHandler(KeyUpEvent, new KeyEventHandler(ViewModel.Button_KeyUp));
        buttonFactory.SetBinding(WidthProperty,
            new Binding(nameof(ViewModel.ButtonWidth)) { Mode = BindingMode.OneWay, Source = ViewModel });
        buttonFactory.SetBinding(ButtonBase.CommandProperty, new Binding(nameof(WsActionCommandModel.Cmd)));
        buttonFactory.SetBinding(ContentProperty, new Binding(nameof(WsActionCommandModel.Content)));
        itemTemplate.VisualTree = buttonFactory;
        ItemsControlMain.ItemTemplate = itemTemplate;
        // Добавить stackPanel.
        FrameworkElementFactory stackPanelFactory = new(typeof(StackPanel));
        stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
        stackPanelFactory.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
        ItemsPanelTemplate itemsPanelTemplate = new(stackPanelFactory);
        ItemsControlMain.ItemsPanel = itemsPanelTemplate;
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public virtual void RefreshViewModel()
    {
        //
    }

    #endregion
}
