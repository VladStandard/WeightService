// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Controls;

namespace WsLabelCore.Pages;

#nullable enable
public class WsBasePage : System.Windows.Controls.UserControl
{
    #region Public and private methods

    /// <summary>
    /// Настроить кнопки.
    /// </summary>
    protected void SetupButtons(WsViewModelBase viewModel, ItemsControl itemsControl)
    {
        // Список кнопок.
        itemsControl.SetBinding(ItemsControl.ItemsSourceProperty,
            new Binding(nameof(viewModel.Commands)) { Mode = BindingMode.OneWay, Source = viewModel });
        // Настрить itemsControl.
        DataTemplate itemTemplate = new();
        FrameworkElementFactory buttonFactory = new(typeof(System.Windows.Controls.Button));
        buttonFactory.SetValue(MarginProperty, new Thickness(2));
        buttonFactory.SetValue(FontWeightProperty, FontWeights.Bold);
        buttonFactory.SetValue(FontSizeProperty, viewModel.FontSizeButton);
        buttonFactory.AddHandler(KeyUpEvent, new System.Windows.Input.KeyEventHandler(viewModel.Button_KeyUp));
        buttonFactory.SetBinding(WidthProperty,
            new Binding(nameof(viewModel.ButtonWidth)) { Mode = BindingMode.OneWay, Source = viewModel });
        buttonFactory.SetBinding(ButtonBase.CommandProperty, new Binding(nameof(WsActionCommandModel.Cmd)));
        buttonFactory.SetBinding(ContentProperty, new Binding(nameof(WsActionCommandModel.Content)));
        itemTemplate.VisualTree = buttonFactory;
        itemsControl.ItemTemplate = itemTemplate;
        // Добавить stackPanel.
        FrameworkElementFactory stackPanelFactory = new(typeof(StackPanel));
        stackPanelFactory.SetValue(StackPanel.OrientationProperty, System.Windows.Controls.Orientation.Horizontal);
        stackPanelFactory.SetValue(HorizontalAlignmentProperty, System.Windows.HorizontalAlignment.Center);
        ItemsPanelTemplate itemsPanelTemplate = new(stackPanelFactory);
        itemsControl.ItemsPanel = itemsPanelTemplate;
    }

    //protected void SetItemFromList<T>(ComboBox comboBox, List<T> list, T item) where T : WsSqlTableBase, new()
    //{
    //    int i = 0;
    //    foreach (T it in list)
    //    {
    //        switch (item.IsIdentityUid)
    //        {
    //            case true:
    //                if (Equals(item.IdentityValueUid, it.IdentityValueUid))
    //                {
    //                    comboBox.SelectedIndex = i;
    //                    return;
    //                }
    //                break;
    //            default:
    //                if (Equals(item.IdentityValueId, it.IdentityValueId))
    //                {
    //                    comboBox.SelectedIndex = i;
    //                    return;
    //                }
    //                break;
    //        }
    //        i++;
    //    }
    //    if (comboBox.SelectedIndex == -1)
    //        comboBox.SelectedIndex = 0;
    //}

    //protected void SetItemViewFromList<T>(ComboBox comboBox, List<T> list, T item) where T : WsSqlViewBase, new()
    //{
    //    int i = 0;
    //    foreach (T it in list)
    //    {
    //        switch (item.Identity.IsUid)
    //        {
    //            case true:
    //                if (Equals(item.Identity.Uid, it.Identity.Uid))
    //                {
    //                    comboBox.SelectedIndex = i;
    //                    return;
    //                }
    //                break;
    //            default:
    //                if (Equals(item.Identity.Id, it.Identity.Id))
    //                {
    //                    comboBox.SelectedIndex = i;
    //                    return;
    //                }
    //                break;
    //        }
    //        i++;
    //    }
    //    if (comboBox.SelectedIndex == -1)
    //        comboBox.SelectedIndex = 0;
    //}

    #endregion
}