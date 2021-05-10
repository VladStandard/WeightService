// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using LabelPrint.Models;
using LabelPrint.ViewModels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

// ReSharper disable CommentTypo

namespace LabelPrint.Views
{
    /// <summary>
    /// Interaction logic for PageSettings.xaml
    /// </summary>
    public partial class PageSettings : Page
    {
        #region Public/Private fields and properties

        // Программные настройки.
        private ProgramSettings _settings;

        #endregion

        #region Constructor

        public PageSettings()
        {
            InitializeComponent();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Перед загрузкой страницы.
        /// </summary>
        public void BeforeLoaded()
        {
            // Получить программные настройки.
            _settings = Utils.GetSettings(this);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// После загрузки страницы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageSettings_OnLoaded(object sender, RoutedEventArgs e)
        {
            ButtonDefault_Click(sender, e);
        }

        /// <summary>
        /// По-умолчанию.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDefault_Click(object sender, RoutedEventArgs e)
        {
            // Размеры по-умолчанию.
            _settings.DefaultSizes();
            // Загрузить из конфига.
            ButtonSqlLoadConfig_Click(sender, e);
        }

        /// <summary>
        /// Аутентификация Windows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fieldSqlIntegratedSecurity_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox field)
            {
                LabeldSqlUser.IsEnabled = fieldSqlUser.IsEnabled =
                    LabeldSqlPassword.IsEnabled = fieldSqlPassword.IsEnabled = !(field.IsChecked == true);
            }
            // Изменилось поле SQL.
            fieldSql_TextChanged(sender, null);
        }

        /// <summary>
        /// Загрузить в интерфейс настройки SQL-подключения.
        /// </summary>
        private void SqlLoadGui()
        {
            //fieldSqlServer.TextChanged -= fieldSql_TextChanged;
            //fieldSqlDb.TextChanged -= fieldSql_TextChanged;
            //fieldSqlUser.TextChanged -= fieldSql_TextChanged;
            //fieldSqlPassword.TextChanged -= fieldSql_TextChanged;
            //fieldSqlIntegratedSecurity.Checked -= fieldSqlIntegratedSecurity_Checked;

            //fieldSqlServer.Text = _settings.SqlHelp.Authentication.Server;
            //fieldSqlDb.Text = _settings.SqlHelp.Authentication.Database;
            //fieldSqlUser.Text = _settings.SqlHelp.Authentication.UserId;
            //fieldSqlPassword.Text = _settings.SqlHelp.Authentication.Password;
            //fieldSqlIntegratedSecurity.IsChecked = _settings.SqlHelp.Authentication.IntegratedSecurity;

            //fieldSqlServer.TextChanged += fieldSql_TextChanged;
            //fieldSqlDb.TextChanged += fieldSql_TextChanged;
            //fieldSqlUser.TextChanged += fieldSql_TextChanged;
            //fieldSqlPassword.TextChanged += fieldSql_TextChanged;
            //fieldSqlIntegratedSecurity.Checked += fieldSqlIntegratedSecurity_Checked;
        }

        /// <summary>
        /// Изменилось поле SQL.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fieldSql_TextChanged(object sender, TextChangedEventArgs e)
        {
            //_settings.SqlHelp.Authentication.Server = fieldSqlServer.Text;
            //_settings.SqlHelp.Authentication.Database = fieldSqlDb.Text;
            //_settings.SqlHelp.Authentication.UserId = fieldSqlUser.Text;
            //_settings.SqlHelp.Authentication.Password = fieldSqlPassword.Text;
            //_settings.SqlHelp.Authentication.IntegratedSecurity = fieldSqlIntegratedSecurity.IsChecked == true;
            //fieldSqlConnectionString.Text = _settings.SqlHelp.Authentication.AsString(EnumConStringLevel.Middle);
        }

        /// <summary>
        /// Загрузить из конфига.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSqlLoadConfig_Click(object sender, RoutedEventArgs e)
        {
            //_settings.SqlHelp.Open(EnumSettingsStorage.UseConfig);
            // Загрузить в интерфейс настройки SQL-подключения.
            SqlLoadGui();
            //fieldSqlConnectionString.Text = _settings.SqlHelp.Authentication.AsString(EnumConStringLevel.Middle);
        }

        /// <summary>
        /// Задать доступ к GUI SQL.
        /// </summary>
        /// <param name="enabled"></param>
        private void SetGuiSqlEnabled(bool enabled)
        {
            WPF.Utils.InvokeControl.SetIsEnabled(LabeldSqlConnectionString, enabled);
            WPF.Utils.InvokeControl.SetIsEnabled(fieldSqlConnectionString, enabled);
            WPF.Utils.InvokeControl.SetIsEnabled(LabeldSqlServer, enabled);
            WPF.Utils.InvokeControl.SetIsEnabled(fieldSqlServer, enabled);
            WPF.Utils.InvokeControl.SetIsEnabled(LabeldSqlDb, enabled);
            WPF.Utils.InvokeControl.SetIsEnabled(fieldSqlDb, enabled);
            WPF.Utils.InvokeControl.SetIsEnabled(LabeldSqlUser, enabled);
            WPF.Utils.InvokeControl.SetIsEnabled(fieldSqlUser, enabled);
            WPF.Utils.InvokeControl.SetIsEnabled(LabeldSqlPassword, enabled);
            WPF.Utils.InvokeControl.SetIsEnabled(fieldSqlPassword, enabled);
            WPF.Utils.InvokeControl.SetIsEnabled(fieldSqlIntegratedSecurity, enabled);
        }

        /// <summary>
        /// Проверить подключение к серверу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSqlCheckConnect_Click(object sender, RoutedEventArgs e)
        {
            SqlCheckConnectAsync();
        }

        /// <summary>
        /// Задача проверки подключения к серверу.
        /// </summary>
        private Task SqlCheckConnectAsync()
        {
            return Task.Run(() =>
            {
                WPF.Utils.InvokeControl.SetBackground(fieldSqlConnectionString, Brushes.Yellow);
                // Задать доступ к GUI SQL.
                SetGuiSqlEnabled(false);
                // Проверить SQL-подключение.
                //if (_settings.AppHelp.SqlConCheck(EnumLanguage.Russian))
                //{
                //    WPF.Utils.InvokeControl.SetBackground(fieldSqlConnectionString, Brushes.Transparent);
                //}
                //// Задать доступ к GUI SQL.
                //SetGuiSqlEnabled(true);
                //MessageBox.Show(_settings.SqlHelp.Status);
            });
        }

        #endregion
    }
}
