// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.Utils;
using DataProjectsCore.Helpers;
using DataShareCore.Helpers;
using System;
using Microsoft.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;
using WeightCore.Managers;
using WeightCore.Zpl;
using static DataShareCore.ShareEnums;

namespace ScalesUI.Forms
{
    public partial class SettingsForm : Form
    {
        #region Private fields and properties

        private readonly AppVersionHelper _appVersion = AppVersionHelper.Instance;
        private readonly DebugHelper _debug = DebugHelper.Instance;
        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        private readonly LogHelper _log = LogHelper.Instance;
        private readonly SessionStateHelper _sessionState = SessionStateHelper.Instance;
        private readonly TaskManagerHelper _taskManager = TaskManagerHelper.Instance;

        #endregion

        #region Constructor and destructor

        public SettingsForm()
        {
            InitializeComponent();

            if (_taskManager.MassaManager != null)
                _taskManager.MassaManager.GetScalePar();
        }

        private void ScaleOptionsForm_Load(object sender, EventArgs e)
        {
            try
            {
                TopMost = !_debug.IsDebug;

                // Загружить при кажом открытии формы.
                if (_sessionState != null)
                    _sessionState.CurrentScale = ScalesUtils.GetScale(_sessionState.Host?.ScaleId);

                // Определить COM-порт.
                DefaultComPortName();

                if (_sessionState?.CurrentScale != null)
                {
                    fieldSendTimeout.Text = _sessionState.CurrentScale.DeviceSendTimeout.ToString();
                    fieldReceiveTimeOut.Text = _sessionState.CurrentScale.DeviceReceiveTimeout.ToString();
                    fieldZebraTcpAddress.Text = _sessionState.CurrentScale.ZebraPrinter.Ip;
                    fieldZebraTcpPort.Text = _sessionState.CurrentScale.ZebraPrinter.Port.ToString();
                    fieldDescription.Text = _sessionState.CurrentScale.Description;
                }

                if (_sessionState?.CurrentWeighingFact != null)
                    fieldCurrentWeightFact.Text = _sessionState.CurrentWeighingFact.SerializeObject();

                fieldGuid.Text = _sessionState?.CurrentScaleId.ToString();
                //fieldSqlConnectionString.Text = Properties.Settings.Default.ConnectionString;
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        #endregion

        #region Public and private methods

        private void fieldComPort_SelectedIndexChanged(object sender, EventArgs e)
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
                _exception.Catch(this, ref ex);
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
                _exception.Catch(this, ref ex);
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
                _sessionState.CurrentScale.DeviceComPort = fieldComPort.Text;
                _sessionState.CurrentScale.DeviceSendTimeout = short.Parse(fieldSendTimeout.Text);
                _sessionState.CurrentScale.DeviceReceiveTimeout = short.Parse(fieldReceiveTimeOut.Text);
                _sessionState.CurrentScale.VerScalesUI = _appVersion.GetCurrentVersion(Assembly.GetExecutingAssembly(), AppVerCountDigits.Use3);
                ScalesUtils.Update(_sessionState.CurrentScale);
                // Settings.
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                result = false;
                _exception.Catch(this, ref ex);
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
                zp.LogoClear(_sessionState.CurrentScale.ZebraPrinter.Ip, _sessionState.CurrentScale.ZebraPrinter.Port);
                zp.FontsClear(_sessionState.CurrentScale.ZebraPrinter.Ip, _sessionState.CurrentScale.ZebraPrinter.Port);
                if (_sessionState.CurrentScale.UseOrder == true)
                {
                    if (_sessionState.CurrentOrder == null)
                    {
                        const string message = "Не определен PLU";
                        const string caption = "Операция недоступна!";
                        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    zp.LogoUpload(_sessionState.CurrentScale.ZebraPrinter.Ip, _sessionState.CurrentScale.ZebraPrinter.Port, _sessionState.CurrentOrder.Template.Logo);
                    zp.FontsUpload(_sessionState.CurrentScale.ZebraPrinter.Ip, _sessionState.CurrentScale.ZebraPrinter.Port, _sessionState.CurrentOrder.Template.Fonts);
                }
                else
                {
                    if (_sessionState.CurrentPlu == null)
                    {
                        const string message = "Не определен PLU";
                        const string caption = "Операция недоступна!";
                        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    zp.LogoUpload(_sessionState.CurrentScale.ZebraPrinter.Ip, _sessionState.CurrentScale.ZebraPrinter.Port, _sessionState.CurrentPlu.Template.Logo);
                    zp.FontsUpload(_sessionState.CurrentScale.ZebraPrinter.Ip, _sessionState.CurrentScale.ZebraPrinter.Port, _sessionState.CurrentPlu.Template.Fonts);
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            try
            {
                CustomMessageBox messageBox = CustomMessageBox.Show(this, @"Прежде чем продолжить калибровку откройте крышку отделителя!",
                    @"ВНИМАНИЕ!", MessageBoxButtons.RetryCancel);
                messageBox.Wait();
                //DialogResult dialogResult = MessageBox.Show(@"Прежде чем продолжить калибровку откройте крышку отделителя!", @"ВНИМАНИЕ!", MessageBoxButtons.RetryCancel);
                if (messageBox.Result == DialogResult.Retry)
                {
                    UseWaitCursor = true;
                    Thread.Sleep(10);
                    Application.DoEvents();

                    ZplConverterHelper zp = new();
                    zp.Сalibration(_sessionState.CurrentScale.ZebraPrinter.Ip, _sessionState.CurrentScale.ZebraPrinter.Port);
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
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
                _exception.Catch(this, ref ex);
            }
        }

        #endregion

        #region Private methods - Управление принтером

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                _taskManager.PrintManager.SendAsync(ZplPipeUtils.ZplPowerOnReset());
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
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
                if (!_sessionState.IsTscPrinter)
                    _taskManager.PrintManager.SendAsync(ZplPipeUtils.ZplCalibration());
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
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
                _taskManager.PrintManager.SendAsync(ZplPipeUtils.ZplPrintConfigurationLabel());
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        #endregion

        #region Private methods - Кнопки по умолчанию

        private void DefaultComPortName()
        {
            try
            {
                fieldComPort.Items.Clear();

                // Получить список COM-портов.
                System.Collections.Generic.List<string> listComPorts = SerialPort.GetPortNames().ToList();
                // Текущий порт из настроек.
                string curPort = string.Empty;
                if (_sessionState?.CurrentScale?.DeviceComPort != null)
                {
                    curPort = _sessionState.CurrentScale.DeviceComPort;
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
                _exception.Catch(this, ref ex);
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
                    _log.Error(ex.Message);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
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

                if (_taskManager.MassaManager != null)
                {
                    _taskManager.MassaManager.GetScalePar();
                    Thread.Sleep(10);
                    Application.DoEvents();

                    if (_taskManager.MassaManager.ResponseParseGetMassa != null)
                    {
                        fieldCurrentMKProp.Text = _taskManager.MassaManager.ResponseParseGetMassa.Message;
                    }

                    if (_taskManager.MassaManager.ResponseParseGetScalePar != null)
                    {
                        fieldCurrentMKProp.Text = $@"{fieldCurrentMKProp.Text}\n{_taskManager.MassaManager.ResponseParseGetScalePar.Message}";
                    }
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
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
                _taskManager.PrintManager.SendAsync(ZplPipeUtils.ZplClearPrintBuffer());
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
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