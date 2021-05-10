// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Win32;
using ScalesCore.Models;
using ScalesCore.Properties;
using ScalesCore.Win.Proc.Helpers;
using ScalesCore.Win.Registry.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Windows.Forms;

namespace ScalesCore.Helpers
{
    /// <summary>
    /// Помощник настроек.
    /// </summary>
    public sealed class SettingsHelper
    {
        #region Design pattern "Singleton"

        private static readonly Lazy<SettingsHelper> _instance = new Lazy<SettingsHelper>(() => new SettingsHelper());
        public static SettingsHelper Instance => _instance.Value;
        private SettingsHelper()
        {
            CurrentLanguage = EnumLanguage.Russian;
        }

        #endregion

        #region Private helpers

        // Помощник приложения.
        private readonly AppHelper _app = AppHelper.Instance;
        // Помощник коллекций.
        private readonly CollectionsHelper _collections = CollectionsHelper.Instance;
        // Помощник процессов.
        private readonly ProcHelper _proc = ProcHelper.Instance;
        // Помощник реестра.
        private readonly RegistryHelper _reg = RegistryHelper.Instance;
        // Помощник Windows.
        private readonly WinHelper _win = WinHelper.Instance;
        // Помощник инфо Windows.
        private readonly WinInfoHelper _winInfo = WinInfoHelper.Instance;

        #endregion

        #region Public fields and properties

        /// <summary>
        /// Главный каталог
        /// </summary>
        public string DirMain { get; set; }

        /// <summary>
        /// Каталог документации.
        /// </summary>
        public string DirDocs { get; set; }

        /// <summary>
        /// Каталог драйверов.
        /// </summary>
        public string DirDrivers { get; set; }

        /// <summary>
        /// Каталог шрифтов.
        /// </summary>
        public string DirFonts { get; set; }

        /// <summary>
        /// Каталог руководств.
        /// </summary>
        public string DirManuals { get; set; }

        /// <summary>
        /// Язык локализации.
        /// </summary>
        public EnumLanguage CurrentLanguage
        {
            get
            {
                var lang = _reg.GetValue<string>(_reg.Root, Settings.Default.RegScalesUI, "Language");
                switch (lang)
                {
                    case "Russian":
                        return EnumLanguage.Russian;
                }
                return EnumLanguage.English;
            }
            set
            {
                _reg.CreateParameter(_reg.Root, Settings.Default.RegScalesUI, "Language");
                _reg.SetValue(_reg.Root, Settings.Default.RegScalesUI, "Language", value, RegistryValueKind.String);
            }
        }

        /// <summary>
        /// Исходный каталог документации.
        /// </summary>
        public string DirSourceDocs { get; } = @"Docs\";

        /// <summary>
        /// Исходный каталог драйвера STM.
        /// </summary>
        public string DirSourceDrivers { get; } = @"..\..\Resources\Drivers\";

        /// <summary>
        /// Исходный каталог руководств.
        /// </summary>
        public string DirSourceManuals { get; } = @"..\..\Resources\Manuals\";

        /// <summary>
        /// Исходный каталог ПО.
        /// </summary>
        public string DirSource
        {
            get
            {
#if DEBUG
                return @"..\ScalesUI2\bin\Debug\";
#else 
                return @"..\ScalesUI2\bin\Release\";
#endif
            }
        }

        /// <summary>
        /// Файл библиотеки log4net.
        /// </summary>
        /// <returns></returns>
        public string Log4netDll =>
#if DEBUG
            @"..\ScalesUI2\bin\Debug\log4net.dll";
#else 
            @"..\ScalesUI2\bin\Release\log4net.dll";
#endif

        /// <summary>
        /// Файл библиотеки ядра весовой платформы.
        /// </summary>
        /// <returns></returns>
        public string ScalesCoreDll =>
#if DEBUG
            @"..\ScalesCore\bin\Debug\ScalesCore.dll";
#else 
            @"..\ScalesCore\bin\Release\ScalesCore.dll";
#endif

        /// <summary>
        /// Меню Windows.
        /// </summary>
        public string MenuScalesUI { get; } = @"%ProgramMenu%\VladimirStandardCorp\ScalesUI";

        /// <summary>
        /// Админ-права.
        /// </summary>
        public bool IsAdmin =>
            System.Net.Dns.GetHostName().Equals("DEV-MAIN") ||  // Домашний ПК Морозов Д.В.
            System.Net.Dns.GetHostName().Equals("PC208") ||     // Рабочий ПК Морозов Д.В.
            System.Net.Dns.GetHostName().Equals("PC0147");      // Рабочий ПК Ивакин Д.В.

        #endregion

        #region Public methods

