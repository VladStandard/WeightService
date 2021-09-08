// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesMsi.Helpers;
using ScalesMsi.Utils;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WixSharp;
using WixSharp.UI.Forms;

namespace ScalesMsi.Dialogs
{
    /// <summary>
    /// Language dialog
    /// </summary>
    public partial class LanguageDialog : ManagedForm
    {
        #region Private helpers

        /// <summary>
        /// Помощник приложения.
        /// </summary>
        private readonly AppHelper _app = AppHelper.Instance;

        #endregion

        #region Dialog methods

        public LanguageDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Локализация.
        /// </summary>
        private void Localization()
        {
            // Language.
            Text = _app.CurrentLocalization == EnumLocalization.English ? @"Setup localization" : @"Настройки локализации";
            groupBoxLanguage.Text = @"[MaintenanceLocalizationDlgLanguage]";

            // ID.
            groupBoxId.Text = @"[MaintenanceLocalizationDlgId]";
            buttonIdPaste.Text = @"[MaintenanceLocalizationDlgIdPasteFromBuffer]";
            buttonIdCheckInSql.Text = @"[MaintenanceLocalizationDlgIdCheckInSql]";
            buttonIdRegistryLoad.Text = @"[MaintenanceLocalizationDlgLoadDefault]";

            // SQL.
            groupBoxSql.Text = @"[MaintenanceLocalizationDlgSqlConStr]";
            labelSqlServer.Text = @"[MaintenanceLocalizationDlgSqlServer]";
            labelSqlDb.Text = @"[MaintenanceLocalizationDlgSqlDb]";
            labelSqlUser.Text = @"[MaintenanceLocalizationDlgSqlUser]";
            labelSqlPassword.Text = @"[MaintenanceLocalizationDlgSqlPassword]";
            buttonSqlConfigLoad.Text = @"[MaintenanceLocalizationDlgLoadDefault]";
            buttonSqlCheckConnect.Text = @"[MaintenanceLocalizationDlgSqlCheckConnect]";
            fieldSqlIntegratedSecurity.Text = @"[MaintenanceLocalizationDlgSqlIntegratedSecurity]";

            // Кнопки.
            buttonRunAs.Text = @"[WixUIRunAs]";
            next.Text = @"[WixUINext]";
            cancel.Text = @"[WixUICancel]";

            // Повышенные права доступа.
            fieldElevatedAccess.Text = @"[MaintenanceLocalizationDlgElevatedRequired]";

            Localize();
        }

        /// <summary>
        /// Загрузка диалога.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LanguageDialog_Load(object sender, EventArgs e)
        {
            ResetLayout();

            // Локализация.
            fieldLanguage_SelectedIndexChanged(sender, e);

            // Повышенные права доступа.
            var isAccess = false;
            try
            {
                isAccess = UACHelper.UACHelper.IsProcessElevated(Process.GetCurrentProcess());
            }
            catch (Exception)
            {
                //
            }
            if (!isAccess)
            {
                fieldElevatedAccess.Visible = true;
                groupBoxSql.Enabled = false;
                groupBoxId.Enabled = false;
                next.Enabled = false;
                buttonRunAs.Visible = true;
            }

            // Загрузить настройки SQL-подключения из конфига.
            buttonSqlLoadConfig_Click(sender, e);
            // Проверить SQL-подключение.
            _app.SqlConCheck(_app.CurrentLocalization);

            // SQL.
            fieldSqlIntegratedSecurity_CheckedChanged(sender, e);
            SqlCheckConnectAsync();

            // ID.
            buttonIdReading_Click(sender, e);
            fieldId.TextChanged += fieldId_TextChanged;
            SqlCheckId(EnumSilentUI.True);

            // Перезапуск под админом.
            if (!isAccess)
            {
                buttonRunAs_Click(sender, e);
            }
        }

        private void ResetLayout()
        {
            fieldLanguage.Items.Add("English");
            fieldLanguage.Items.Add("Russian");
            fieldLanguage.SelectedIndex = (int)_app.CurrentLocalization;
            fieldLanguage.SelectedIndexChanged += fieldLanguage_SelectedIndexChanged;
            fieldLanguage.KeyUp += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                    cancel_Click(s, e);
                if (e.KeyCode == Keys.Enter)
                {
                    next_Click(s, e);
                }
            };

            var bHeight = (int)(next.Height * 2.3);
            var upShift = bHeight - bottomPanel.Height;
            bottomPanel.Top -= upShift;
            bottomPanel.Height = bHeight;
        }

        #endregion

        #region Private methods - Localization

