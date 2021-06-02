// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesMsi.Properties;
using ScalesMsi.Utils;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using WixSharp;
using WixSharp.CommonTasks;

// ReSharper disable HollowTypeName
// ReSharper disable InconsistentNaming

namespace ScalesMsi.Helpers
{
    /// <summary>
    /// Помощник компонент.
    /// </summary>
    internal class WixSharpHelper
    {
        #region Design pattern "Lazy Singleton"

        private static WixSharpHelper _instance;
        public static WixSharpHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public WixSharpHelper() { SetupDefault(); }

        public void SetupDefault()
        {
            DefaultFeatureLabelPrint();
            DefaultFeatureScalesUI();
            DefaultFeatureTapangaMaha();
            DefaultFeatureMassa();
        }

        #endregion

        #region Public fields and properties - ScalesUI features

        /// <summary>
        /// Опция установщика Печать этикеток.
        /// </summary>
        public Feature FeatureScalesUI { get; private set; }

        /// <summary>
        /// Опция установщика Документация.
        /// </summary>
        public Feature FeatureScalesUIDocs { get; private set; }

        /// <summary>
        /// Опция установщика Руководства.
        /// </summary>
        public Feature FeatureScalesUIManuals { get; private set; }

        /// <summary>
        /// Опция установщика Драйвер STM.
        /// </summary>
        public Feature FeatureMassaDriver { get; private set; }

        #endregion

        #region Public fields and properties - TapangaMaha features

        /// <summary>
        /// Опция установщика TapangaMaha.
        /// </summary>
        public Feature FeatureTapangaMaha { get; private set; }

        #endregion

        #region Public fields and properties - Massa features

        /// <summary>
        /// Опция установщика Весовой терминал.
        /// </summary>
        public Feature FeatureMassaScalesTerminal { get; private set; }
        /// <summary>
        /// Опция установщика Massa-K.
        /// </summary>
        public Feature FeatureMassa { get; private set; }

        #endregion

        #region Public fields and properties - LabelPrint features

        /// <summary>
        /// Опция установщика LabelPrint.
        /// </summary>
        public Feature FeatureLabelPrint { get; private set; }

        /// <summary>
        /// Опция установщика Документация LabelPrint.
        /// </summary>
        public Feature FeatureLabelPrintDocs { get; private set; }

        #endregion

        #region Public fields and properties

        /// <summary>
        /// Аргементы WinSharp.
        /// </summary>
        public SetupEventArgs Args { get; set; } = default;

        #endregion

        #region Public methods - Features

        public void DefaultFeatureLabelPrint()
        {
            FeatureLabelPrintDocs = new Feature("Documentation", "Документация LabelPrint", false, true)
            { Id = new Id("LabelPrintDocumentation") };

            FeatureLabelPrint = new Feature("LabelPrint", "WPF печать этикеток", false, true)
            {
                Id = new Id("LabelPrint"),
                //ConfigurableDir = "INSTALLDIR",
                Display = FeatureDisplay.expand,
                Children = new List<Feature>()
                {
                    FeatureLabelPrintDocs,
                },
            };
        }

        public void DefaultFeatureScalesUI()
        {
            FeatureScalesUIDocs = new Feature("Documentation", "Документация ScalesUI", false, true)
            {
                Id = new Id("ScalesUIDocumentation"),
            };
            FeatureScalesUIManuals = new Feature("Manuals", "Руководства ScalesUI", false, true)
            {
                Id = new Id("ScalesUIManuals"),
            };
            FeatureScalesUI = new Feature("ScalesUI", "Печать этикеток", false, true)
            {
                Id = new Id("ScalesUI"),
                //ConfigurableDir = "INSTALLDIR",
                Display = FeatureDisplay.expand,
                Children = new List<Feature>()
                {
                    FeatureScalesUIDocs,
                    FeatureScalesUIManuals,
                },
            };
        }

        public void DefaultFeatureTapangaMaha()
        {
            FeatureTapangaMaha = new Feature("TapangaMaha", "TapangaMaha", false, true)
            {
                Id = new Id("TapangaMaha"),
                //ConfigurableDir = "INSTALLDIR",
                Display = FeatureDisplay.expand,
                Children = new List<Feature>()
                {
                    //
                },
            };
        }

