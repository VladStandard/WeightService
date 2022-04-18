// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.DataModels;
using DataCore.DAL.Utils;
using DataCore.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;
using WeightCore.Zpl;
using static DataCore.ShareEnums;
using LocalizationCore = DataCore.Localization.Core;

namespace ScalesUI.Forms
{
    public partial class SettingsForm : Form
    {
        #region Private fields and properties

        private AppVersionHelper AppVersion { get; set; } = AppVersionHelper.Instance;
        private DebugHelper Debug { get; set; } = DebugHelper.Instance;
        private ExceptionHelper Exception { get; set; } = ExceptionHelper.Instance;
        private LogHelper Log { get; set; } = LogHelper.Instance;
        private SessionStateHelper SessionState { get; set; } = SessionStateHelper.Instance;

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

                // Загружить при кажом открытии формы.
                //if (SessionState != null)
                //    SessionState.CurrentScale = ScalesUtils.GetScale(SessionState.Host?.ScaleId);
                if (SessionState != null)
                    SessionState.CurrentScale = SessionState.DataAccess.Dal.Crud
                        .GetEntity<DataCore.DAL.TableScaleModels.ScaleEntity>(SessionState.Host?.ScaleId);

                // Определить COM-порт.
                DefaultComPortName();

                if (SessionState?.CurrentScale != null)
                {
                    fieldSendTimeout.Text = SessionState.CurrentScale.DeviceSendTimeout.ToString();
                    fieldReceiveTimeOut.Text = SessionState.CurrentScale.DeviceReceiveTimeout.ToString();
                    fieldZebraTcpAddress.Text = SessionState.CurrentScale.PrinterMain.Ip;
                    fieldZebraTcpPort.Text = SessionState.CurrentScale.PrinterMain.Port.ToString();
                    fieldDescription.Text = SessionState.CurrentScale.Description;
                }

                if (SessionState?.CurrentWeighingFact != null)
                    fieldCurrentWeightFact.Text = SessionState.CurrentWeighingFact.SerializeAsXmlWithEmptyNamespaces();
                    //fieldCurrentWeightFact.Text = SessionState.CurrentWeighingFact.SerializeObject();

                fieldGuid.Text = SessionState?.CurrentScaleId.ToString();
                //fieldSqlConnectionString.Text = Properties.Settings.Default.ConnectionString;
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
        }

        #endregion

        #region Public and private methods

        private void FieldComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fielcComPortFind.Text = @"COM-порт не существует!";
                string curPort = fieldComPort.Items[fieldComPort.SelectedIndex].ToString();
                fielcComPortFind.Text = $@"{curPort}-порт не существует!";

