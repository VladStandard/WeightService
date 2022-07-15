// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Sql.TableDirectModels;
using System;
using System.Threading;
using System.Windows.Forms;
using MDSoft.BarcodePrintUtils;
using WeightCore.Gui;
using WeightCore.Helpers;
using WeightCore.Zpl;

namespace ScalesUI.Forms
{
    /// <summary>
    /// Settings form.
    /// </summary>
    public partial class SettingsForm : Form
    {
        #region Private fields and properties

        private DebugHelper Debug { get; } = DebugHelper.Instance;
        private UserSessionHelper UserSession { get; } = UserSessionHelper.Instance;

        /// <summary>
        /// Constructor.
        /// </summary>
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
                ZplConverterHelper zplConverter = new();
                if (UserSession.Scale.PrinterMain != null)
                {
                    zplConverter.LogoClear(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port);
                    zplConverter.FontsClear(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port);
                    if (UserSession.Scale.IsOrder)
                    {
                        if (UserSession.Order == null)
                        {
                            const string message = "Не определен PLU";
                            const string caption = "Операция недоступна!";
                            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        zplConverter.LogoUpload(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port,
                            UserSession.Order.Template.Logo);
                        zplConverter.FontsUpload(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port,
                            UserSession.Order.Template.Fonts);
                    }
                    else
                    {
                        TemplateDirect template = UserSession.Plu.LoadTemplate();
                        zplConverter.LogoUpload(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port, template.Logo);
                        zplConverter.FontsUpload(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port, template.Fonts);
                    }
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
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocaleCore.Scales.OperationControl;
                wpfPageLoader.MessageBox.Message = LocaleCore.Print.WarningOpenCover;
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonRetryVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonCancelVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.Localization();
                wpfPageLoader.ShowDialog(this);
                DialogResult result = wpfPageLoader.MessageBox.Result;
                wpfPageLoader.Close();
                if (result == DialogResult.Retry && UserSession.Scale.PrinterMain != null)
                {
                    ZplConverterHelper zplConverter = new();
                    zplConverter.Сalibration(UserSession.Scale.PrinterMain.Ip, UserSession.Scale.PrinterMain.Port);
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

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                UserSession.ManagerControl.PrintMain.SendCmd(MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ZplPowerOnReset());
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
                    case PrintBrand.Default:
                        break;
                    case PrintBrand.Zebra:
                        UserSession.ManagerControl.PrintMain.SendCmd(MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ZplCalibration());
                        break;
                    case PrintBrand.TSC:
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
                UserSession.ManagerControl.PrintMain.SendCmd(MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ZplPrintConfigurationLabel());
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void ButtonMassaParam_Click(object sender, EventArgs e)
        {
            try
            {
                fieldCurrentMKProp.Clear();

                //_taskManager.MassaManager.GetScalePar();
                //Thread.Sleep(10);
                //Application.DoEvents();
                fieldCurrentMKProp.Text = UserSession.ManagerControl.Massa.ResponseParseGet.Message;
                fieldCurrentMKProp.Text = $@"{fieldCurrentMKProp.Text}\n{UserSession.ManagerControl.Massa.ResponseParseScalePar.Message}";
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
                UserSession.ManagerControl.PrintMain.SendCmd(MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ZplClearPrintBuffer);
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        #endregion
    }
}
