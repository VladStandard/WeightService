﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.Utils;
using DataProjectsCore.Utils;
using DataShareCore.Helpers;
using log4net;
using System;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Managers;
using WeightCore.Models;
using WeightCore.Zpl;
using static DataShareCore.ShareEnums;

namespace ScalesUI.Forms
{
    public partial class SettingsForm : Form
    {
        #region Private fields and properties

        private readonly AppVersionHelper _appVersion = AppVersionHelper.Instance;
        private readonly SessionState _ws = SessionState.Instance;
        private readonly TaskManagerEntity _taskManager = TaskManagerEntity.Instance;
        private readonly LogUtils _logUtils = LogUtils.Instance;

        #endregion

        #region Form methods

        public SettingsForm()
        {
            InitializeComponent();

            if (_taskManager.MassaManager != null)
                _taskManager.MassaManager.GetScalePar();
        }

        private void ScaleOptionsForm_Load(object sender, EventArgs e)
        {
            TopMost = _ws is null ? false : !_ws.IsDebug;

            // Загружить при кажом открытии формы.
            if (_ws != null)
                _ws.CurrentScale = ScalesUtils.GetScale(_ws.Host?.CurrentScaleId);

            // Определить COM-порт.
            DefaultComPortName();

            if (_ws?.CurrentScale != null)
            {
                fieldSendTimeout.Text = _ws.CurrentScale.DeviceSendTimeout.ToString();
                fieldReceiveTimeOut.Text = _ws.CurrentScale.DeviceReceiveTimeout.ToString();
                fieldZebraTcpAddress.Text = _ws.CurrentScale.ZebraPrinter.Ip;
                fieldZebraTcpPort.Text = _ws.CurrentScale.ZebraPrinter.Port.ToString();
                fieldDescription.Text = _ws.CurrentScale.Description;
            }

            if (_ws?.CurrentWeighingFact != null)
                fieldCurrentWeightFact.Text = _ws.CurrentWeighingFact.SerializeObject();

            fieldGuid.Text = _ws?.CurrentScaleId.ToString();
            //fieldSqlConnectionString.Text = Properties.Settings.Default.ConnectionString;
        }

        #endregion

        #region Private methods

        private void fieldComPort_SelectedIndexChanged(object sender, EventArgs e)
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

