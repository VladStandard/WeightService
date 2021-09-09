// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Win32;
using MvvmHelpers;
using ScalesCore.Models;
using ScalesCore.Properties;
using ScalesCore.Utils;
using ScalesCore.Win.Registry.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ScalesCore.Helpers
{
    /// <summary>
    /// Помощник приложения.
    /// </summary>
    public sealed class AppHelper : BaseViewModel
    {
        #region Design pattern "Singleton"

        private static readonly Lazy<AppHelper> _instance = new Lazy<AppHelper>(() => new AppHelper());
        public static AppHelper Instance => _instance.Value;
        private AppHelper() 
        {
            // Заполнить GUID из реестра.
            GuidSetValue(_reg.GetValue<string>(_reg.Root, Settings.Default.RegScalesUI, "GUID"));
        }

        #endregion

        #region Private fields and properties

        // Помощник реестра.
        private readonly RegistryHelper _reg = RegistryHelper.Instance;

        #endregion

        #region Public fields and properties - GUID

        /// <summary>
        /// Помощник SQL.
        /// </summary>
        public SqlHelper SqlHelp = SqlHelper.Instance;

        private Guid _guidValue;
        /// <summary>
        /// Глобальный идентификатор.
        /// </summary>
        public Guid GuidValue
        {
            get => _guidValue;
            set
            {
                _guidValue = value;
                OnPropertyChanged();
            }
        }

        private string _guidStatus;
        /// <summary>
        /// Статус.
        /// </summary>
        public string GuidStatus
        {
            get => _guidStatus;
            set
            {
                _guidStatus = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Public fields and properties

        private string _status;
        /// <summary>
        /// Статус.
        /// </summary>
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Мютекс проверки потворного запуска.
        /// </summary>
        public Mutex InstanceCheckMutex { get; set; }

        /// <summary>
        /// Получить описание проекта.
        /// </summary>
        /// <returns></returns>
        public string GetDescription(Assembly assembly)
        {
            string result = string.Empty;
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                AssemblyDescriptionAttribute descriptionAttribute = (AssemblyDescriptionAttribute)attributes[0];
                result = descriptionAttribute.Description;
            }
            return result;
        }

        /// <summary>
        /// Текущая версия.
        /// </summary>
        /// <param name="countDigits"></param>
        /// <param name="stringFormats"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public string GetCurrentVersion(EnumVerCountDigits countDigits, List<EnumStringFormat> stringFormats = null, Version version = null)
        {
            if (version == null)
                version = Assembly.GetExecutingAssembly().GetName().Version;
            string version1;
            string version2;
            string version3;
            string version4;
            if (stringFormats == null || stringFormats.Count == 0)
                stringFormats = new List<EnumStringFormat>() { EnumStringFormat.Use1, EnumStringFormat.Use2, EnumStringFormat.Use2 };

            EnumStringFormat formatMajor = stringFormats[0];
            EnumStringFormat formatMinor = EnumStringFormat.AsString;
            EnumStringFormat formatBuild = EnumStringFormat.AsString;
            EnumStringFormat formatRevision = EnumStringFormat.AsString;
            if (stringFormats.Count > 1)
                formatMinor = stringFormats[1];
            if (stringFormats.Count > 2)
                formatBuild = stringFormats[2];
            if (stringFormats.Count > 3)
                formatRevision = stringFormats[3];

            string major = GetCurrentVersionFormat(version.Major, formatMajor);
            string minor = GetCurrentVersionFormat(version.Minor, formatMinor);
            string build = GetCurrentVersionFormat(version.Build, formatBuild);
            string revision = GetCurrentVersionFormat(version.Revision, formatRevision);
            version4 = $"{major}.{minor}.{build}.{revision}";
            version3 = $"{major}.{minor}.{build}";
            version2 = $"{major}.{minor}";
            version1 = $"{major}";

            return countDigits == EnumVerCountDigits.Use1
                ? version1 : countDigits == EnumVerCountDigits.Use2
                ? version2 : countDigits == EnumVerCountDigits.Use3
                ? version3 : version4;
        }

        /// <summary>
        /// Подстрока текущей версии.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GetCurrentVersionSubString(string input)
        {
            string result = string.Empty;
            int idx = input.LastIndexOf('.');
            if (idx >= 0)
                result = input.Substring(0, idx);
            return result;
        }

        /// <summary>
        /// Форматировання подстрока текущей версии.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string GetCurrentVersionFormat(int input, EnumStringFormat format)
        {
            switch (format)
            {
                case EnumStringFormat.Use1:
                    return $"{input:D1}";
                case EnumStringFormat.Use2:
                    return $"{input:D2}";
                case EnumStringFormat.Use3:
                    return $"{input:D3}";
                case EnumStringFormat.Use4:
                    return $"{input:D4}";
            }
            return $"{input:D}";
        }

        /// <summary>
        /// Проверить повторный запуск.
        /// </summary>
        /// <returns></returns>
        public bool InstanceCheck()
        {
            InstanceCheckMutex = new Mutex(true, "ScalesUI", out bool result);
            return result;
        }

        /// <summary>
        /// Получить заголовок ПО.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="useShort"></param>
        /// <param name="version"></param>
        /// <param name="useGuid"></param>
        /// <returns></returns>
        public string GetMainFormText(Assembly assembly, bool useShort, Version version = null, bool useGuid = true)
        {
            string strGuid = useGuid ? $".GUID: { GuidToString() }" : string.Empty;
            if (useShort)
                return $@"{GetDescription(assembly)} {GetCurrentVersion(EnumVerCountDigits.Use3, null, version)}{strGuid}";
            else
                return $@"{GetDescription(assembly)}. {Assembly.GetExecutingAssembly().GetName().Name} {GetCurrentVersion(EnumVerCountDigits.Use3, null, version)}{strGuid}";
        }

        /// <summary>
        /// Задать новый размер формы.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="startPosition"></param>
        public void SetNewSize(Form form, FormStartPosition startPosition = FormStartPosition.CenterScreen)
        {
            if (form == null)
                return;

            if (Application.OpenForms.Count > 0)
            {
                form.Width = Application.OpenForms[0].Width;
                form.Height = Application.OpenForms[0].Height;
                form.Left = Application.OpenForms[0].Left;
                form.Top = Application.OpenForms[0].Top;
            }
            form.StartPosition = startPosition;
        }

        #endregion

        #region Public/Private methods - GUID

        /// <summary>
        /// Существует.
        /// </summary>
        /// <returns></returns>
        public bool GuidExists()
        {
            bool defValue = GuidValue.ToString().Equals("00000000-0000-0000-0000-000000000000", StringComparison.InvariantCultureIgnoreCase);
            if (GuidValue != null && !defValue)
                return true;
            return false;
        }

        /// <summary>
        /// Создать.
        /// </summary>
        /// <returns></returns>
        public Guid GuidCreate()
        {
            GuidValue = Guid.NewGuid();
            return GuidValue;
        }

        /// <summary>
        /// Задать значение.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GuidSetValue(string value)
        {
            try
            {
                bool result = Guid.TryParse(value, out Guid guid);
                GuidValue = guid;
                return result;
            }
            catch (Exception ex)
            {
                GuidStatus = $@"Ошибка назначения GUID! {ex.Message}";
            }
            return false;
        }

        /// <summary>
        /// В строку.
        /// </summary>
        /// <returns></returns>
        public string GuidToString()
        {
            return GuidValue.ToString().ToUpper();
        }

        #endregion

        #region Public methods - ScalesUI

        /// <summary>
        /// Сохранить настройки GUID в реестре.
        /// </summary>
        public void GuidRegSave()
        {
            _reg.CreateParameter(_reg.Root, Settings.Default.RegScalesUI, "GUID");
            _reg.SetValue(_reg.Root, Settings.Default.RegScalesUI, "GUID", GuidToString(), RegistryValueKind.String);
        }

        /// <summary>
        /// Каталог установки.
        /// </summary>
        public string InstallDir
        {
            get => _reg.GetValue<string>(_reg.Root, Settings.Default.RegScalesUI, "INSTALLDIR");
            set
            {
                // Создать разделы реестра.
                if (!_reg.Exists(_reg.Root, Settings.Default.RegVladimirStandardCorp))
                    _reg.CreateSubKey(_reg.Root, Settings.Default.RegVladimirStandardCorp);
                if (!_reg.Exists(_reg.Root, Settings.Default.RegScalesUI))
                    _reg.CreateSubKey(_reg.Root, Settings.Default.RegScalesUI);
                if (!_reg.Exists(_reg.Root, Settings.Default.RegScalesUISql))
                    _reg.CreateSubKey(_reg.Root, Settings.Default.RegScalesUISql);
                // Создать параметр.
                if (!_reg.Exists(_reg.Root, Settings.Default.RegScalesUI, "INSTALLDIR"))
                    _reg.CreateParameter(_reg.Root, Settings.Default.RegScalesUI, "INSTALLDIR");
                // Записать новое значение.
                if (value.EndsWith(@"\"))
                    value = value.Substring(0, value.Length - 1);
                _reg.SetValue(_reg.Root, Settings.Default.RegScalesUI, "INSTALLDIR", value, RegistryValueKind.String);
            }
        }

        #endregion

        #region Public methods - SQL

        /// <summary>
        /// Сохранить настройки SQL в реестре.
        /// </summary>
        public void SqlRegSave()
        {
            _reg.CreateParameter(_reg.Root, Settings.Default.RegScalesUISql, nameof(SqlHelp.Authentication.Server));
            _reg.SetValue(_reg.Root, Settings.Default.RegScalesUISql, nameof(SqlHelp.Authentication.Server), SqlHelp.Authentication.Server, RegistryValueKind.String);
            _reg.CreateParameter(_reg.Root, Settings.Default.RegScalesUISql, nameof(SqlHelp.Authentication.Database));
            _reg.SetValue(_reg.Root, Settings.Default.RegScalesUISql, nameof(SqlHelp.Authentication.Database), SqlHelp.Authentication.Database, RegistryValueKind.String);
            _reg.CreateParameter(_reg.Root, Settings.Default.RegScalesUISql, nameof(SqlHelp.Authentication.UserId));
            _reg.SetValue(_reg.Root, Settings.Default.RegScalesUISql, nameof(SqlHelp.Authentication.UserId), SqlHelp.Authentication.UserId, RegistryValueKind.String);
            _reg.CreateParameter(_reg.Root, Settings.Default.RegScalesUISql, nameof(SqlHelp.Authentication.Password));
            _reg.SetValue(_reg.Root, Settings.Default.RegScalesUISql, nameof(SqlHelp.Authentication.Password), SqlHelp.Authentication.Password, RegistryValueKind.String);
            _reg.CreateParameter(_reg.Root, Settings.Default.RegScalesUISql, nameof(SqlHelp.Authentication.IntegratedSecurity));
            _reg.SetValue(_reg.Root, Settings.Default.RegScalesUISql, nameof(SqlHelp.Authentication.IntegratedSecurity), SqlHelp.Authentication.IntegratedSecurity, RegistryValueKind.String);
        }

        /// <summary>
        /// Проверить SQL-подключение.
        /// </summary>
        /// <returns></returns>
        public bool SqlConCheck(EnumLanguage language)
        {
            if (string.IsNullOrEmpty(SqlHelp.Authentication.Server) || string.IsNullOrEmpty(SqlHelp.Authentication.Database))
            {
                Status = language == EnumLanguage.English ? @"Error connecting to SQL-server!" : "Ошибка настроек подключения к SQL-серверу!";
                return false;
            }

            if (!SqlHelp.Authentication.IntegratedSecurity && (string.IsNullOrEmpty(SqlHelp.Authentication.UserId) ||
                                                            string.IsNullOrEmpty(SqlHelp.Authentication.Password)))
            {
                Status = language == EnumLanguage.English ? @"Error connecting to SQL-server!" : "Ошибка настроек подключения к SQL-серверу!";
                return false;
            }

            SqlHelp.Open(EnumSettingsStorage.UseParams, SqlHelp.Authentication.Server, SqlHelp.Authentication.Database, SqlHelp.Authentication.IntegratedSecurity, SqlHelp.Authentication.UserId, SqlHelp.Authentication.Password);
            SqlHelp.OpenConnection(language);
            if (SqlHelp.Connection.State == System.Data.ConnectionState.Open)
            {
                Status = language == EnumLanguage.English ? @"Connection to SQL server completed successfully." : "Подключение к SQL-серверу выполнено успешно.";
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверить наличие GUID.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool SqlExistsGuid(string guid)
        {
            if (SqlHelp.Connection.State == System.Data.ConnectionState.Open)
            {
                Collection<Collection<object>> records = SqlHelp.SelectData(SqlUtils.QueryFindGuid, new Collection<string>() { "RESULT" }, 
                    new Collection<SqlParam>() { new SqlParam("GUID", guid) });
                foreach (Collection<object> rec in records)
                {
                    foreach (object field in rec)
                    {
                        if (field.ToString().Equals("TRUE", StringComparison.InvariantCultureIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion
    }
}
