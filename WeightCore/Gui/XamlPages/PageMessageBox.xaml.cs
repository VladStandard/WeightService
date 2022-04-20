// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Controls;
using WeightCore.Helpers;

namespace WeightCore.Gui.XamlPages
{
    /// <summary>
    /// Interaction logic for PageMessageBox.xaml
    /// </summary>
    public partial class PageMessageBox : UserControl
    {
        #region Private fields and properties

        public SessionStateHelper SessionState { get; private set; } = SessionStateHelper.Instance;

        public MessageBoxEntity MessageBox { get; set; } = new MessageBoxEntity();

        #endregion

        #region Constructor and destructor

        public PageMessageBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Public and private methods

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ushort colCount = GetGridColCount();
            ushort rowCount = GetGridRowCount();
            Grid gridMain = GetGridMain(colCount, rowCount);

            ushort row = 0;
            GetFieldCaption(gridMain, colCount, ref row);
            GetFieldMessage(gridMain, colCount, ref row);

            ushort col = 0;
            GetButtonCustom(gridMain, ref col, row);
            GetButtonYes(gridMain, ref col, row);
            GetButtonRetry(gridMain, ref col, row);
            GetButtonNo(gridMain, ref col, row);
            GetButtonIgnore(gridMain, ref col, row);
            GetButtonCancel(gridMain, ref col, row);
            GetButtonAbort(gridMain, ref col, row);
            GetButtonOk(gridMain, ref col, row);

            borderMain.Child = gridMain;
        }

        private Grid GetGridMain(ushort colCount, ushort rowCount)
        {
            Grid gridMain = new()
            {
                DataContext = $"{{DynamicResource {nameof(MessageBox)}}}",
                Margin = new System.Windows.Thickness(2),
            };
            gridMain.KeyUp += Button_KeyUp;

            Grid.SetColumn(gridMain, 0);
            for (ushort col = 0; col < colCount; col++)
            {
                ColumnDefinition column = new() { Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) };
                gridMain.ColumnDefinitions.Add(column);
            }

            Grid.SetRow(gridMain, 0);
            if (rowCount <= 1)
            {
                RowDefinition row = new() { Height = new System.Windows.GridLength(MessageBox.SizeCaption, System.Windows.GridUnitType.Star) };
                gridMain.RowDefinitions.Add(row);
            }
            else if (rowCount == 2)
            {
                RowDefinition row = new() { Height = new System.Windows.GridLength(MessageBox.SizeMessage, System.Windows.GridUnitType.Star) };
                gridMain.RowDefinitions.Add(row);
                RowDefinition row2 = new() { Height = new System.Windows.GridLength(MessageBox.SizeButton, System.Windows.GridUnitType.Star) };
                gridMain.RowDefinitions.Add(row2);
            }
            else if (rowCount == 3)
            {
                RowDefinition row = new() { Height = new System.Windows.GridLength(MessageBox.SizeCaption, System.Windows.GridUnitType.Star) };
                gridMain.RowDefinitions.Add(row);
                RowDefinition row2 = new() { Height = new System.Windows.GridLength(MessageBox.SizeMessage, System.Windows.GridUnitType.Star) };
                gridMain.RowDefinitions.Add(row2);
                RowDefinition row3 = new() { Height = new System.Windows.GridLength(MessageBox.SizeButton, System.Windows.GridUnitType.Star) };
                gridMain.RowDefinitions.Add(row3);
            }

