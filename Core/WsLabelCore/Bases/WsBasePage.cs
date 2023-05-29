// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Controls;

namespace WsLabelCore.Bases;

/// <summary>
/// Базовый класс Controls.UserControl.
/// </summary>
#nullable enable
public class WsBasePage : UserControl
{
    #region Public and private fields, properties, constructor

    internal WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    public WsBaseViewModel ViewModel { get; }
    private Grid GridMain { get; }

    public WsBasePage(WsBaseViewModel viewModel)
    {
        ViewModel = viewModel;

        // Таблица.
        GridMain = new() { Margin = new(2) };
        GridMain.ColumnDefinitions.Add(new() { Width = new(1, GridUnitType.Star) });
        GridMain.RowDefinitions.Add(new() { Height = new(1, GridUnitType.Star) });
        GridMain.RowDefinitions.Add(new() { Height = new(1, GridUnitType.Star) });
        // ScrollViewer.
        ScrollViewer scrollViewer = new() { VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        Grid.SetRow(scrollViewer, 0);
        Grid.SetColumn(scrollViewer, 0);
        GridMain.Children.Add(scrollViewer);
        // Сообщение.
        TextBlock textBlockMessage = new() { Margin = new(2), FontStretch = FontStretches.Expanded,
            FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center, TextWrapping = TextWrapping.Wrap,
        };
        scrollViewer.Content = textBlockMessage;
        textBlockMessage.SetBinding(TextBlock.TextProperty,
            new Binding(nameof(ViewModel.Message)) { Mode = BindingMode.OneWay, Source = ViewModel });
        textBlockMessage.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.MessageVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel });
        textBlockMessage.FontSize = ViewModel.FontSizeMessage;

        // Настроить кнопки.
        SetupButtons(ViewModel);
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Настроить кнопки.
    /// </summary>
    protected void SetupButtons(WsBaseViewModel viewModel)
    {
        // Список кнопок.
        ItemsControl itemsControl = new() { Margin = new(2) };
        Grid.SetRow(itemsControl, 1);
        Grid.SetColumn(itemsControl, 0);
        GridMain.Children.Add(itemsControl);
        itemsControl.SetBinding(ItemsControl.ItemsSourceProperty,
            new Binding(nameof(viewModel.Commands)) { Mode = BindingMode.OneWay, Source = viewModel });
        // Настрить itemsControl.
        DataTemplate itemTemplate = new();
        FrameworkElementFactory buttonFactory = new(typeof(Button));
        buttonFactory.SetValue(MarginProperty, new Thickness(2));
        buttonFactory.SetValue(FontWeightProperty, FontWeights.Bold);
        buttonFactory.SetValue(FontSizeProperty, viewModel.FontSizeButton);
        buttonFactory.AddHandler(KeyUpEvent, new KeyEventHandler(viewModel.Button_KeyUp));
        buttonFactory.SetBinding(WidthProperty,
            new Binding(nameof(viewModel.ButtonWidth)) { Mode = BindingMode.OneWay, Source = viewModel });
        buttonFactory.SetBinding(ButtonBase.CommandProperty, new Binding(nameof(WsActionCommandModel.Cmd)));
        buttonFactory.SetBinding(ContentProperty, new Binding(nameof(WsActionCommandModel.Content)));
        itemTemplate.VisualTree = buttonFactory;
        itemsControl.ItemTemplate = itemTemplate;
        // Добавить stackPanel.
        FrameworkElementFactory stackPanelFactory = new(typeof(StackPanel));
        stackPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
        stackPanelFactory.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
        ItemsPanelTemplate itemsPanelTemplate = new(stackPanelFactory);
        itemsControl.ItemsPanel = itemsPanelTemplate;
    }

    public virtual void RefreshViewModel() { }

    #endregion
}
