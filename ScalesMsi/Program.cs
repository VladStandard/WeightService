// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesMsi.Dialogs;
using ScalesMsi.Helpers;
using ScalesMsi.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WixSharp;
using WixSharp.CommonTasks;

namespace ScalesMsi
{
    internal static class Program
    {
        #region Private helpers

        // Помощник приложения.
        private static readonly AppHelper App = AppHelper.Instance;

        #endregion

        #region Private methods

        /// <summary>
        /// Главный метод.
        /// </summary>
        private static void Main()
        {
            try
            {
                var project = new ManagedProject()
                {
                    Name = "ScalesUI",
                    GUID = new Guid("6fe30b47-2577-43ad-9095-1861ba25889b"),
                    LicenceFile = @"Docs\License.rtf",
                    UI = WUI.WixUI_Advanced,
                    //SourceBaseDir = Strings.DirProgramScalesUI,
                    SourceBaseDir = Environment.CurrentDirectory,
                    OutFileName = "ScalesUI",
                    InstallPrivileges = InstallPrivileges.elevated,
                    Language = @"ru-RU",
                    Version = new Version(App.GetCurrentVersion(EnumVerCountDigits.Use3)),
                    DefaultFeature = App.Wix.FeatureScalesUI,
                    ManagedUI = new ManagedUI(),
                    ControlPanelInfo = { Manufacturer = "Владимирский стандарт" },
                    InstallScope = InstallScope.perMachine, // [ALLUSERS]
                    Properties = App.Wix.GetProperties().ToArray(),
                    Dirs = App.Wix.GetDirs().ToArray(),
                };
                // Диалоги установки.
                project.ManagedUI.InstallDialogs
                    .Add<LanguageDialog>()
                    .Add<WelcomeDialog>()
                    //.Add<LicenseDialog>()
                    .Add<SetupTypeDialog>()
                    .Add<FeaturesDialog>()
                    .Add<InstallDirDialog>()
                    .Add<ProgressDialog>()
                    .Add<ExitDialog>();
                // Диалоги изменения.
                project.ManagedUI.ModifyDialogs
                    .Add<MaintenanceTypeDialog>()
                    .Add<FeaturesDialog>()
                    .Add<ProgressDialog>()
                    .Add<ExitDialog>();
                // Добавить файлы в область видимости MSI.
                foreach (var item in Strings.ListMsiLib)
                {
                    // ReSharper disable once RedundantAssignment
                    var fileName = string.Empty;
#if DEBUG
                    fileName = @"bin\Debug\" + item;
#else
                    fileName = @"bin\Release\" + item;
#endif
                    if (System.IO.File.Exists(fileName))
                    {
                        //project.Actions.OfType<ManagedAction>().Single().AddRefAssembly(typeof(ExternalAsm.Utils).Assembly.Location);
                        //project.Actions.OfType<ManagedAction>().Single().AddRefAssembly(fileName);
                        project.DefaultRefAssemblies.Add(fileName);
                    }
                }
                // При желании включите возможность восстановления установки, даже если исходный MSI-файл больше не доступен.
                project.EnableResilientPackage();
                // Обязательно условие по .Net Framework.
                project.SetNetFxPrerequisite(Condition.Net48_Installed, App.CurrentLocalization == EnumLocalization.English
                    ? "Please install .Net Framework 4.8 first." : "Сначала установите .Net Framework 4.8.");
                // project.PreserveTempFiles = true;
                //project.WixSourceGenerated += WixSourceGenerated;
                project.BeforeInstall += BeforeInstall;
                project.AfterInstall += AfterInstall;
                project.Load += Load;
                project.Localize();
                //SaveSettings();
                project.BuildMsi();
            }
            catch (Exception ex)
            {
                MessageBox.Show((App.CurrentLocalization == EnumLocalization.English ? @"MSI error (main method)!" : "Ошибка MSI (главный метод)!") +
                    Environment.NewLine + ex.Message);
            }
        }

        private static void DriverCheckAndExtract(SetupEventArgs e)
        {
            var features = new List<string>();
            foreach (var feature in e.Session.Features)
            {
                features.Add(feature.Name);
            }
            if (features?.Count > 0 && features.Contains(App.Wix.FeatureMassaDriver.Name))
                App.DriverExtract();
        }

        /// <summary>
        /// Перед установкой.
        /// </summary>
        /// <param name="e"></param>
        private static void BeforeInstall(SetupEventArgs e)
        {
            try
            {
                App.Wix.Args = e;
                if (e.IsInstalling)
                {
                    DriverCheckAndExtract(e);
                    //App.DirClear(EnumComponents.ScalesUI);
                }
                if (e.IsRepairing)
                {
                    //App.DirClear(EnumComponents.MassaDriver);
                }
                if (e.IsModifying)
                {
                    //App.DirClear(EnumComponents.ScalesUIBinaries);
                }
                if (e.IsUninstalling)
                {
                    App.DirClear(EnumComponents.ScalesUI);
                }

            }
            catch (Exception)
            {
                //MessageBox.Show((_app.CurrentLocalization == EnumLocalization.English ? @"MSI error (before install)!" : "Ошибка MSI (перед установкой)!") +
                //    Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// После установки.
        /// </summary>
        /// <param name="e"></param>
        private static void AfterInstall(SetupEventArgs e)
        {
            try
            {
                App.Wix.Args = e;
                if (e.IsInstalling)
                {
                    //App.DriverExtract();
                }
                if (e.IsRepairing)
                {
                    //App.DriverExtract();
                }
                if (e.IsModifying)
                {
                    //App.DriverExtract();
                    //App.DirClear(EnumComponents.ScalesUIBinaries);
                }
                if (e.IsUninstalling)
                {
                    App.DirClear(EnumComponents.ScalesUI);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((App.CurrentLocalization == EnumLocalization.English ? @"MSI error (after install)!" : "Ошибка MSI (перед установкой)!") +
                    Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Локализация.
        /// </summary>
        /// <param name="project"></param>
        private static void Localize(this ManagedProject project)
        {
            project.AddBinary(new Binary(new Id("en_xsl"), "WixUI_en-us.wxl"));
            project.AddBinary(new Binary(new Id("ru_xsl"), "WixUI_ru-RU.wxl"));
            project.UIInitialized += args =>
            {
                App.Wix.Args = args;
            };
        }

        private static void Load(SetupEventArgs e)
        {
            //e.Session[Strings.PropInstallDirLabelPrint] = Strings.DirProgramLabelPrint;
        }

        #endregion
    }
}
