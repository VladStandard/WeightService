// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDirectModels;
using DataCore.Sql;
using System.Windows.Forms;
using WeightCore.Helpers;
using DataCore.Sql.Controllers;

namespace WeightCore.Gui.XamlPages
{
    /// <summary>
    /// Interaction logic for PageSqlSettings.xaml
    /// </summary>
    public partial class PageSqlSettings : System.Windows.Controls.UserControl
    {
        #region Private fields and properties

        public UserSessionHelper UserSession { get; private set; } = UserSessionHelper.Instance;
        public SqlViewModelEntity SqlViewModel { get; set; }
        public int RowCount { get; } = 5;
        public int ColumnCount { get; } = 4;
        public int PageSize { get; } = 20;
        public DialogResult Result { get; private set; }

        #endregion

        #region Constructor and destructor

        public PageSqlSettings()
        {
            InitializeComponent();

            object context = FindResource("SqlViewModel");
            if (context is SqlViewModelEntity sqlViewModel)
            {
                sqlViewModel = SqlViewModel;
            }
            SqlViewModel = UserSession.SqlViewModel;
        }

        #endregion

        #region Public and private methods

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            UserSession.SqlViewModel.SetupTasks(UserSession.Host?.ScaleId);

            System.Windows.Controls.Grid gridTasks = new();
            
            // Columns.
            for (int col = 0; col < 2; col++)
            {
                System.Windows.Controls.ColumnDefinition column = new()
                {
                    Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star)
                };
                gridTasks.ColumnDefinitions.Add(column);
            }
            
            // Rows.
            for (int row = 0; row < UserSession.SqlViewModel.Tasks.Count; row++)
            {
                // Row.
                System.Windows.Controls.RowDefinition rows = new()
                {
                    Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star)
                };
                gridTasks.RowDefinitions.Add(rows);
                // Task caption.
                System.Windows.Controls.Label labelTaskCaption = new()
                {
                    Content = UserSession.SqlViewModel.Tasks[row].TaskType.Name,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                };
                System.Windows.Controls.Grid.SetColumn(labelTaskCaption, 0);
                System.Windows.Controls.Grid.SetRow(labelTaskCaption, row);
                gridTasks.Children.Add(labelTaskCaption);
                // Task enabled.
                System.Windows.Controls.ComboBox comboBoxTaskEnabled = new()
                {
                    Width = 100,
                    Height = 30,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    Tag = UserSession.SqlViewModel.Tasks[row]
                };
                System.Windows.Controls.ComboBoxItem itemTrue = new() { Content = "True" };
                comboBoxTaskEnabled.Items.Add(itemTrue);
                System.Windows.Controls.ComboBoxItem itemFalse = new() { Content = "False" };
                comboBoxTaskEnabled.Items.Add(itemFalse);
                comboBoxTaskEnabled.SelectedItem = UserSession.SqlViewModel.Tasks[row].Enabled ? itemTrue : itemFalse;
                System.Windows.Controls.Grid.SetColumn(comboBoxTaskEnabled, 1);
                System.Windows.Controls.Grid.SetRow(comboBoxTaskEnabled, row);
                gridTasks.Children.Add(comboBoxTaskEnabled);
            }
            // Fill tab.
            tabTasks.Content = gridTasks;
        }

        public void ButtonOk_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (tabTasks.Content is System.Windows.Controls.Grid gridTasks)
            {
                foreach (object item in gridTasks.Children)
                {
                    if (item is System.Windows.Controls.ComboBox comboBoxTaskEnabled)
                    {
                        if (comboBoxTaskEnabled.Tag is TaskDirect taskItem)
                        {
                            if (comboBoxTaskEnabled.SelectedItem is System.Windows.Controls.ComboBoxItem itemSelected)
                            {
                                TasksUtils.SaveTask(taskItem, 
                                    string.Equals(itemSelected.Content.ToString(), "True", System.StringComparison.InvariantCultureIgnoreCase));
                            }
                        }
                    }
                }
            }

            Result = DialogResult.OK;
            UserSession.IsWpfPageLoaderClose = true;
        }

        public void ButtonClose_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Result = DialogResult.Cancel;
            UserSession.IsWpfPageLoaderClose = true;
        }

        #endregion
    }
}
