// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

using System.Windows.Controls;
using System.Windows.Input;
using Button = System.Windows.Controls.Button;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

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
    }

    #endregion

    #region Public and private methods

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        ushort colCount = GetGridColCount();
        ushort rowCount = GetGridRowCount();
        Grid grid = GetGrid(colCount, rowCount);

        ushort row = 0;
        GetFieldCaption(grid, colCount, ref row);
        GetFieldMessage(grid, colCount, ref row);

        ushort col = 0;
        GetButtonCustom(grid, ref col, row);
        GetButtonYes(grid, ref col, row);
        GetButtonRetry(grid, ref col, row);
        GetButtonNo(grid, ref col, row);
        GetButtonIgnore(grid, ref col, row);
        GetButtonCancel(grid, ref col, row);
        GetButtonAbort(grid, ref col, row);
        GetButtonOk(grid, ref col, row);

        borderMain.Child = grid;
        SetButtonFocus(grid);
    }

    private Grid GetGrid(ushort colCount, ushort rowCount)
    {
        Grid grid = new()
        {
            DataContext = $"{{DynamicResource {nameof(ViewModel)}}}",
            Margin = new(2),
        };
        grid.KeyUp += Button_KeyUp;

        Grid.SetColumn(grid, 0);
        for (ushort col = 0; col < colCount; col++)
        {
            ColumnDefinition column = new() { Width = new(1, GridUnitType.Star) };
            grid.ColumnDefinitions.Add(column);
        }

        Grid.SetRow(grid, 0);
        if (rowCount <= 1)
        {
            RowDefinition row = new() { Height = new(ViewModel.SizeCaption, GridUnitType.Star) };
            grid.RowDefinitions.Add(row);
        }
        else if (rowCount == 2)
        {
            RowDefinition row = new() { Height = new(ViewModel.SizeMessage, GridUnitType.Star) };
            grid.RowDefinitions.Add(row);
            RowDefinition row2 = new() { Height = new(ViewModel.SizeButton, GridUnitType.Star) };
            grid.RowDefinitions.Add(row2);
        }
        else if (rowCount == 3)
        {
            RowDefinition row = new() { Height = new(ViewModel.SizeCaption, GridUnitType.Star) };
            grid.RowDefinitions.Add(row);
            RowDefinition row2 = new() { Height = new(ViewModel.SizeMessage, GridUnitType.Star) };
            grid.RowDefinitions.Add(row2);
            RowDefinition row3 = new() { Height = new(ViewModel.SizeButton, GridUnitType.Star) };
            grid.RowDefinitions.Add(row3);
        }

        FocusManager.SetIsFocusScope(grid, true);
        return grid;
    }

    private ushort GetGridColCount()
    {
        ushort count = 0;
        if (ViewModel.ButtonVisibility.ButtonCustomVisibility == Visibility.Visible)
            count++;
        if (ViewModel.ButtonVisibility.ButtonYesVisibility == Visibility.Visible)
            count++;
        if (ViewModel.ButtonVisibility.ButtonRetryVisibility == Visibility.Visible)
            count++;
        if (ViewModel.ButtonVisibility.ButtonNoVisibility == Visibility.Visible)
            count++;
        if (ViewModel.ButtonVisibility.ButtonIgnoreVisibility == Visibility.Visible)
            count++;
        if (ViewModel.ButtonVisibility.ButtonCancelVisibility == Visibility.Visible)
            count++;
        if (ViewModel.ButtonVisibility.ButtonAbortVisibility == Visibility.Visible)
            count++;
        if (ViewModel.ButtonVisibility.ButtonOkVisibility == Visibility.Visible)
            count++;
        return count;
    }

    private ushort GetGridRowCount()
    {
        ushort count = 1;
        if (!string.IsNullOrEmpty(ViewModel.Caption))
            count++;
        if (!string.IsNullOrEmpty(ViewModel.Message))
            count++;
        return count;
    }

    private void GetFieldCaption(Grid grid, ushort colCount, ref ushort row)
    {
        if (!string.IsNullOrEmpty(ViewModel.Caption))
        {
            TextBlock field = new()
            {
                DataContext = $"{{DynamicResource {nameof(ViewModel)}}}",
                Margin = new(2),
                FontSize = ViewModel.FontSizeCaption,
                FontWeight = FontWeights.Bold,
                FontStretch = FontStretches.Expanded,
                TextDecorations = TextDecorations.Underline,
                TextWrapping = TextWrapping.Wrap,
                Background = System.Windows.Media.Brushes.Transparent,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
            };
            Binding binding = new("Caption") { Mode = BindingMode.OneWay, IsAsync = true, Source = ViewModel };
            BindingOperations.SetBinding(field, TextBlock.TextProperty, binding);
            field.KeyUp += Button_KeyUp;
            Grid.SetColumn(field, 0);
            Grid.SetColumnSpan(field, colCount);
            Grid.SetRow(field, row);
            grid.Children.Add(field);
            row++;
        }
    }

    private void GetFieldMessage(Grid grid, ushort colCount, ref ushort row)
    {
        if (!string.IsNullOrEmpty(ViewModel.Message))
        {
            ScrollViewer scrollViewer = new() { VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
            TextBlock field = new()
            {
                DataContext = $"{{DynamicResource {nameof(ViewModel)}}}",
                Margin = new(2),
                FontSize = ViewModel.FontSizeMessage,
                FontWeight = FontWeights.Regular,
                FontStretch = FontStretches.Normal,
                TextWrapping = TextWrapping.Wrap,
                Background = System.Windows.Media.Brushes.Transparent,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
            };
            Binding binding = new("Message") { Mode = BindingMode.OneWay, IsAsync = true, Source = ViewModel };
            BindingOperations.SetBinding(field, TextBlock.TextProperty, binding);
            field.KeyUp += Button_KeyUp;

            scrollViewer.Content = field;
            Grid.SetColumn(scrollViewer, 0);
            Grid.SetColumnSpan(scrollViewer, colCount);
            Grid.SetRow(scrollViewer, row);
            grid.Children.Add(scrollViewer);
            row++;
        }
    }

    private void GetButtonCustom(Grid grid, ref ushort col, ushort row)
    {
        if (ViewModel.ButtonVisibility.ButtonCustomVisibility == Visibility.Visible)
        {
            Button button = new()
            {
                Content = ViewModel.ButtonVisibility.ButtonCustomContent,
                Margin = new(2),
                FontSize = ViewModel.FontSizeButton,
                FontWeight = FontWeights.Bold,
            };
            Grid.SetColumn(button, col);
            Grid.SetRow(button, row);
            grid.Children.Add(button);
            button.Click += ButtonCustom_OnClick;
            col++;
        }
    }

    private void GetButtonYes(Grid grid, ref ushort col, ushort row)
    {
        if (ViewModel.ButtonVisibility.ButtonYesVisibility == Visibility.Visible)
        {
            Button button = new()
            {
                Content = ViewModel.ButtonVisibility.ButtonYesContent,
                Margin = new(2),
                FontSize = ViewModel.FontSizeButton,
                FontWeight = FontWeights.Bold,
            };
            Grid.SetColumn(button, col);
            Grid.SetRow(button, row);
            grid.Children.Add(button);
            button.Click += ButtonYes_OnClick;
            col++;
        }
    }

    private void GetButtonRetry(Grid grid, ref ushort col, ushort row)
    {
        if (ViewModel.ButtonVisibility.ButtonRetryVisibility == Visibility.Visible)
        {
            Button button = new()
            {
                Content = ViewModel.ButtonVisibility.ButtonRetryContent,
                Margin = new(2),
                FontSize = ViewModel.FontSizeButton,
                FontWeight = FontWeights.Bold,
            };
            Grid.SetColumn(button, col);
            Grid.SetRow(button, row);
            grid.Children.Add(button);
            button.Click += ButtonRetry_OnClick;
            col++;
        }
    }

    private void GetButtonNo(Grid grid, ref ushort col, ushort row)
    {
        if (ViewModel.ButtonVisibility.ButtonNoVisibility == Visibility.Visible)
        {
            Button button = new()
            {
                Content = ViewModel.ButtonVisibility.ButtonNoContent,
                Margin = new(2),
                FontSize = ViewModel.FontSizeButton,
                FontWeight = FontWeights.Bold,
            };
            Grid.SetColumn(button, col);
            Grid.SetRow(button, row);
            grid.Children.Add(button);
            button.Click += ButtonNo_OnClick;
            col++;
        }
    }

    private void GetButtonIgnore(Grid grid, ref ushort col, ushort row)
    {
        if (ViewModel.ButtonVisibility.ButtonIgnoreVisibility == Visibility.Visible)
        {
            Button button = new()
            {
                Content = ViewModel.ButtonVisibility.ButtonIgnoreContent,
                Margin = new(2),
                FontSize = ViewModel.FontSizeButton,
                FontWeight = FontWeights.Bold,
            };
            Grid.SetColumn(button, col);
            Grid.SetRow(button, row);
            grid.Children.Add(button);
            button.Click += ButtonIgnore_OnClick;
            col++;
        }
    }

    private void GetButtonCancel(Grid grid, ref ushort col, ushort row)
    {
        if (ViewModel.ButtonVisibility.ButtonCancelVisibility == Visibility.Visible)
        {
            Button button = new()
            {
                Content = ViewModel.ButtonVisibility.ButtonCancelContent,
                Margin = new(2),
                FontSize = ViewModel.FontSizeButton,
                FontWeight = FontWeights.Bold,
            };
            Grid.SetColumn(button, col);
            Grid.SetRow(button, row);
            grid.Children.Add(button);
            button.Click += ButtonCancel_OnClick;
            col++;
        }
    }

    private void GetButtonAbort(Grid grid, ref ushort col, ushort row)
    {
        if (ViewModel.ButtonVisibility.ButtonAbortVisibility == Visibility.Visible)
        {
            Button button = new()
            {
                Content = ViewModel.ButtonVisibility.ButtonAbortContent,
                Margin = new(2),
                FontSize = ViewModel.FontSizeButton,
                FontWeight = FontWeights.Bold,
            };
            Grid.SetColumn(button, col);
            Grid.SetRow(button, row);
            grid.Children.Add(button);
            button.Click += ButtonAbort_OnClick;
            col++;
        }
    }

    private void GetButtonOk(Grid grid, ref ushort col, ushort row)
    {
        if (ViewModel.ButtonVisibility.ButtonOkVisibility == Visibility.Visible)
        {
            Button button = new()
            {
                Content = ViewModel.ButtonVisibility.ButtonOkContent,
                Margin = new(2),
                FontSize = ViewModel.FontSizeButton,
                FontWeight = FontWeights.Bold,
            };
            Grid.SetColumn(button, col);
            Grid.SetRow(button, row);
            grid.Children.Add(button);
            button.Click += ButtonOk_OnClick;
            col++;
        }
    }

    private void SetButtonFocus(Grid grid)
    {
        foreach (object child in grid.Children)
        {
            if (child is Button button)
            {
                //button.IsDefault = true;
                //button.Focus();
                button.KeyUp += Button_KeyUp;
                button.Focusable = true;
                Keyboard.Focus(button);
                FocusManager.SetFocusedElement(grid, button);
            }
        }

        //FocusNavigationDirection focusNavigationDirection = FocusNavigationDirection.First;
        //TraversalRequest request = new(focusNavigationDirection);
        //UIElement element = Keyboard.FocusedElement as UIElement;
        //if (element is not null)
        //{
        //    element.MoveFocus(request);
        //}
    }

    #endregion

    #region Public and private methods - Actions

    private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnOk();
    }

    private void ButtonYes_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnOk();
    }

    private void ButtonCustom_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnCancel();
    }

    private void ButtonRetry_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnCancel();
    }

    private void ButtonNo_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnCancel();
    }

    private void ButtonIgnore_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnCancel();
    }

    private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnCancel();
    }

    private void ButtonAbort_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.ActionReturnCancel();
    }

    private void Button_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key.Equals(Key.Escape)) ButtonCancel_OnClick(sender, e);
    }

    #endregion
}