// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeightCore.Helpers;

namespace WeightCore.XamlPages
{
    /// <summary>
    /// Interaction logic for PageMessageBox.xaml
    /// </summary>
    public partial class PageMessageBox : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string memberName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

        #endregion

        #region Private fields and properties

        public SessionStateHelper SessionState { get; set; } = SessionStateHelper.Instance;

        public MessageBoxHelper MessageBox { get; set; } = MessageBoxHelper.Instance;

        #endregion

        #region Constructor and destructor

        public PageMessageBox()
        {
            InitializeComponent();

            object context = FindResource("MessageBox");
            if (context is MessageBoxHelper messageBox)
            {
                messageBox.Caption = MessageBox.Caption;
                messageBox.Message = MessageBox.Message;

                messageBox.ButtonYesVisibility = MessageBox.ButtonYesVisibility;
                messageBox.ButtonRetryVisibility = MessageBox.ButtonRetryVisibility;
                messageBox.ButtonNoVisibility = MessageBox.ButtonNoVisibility;
                messageBox.ButtonIgnoreVisibility = MessageBox.ButtonIgnoreVisibility;
                messageBox.ButtonCancelVisibility = MessageBox.ButtonCancelVisibility;
                messageBox.ButtonAbortVisibility = MessageBox.ButtonAbortVisibility;
                messageBox.ButtonOkVisibility = MessageBox.ButtonOkVisibility;
            }
        }

        #endregion

        #region Public and private methods

        private ushort GetColCount()
        {
            ushort count = 0;
            if (MessageBox.ButtonYesVisibility == System.Windows.Visibility.Visible)
                count++;
            if (MessageBox.ButtonRetryVisibility == System.Windows.Visibility.Visible)
                count++;
            if (MessageBox.ButtonNoVisibility == System.Windows.Visibility.Visible)
                count++;
            if (MessageBox.ButtonIgnoreVisibility == System.Windows.Visibility.Visible)
                count++;
            if (MessageBox.ButtonCancelVisibility == System.Windows.Visibility.Visible)
                count++;
            if (MessageBox.ButtonAbortVisibility == System.Windows.Visibility.Visible)
                count++;
            if (MessageBox.ButtonOkVisibility == System.Windows.Visibility.Visible)
                count++;
            return count;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Controls.Grid gridButtons = new()
            {
                Margin = new System.Windows.Thickness(2),
            };
            System.Windows.Controls.Grid.SetColumn(gridButtons, 0);
            System.Windows.Controls.Grid.SetRow(gridButtons, 2);
            // Columns.
            ushort col = 0;
            for (col = 0; col < GetColCount(); col++)
            {
                System.Windows.Controls.ColumnDefinition column = new()
                {
                    Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star)
                };
                gridButtons.ColumnDefinitions.Add(column);
            }
            col = 0;
            // Buttons.
            if (MessageBox.ButtonYesVisibility == System.Windows.Visibility.Visible)
            {
                System.Windows.Controls.Button button = new()
                {
                    Content = MessageBox.ButtonYes,
                    Margin = new System.Windows.Thickness(2),
                };
                System.Windows.Controls.Grid.SetColumn(button, col);
                System.Windows.Controls.Grid.SetRow(button, 0);
                gridButtons.Children.Add(button);
                button.Click += ButtonYes_OnClick;
                col++;
            }
            if (MessageBox.ButtonRetryVisibility == System.Windows.Visibility.Visible)
            {
                System.Windows.Controls.Button button = new()
                {
                    Content = MessageBox.ButtonRetry,
                    Margin = new System.Windows.Thickness(2),
                };
                System.Windows.Controls.Grid.SetColumn(button, col);
                System.Windows.Controls.Grid.SetRow(button, 0);
                gridButtons.Children.Add(button);
                button.Click += ButtonRetry_OnClick;
                col++;
            }
            if (MessageBox.ButtonNoVisibility == System.Windows.Visibility.Visible)
            {
                System.Windows.Controls.Button button = new()
                {
                    Content = MessageBox.ButtonNo,
                    Margin = new System.Windows.Thickness(2),
                };
                System.Windows.Controls.Grid.SetColumn(button, col);
                System.Windows.Controls.Grid.SetRow(button, 0);
                gridButtons.Children.Add(button);
                button.Click += ButtonNo_OnClick;
                col++;
            }
            if (MessageBox.ButtonIgnoreVisibility == System.Windows.Visibility.Visible)
            {
                System.Windows.Controls.Button button = new()
                {
                    Content = MessageBox.ButtonIgnore,
                    Margin = new System.Windows.Thickness(2),
                };
                System.Windows.Controls.Grid.SetColumn(button, col);
                System.Windows.Controls.Grid.SetRow(button, 0);
                gridButtons.Children.Add(button);
                button.Click += ButtonIgnore_OnClick;
                col++;
            }
            if (MessageBox.ButtonCancelVisibility == System.Windows.Visibility.Visible)
            {
                System.Windows.Controls.Button button = new()
                {
                    Content = MessageBox.ButtonCancel,
                    Margin = new System.Windows.Thickness(2),
                };
                System.Windows.Controls.Grid.SetColumn(button, col);
                System.Windows.Controls.Grid.SetRow(button, 0);
                gridButtons.Children.Add(button);
                button.Click += ButtonCancel_OnClick;
                col++;
            }
            if (MessageBox.ButtonAbortVisibility == System.Windows.Visibility.Visible)
            {
                System.Windows.Controls.Button button = new()
                {
                    Content = MessageBox.ButtonAbort,
                    Margin = new System.Windows.Thickness(2),
                };
                System.Windows.Controls.Grid.SetColumn(button, col);
                System.Windows.Controls.Grid.SetRow(button, 0);
                gridButtons.Children.Add(button);
                button.Click += ButtonAbort_OnClick;
                col++;
            }
            if (MessageBox.ButtonOkVisibility == System.Windows.Visibility.Visible)
            {
                System.Windows.Controls.Button button = new()
                {
                    Content = MessageBox.ButtonOk,
                    Margin = new System.Windows.Thickness(2),
                };
                System.Windows.Controls.Grid.SetColumn(button, col);
                System.Windows.Controls.Grid.SetRow(button, 0);
                gridButtons.Children.Add(button);
                button.Click += ButtonOk_OnClick;
                col++;
            }
            // Fill tab.
            gridMain.Children.Add(gridButtons);
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
