// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesMsi.Helpers;
using ScalesMsi.Properties;
using ScalesMsi.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WixSharp.UI.Forms;

namespace ScalesMsi.Dialogs
{
    /// <summary>
    /// The standard Exit dialog
    /// </summary>
    public partial class ExitDialog : ManagedForm
    {
        #region Private helpers

        // Помощник приложения.
        private readonly AppHelper _app = AppHelper.Instance;
        // Помощник XML.
        private readonly XmlHelper _xml = XmlHelper.Instance;
        // Помощник Windows.
        private readonly WinHelper _win = WinHelper.Instance;
        // Помощник компонент.
        private readonly WixSharpHelper _wixSharp = WixSharpHelper.Instance;

        #endregion

        #region Dialog methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ExitDialog"/> class.
        /// </summary>
        public ExitDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Локализция.
        /// </summary>
        private void Localization()
        {
            if (_app.Wix.Args.IsUninstalling || _app.Wix.Args.IsModifying)
            {
                fieldScalesUIRun.Visible = false;
                fieldTapangaMahaRun.Visible = false;
                fieldLabelPrintRun.Visible = false;
                fieldRunManual.Visible = false;
            }

            image.Image = Runtime.Session.GetResourceBitmap("WixUI_Bmp_Dialog");
            description.Text = @"[UserExitDescription2]";
            if (Shell.UserInterrupted || Shell.Log.Contains("User cancelled installation."))
            {
                title.Text = @"[UserExitTitle]";
                description.Text = @"[UserExitDescription1]";
            }
            else if (Shell.ErrorDetected)
            {
                title.Text = @"[FatalErrorTitle]";
                description.Text = Shell.CustomErrorDescription ?? "[FatalErrorDescription1]";
            }
            fieldRunDriver.Text = @"[ExitDialogRunDriver]";
            fieldErrorDriver.Text = @"[ExitDialogErrorDriver]";
            fieldErrorDriver.Left = fieldRunDriver.Left;
            fieldErrorDriver.Top = fieldRunDriver.Top;
            fieldRunScalesTerminal.Text = @"[ExitDialogRunScalesTerminal]";
            fieldErrorScalesTerminal.Text = @"[ExitDialogErrorScalesTerminal]";
            fieldErrorScalesTerminal.Left = fieldRunScalesTerminal.Left;
            fieldErrorScalesTerminal.Top = fieldRunScalesTerminal.Top;
            fieldRunManual.Text = @"[ExitDialogRunManual]";

            fieldScalesUIRun.Text = @"[ExitDialogScalesUIRun]";
            fieldTapangaMahaRun.Text = @"[ExitDialogTapangaMahaRun]";
            fieldLabelPrintRun.Text = @"[ExitDialogLabelPrintRun]";

            fieldScalesUIConfig.Text = fieldScalesUIConfig.Checked ? @"[ExitDialogScalesUIConfigChange]" : @"[ExitDialogScalesUIConfigError]";
            fieldTapangaMahaConfig.Text = fieldTapangaMahaConfig.Checked ? @"[ExitDialogTapangaMahaConfigChange]" : @"[ExitDialogTapangaMahaConfigError]";
            fieldLabelPrintConfig.Text = fieldLabelPrintConfig.Checked ? @"[ExitDialogLabelPrintConfigChange]" : @"[ExitDialogLabelPrintConfigError]";

            Localize();
            ResetLayout();
        }

        /// <summary>
        /// После загрузки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitDialog_Load(object sender, System.EventArgs e)
        {
            SaveSettings();
            Localization();
        }

