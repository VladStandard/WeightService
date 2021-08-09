using ScalesUI.Common;
using ScalesUI.Helpers;
using System.Windows;
using System.Windows.Forms;
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

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            
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