        /// <summary>
        /// Настроить и проверить каталоги.
        /// </summary>
        /// <param name="installDir"></param>
        /// <param name="silentUI"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public bool SetupAndCheckDirs(string installDir, EnumSilentUI silentUI, EnumLanguage language)
        {
            if (string.IsNullOrEmpty(installDir))
                return false;

            DirDocs = string.Empty;
            DirDrivers = string.Empty;
            DirFonts = string.Empty;
            DirManuals = string.Empty;
            DirMain = installDir + (installDir.EndsWith(@"\") ? "" : @"\");

            if (!Directory.Exists(DirMain))
            {
                var message = language == EnumLanguage.English ? $@"Directory '{DirMain}' not exists!" : $@"Каталог '{DirMain}' не существует!";
                Console.WriteLine(message);
                if (silentUI == EnumSilentUI.False)
                    MessageBox.Show(message);
                DirMain = string.Empty;
                return false;
            }

            Environment.CurrentDirectory = DirMain;
            DirDocs = DirMain + @"Docs";
            DirDrivers = DirMain + @"Drivers";
            DirFonts = DirMain + @"frx";
            DirManuals = DirMain + @"Manuals";

            return true;
        }

        /// <summary>
        /// Установить.
        /// </summary>
        public EnumResult DirCreate()
        {
            try
            {
                // Создать каталоги и перенести файлы.
                DirCreateAndMoveFiles(DirDocs, _collections.Docs);
                DirCreateAndMoveFiles(DirManuals, _collections.Manuals);
                DirCreateAndMoveFiles(DirDrivers, _collections.DriversArchives);

                Console.WriteLine(@"Install complete.");
                return EnumResult.Good;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Install error: " + ex.Message);
            }
            return EnumResult.Error;
        }

        /// <summary>
        /// Создать каталоги и перенести файлы.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="files"></param>
        public void DirCreateAndMoveFiles(string dir, Collection<string> files)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                Console.WriteLine(@"Directory created.");
            }

            foreach (var file in files)
            {
                if (File.Exists(DirMain + @"\" + file))
                {
                    File.Move(DirMain + @"\" + file, dir + @"\" + file);
                    Console.WriteLine($@"File '{file}' moved to '{dir}'.");
                }
            }
        }

        /// <summary>
        /// Распаковать архивы драйверов.
        /// </summary>
        public void ExtractDrivers()
        {
            foreach (var arch in _collections.DriversArchives)
            {
                if (arch.EndsWith(".zip"))
                {
                    if (!File.Exists(DirDrivers + @"\" + arch))
                        return;
                    // Распаковать архивы.
                    ZipFile.ExtractToDirectory(DirDrivers + @"\" + arch, DirDrivers);
                    // Удалить архивы.
                    File.Delete(DirDrivers + @"\" + arch);
                }
            }
        }

        /// <summary>
        /// Установить драйвера.
        /// </summary>
        /// <param name="assembly"></param>
        public void InstallDrivers(Assembly assembly)
        {
            // Определить имя файла драйвера.
            string driverFileName;
            // Windows 8 - 10.
            if ((_winInfo.MajorVersion == 6 && _winInfo.MinorVersion >= 2) || (_winInfo.MajorVersion > 6))
            {
                driverFileName = _collections.GetDriverFileName(Environment.Is64BitOperatingSystem ? EnumWinVersion.Win10x64 : EnumWinVersion.Win10x32);
            }
            // Windows 7.
            //else if (_winInfo.MajorVersion == 6 && _winInfo.MinorVersion == 1)
            else
            {
                driverFileName = _collections.GetDriverFileName(Environment.Is64BitOperatingSystem ? EnumWinVersion.Win7x64 : EnumWinVersion.Win7x32);
            }

            // Проверить установку драйвера.
            if (_win.SearchingSoftware(EnumWinProvider.Registry, "Virtual Comport Driver", EnumStringTemplate.Equals).Vendor.
                Equals("STMicroelectronics", StringComparison.InvariantCultureIgnoreCase))
                return;

            // Запустить установку драйвера.
            if (!string.IsNullOrEmpty(driverFileName))
            {
                if (MessageBox.Show(@"Драйвер весов не обнаружен. Установить?", _app.GetDescription(assembly), MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    //MessageBox.Show(DirDrivers + @"\" + driverFileName);
                    _proc.Run(DirDrivers + @"\" + driverFileName, string.Empty, true, ProcessWindowStyle.Normal, true);
                }
            }
        }

        /// <summary>
        /// Удалить.
        /// </summary>
        public EnumResult DirClear()
        {
            try
            {
                // Документация.
                DirClear(DirDocs);
                // Драйвера.
                DirClear(DirDrivers);
                // Шрифты.
                DirClear(DirFonts);
                // Руководства.
                DirClear(DirManuals);

                Console.WriteLine(@"Uninstall complete.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Uninstall error: " + ex.Message);
            }
            return EnumResult.Error;
        }

        /// <summary>
        /// Очистка и удаление каталога.
        /// </summary>
        /// <param name="dir"></param>
        public void DirClear(string dir)
        {
            if (Directory.Exists(dir))
            {
                foreach (var dirIn in Directory.GetDirectories(dir))
                {
                    DirClear(dirIn);
                }
                foreach (var file in Directory.GetFiles(dir))
                {
                    Console.WriteLine($@"File '{file}' deleted from '{dir}'.");
                    File.Delete(file);
                }
                Directory.Delete(dir);
                Console.WriteLine($@"Directory '{dir}' cleared.");
            }
        }

        /// <summary>
        /// Полное имя конфигурационного файла в "C:\Program Files (x86)\".
        /// </summary>
        /// <returns></returns>
        public string GetScalesConfigFileName()
        {
            return $@"{DirMain}ScalesUI.exe.config";
        }

        #endregion
    }
}