                foreach (string portName in SerialPort.GetPortNames())
                {
                    if (portName.Equals(curPort, StringComparison.InvariantCultureIgnoreCase))
                    {
                        fielcComPortFind.Text = $@"{curPort}-порт найден.";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;
                Thread.Sleep(10);
                Application.DoEvents();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void ButtonSaveOption_Click(object sender, EventArgs e)
        {
            bool result = true;
            try
            {
                // GUI.
                UseWaitCursor = true;
                Thread.Sleep(10);
                Application.DoEvents();
                // Data.
                SessionState.CurrentScale.DeviceComPort = fieldComPort.Text;
                SessionState.CurrentScale.DeviceSendTimeout = short.Parse(fieldSendTimeout.Text);
                SessionState.CurrentScale.DeviceReceiveTimeout = short.Parse(fieldReceiveTimeOut.Text);
                SessionState.CurrentScale.VerScalesUi = AppVersion.GetCurrentVersion(Assembly.GetExecutingAssembly(), AppVerCountDigits.Use3);
                //ScalesUtils.Update(SessionState.CurrentScale);
                SessionState.DataAccess.Dal.Crud.UpdateEntity(SessionState.CurrentScale);
                // Settings.
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                result = false;
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                UseWaitCursor = false;
            }

            // GUI.
            if (result)
                ButtonClose_Click(sender, e);
        }

        private void ButtonUploadResources_Click(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;
                Thread.Sleep(10);
                Application.DoEvents();

                ZplConverterHelper zp = new();
                zp.LogoClear(SessionState.CurrentScale.PrinterMain.Ip, SessionState.CurrentScale.PrinterMain.Port);
                zp.FontsClear(SessionState.CurrentScale.PrinterMain.Ip, SessionState.CurrentScale.PrinterMain.Port);
                if (SessionState.CurrentScale.UseOrder == true)
                {
                    if (SessionState.CurrentOrder == null)
                    {
                        const string message = "Не определен PLU";
                        const string caption = "Операция недоступна!";
                        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    zp.LogoUpload(SessionState.CurrentScale.PrinterMain.Ip, SessionState.CurrentScale.PrinterMain.Port, SessionState.CurrentOrder.Template.Logo);
                    zp.FontsUpload(SessionState.CurrentScale.PrinterMain.Ip, SessionState.CurrentScale.PrinterMain.Port, SessionState.CurrentOrder.Template.Fonts);
                }
                else
                {
                    if (SessionState.CurrentPlu == null)
                    {
                        const string message = "Не определен PLU";
                        const string caption = "Операция недоступна!";
                        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    zp.LogoUpload(SessionState.CurrentScale.PrinterMain.Ip, SessionState.CurrentScale.PrinterMain.Port, SessionState.CurrentPlu.Template.Logo);
                    zp.FontsUpload(SessionState.CurrentScale.PrinterMain.Ip, SessionState.CurrentScale.PrinterMain.Port, SessionState.CurrentPlu.Template.Fonts);
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void BtnCalibrate_Click(object sender, EventArgs e)
        {
            try
            {
                // WPF MessageBox.
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocalizationCore.Scales.OperationControl;
                wpfPageLoader.MessageBox.Message = LocalizationCore.Print.WarningOpenCover;
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonRetryVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonCancelVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.Localization();
                wpfPageLoader.ShowDialog(this);
                DialogResult result = wpfPageLoader.MessageBox.Result;
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
                if (result == DialogResult.Retry)
                {
                    UseWaitCursor = true;
                    Thread.Sleep(10);
                    Application.DoEvents();

                    ZplConverterHelper zp = new();
                    zp.Сalibration(SessionState.CurrentScale.PrinterMain.Ip, SessionState.CurrentScale.PrinterMain.Port);
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                UseWaitCursor = false;
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
                Exception.Catch(this, ref ex, true);
            }
        }

        #endregion

        #region Private methods - Управление принтером

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.Manager.PrintMain.SendCmd(ZplPipeUtils.ZplPowerOnReset());
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        /// <summary>
        /// Калибровать.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPrintCalibrate_Click(object sender, EventArgs e)
        {
            try
            {
                //_taskManager.PrintManager.PrintControl.CmdCalibrate();
                switch (SessionState.PrintBrandMain)
                {
                    case WeightCore.Print.PrintBrand.Default:
                        break;
                    case WeightCore.Print.PrintBrand.Zebra:
                        SessionState.Manager.PrintMain.SendCmd(ZplPipeUtils.ZplCalibration());
                        break;
                    case WeightCore.Print.PrintBrand.TSC:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void ButtonPrintOptions_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.Manager.PrintMain.SendCmd(ZplPipeUtils.ZplPrintConfigurationLabel());
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        #endregion

        #region Private methods - Кнопки по умолчанию

        private void DefaultComPortName([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                fieldComPort.Items.Clear();

                // Получить список COM-портов.
                System.Collections.Generic.List<string> listComPorts = SerialPort.GetPortNames().ToList();
                // Текущий порт из настроек.
                string curPort = string.Empty;
                if (SessionState?.CurrentScale?.DeviceComPort != null)
                {
                    curPort = SessionState.CurrentScale.DeviceComPort;
                    if (!string.IsNullOrEmpty(curPort))
                    {
                        bool find = false;
                        foreach (string portName in listComPorts)
                        {
                            if (portName.Equals(curPort, StringComparison.InvariantCultureIgnoreCase))
                            {
                                find = true;
                                break;
                            }
                        }
                        if (!find)
                            fieldComPort.Items.Add(curPort);
                    }
                }
                // Сортировка.
                listComPorts = listComPorts.OrderBy(o => o).ToList();
                // Заполнить список.
                foreach (string portName in listComPorts)
                {
                    fieldComPort.Items.Add(portName);
                    fieldComPort.Text = curPort;
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true, filePath, lineNumber, memberName);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void ButtonSqlCheck_Click(object sender, EventArgs e)
        {
            try
            {
                using SqlConnection con = new(fieldSqlConnectionString.Text);
                con.Open();
                try
                {
                    //Properties.Settings.Default.ConnectionString = fieldSqlConnectionString.Text;
                    labelSqlStatus.Text = @"Подключение выполнено.";
                }
                catch (Exception ex)
                {
                    labelSqlStatus.Text = $@"Ошибка подключения! {ex.Message}";
                    Log.Error(ex.Message);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void ButtonMassaParam_Click(object sender, EventArgs e)
        {
            try
            {
                fieldCurrentMKProp.Clear();

                if (SessionState.Manager.Massa != null)
                {
                    //_taskManager.MassaManager.GetScalePar();
                    //Thread.Sleep(10);
                    //Application.DoEvents();

                    if (SessionState.Manager.Massa.ResponseParseGet != null)
                    {
                        fieldCurrentMKProp.Text = SessionState.Manager.Massa.ResponseParseGet.Message;
                    }

                    if (SessionState.Manager.Massa.ResponseParseScalePar != null)
                    {
                        fieldCurrentMKProp.Text = $@"{fieldCurrentMKProp.Text}\n{SessionState.Manager.Massa.ResponseParseScalePar.Message}";
                    }
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void ButtonPrintCancelAll_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.Manager.PrintMain.SendCmd(ZplPipeUtils.ZplClearPrintBuffer());
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        #endregion

        #region Commented

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