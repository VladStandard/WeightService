// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Settings;
using DataCore.Sql.TableDirectModels;
using System;
using System.Threading;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;
using WeightCore.Zpl;

namespace ScalesUI.Forms
{
    public partial class SettingsForm : Form
    {
        #region Private fields and properties

        private AppVersionHelper AppVersion { get; set; } = AppVersionHelper.Instance;
        private DebugHelper Debug { get; } = DebugHelper.Instance;
        private UserSessionHelper UserSession { get; } = UserSessionHelper.Instance;

        #endregion

        #region Constructor and destructor

        public SettingsForm()
        {
            InitializeComponent();

            //if (_taskManager.MassaManager != null)
            //    _taskManager.MassaManager.GetScalePar();
        }

        private void ScaleOptionsForm_Load(object sender, EventArgs e)
        {
            try
            {
                TopMost = !Debug.IsDebug;

                // Определить COM-порт.
                if (UserSession?.WeighingFact != null)
                    fieldCurrentWeightFact.Text = UserSession.WeighingFact.SerializeAsXml<WeighingFactDirect>(true);
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        #endregion

        #region Public and private methods

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.Sleep(10);
                Application.DoEvents();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void ButtonSaveOption_Click(object sender, EventArgs e)
        {
            bool result = true;
            try
            {
                // Data.
                UserSession.DataAccess.Crud.UpdateEntity(UserSession.Scale);
                // Settings.
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                result = false;
                GuiUtils.WpfForm.CatchException(this, ex);
            }

            // GUI.
            if (result)
                ButtonClose_Click(sender, e);
        }

        private void ButtonUploadResources_Click(object sender, EventArgs e)
        {
            try
            {
                ZplConverterHelper zp = new();
                zp.LogoClear(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port);
                zp.FontsClear(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port);
                if (UserSession.Scale.IsOrder == true)
                {
                    if (UserSession.Order == null)
                    {
                        const string message = "Не определен PLU";
                        const string caption = "Операция недоступна!";
                        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    zp.LogoUpload(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port, UserSession.Order.Template.Logo);
                    zp.FontsUpload(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port, UserSession.Order.Template.Fonts);
                }
                else
                {
                    if (UserSession.Plu == null)
                    {
                        const string message = "Не определен PLU";
                        const string caption = "Операция недоступна!";
                        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    zp.LogoUpload(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port, UserSession.Plu.Template.Logo);
                    zp.FontsUpload(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port, UserSession.Plu.Template.Fonts);
                }
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void BtnCalibrate_Click(object sender, EventArgs e)
        {
            try
            {
                // WPF MessageBox.
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocaleCore.Scales.OperationControl;
                wpfPageLoader.MessageBox.Message = LocaleCore.Print.WarningOpenCover;
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonRetryVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonCancelVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.Localization();
                wpfPageLoader.ShowDialog(this);
                DialogResult result = wpfPageLoader.MessageBox.Result;
                wpfPageLoader.Close();
                if (result == DialogResult.Retry)
                {
                    ZplConverterHelper zp = new();
                    zp.Сalibration(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port);
                }
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void ButtonGenerateException_Click(object sender, EventArgs e)
        {
            try
            {
                throw new Exception("Test exception", new Exception("Test inner exception"));
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        #endregion

        #region Private methods - Управление принтером

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                UserSession.ManagerControl.PrintMain.SendCmd(ZplUtils.ZplPowerOnReset());
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void ButtonPrintCalibrate_Click(object sender, EventArgs e)
        {
            try
            {
                //_taskManager.PrintManager.PrintControl.CmdCalibrate();
                switch (UserSession.PrintBrandMain)
                {
                    case WeightCore.Print.PrintBrand.Default:
                        break;
                    case WeightCore.Print.PrintBrand.Zebra:
                        UserSession.ManagerControl.PrintMain.SendCmd(ZplUtils.ZplCalibration());
                        break;
                    case WeightCore.Print.PrintBrand.TSC:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void ButtonPrintOptions_Click(object sender, EventArgs e)
        {
            try
            {
                UserSession.ManagerControl.PrintMain.SendCmd(ZplUtils.ZplPrintConfigurationLabel());
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        #endregion

        #region Private methods - Кнопки по умолчанию

        private void ButtonMassaParam_Click(object sender, EventArgs e)
        {
            try
            {
                fieldCurrentMKProp.Clear();

                if (UserSession.ManagerControl.Massa != null)
                {
                    //_taskManager.MassaManager.GetScalePar();
                    //Thread.Sleep(10);
                    //Application.DoEvents();

                    if (UserSession.ManagerControl.Massa.ResponseParseGet != null)
                    {
                        fieldCurrentMKProp.Text = UserSession.ManagerControl.Massa.ResponseParseGet.Message;
                    }

                    if (UserSession.ManagerControl.Massa.ResponseParseScalePar != null)
                    {
                        fieldCurrentMKProp.Text = $@"{fieldCurrentMKProp.Text}\n{UserSession.ManagerControl.Massa.ResponseParseScalePar.Message}";
                    }
                }
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void ButtonPrintCancelAll_Click(object sender, EventArgs e)
        {
            try
            {
                UserSession.ManagerControl.PrintMain.SendCmd(ZplUtils.ZplClearPrintBuffer);
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        //https://developer.zebra.com/thread/33293
        //string zplImageData = string.Empty;
        //byte[] binaryData = System.IO.File.ReadAllBytes(@Server.MapPath("Temp/abcd.png"));
        //foreach (Byte b in binaryData)

        //        {
        //            string hexRep = String.Format("{0:X}", b);
        //            if (hexRep.Length == 1)
        //                hexRep = "0" + hexRep;
        //            zplImageData += hexRep;
        //        }

        //string s = @"^XA^MNY~DYE:COMPOUND.PNG,P,P," + binaryData.Length + ",," + zplImageData + @"^XZ^FO28,260^IME:COMPOUND.PNG^FS^XZ";
        //RawPrinterHelper.SendStringToPrinter(@"\\info-0004\ZDesigner 105SL 203DPI Zebra", s);

        #endregion
    }
}