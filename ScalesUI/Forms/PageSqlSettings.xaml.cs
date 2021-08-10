using ScalesUI.Common;
using System.Windows;
using System.Windows.Forms;
using WeightCore.Db;
using UserControl = System.Windows.Controls.UserControl;

namespace ScalesUI.Forms
{
    /// <summary>
    /// Interaction logic for PageSqlSettings.xaml
    /// </summary>
    public partial class PageSqlSettings : UserControl
    {
        #region Private fields and properties

        public SessionState _ws { get; private set; } = SessionState.Instance;
        public SqlHelper _sqlHelper { get; private set; } = SqlHelper.Instance;
        public int RowCount { get; } = 5;
        public int ColumnCount { get; } = 4;
        public int PageSize { get; } = 20;
        public DialogResult Result { get; private set; }

        #endregion

        #region Constructor and destructor

        public PageSqlSettings()
        {
            InitializeComponent();

            var context = FindResource("ViewModelSqlHelper");
            if (context is SqlHelper sqlHelper)
            {
                _sqlHelper = sqlHelper;
            }
        }

        #endregion

        #region Public and private methods

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _sqlHelper.SetupTasks(_ws.CurrentScale.Description);

            System.Windows.Controls.Grid gridTasks = new System.Windows.Controls.Grid();
            for (int col = 0; col < 2; col++)
            {
                System.Windows.Controls.ColumnDefinition column = new System.Windows.Controls.ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };
                gridTasks.ColumnDefinitions.Add(column);
            }
            for (int row = 0; row < _sqlHelper.TaskItems.Count; row++)
            {
                System.Windows.Controls.RowDefinition rows = new System.Windows.Controls.RowDefinition() { Height = new GridLength(1, GridUnitType.Star) };
                gridTasks.RowDefinitions.Add(rows);
                System.Windows.Controls.Label labelTaskCaption = new System.Windows.Controls.Label { 
                    Content = _sqlHelper.TaskItems[row].TaskTypeName,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                };
                System.Windows.Controls.Grid.SetColumn(labelTaskCaption, 0);
                System.Windows.Controls.Grid.SetRow(labelTaskCaption, row);
                System.Windows.Controls.Label labelTaskEnabled = new System.Windows.Controls.Label { 
                    Content = _sqlHelper.TaskItems[row].Enabled,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                };
                System.Windows.Controls.Grid.SetColumn(labelTaskEnabled, 1);
                System.Windows.Controls.Grid.SetRow(labelTaskEnabled, row);
                gridTasks.Children.Add(labelTaskCaption);
                gridTasks.Children.Add(labelTaskEnabled);
            }

            tabTasks.Content = gridTasks;
        }

        public void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            Result = DialogResult.OK;
            _ws.IsWpfPageLoaderClose = true;
        }

        public void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            Result = DialogResult.Cancel;
            _ws.IsWpfPageLoaderClose = true;
        }

        #endregion
    }
}
