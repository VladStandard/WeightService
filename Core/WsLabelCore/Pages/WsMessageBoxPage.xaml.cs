// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

using System.Windows.Controls;

namespace WsLabelCore.Pages;

/// <summary>
/// Interaction logic for WsMessageBoxPage.xaml
/// </summary>
public partial class WsMessageBoxPage : INavigableView<WsMessageBoxViewModel>
{
    #region Public and private fields, properties, constructor

    public WsMessageBoxViewModel ViewModel { get; }

    public WsMessageBoxPage(WsMessageBoxViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        
        // Сообщение.
        fieldMessage.SetBinding(TextBlock.TextProperty,
            new Binding(nameof(ViewModel.Message)) { Mode = BindingMode.OneWay, Source = ViewModel });
        fieldMessage.SetBinding(VisibilityProperty,
            new Binding(nameof(ViewModel.MessageVisibility)) { Mode = BindingMode.OneWay, Source = ViewModel });
        fieldMessage.FontSize = ViewModel.FontSizeMessage;

        // Список кнопок.
        itemsControl.SetBinding(ItemsControl.ItemsSourceProperty,
            new Binding(nameof(ViewModel.Commands)) { Mode = BindingMode.OneWay, Source = ViewModel });
        // Настрить itemsControl.
        DataTemplate itemTemplate = new();
        FrameworkElementFactory buttonFactory = new(typeof(System.Windows.Controls.Button));
        buttonFactory.SetValue(MarginProperty, new Thickness(2));
        buttonFactory.SetValue(FontWeightProperty, FontWeights.Bold);
        buttonFactory.SetValue(FontSizeProperty, ViewModel.FontSizeButton);
        buttonFactory.AddHandler(KeyUpEvent, new System.Windows.Input.KeyEventHandler(Button_KeyUp));
        //buttonFactory.SetBinding(WidthProperty,
        //new Binding($"{nameof(ViewModel.ButtonWidthPercent)}") { Mode = BindingMode.OneWay, Source = ViewModel });
        buttonFactory.SetBinding(WidthProperty,
            new Binding(nameof(this.Width)) { Mode = BindingMode.OneWay, Source = this }); // Auto
        buttonFactory.SetBinding(ButtonBase.CommandProperty, new Binding(nameof(WsActionCommandModel.Cmd)));
        //buttonFactory.SetBinding(ContentProperty, new Binding(nameof(WsActionCommandModel.Content)));
        buttonFactory.SetBinding(ContentProperty,
            new Binding(nameof(this.Width)) { Mode = BindingMode.OneWay, Source = this });
        itemTemplate.VisualTree = buttonFactory;
        itemsControl.ItemTemplate = itemTemplate;
        // Добавить stackPanel.
        FrameworkElementFactory stackPanelFactory = new(typeof(StackPanel));
        stackPanelFactory.SetValue(StackPanel.OrientationProperty, System.Windows.Controls.Orientation.Horizontal);
        stackPanelFactory.SetValue(HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
        //stackPanelFactory.SetBinding(WidthProperty,
        //    new Binding(nameof(itemsControl.Width)) { Mode = BindingMode.OneWay, Source = itemsControl }); // Auto
        ItemsPanelTemplate itemsPanelTemplate = new(stackPanelFactory);
        itemsControl.ItemsPanel = itemsPanelTemplate;
    }

    #endregion

    #region Public and private methods

    private void Button_KeyUp(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Escape:
                ViewModel.ActionCancel.Relay();
                break;
            case Key.Enter:
                ViewModel.ActionOk.Relay();
                break;
        }
    }

    #endregion
}