        public void DefaultFeatureMassa()
        {
            FeatureMassaDriver = new Feature("MassaDriver", "STM-драйвер (Virtual COM Port)", true, true)
            {
                Id = new Id("MassaDriver"),
            };
            FeatureMassaScalesTerminal = new Feature("MassaScalesTerminal", "Весовой терминал 100", true, true)
            {
                Id = new Id("MassaScalesTerminal"),
            };
            FeatureMassa = new Feature("Massa-K", "Масса-К", true, true)
            {
                Id = new Id("Massa"),
                //ConfigurableDir = "INSTALLDIR",
                Display = FeatureDisplay.expand,
                Children = new List<Feature>()
                {
                    FeatureMassaDriver,
                    FeatureMassaScalesTerminal,
                },
            };
        }

        /// <summary>
        /// Задать список компонент.
        /// </summary>
        /// <param name="features"></param>
        public void SetSettingsFeatures(List<string> features)
        {
            Settings.Default.Features = new StringCollection();
            features.ForEach(item => Settings.Default.Features.Add(item));
        }

        /// <summary>
        /// Получить список компонент.
        /// </summary>
        public List<string> GetSettingsFeatures()
        {
            var result = new List<string>();
            foreach (var feature in Settings.Default.Features)
            {
                result.Add(feature);
            }
            return result;
        }

        #endregion

        #region Public methods - Properties

        /// <summary>
        /// Добавить свойства.
        /// </summary>
        /// <param name="project"></param>
        public List<Property> GetProperties()
        {
            var propInstallDir = new Property(new Id(Strings.GetFeatureId(Strings.PropInstallDir)), Strings.PropInstallDir, Strings.DirProgram);
            var propLabelPrint = new Property(new Id(Strings.GetFeatureId(Strings.PropInstallDirLabelPrint)), Strings.PropInstallDirLabelPrint, Strings.DirProgramLabelPrint);
            var propLabelPrintDocs = new Property(new Id(Strings.GetFeatureId(Strings.PropInstallDirLabelPrintDocs)), Strings.PropInstallDirLabelPrintDocs, Strings.DirProgramLabelPrintDocs);
            var propScalesUI = new Property(new Id(Strings.GetFeatureId(Strings.PropInstallDirScalesUI)), Strings.PropInstallDirScalesUI, Strings.DirProgramScalesUI);
            var propScalesUIDocs = new Property(new Id(Strings.GetFeatureId(Strings.PropInstallDirScalesUIDocs)), Strings.PropInstallDirScalesUIDocs, Strings.DirProgramScalesUIDocs);
            var propScalesUIDrivers = new Property(new Id(Strings.GetFeatureId(Strings.PropInstallDirMassaDriver)), Strings.PropInstallDirMassaDriver, Strings.DirProgramMassaDrivers);
            var propScalesUIManuals = new Property(new Id(Strings.GetFeatureId(Strings.PropInstallDirScalesUIManuals)), Strings.PropInstallDirScalesUIManuals, Strings.DirProgramScalesUIManuals);
            var propTapangaMaha = new Property(new Id(Strings.GetFeatureId(Strings.PropInstallDirTapangaMaha)), Strings.PropInstallDirTapangaMaha, Strings.DirProgramTapangaMaha);

            return new List<Property> { propInstallDir, propLabelPrint, propLabelPrintDocs, propScalesUI, propScalesUIDocs, propScalesUIDrivers, propScalesUIManuals, propTapangaMaha };
        }

        #endregion

        #region Public methods - Dirs

