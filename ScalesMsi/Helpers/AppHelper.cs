// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesMsi.Models;
using ScalesMsi.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

// ReSharper disable HollowTypeName
// ReSharper disable ClassTooBig

namespace ScalesMsi.Helpers
{
    /// <summary>
    /// Помощник приложения.
    /// </summary>
    internal class AppHelper : INotifyPropertyChanged
    {
        #region Design pattern "Lazy Singleton"

        private static AppHelper _instance;
        public static AppHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public AppHelper() { SetupDefault(); }

        public void SetupDefault()
        {
            try
            {
                IdStatus = string.Empty;
                IdValue = Convert.ToInt32(Properties.Settings.Default.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show((CurrentLocalization == EnumLocalization.English ? @"ID default error!" : "Ошибка назначения ID по-умолчанию!") +
                                Environment.NewLine + ex.Message);
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        #endregion

        #region Public helpers

        /// <summary>
        /// Помощник процессов.
        /// </summary>
        public readonly ProcHelper Proc = ProcHelper.Instance;
        /// <summary>
        /// Помощник SQL.
        /// </summary>
        public readonly SqlHelper Sql = SqlHelper.Instance;
        /// <summary>
        /// Помощник Windows.
        /// </summary>
        public readonly WinHelper Win = WinHelper.Instance;
        /// <summary>
        /// Помощник инфо Windows.
        /// </summary>
        public readonly WinInfoHelper WinInfo = WinInfoHelper.Instance;
        /// <summary>
        /// Помощник компонент.
        /// </summary>
        public readonly WixSharpHelper Wix = WixSharpHelper.Instance;
        public readonly WmiHelper Wmi = WmiHelper.Instance;

        #endregion

        #region Public fields and properties

        private int _idValue;
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int IdValue
        {
            get => _idValue;
            set
            {
                _idValue = value;
                OnPropertyRaised();
            }
        }

        private string _idStatus;
        /// <summary>
        /// Статус.
        /// </summary>
        public string IdStatus
        {
            get => _idStatus;
            set
            {
                _idStatus = value;
                OnPropertyRaised();
            }
        }

        /// <summary>
        /// Язык локализации.
        /// </summary>
        public EnumLocalization CurrentLocalization { get; set; } = EnumLocalization.Russian;

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
                OnPropertyRaised();
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
            var result = string.Empty;
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                var descriptionAttribute = (AssemblyDescriptionAttribute)attributes[0];
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
                stringFormats = new List<EnumStringFormat> { EnumStringFormat.Use1, EnumStringFormat.Use2, EnumStringFormat.Use2 };

            var formatMajor = stringFormats[0];
            var formatMinor = EnumStringFormat.AsString;
            var formatBuild = EnumStringFormat.AsString;
            var formatRevision = EnumStringFormat.AsString;
            if (stringFormats.Count > 1)
                formatMinor = stringFormats[1];
            if (stringFormats.Count > 2)
                formatBuild = stringFormats[2];
            if (stringFormats.Count > 3)
                formatRevision = stringFormats[3];

            var major = GetCurrentVersionFormat(version.Major, formatMajor);
            var minor = GetCurrentVersionFormat(version.Minor, formatMinor);
            var build = GetCurrentVersionFormat(version.Build, formatBuild);
            var revision = GetCurrentVersionFormat(version.Revision, formatRevision);
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
            var result = string.Empty;
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
        /// <param name="useId"></param>
        /// <returns></returns>
        public string GetMainFormText(Assembly assembly, bool useShort, Version version = null, bool useId = true)
        {
            var strId = useId ? $".ID: { IdToString() }" : string.Empty;
            if (useShort)
                return $@"{GetDescription(assembly)} {GetCurrentVersion(EnumVerCountDigits.Use3, null, version)}{strId}";
            else
                return $@"{GetDescription(assembly)}. {Assembly.GetExecutingAssembly().GetName().Name} {GetCurrentVersion(EnumVerCountDigits.Use3, null, version)}{strId}";
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

        #region Public methods - ID

        /// <summary>
        /// Существует.
        /// </summary>
        /// <returns></returns>
        public bool IdExists()
        {
            //var defValue = IdValue.ToString().Equals("0", StringComparison.InvariantCultureIgnoreCase);
            //if (IdValue > 0 && !defValue)
            //    return true;
            //return false;
            return IdValue > 0;
        }

        /// <summary>
        /// Создать.
        /// </summary>
        /// <returns></returns>
        public int IdCreate()
        {
            IdValue = 0;
            return IdValue;
        }

        /// <summary>
        /// Задать значение.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void IdSetValue(string value)
        {
            try
            {
                IdStatus = string.Empty;
                int.TryParse(value, out var id);
                IdValue = id;
            }
            catch (Exception ex)
            {
                MessageBox.Show((CurrentLocalization == EnumLocalization.English ? @"ID setup error!" : "Ошибка назначения ID!") +
                    Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// В строку.
        /// </summary>
        /// <returns></returns>
        public string IdToString()
        {
            return IdValue.ToString();
        }

        #endregion

        #region Public methods - SQL

        /// <summary>
        /// Проверить SQL-подключение.
        /// </summary>
        /// <returns></returns>
        public bool SqlConCheck(EnumLocalization language)
        {
            if (string.IsNullOrEmpty(Sql.Authentication.Server) || string.IsNullOrEmpty(Sql.Authentication.Database))
            {
                Status = language == EnumLocalization.English ? @"Error connecting to SQL-server!" : "Ошибка настроек подключения к SQL-серверу!";
                return false;
            }

            if (!Sql.Authentication.IntegratedSecurity && (string.IsNullOrEmpty(Sql.Authentication.UserId) ||
                                                            string.IsNullOrEmpty(Sql.Authentication.Password)))
            {
                Status = language == EnumLocalization.English ? @"Error connecting to SQL-server!" : "Ошибка настроек подключения к SQL-серверу!";
                return false;
            }

            Sql.Open(EnumSettingsStorage.UseParams, Sql.Authentication.Server, Sql.Authentication.Database, Sql.Authentication.IntegratedSecurity, Sql.Authentication.UserId, Sql.Authentication.Password);
            Sql.OpenConnection(language);
            if (Sql.Connection.State == System.Data.ConnectionState.Open)
            {
                Status = language == EnumLocalization.English ? @"Connection to SQL server completed successfully." : "Подключение к SQL-серверу выполнено успешно.";
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверить наличие ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SqlExistsId(string id)
        {
            if (Sql.Connection.State == System.Data.ConnectionState.Open)
            {
                var records = Sql.SelectData(SqlUtils.QueryFindId, new Collection<string> { "RESULT" },
                    new Collection<SqlParam> { new SqlParam("ID", id) });
                foreach (var rec in records)
                {
                    foreach (var field in rec)
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

        /// <summary>
        /// Получить список ID.
        /// </summary>
        /// <returns></returns>
        public string[] SqlGetIds()
        {
            var result = new string[0];
            if (Sql.Connection.State == System.Data.ConnectionState.Open)
            {
                var records = Sql.SelectData(SqlUtils.QueryGetIds, new Collection<string> { "Id" });
                result = new string[records.Count];
                for (var i = 0; i < records.Count; i++)
                {
                    result[i] = records[i][0].ToString();
                }
            }
            return result;
        }

        #endregion

        #region Public methods - Dirs

        /// <summary>
        /// Перенести файлы.
        /// </summary>
        /// <param name="dirDest"></param>
        /// <param name="files"></param>
        public void DirMoveFiles(string dirDest, Collection<string> files)
        {
            try
            {
                if (dirDest.EndsWith(@"\"))
                    dirDest = dirDest.Substring(0, dirDest.Length - 1);
                if (!Directory.Exists(dirDest))
                    Directory.CreateDirectory(dirDest);

                foreach (var file in files)
                {
                    if (File.Exists(Strings.DirProgramScalesUI + $@"\{file}"))
                    {
                        if (File.Exists(dirDest + $@"\{file}"))
                            File.Delete(dirDest + $@"\{file}");
                        File.Move(Strings.DirProgramScalesUI + $@"\{file}", dirDest + $@"\{file}");
                    }
                }
                foreach (var file in files)
                {
                    if (File.Exists(Strings.DirProgramScalesUI + $@"\{file}"))
                        File.Delete(Strings.DirProgramScalesUI + $@"\{file}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((CurrentLocalization == EnumLocalization.English ? @"Install error!" : "Ошибка установки!") +
                    Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Имя файла установки драйвера.
        /// </summary>
        /// <param name="winVersion">Версия WiNdows</param>
        /// <returns></returns>
        public string DriverGetFileName(EnumWinVersion winVersion)
        {
            var result = string.Empty;
            if (winVersion == EnumWinVersion.Win7x64)
                result = "VCP_V1.5.0_Setup_W7_x64_64bits.exe";
            if (winVersion == EnumWinVersion.Win7x32)
                result = "VCP_V1.5.0_Setup_W7_x86_32bits.exe";
            if (winVersion == EnumWinVersion.Win10x64)
                result = "VCP_V1.5.0_Setup_W8_x64_64bits.exe";
            if (winVersion == EnumWinVersion.Win10x32)
                result = "VCP_V1.5.0_Setup_W8_x86_32bits.exe";
            if (!string.IsNullOrEmpty(result))
                return result;
            else
                throw new ArgumentException("Ошибка версии Windows!");
        }

        /// <summary>
        /// Распаковать архивы драйверов.
        /// </summary>
        public void DriverExtract()
        {
            try
            {
                foreach (var arch in Strings.ListMassaDrivers)
                {
                    if (arch.EndsWith(".zip"))
                    {
                        var path = Strings.DirSourceMassa + @"\" + arch;
                        var fi = new FileInfo(path);
                        if (!fi.Exists)
                        {
                            MessageBox.Show($@"Файл не найден{Environment.NewLine}{fi.FullName}");
                            return;
                        }
                        // Зачистить локальный каталог.
                        if (Directory.Exists(Strings.DirProgramMassaDrivers))
                            Directory.Delete(Strings.DirProgramMassaDrivers, true);
                        // Создать локальный каталог.
                        Directory.CreateDirectory(Strings.DirProgramMassaDrivers);
                        var localPath = $@"{Strings.DirProgramMassaDrivers}\{arch}";
                        // Скопировать архив.
                        File.Copy(fi.FullName, localPath);
                        // Распаковать архив.
                        ZipFile.ExtractToDirectory(localPath, Strings.DirProgramMassaDrivers);
                        // Удалить архив.
                        File.Delete(localPath);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                if (!(ex.InnerException is null))
                    msg += Environment.NewLine + ex.InnerException.Message;
                MessageBox.Show(@"Ошибка распаковки архива." + Environment.NewLine + msg);
            }
        }

        /// <summary>
        /// Проверить установку драйвера.
        /// </summary>
        /// <param name="silent"></param>
        /// <returns></returns>
        public bool CheckDriverInstall(EnumSilentUI silent)
        {
            var result = false;
            foreach (var item in Win.SearchingSoftware(EnumWinProvider.Registry, Strings.AppDriverName, EnumStringTemplate.Contains))
            {
                if (item.Vendor.Equals(Strings.AppDriverVendor, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (silent == EnumSilentUI.False)
                        MessageBox.Show(CurrentLocalization == EnumLocalization.English ? "Driver is already installed." : "Драйвер уже установлен.");
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Установить драйвера.
        /// </summary>
        public void DriverInstall(EnumSilentUI silent)
        {
            // Определить имя файла драйвера.
            string driverFileName;
            // Windows 8 - 10.
            if ((WinInfo.MajorVersion == 6 && WinInfo.MinorVersion >= 2) || (WinInfo.MajorVersion > 6))
            {
                driverFileName = DriverGetFileName(Environment.Is64BitOperatingSystem ? EnumWinVersion.Win10x64 : EnumWinVersion.Win10x32);
            }
            else
            {
                driverFileName = DriverGetFileName(Environment.Is64BitOperatingSystem ? EnumWinVersion.Win7x64 : EnumWinVersion.Win7x32);
            }

            // Проверить файл.
            var driverPath = Strings.DirProgramMassaDrivers + @"\" + driverFileName;
            var fi = new FileInfo(driverPath);
            if (!fi.Exists)
            {
                var msg = CurrentLocalization == EnumLocalization.English ? @"File STM-driver not found!" : @"Файл STM-драйвера не обнаружен!";
                MessageBox.Show(msg);
                return;
            }

            // Запустить установку драйвера.
            var isProcRun = true;
            if (silent == EnumSilentUI.False)
            {
                var question = CurrentLocalization == EnumLocalization.English ? @"Install STM-driver?" : @"Установить STM-драйвер?";
                isProcRun = MessageBox.Show(question, GetDescription(Assembly.GetExecutingAssembly()),
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
            }
            if (isProcRun)
            {
                Proc.Run(driverPath, string.Empty, true, ProcessWindowStyle.Normal, true, false);
            }
        }

        /// <summary>
        /// Удаление каталога.
        /// </summary>
        /// <param name="components"></param>
        public void DirClear(EnumComponents components)
        {
            switch (components)
            {
                case EnumComponents.ScalesUI:
                    DirClear(Strings.DirProgramScalesUIDocs, null);
                    DirClear(Strings.DirProgramScalesUI, Strings.ListScalesBin);
                    DirClear(Strings.DirProgramScalesUIManuals, null);
                    DirClear(Strings.DirProgramScalesUI, null);
                    DirClear(Strings.DirProgramMassaDrivers, Strings.ListMassaDriversForDelete);
                    break;
                case EnumComponents.MassaDriver:
                    DirClear(Strings.DirProgramMassaDrivers, Strings.ListMassaDriversForDelete);
                    break;
                case EnumComponents.ScalesUIBinaries:
                    DirClear(Strings.DirProgramScalesUI, Strings.ListScalesBin);
                    break;
                case EnumComponents.ScalesUIDocs:
                    DirClear(Strings.DirProgramScalesUIDocs, Strings.ListScalesDocs);
                    break;
                case EnumComponents.ScalesUIManuals:
                    DirClear(Strings.DirProgramScalesUIManuals, Strings.ListScalesManuals);
                    break;
                case EnumComponents.LabelPrint:
                    DirClear(Strings.DirProgramLabelPrint, Strings.ListLabelPrint);
                    break;
                case EnumComponents.TapangaMaha:
                    DirClear(Strings.DirProgramTapangaMaha, Strings.ListTapangaMaha);
                    break;
            }
        }

        /// <summary>
        /// Удаление каталога.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="files"></param>
        public void DirClear(string dir, List<string> files)
        {
            if (Directory.Exists(dir))
            {
                foreach (var subDir in Directory.GetDirectories(dir))
                {
                    FilesClear(subDir, files);
                    DirClear(subDir, files);
                    Directory.Delete(subDir);
                }
                FilesClear(dir, files);
                foreach (var subDir in Directory.GetDirectories(dir))
                {
                    Directory.Delete(subDir, true);
                }
                Directory.Delete(dir, true);
            }
        }

        /// <summary>
        /// Удаление файлов.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="files"></param>
        public void FilesClear(string dir, List<string> files)
        {
            try
            {
                if (Directory.Exists(dir))
                {
                    if (files?.Count > 0)
                    {
                        foreach (var file in Directory.GetFiles(dir))
                        {
                            if (files.Contains(Path.GetFileName(file)))
                            {
                                File.Delete(file);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    (CurrentLocalization == EnumLocalization.English
                        ? "File deletion error!" + Environment.NewLine + "Try to close the file and run the installer with elevated access rights."
                        : "Ошибка удаления файла!" + Environment.NewLine + "Попробуйте закрыть файл и запустить инсталлятор под повышенными правами доступа.") +
                    Environment.NewLine + ex.Message);
            }
        }

        #endregion
    }
}