            return gridMain;
        }

        private ushort GetGridColCount()
        {
            ushort count = 0;
            if (MessageBox.VisibilitySettings.ButtonCustomVisibility == System.Windows.Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonYesVisibility == System.Windows.Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonRetryVisibility == System.Windows.Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonNoVisibility == System.Windows.Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonIgnoreVisibility == System.Windows.Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonCancelVisibility == System.Windows.Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonAbortVisibility == System.Windows.Visibility.Visible)
                count++;
            if (MessageBox.VisibilitySettings.ButtonOkVisibility == System.Windows.Visibility.Visible)
                count++;
            return count;
        }

        private ushort GetGridRowCount()
        {
            ushort count = 1;
            if (!string.IsNullOrEmpty(MessageBox.Caption))
                count++;
            if (!string.IsNullOrEmpty(MessageBox.Message))
                count++;
            return count;
        }

        private void GetFieldCaption(Grid gridMain, ushort colCount, ref ushort row)
        {
            if (!string.IsNullOrEmpty(MessageBox.Caption))
            {
                TextBlock field = new()
                {
                    DataContext = $"{{DynamicResource {nameof(MessageBox)}}}",
                    Margin = new System.Windows.Thickness(2),
                    FontSize = MessageBox.FontSizeCaption,
                    FontWeight = System.Windows.FontWeights.Bold,
                    FontStretch = System.Windows.FontStretches.Expanded,
                    TextDecorations = System.Windows.TextDecorations.Underline,
                    TextWrapping = System.Windows.TextWrapping.Wrap,
                    Background = System.Windows.Media.Brushes.Transparent,
                    TextAlignment = System.Windows.TextAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                };
                System.Windows.Data.Binding binding = new("Caption") { Mode = System.Windows.Data.BindingMode.OneWay, IsAsync = true, Source = MessageBox };
                System.Windows.Data.BindingOperations.SetBinding(field, TextBlock.TextProperty, binding);
                field.KeyUp += Button_KeyUp;
                Grid.SetColumn(field, 0);
                Grid.SetColumnSpan(field, colCount);
                Grid.SetRow(field, row);
                gridMain.Children.Add(field);
                row++;
            }
        }

        private void GetFieldMessage(Grid gridMain, ushort colCount, ref ushort row)
        {
            if (!string.IsNullOrEmpty(MessageBox.Message))
            {
                ScrollViewer scrollViewer = new() { VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
                TextBlock field = new()
                {
                    DataContext = $"{{DynamicResource {nameof(MessageBox)}}}",
                    Margin = new System.Windows.Thickness(2),
                    FontSize = MessageBox.FontSizeMessage,
                    FontWeight = System.Windows.FontWeights.Regular,
                    FontStretch = System.Windows.FontStretches.Normal,
                    TextWrapping = System.Windows.TextWrapping.Wrap,
                    Background = System.Windows.Media.Brushes.Transparent,
                    TextAlignment = System.Windows.TextAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                };
                System.Windows.Data.Binding binding = new("Message") { Mode = System.Windows.Data.BindingMode.OneWay, IsAsync = true, Source = MessageBox };
                System.Windows.Data.BindingOperations.SetBinding(field, TextBlock.TextProperty, binding);
                field.KeyUp += Button_KeyUp;

                scrollViewer.Content = field;
                Grid.SetColumn(scrollViewer, 0);
                Grid.SetColumnSpan(scrollViewer, colCount);
                Grid.SetRow(scrollViewer, row);
                gridMain.Children.Add(scrollViewer);
                row++;
            }
        }

        private void GetButtonCustom(Grid gridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonCustomVisibility == System.Windows.Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonCustomContent,
                    Margin = new System.Windows.Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = System.Windows.FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                gridMain.Children.Add(button);
                button.Click += ButtonCustom_OnClick;
                col++;
            }
        }

        private void GetButtonYes(Grid gridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonYesVisibility == System.Windows.Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonYesContent,
                    Margin = new System.Windows.Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = System.Windows.FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                gridMain.Children.Add(button);
                button.Click += ButtonYes_OnClick;
                col++;
            }
        }

        private void GetButtonRetry(Grid gridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonRetryVisibility == System.Windows.Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonRetryContent,
                    Margin = new System.Windows.Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = System.Windows.FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                gridMain.Children.Add(button);
                button.Click += ButtonRetry_OnClick;
                col++;
            }
        }

        private void GetButtonNo(Grid gridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonNoVisibility == System.Windows.Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonNoContent,
                    Margin = new System.Windows.Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = System.Windows.FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                gridMain.Children.Add(button);
                button.Click += ButtonNo_OnClick;
                col++;
            }
        }

        private void GetButtonIgnore(Grid gridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonIgnoreVisibility == System.Windows.Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonIgnoreContent,
                    Margin = new System.Windows.Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = System.Windows.FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                gridMain.Children.Add(button);
                button.Click += ButtonIgnore_OnClick;
                col++;
            }
        }

        private void GetButtonCancel(Grid gridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonCancelVisibility == System.Windows.Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonCancelContent,
                    Margin = new System.Windows.Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = System.Windows.FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                gridMain.Children.Add(button);
                button.Click += ButtonCancel_OnClick;
                col++;
            }
        }

        private void GetButtonAbort(Grid gridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonAbortVisibility == System.Windows.Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonAbortContent,
                    Margin = new System.Windows.Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = System.Windows.FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                gridMain.Children.Add(button);
                button.Click += ButtonAbort_OnClick;
                col++;
            }
        }

        private void GetButtonOk(Grid gridMain, ref ushort col, ushort row)
        {
            if (MessageBox.VisibilitySettings.ButtonOkVisibility == System.Windows.Visibility.Visible)
            {
                Button button = new()
                {
                    Content = MessageBox.VisibilitySettings.ButtonOkContent,
                    Margin = new System.Windows.Thickness(2),
                    FontSize = MessageBox.FontSizeButton,
                    FontWeight = System.Windows.FontWeights.Bold,
                };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);
                gridMain.Children.Add(button);
                button.Click += ButtonOk_OnClick;
                col++;
            }
        }

        #endregion

        #region Public and private methods - Actions

        public void ButtonCustom_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.Retry;
            SessionState.IsWpfPageLoaderClose = true;
        }

        public void ButtonYes_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.Yes;
            SessionState.IsWpfPageLoaderClose = true;
        }

        public void ButtonRetry_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.Retry;
            SessionState.IsWpfPageLoaderClose = true;
        }

        public void ButtonNo_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.No;
            SessionState.IsWpfPageLoaderClose = true;
        }

        public void ButtonIgnore_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.Ignore;
            SessionState.IsWpfPageLoaderClose = true;
        }

        public void ButtonCancel_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.Cancel;
            SessionState.IsWpfPageLoaderClose = true;
        }

        public void ButtonAbort_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.Abort;
            SessionState.IsWpfPageLoaderClose = true;
        }

        public void ButtonOk_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.OK;
            SessionState.IsWpfPageLoaderClose = true;
        }

        public void ButtonClose_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Result = System.Windows.Forms.DialogResult.Cancel;
            SessionState.IsWpfPageLoaderClose = true;
        }

        private void Button_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                ButtonCancel_OnClick(sender, e);
            }
        }

        #endregion
    }
}