        /// <summary>
        /// Каталог ScalesUI.
        /// </summary>
        /// <returns></returns>
        public Dir GetDirScalesUI()
        {
            // Основные компоненты.
            var result = new Dir()
            {
                Name = @"ScalesUI",
                Id = new Id(Strings.PropInstallDirScalesUI),
                Feature = FeatureScalesUI,
            };
            // Ярлыки.
            var fileShortcut1 = new FileShortcut(FeatureScalesUI, @"ScalesUI", $@"{Strings.MenuVladimirStandardCorp}") { WorkingDirectory = @"INSTALLDIR" };
            var fileShortcut2 = new FileShortcut(FeatureScalesUI, @"ScalesUI", @"%Desktop%") { WorkingDirectory = @"INSTALLDIR" };
            var fileShortcut3 = new FileShortcut(FeatureScalesUI, @"ScalesUI", @"%Startup%") { WorkingDirectory = @"INSTALLDIR" };
            // Главный файл.
            result.AddFile(new File(FeatureScalesUI, Strings.DirSourceScalesUI + @"\ScalesUI.exe", fileShortcut1, fileShortcut2, fileShortcut3));
            // Бинарные файлы.
            foreach (var item in Strings.ListScalesBin)
            {
                result.AddFile(new File(FeatureScalesUI, Strings.DirSourceScalesUI + $@"\{item}"));
            }
            return result;
        }

        /// <summary>
        /// Меню удаления.
        /// </summary>
        /// <returns></returns>
        public Dir GetDirScalesUIUninstall()
        {
            return new Dir(Strings.MenuVladimirStandardCorp,
                new ExeFileShortcut(FeatureScalesUI, "Uninstall VladimirStandardCorp", "[System64Folder]msiexec.exe", "/x [ProductCode]"));
        }

        /// <summary>
        /// Каталог документации ScalesUI.
        /// </summary>
        /// <returns></returns>
        public Dir GetDirScalesUIDocs()
        {
            var result = new Dir()
            {
                Name = @"ScalesUIDocs",
                Id = new Id(Strings.PropInstallDirScalesUIDocs),
                Feature = FeatureScalesUIDocs,
            };
            foreach (var item in Strings.ListScalesDocs)
            {
                result.AddFile(new File(FeatureScalesUIDocs, Strings.DirSourceScalesUIDocs + $@"\{item}"));
            }
            return result;
        }

        /// <summary>
        /// Каталог драйверов.
        /// </summary>
        /// <returns></returns>
        public Dir GetDirMassaDriver()
        {
            var result = new Dir()
            {
                Name = @"MassaDriver",
                Id = new Id(Strings.PropInstallDirMassaDriver),
                Feature = FeatureMassaDriver,
            };
            return result;
        }

        /// <summary>
        /// Каталог руководств ScalesUI.
        /// </summary>
        /// <returns></returns>
        public Dir GetDirScalesUIManuals()
        {
            var result = new Dir()
            {
                Name = @"ScalesUIManuals",
                Id = new Id(Strings.PropInstallDirScalesUIManuals),
                Feature = FeatureScalesUIManuals,
            };
            foreach (var item in Strings.ListScalesManuals)
            {
                result.AddFile(new File(FeatureScalesUIManuals, Strings.DirSourceScalesUIManuals + $@"\{item}"));
            }
            return result;
        }

        /// <summary>
        /// Каталог Масса-К.
        /// </summary>
        /// <returns></returns>
        public Dir GetDirMassa()
        {
            // Основные компоненты.
            var result = new Dir()
            {
                Name = @"Massa-K",
                Id = new Id(Strings.PropInstallDirMassa),
                Feature = FeatureMassa,
            };
            // Главный файл.
            //result.AddFile(new File(FeatureMassa, Strings.DirSourceMassa + @"\ScalesTerminalSetup_V1.3.191.exe"));
            // Бинарные файлы.
            //foreach (var item in Strings.ListTapangaMaha)
            //{
            //    result.AddFile(new File(FeatureMassa, Strings.DirSourceMassa + $@"\{item}"));
            //}
            return result;
        }

        /// <summary>
        /// Каталог TapangaMaha.
        /// </summary>
        /// <returns></returns>
        public Dir GetDirTapangaMaha()
        {
            // Основные компоненты.
            var result = new Dir()
            {
                Name = @"TapangaMaha",
                Id = new Id(Strings.PropInstallDirTapangaMaha),
                Feature = FeatureTapangaMaha,
            };
            // Ярлыки.
            var fileShortcut1 = new FileShortcut(FeatureTapangaMaha, @"TapangaMaha", $@"{Strings.MenuVladimirStandardCorp}") { WorkingDirectory = @"INSTALLDIR" };
            var fileShortcut2 = new FileShortcut(FeatureTapangaMaha, @"TapangaMaha", @"%Desktop%") { WorkingDirectory = @"INSTALLDIR" };
            var fileShortcut3 = new FileShortcut(FeatureTapangaMaha, @"TapangaMaha", @"%Startup%") { WorkingDirectory = @"INSTALLDIR" };
            // Главный файл.
            result.AddFile(new File(FeatureTapangaMaha, Strings.DirSourceTapangaMaha + @"\TapangaMaha.exe", fileShortcut1, fileShortcut2, fileShortcut3));
            // Бинарные файлы.
            foreach (var item in Strings.ListTapangaMaha)
            {
                result.AddFile(new File(FeatureTapangaMaha, Strings.DirSourceTapangaMaha + $@"\{item}"));
            }
            return result;
        }