        /// <summary>
        /// Сбросить выравнивание.
        /// </summary>
        private void ResetLayout()
        {
            // The form controls are properly anchored and will be correctly resized on parent form
            // resizing. However the initial sizing by WinForm runtime doesn't do a good job with DPI
            // other than 96. Thus manual resizing is the only reliable option apart from going WPF.

            var bHeight = (int)(next.Height * 2.3);

            var upShift = bHeight - bottomPanel.Height;
            bottomPanel.Top -= upShift;
            bottomPanel.Height = bHeight;

            imgPanel.Height = ClientRectangle.Height - bottomPanel.Height;
            float height = image.Image.Height;
            float ratio = image.Image.Width / height;
            image.Width = (int)(image.Height * ratio);

            textPanel.Left = image.Right + 5;
            textPanel.Width = bottomPanel.Width - image.Width - 10;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Сохранить настройки.
        /// </summary>
        /// <returns></returns>
        public void SaveSettings()
        {
            // ScalesUI.
            try
            {
                fieldScalesUIConfig.Checked = false;
                var fileName = Strings.DirProgramScalesUI + @"\ScalesUI.exe.config";
                if (File.Exists(fileName))
                {
                    var checks = new List<bool>
                    {
                        _xml.Write(fileName, @"name=""ConnectionString""", Settings.Default.ConnectionString.Trim(' '), false),
                        _xml.Write(fileName, @"name=""Id""", Settings.Default.Id, false)
                    };
                    fieldScalesUIConfig.Checked = checks.TrueForAll(item => item);
                }
            }
            catch (Exception)
            {
                //
            }

            // TapangaMaha.
            try
            {
                fieldTapangaMahaConfig.Checked = false;
                var fileName = Strings.DirProgramTapangaMaha + @"\TapangaMaha.exe.config";
                if (File.Exists(fileName))
                {
                    var checks = new List<bool>
                    {
                        _xml.Write(fileName, @"name=""ConnectionString""", Settings.Default.ConnectionString.Trim(' '), false),
                        _xml.Write(fileName, @"name=""Id""", Settings.Default.Id, false)
                    };
                    fieldTapangaMahaConfig.Checked = checks.TrueForAll(item => item);
                }
            }
            catch (Exception)
            {
                //
            }

            // LabelPrint.
            try
            {
                fieldLabelPrintConfig.Checked = false;
                var fileName = Strings.DirProgramLabelPrint + @"\LabelPrint.exe.config";
                if (File.Exists(fileName))
                {
                    var checks = new List<bool>
                    {
                        _xml.Write(fileName, @"name=""ConnectionString""", Settings.Default.ConnectionString.Trim(' '), false),
                        _xml.Write(fileName, @"name=""Id""", Settings.Default.Id, false)
                    };
                    fieldLabelPrintConfig.Checked = checks.TrueForAll(item => item);
                }
            }
            catch (Exception)
            {
                //
            }

            // Сторонее ПО.
            try
            {
                fieldRunDriver.Checked = false;
                fieldRunScalesTerminal.Checked = false;
                // Компненты.
                var features = _wixSharp.GetSettingsFeatures();
                if (features?.Count > 0 && features.Contains(_wixSharp.FeatureMassaDriver.Name))
                {
                    // Проверить установку драйвера.
                    // Нет установки -> надо поставить.
                    if (!_app.CheckDriverInstall(EnumSilentUI.True))
                    {
                        // Проверить наличие файлов установки в сетевом каталоге.
                        var checks = new List<bool>();
                        foreach (var fileName in Strings.ListMassaDrivers)
                        {
                            var fullName = Strings.DirSourceMassa + $@"\{fileName}";
                            checks.Add(File.Exists(fullName));
                            //MessageBox.Show($@"File.Exists({fullName}): {File.Exists(fullName)}");
                        }

                        fieldErrorDriver.Visible = !(fieldRunDriver.Checked =
                            fieldRunDriver.Visible = checks.TrueForAll(x => true));
                    }
                }

                // Весовой терминал 100.
                //if (feature.Equals(_app.Wix.FeatureMassaScalesTerminal.Name))
                //{
                //    fieldRunScalesTerminal.Checked = true;
                //    // Проверить установку.
                //    MessageBox.Show($@"Проверить установку Весовой терминал 100.");
                //    foreach (var item in _win.SearchingSoftware(EnumWinProvider.Registry, "Масса-К Весовой терминал 100", EnumStringTemplate.Contains))
                //    {
                //        MessageBox.Show($@"item.Vendor: {item.Vendor}");
                //        if (item.Vendor.Equals("STMicroelectronics", StringComparison.InvariantCultureIgnoreCase))
                //        {
                //            //
                //        }
                //    }
                //}
            }
            catch (Exception)
            {
                //
            }
        }

        /// <summary>
        /// завершить установку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void finish_Click(object sender, System.EventArgs e)
        {
            // Установка драйверов.
            if (fieldRunDriver.Checked)
            {
                // Установить драйвера.
                _app.DriverInstall(EnumSilentUI.False);
            }

            // Установка весового терминала.
            if (fieldRunScalesTerminal.Checked)
            {
                // Установить драйвера.
                //_app.DriverInstall(EnumSilentUI.False);
            }

            // Открыть руководство пользователя.
            if (fieldRunManual.Checked)
            {
                foreach (var manual in Strings.ListScalesManuals)
                {
                    var taskMsi = _app.Proc.RunAsync(Strings.DirProgramScalesUIManuals + $@"\{manual}");
                    taskMsi.Wait();
                }
            }

            // Запустить приложение ScalesUI.
            if (fieldScalesUIRun.Checked)
            {
                var taskMsi = _app.Proc.RunAsync(Strings.DirProgramScalesUI + @"\ScalesUI.exe");
                taskMsi.Wait();
            }

            // Запустить приложение TapangaMaha.
            if (fieldTapangaMahaRun.Checked)
            {
                var taskMsi = _app.Proc.RunAsync(Strings.DirProgramTapangaMaha + @"\TapangaMaha.exe");
                taskMsi.Wait();
            }

            // Запустить приложение LabelPrint.
            if (fieldLabelPrintRun.Checked)
            {
                var taskMsi = _app.Proc.RunAsync(Strings.DirProgramLabelPrint + @"\LabelPrint.exe");
                taskMsi.Wait();
            }

            Shell.Exit();
        }

        /// <summary>
        /// Смотреть лог.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string wixSharpDir = Path.Combine(Path.GetTempPath(), @"WixSharp");
                if (!Directory.Exists(wixSharpDir))
                    Directory.CreateDirectory(wixSharpDir);

                string logFile = Path.Combine(wixSharpDir, Runtime.ProductName + ".log");
                File.WriteAllText(logFile, Shell.Log);
                Process.Start(logFile);
            }
            catch
            {
                //Catch all, we don't want the installer to crash in an
                //attempt to view the log.
            }
        }

        #endregion
    }
}