                _logUtils.Error(ex.Message);
                //MessageBox.Show(@"Ошибка закрытия!" + Environment.NewLine + ex.Message);
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
                _ws.CurrentScale.DeviceComPort = fieldComPort.Text;
                _ws.CurrentScale.DeviceSendTimeout = short.Parse(fieldSendTimeout.Text);
                _ws.CurrentScale.DeviceReceiveTimeout = short.Parse(fieldReceiveTimeOut.Text);
                _ws.CurrentScale.VerScalesUI = _appVersion.GetCurrentVersion(Assembly.GetExecutingAssembly(), AppVerCountDigits.Use3);
                ScalesUtils.Update(_ws.CurrentScale);
                // Settings.
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                result = false;
                _logUtils.Error(ex.Message);
                MessageBox.Show(@"Ошибка сохранения настроек!" + Environment.NewLine + ex.Message);
            }
            finally
            {
                // GUI.
                UseWaitCursor = false;
                Thread.Sleep(10);
                Application.DoEvents();
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

                ZplConverterHelper zp = new ZplConverterHelper();
                zp.LogoClear(_ws.CurrentScale.ZebraPrinter.Ip, _ws.CurrentScale.ZebraPrinter.Port);
                zp.FontsClear(_ws.CurrentScale.ZebraPrinter.Ip, _ws.CurrentScale.ZebraPrinter.Port);
                if (_ws.CurrentScale.UseOrder == true)
                {
                    if (_ws.CurrentOrder == null)
                    {
                        const string message = "Не определен PLU";
                        const string caption = "Операция недоступна!";
                        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    zp.LogoUpload(_ws.CurrentScale.ZebraPrinter.Ip, _ws.CurrentScale.ZebraPrinter.Port, _ws.CurrentOrder.Template.Logo);
                    zp.FontsUpload(_ws.CurrentScale.ZebraPrinter.Ip, _ws.CurrentScale.ZebraPrinter.Port, _ws.CurrentOrder.Template.Fonts);
                }
                else
                {
                    if (_ws.CurrentPlu == null)
                    {
                        const string message = "Не определен PLU";
                        const string caption = "Операция недоступна!";
                        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    zp.LogoUpload(_ws.CurrentScale.ZebraPrinter.Ip, _ws.CurrentScale.ZebraPrinter.Port, _ws.CurrentPlu.Template.Logo);
                    zp.FontsUpload(_ws.CurrentScale.ZebraPrinter.Ip, _ws.CurrentScale.ZebraPrinter.Port, _ws.CurrentPlu.Template.Fonts);
                }
            }
            catch (Exception ex)
            {
                _logUtils.Error(ex.Message);
                MessageBox.Show(@"Ошибка выгрузки ресурсов текущего шаблона!" + Environment.NewLine + ex.Message);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            CustomMessageBox messageBox = CustomMessageBox.Show(this, @"Прежде чем продолжить калибровку откройте крышку отделителя!",
                @"ВНИМАНИЕ!", MessageBoxButtons.RetryCancel);
            messageBox.Wait();
            //DialogResult dialogResult = MessageBox.Show(@"Прежде чем продолжить калибровку откройте крышку отделителя!", @"ВНИМАНИЕ!", MessageBoxButtons.RetryCancel);
            if (messageBox.Result == DialogResult.Retry)
            {
                try
                {
                    UseWaitCursor = true;
                    Thread.Sleep(10);
                    Application.DoEvents();

                    ZplConverterHelper zp = new ZplConverterHelper();
                    zp.Сalibration(_ws.CurrentScale.ZebraPrinter.Ip, _ws.CurrentScale.ZebraPrinter.Port);
                }
                catch (Exception ex)
                {
                    _logUtils.Error(ex.Message);
                    MessageBox.Show(@"Ошибка калибровки!" + Environment.NewLine + ex.Message);
                }
                finally
                {
                    UseWaitCursor = false;
                }
            }
        }

        private void ButtonGenerateException([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                throw new Exception("Test exception", new Exception("Test inner exception"));
            }
            catch (Exception ex)
            {
                _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                if (ex.InnerException != null)
                    _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += Environment.NewLine + ex.InnerException.Message;
                CustomMessageBox.Show(this, @"Генерация тестовой ошибки!" + Environment.NewLine + msg, Messages.Exception);
            }
        }

        private void ButtonGenerateException_Click(object sender, EventArgs e)
        {
            ButtonGenerateException();
        }

        #endregion

        #region Private methods - Управление принтером

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            _taskManager.PrintManager.SendAsync(ZplPipeUtils.ZplPowerOnReset());
        }

        /// <summary>
        /// Калибровать.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPrintCalibrate_Click(object sender, EventArgs e)
        {
            if (_ws.IsTscPrinter)
                _taskManager.PrintManager.PrintControl.Cmd.Calibrate(true, true);
            else
                _taskManager.PrintManager.SendAsync(ZplPipeUtils.ZplCalibration());
        }

        private void ButtonPrintOptions_Click(object sender, EventArgs e)
        {
            _taskManager.PrintManager.SendAsync(ZplPipeUtils.ZplPrintConfigurationLabel());
        }

        #endregion

        #region Private methods - Кнопки по умолчанию

        private void DefaultComPortName()
        {
            fieldComPort.Items.Clear();

            // Получить список COM-портов.
            System.Collections.Generic.List<string> listComPorts = SerialPort.GetPortNames().ToList();
            // Текущий порт из настроек.
            string curPort = string.Empty;
            if (_ws?.CurrentScale?.DeviceComPort != null)
            {
                curPort = _ws.CurrentScale.DeviceComPort;
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

        private void ButtonSqlCheck_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(fieldSqlConnectionString.Text))
            {
                con.Open();
                try
                {
                    //Properties.Settings.Default.ConnectionString = fieldSqlConnectionString.Text;
                    labelSqlStatus.Text = @"Подключение выполнено.";
                }
                catch (Exception ex)
                {
                    labelSqlStatus.Text = $@"Ошибка подключения! {ex.Message}";
                    _logUtils.Error(ex.Message);
                }
                con.Close();
            }
        }

        private void ButtonMassaParam_Click(object sender, EventArgs e)
        {
            fieldCurrentMKProp.Clear();

            if (_taskManager.MassaManager != null)
            {
                _taskManager.MassaManager.GetScalePar();
                Thread.Sleep(10);
                Application.DoEvents();

                if (_taskManager.MassaManager.DeviceParameters != null)
                {
                    fieldCurrentMKProp.Text = _taskManager.MassaManager.DeviceParameters.GetMessage();
                }

                if (_taskManager.MassaManager.DeviceError != null)
                {
                    fieldCurrentMKProp.Text = $@"{fieldCurrentMKProp.Text}\n{_taskManager.MassaManager.DeviceError.GetMessage()}";
                }
            }
        }

        private void ButtonPrintCancelAll_Click(object sender, EventArgs e)
        {
            _taskManager.PrintManager.SendAsync(ZplPipeUtils.ZplClearPrintBuffer());
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