        /// <summary>
        /// Меню удаления.
        /// </summary>
        /// <returns></returns>
        public Dir GetDirTapangaMahaUninstall()
        {
            return new Dir(Strings.MenuVladimirStandardCorp,
                new ExeFileShortcut(FeatureTapangaMaha, "Uninstall VladimirStandardCorp", "[System64Folder]msiexec.exe", "/x [ProductCode]"));
        }

        /// <summary>
        /// Каталог LabelPrint.
        /// </summary>
        /// <returns></returns>
        public Dir GetDirLabelPrint()
        {
            // Основные компоненты.
            var result = new Dir()
            {
                Name = @"LabelPrint",
                Id = new Id(Strings.PropInstallDirLabelPrint),
                Feature = FeatureLabelPrint,
            };
            // Ярлыки.
            var fileShortcut1 = new FileShortcut(FeatureLabelPrint, @"LabelPrint", $@"{Strings.MenuVladimirStandardCorp}") { WorkingDirectory = @"INSTALLDIR" };
            var fileShortcut2 = new FileShortcut(FeatureLabelPrint, @"LabelPrint", @"%Desktop%") { WorkingDirectory = @"INSTALLDIR" };
            var fileShortcut3 = new FileShortcut(FeatureLabelPrint, @"LabelPrint", @"%Startup%") { WorkingDirectory = @"INSTALLDIR" };
            // Главный файл.
            result.AddFile(new File(FeatureLabelPrint, Strings.DirSourceLabelPrint + @"\LabelPrint.exe", fileShortcut1, fileShortcut2, fileShortcut3));
            // Бинарные файлы.
            foreach (var item in Strings.ListLabelPrint)
            {
                result.AddFile(new File(FeatureLabelPrint, Strings.DirSourceLabelPrint + $@"\{item}"));
            }
            return result;
        }

        /// <summary>
        /// Каталог документации LabelPrint.
        /// </summary>
        /// <returns></returns>
        public Dir GetDirLabelPrintDocs()
        {
            // Основные компоненты.
            var result = new Dir()
            {
                Name = @"LabelPrintDocs",
                Id = new Id(Strings.PropInstallDirLabelPrintDocs),
                Feature = FeatureLabelPrintDocs,
            };
            // Бинарные файлы.
            foreach (var item in Strings.ListLabelPrintDocs)
            {
                result.AddFile(new File(FeatureLabelPrintDocs, Strings.DirSourceLabelPrint + $@"\{item}"));
            }
            return result;
        }

        /// <summary>
        /// Меню удаления.
        /// </summary>
        /// <returns></returns>
        public Dir GetDirLabelPrintUninstall()
        {
            return new Dir(Strings.MenuVladimirStandardCorp,
                new ExeFileShortcut(FeatureLabelPrint, "Uninstall VladimirStandardCorp", "[System64Folder]msiexec.exe", "/x [ProductCode]"));
        }

        /// <summary>
        /// Получить объекты компонентов.
        /// </summary>
        /// <returns></returns>
        public List<Dir> GetDirs()
        {
            return new List<Dir> {
                GetDirTapangaMaha(), GetDirTapangaMahaUninstall(),
                GetDirScalesUI(), GetDirScalesUIDocs(), GetDirScalesUIManuals(), GetDirScalesUIUninstall(),
                GetDirLabelPrint(), GetDirLabelPrintDocs(), GetDirLabelPrintUninstall(),
                GetDirMassa(), GetDirMassaDriver(),
            };
        }

        #endregion
    }
}