        /// <summary>
        /// Изменилось поле смены локализации.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fieldLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _app.CurrentLocalization = (EnumLocalization)fieldLanguage.SelectedIndex;
            var runtime = ((IManagedDialog)this).Shell.MsiRuntime();
            switch (_app.CurrentLocalization)
            {
                case EnumLocalization.Russian:
                    runtime.UIText.InitFromWxl(_app.Wix.Args.Session.ReadBinary("ru_xsl"));
                    break;
                default:
                    runtime.UIText.InitFromWxl(_app.Wix.Args.Session.ReadBinary("en_xsl"));
                    break;
            }

            Localization();
        }

        #endregion

        #region Private methods - SQL

        /// <summary>
        /// Загрузить в интерфейс настройки SQL-подключения.
        /// </summary>
        private void SqlLoadGui()
        {
            fieldSqlServer.TextChanged -= fieldSql_TextChanged;
            fieldSqlDb.TextChanged -= fieldSql_TextChanged;
            fieldSqlUser.TextChanged -= fieldSql_TextChanged;
            fieldSqlPassword.TextChanged -= fieldSql_TextChanged;
            fieldSqlIntegratedSecurity.CheckedChanged -= fieldSqlIntegratedSecurity_CheckedChanged;

            fieldSqlServer.Text = _app.Sql.Authentication.Server;
            fieldSqlDb.Text = _app.Sql.Authentication.Database;
            fieldSqlUser.Text = _app.Sql.Authentication.UserId;
            fieldSqlPassword.Text = _app.Sql.Authentication.Password;
            fieldSqlIntegratedSecurity.Checked = _app.Sql.Authentication.IntegratedSecurity;

            fieldSqlServer.TextChanged += fieldSql_TextChanged;
            fieldSqlDb.TextChanged += fieldSql_TextChanged;
            fieldSqlUser.TextChanged += fieldSql_TextChanged;
            fieldSqlPassword.TextChanged += fieldSql_TextChanged;
            fieldSqlIntegratedSecurity.CheckedChanged += fieldSqlIntegratedSecurity_CheckedChanged;
        }

        /// <summary>
        /// Загрузить настройки SQL-подключения из конфига.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSqlLoadConfig_Click(object sender, EventArgs e)
        {
            _app.Sql.Open(EnumSettingsStorage.UseConfig);
            // Загрузить в интерфейс настройки SQL-подключения.
            SqlLoadGui();
        }

        /// <summary>
        /// Изменилось поле SQL.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fieldSql_TextChanged(object sender, EventArgs e)
        {
            _app.Sql.Authentication.Server = fieldSqlServer.Text;
            _app.Sql.Authentication.Database = fieldSqlDb.Text;
            _app.Sql.Authentication.UserId = fieldSqlUser.Text;
            _app.Sql.Authentication.Password = fieldSqlPassword.Text;
            _app.Sql.Authentication.IntegratedSecurity = fieldSqlIntegratedSecurity.Checked;

            fieldConnectionString.Text = _app.Sql.Authentication.AsString(EnumConStringLevel.Middle);
        }

        /// <summary>
        /// Проверить SQL-подключение.
        /// </summary>
        private void SqlCheckConnectAsync()
        {
            fieldConnectionString.BackColor = Color.Yellow;
            groupBoxSql.Enabled = false;
            next.Enabled = false;
            groupBoxId.Enabled = false;
            // Проверить SQL-подключение.
            if (_app.SqlConCheck(_app.CurrentLocalization))
            {
                fieldConnectionString.BackColor = SystemColors.Window;
                groupBoxId.Enabled = true;
                next.Enabled = true;
            }
            groupBoxSql.Enabled = true;
        }

        /// <summary>
        /// Проверить подключение к серверу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSqlCheckConnect_Click(object sender, EventArgs e)
        {
            SqlCheckConnectAsync();
        }

        /// <summary>
        /// Аутентификация Windows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fieldSqlIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox field)
            {
                labelSqlUser.Enabled = fieldSqlUser.Enabled =
                    labelSqlPassword.Enabled = fieldSqlPassword.Enabled = !field.Checked;
            }
            fieldSql_TextChanged(sender, e);
        }

        #endregion

        #region Private methods - ID

        /// <summary>
        /// Изменился выбранный ID.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fieldIdFromDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fieldIdFromDb.Items.Count > 0)
            {
                fieldId.Text = fieldIdFromDb.Items[fieldIdFromDb.SelectedIndex].ToString();
            }
        }
        
        /// <summary>
        /// Изменилось поле ID.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fieldId_TextChanged(object sender, EventArgs e)
        {
            _app.SetupDefault();
            _app.IdSetValue(fieldId.Text);
        }

        /// <summary>
        /// Проверить ID в таблице БД.
        /// </summary>
        /// <param name="silent"></param>
        private void SqlCheckId(EnumSilentUI silent)
        {
            fieldId.BackColor = Color.Yellow;
            groupBoxSql.Enabled = false;
            groupBoxId.Enabled = false;
            next.Enabled = false;
            // Проверить наличие ID.
            var allow = _app.SqlExistsId(fieldId.Text);
            if (allow && silent == EnumSilentUI.False)
            {
                MessageBox.Show(_app.CurrentLocalization == EnumLocalization.English
                    ? @"ID is found."
                    : "Идентификатор успешно обнаружен.");
            }

            if (!allow)
            {
                if (silent == EnumSilentUI.False)
                {
                    MessageBox.Show(_app.CurrentLocalization == EnumLocalization.English
                        ? @"GUID not found!"
                        : "Идентификатор не обнаружен!");
                    //var question = _app.CurrentLocalization == EnumLocalization.English
                    //    ? "Unlock for admin?"
                    //    : "Разблокировать для администратора?";
                    //allow = MessageBox.Show(question, _app.Wix.Args.ProductName, MessageBoxButtons.YesNo,
                    //    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
                }
            }

            if (allow)
            {
                fieldId.BackColor = SystemColors.Window;
                next.Enabled = true;
            }

            groupBoxSql.Enabled = true;
            groupBoxId.Enabled = true;
        }

        /// <summary>
        /// Получить список ID в таблице БД.
        /// </summary>
        private void SqlGetIds()
        {
            fieldIdFromDb.Items.Clear();
            fieldIdFromDb.Items.AddRange(_app.SqlGetIds());
            if (fieldIdFromDb.Items.Count > 0)
                fieldIdFromDb.SelectedIndex = 0;
            fieldIdFromDb_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Прочитать ID из настроек.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonIdReading_Click(object sender, EventArgs e)
        {
            //fieldId.Text = Properties.Settings.Default.Id;
            // Получить список ID в таблице БД.
            SqlGetIds();
        }

        /// <summary>
        /// Вставить из буфера обмена.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonIdPaste_Click(object sender, EventArgs e)
        {
            fieldId.Text = Clipboard.GetText();
        }

        /// <summary>
        /// Проверить ID в таблице БД.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonIdCheckInSql_Click(object sender, EventArgs e)
        {
            SqlCheckId(EnumSilentUI.False);
        }

        #endregion

        #region Private methods - Управление

        /// <summary>
        /// Сохранить настройки.
        /// </summary>
        private void SaveSettings()
        {
            Properties.Settings.Default.Id = fieldId.Text;
            Properties.Settings.Default.ConnectionString = fieldConnectionString.Text;
        }

        /// <summary>
        /// Кнопка Далее.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void next_Click(object sender, EventArgs e)
        {
            // Изменилось поле смены локализации.
            fieldLanguage_SelectedIndexChanged(sender, e);
            // Изменилось поле ID.
            fieldId_TextChanged(sender, e);
            // Изменилось поле SQL.
            fieldSql_TextChanged(sender, e);

            // ID.
            if (!_app.IdExists())
            {
                var message = _app.CurrentLocalization == EnumLocalization.English ? @"Fill ID!" : @"Заполните поле идентификатора!";
                MessageBox.Show(message, _app.GetDescription(System.Reflection.Assembly.GetExecutingAssembly()));
                fieldId.Select();
                return;
            }
            // Сохранить настройки ID в реестре.
            //_app.GuidRegSave();

            // SQL.
            if (!_app.Sql.Authentication.Exists())
            {
                var message = _app.CurrentLocalization == EnumLocalization.English ? @"Fill SQL-fields!" : @"Заполните SQL-поля!";
                MessageBox.Show(message, _app.GetDescription(System.Reflection.Assembly.GetExecutingAssembly()));
                fieldId.Select();
                return;
            }
            // Закрыть SQL-подключение.
            _app.Sql.CloseConnection(_app.CurrentLocalization);

            // Сохранить настройки.
            SaveSettings();

            Shell.GoNext();
        }

        /// <summary>
        /// Кнопка Выход.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, EventArgs e)
        {
            Shell.Cancel();
        }

        /// <summary>
        /// Кнопка RunAs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRunAs_Click(object sender, EventArgs e)
        {
            var taskMsi = _app.Proc.RunMsiAsync("ScalesUI");
            taskMsi.Wait();

            Shell.Exit();
        }

        #endregion
    }
}