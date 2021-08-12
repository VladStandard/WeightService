using ScalesUI.Common;
using System.Windows.Forms;
using WeightCore.DAL.TableModels;
using WeightCore.DAL.Utils;
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
        public int RowCount { get; } = 5;
        public int ColumnCount { get; } = 4;
        public int PageSize { get; } = 20;
        public DialogResult Result { get; private set; }

        #endregion

        #region Constructor and destructor

        public PageSqlSettings()
        {
            InitializeComponent();

            //var context = FindResource("ViewModelSqlHelper");
            //if (context is SqlHelper sqlHelper)
            //{
            //    _ws.SqlItem = sqlHelper;
            //}
        }

        #endregion

        #region Public and private methods

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _ws.SqlItem.SetupTasks(_ws.Host?.CurrentScaleId);

            System.Windows.Controls.Grid gridTasks = new System.Windows.Controls.Grid();
            // Columns.
            for (int col = 0; col < 2; col++)
            {
                System.Windows.Controls.ColumnDefinition column = new System.Windows.Controls.ColumnDefinition()
                {
                    Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star)
                };
                gridTasks.ColumnDefinitions.Add(column);
            }
            // Rows.
            for (int row = 0; row < _ws.SqlItem.Tasks.Count; row++)
            {
                // Row.
                System.Windows.Controls.RowDefinition rows = new System.Windows.Controls.RowDefinition()
                {
                    Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star)
                };
                gridTasks.RowDefinitions.Add(rows);
                // Task caption.
                System.Windows.Controls.Label labelTaskCaption = new System.Windows.Controls.Label
                {
                    Content = _ws.SqlItem.Tasks[row].TaskType.Name,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                };
                System.Windows.Controls.Grid.SetColumn(labelTaskCaption, 0);
                System.Windows.Controls.Grid.SetRow(labelTaskCaption, row);
                gridTasks.Children.Add(labelTaskCaption);
                // Task enabled.
                System.Windows.Controls.ComboBox comboBoxTaskEnabled = new System.Windows.Controls.ComboBox()
                {
                    Width = 100,
                    Height = 30,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
                    Tag = _ws.SqlItem.Tasks[row]
                };
                System.Windows.Controls.ComboBoxItem itemTrue = new System.Windows.Controls.ComboBoxItem() { Content = "True" };
                comboBoxTaskEnabled.Items.Add(itemTrue);
                System.Windows.Controls.ComboBoxItem itemFalse = new System.Windows.Controls.ComboBoxItem() { Content = "False" };
                comboBoxTaskEnabled.Items.Add(itemFalse);
                comboBoxTaskEnabled.SelectedItem = _ws.SqlItem.Tasks[row].Enabled ? itemTrue : itemFalse;
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
                        if (comboBoxTaskEnabled.Tag is TaskEntity taskItem)
                        {
                            if (comboBoxTaskEnabled.SelectedItem is System.Windows.Controls.ComboBoxItem itemSelected)
                            {
                                TasksUtils.SaveTask(taskItem, taskItem.TaskType, taskItem.Scale.Id,
                                    string.Equals(itemSelected.Content.ToString(), "True", System.StringComparison.InvariantCultureIgnoreCase));
                            }
                        }
                    }
                }
            }

            Result = DialogResult.OK;
            _ws.IsWpfPageLoaderClose = true;
        }

        public void ButtonClose_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Result = DialogResult.Cancel;
            _ws.IsWpfPageLoaderClose = true;
        }

        #endregion
    }
}
