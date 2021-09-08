// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.Utils;
using DataShareCore;
using log4net;
using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Managers;
using WeightCore.Models;
using WeightCore.WinForms.Utils;

namespace ScalesUI.Forms
{
    public partial class MainForm : Form
    {
        #region Private fields and properties

        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly SessionState _ws = SessionState.Instance;
        private TaskManagerEntity _taskManager = TaskManagerEntity.Instance;
        private LogUtils _logUtils = LogUtils.Instance;

        #endregion

        #region MainForm methods

        public MainForm()
        {
            InitializeComponent();

            FormBorderStyle = _ws.IsDebug ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
            TopMost = !_ws.IsDebug;
            fieldResolution.Visible = _ws.IsDebug;
            fieldResolution.SelectedIndex = _ws.IsDebug ? 1 : 0;
            //_mouse.Owner = this;
        }

        private void ActionFormLoad([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (_ws.CurrentScale != null)
            {
                // _ws.CurrentScale.Load(_app.GuidToString());
                buttonSelectOrder.Visible = !(buttonSelectPlu.Visible = !(_ws.CurrentScale.UseOrder==true));
            }

            //_mouse.Init(progressBarCountBox);

            // Загрузить ресурсы.
            LoadResources();

            // Callbacks.
            _ws.NotifyProductDate += NotifyProductDate;
            _ws.NotifyPlu += NotifyPlu;
            _ws.NotifyLabelsCount += NotifyLabelsCount;
            //_ws.NotifyLabelsCurrent += NotifyCurrentBox;
            _ws.LabelsCurrentRefresh = LabelsCurrentRefresh;
            _ws.NotifyKneading += NotifyKneading;

            _ws.NewPallet();

            // Manager tasks.
            StartTaskManager();

            _logUtils.Information("The program is runned", filePath, memberName, lineNumber);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ActionFormLoad();
        }

        private void LoadResources([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                System.Resources.ResourceManager resourceManager = new("ScalesUI.Properties.Resources", Assembly.GetExecutingAssembly());
                object exit = resourceManager.GetObject("exit_2");
                if (exit != null)
                {
                    Bitmap bmpExit = new((Bitmap)exit);
                    pictureBoxClose.Image = bmpExit;
                }

                Text = _ws.AppVersion;
                switch (_ws.SqlViewModel.PublishType)
                {
                    case ShareEnums.PublishType.Debug:
                    case ShareEnums.PublishType.Dev:
                        fieldTitle.Text = $@"{_ws.AppVersion}.  {_ws.CurrentScale.Description}. SQL: {_ws.SqlViewModel.PublishDescription}.";
                        fieldTitle.BackColor = Color.Yellow;
                        break;
                    case ShareEnums.PublishType.Release:
                        fieldTitle.Text = $@"{_ws.AppVersion}.  {_ws.CurrentScale.Description}.";
                        fieldTitle.BackColor = Color.LightGreen;
                        break;
                    case ShareEnums.PublishType.Default:
                    default:
                        fieldTitle.Text = $@"{_ws.AppVersion}.  {_ws.CurrentScale.Description}. SQL: {_ws.SqlViewModel.PublishDescription}.";
                        fieldTitle.BackColor = Color.DarkRed;
                        break;
                }

                fieldKneading.Text = string.Empty;
                fieldProductDate.Text = string.Empty;
            }
            catch (Exception ex)
            {
                _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                if (ex.InnerException != null)
                    _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += Environment.NewLine + ex.InnerException.Message;
                CustomMessageBox.Show(this, @"Ошибка загрузки ресурсов!" + Environment.NewLine + msg, Messages.Exception);
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.Alt && e.KeyCode == Keys.Q)
                || (e.Alt && e.KeyCode == Keys.X)
                || (e.Control && e.KeyCode == Keys.Q)
                || e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void ActionFormClosing(FormClosingEventArgs e, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                bool isClose;
                if (_ws.IsDebug)
                {
                    isClose = true;
                }
                else
                {
                    using PasswordForm pinForm = new() { TopMost = !_ws.IsDebug };
                    isClose = pinForm.ShowDialog() == DialogResult.OK;
                    pinForm.Close();
                }
                Application.DoEvents();
                if (isClose)
                {
                    StopTaskManager();
                    //_mouse?.Close();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
                _logUtils.Information("The program is closed", filePath, memberName, lineNumber);
            }
            catch (Exception ex)
            {
                _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                if (ex.InnerException != null)
                    _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ActionFormClosing(e);
        }

        #endregion

        #region Public and private methods - Tasks

        private async Task CallbackMemoryManagerAsync(int wait)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            char ch = StringUtils.GetProgressChar(_taskManager.MemoryManagerProgressChar);
            await AsyncControl.Properties.SetText.Async(fieldMemoryManager,
                $"Использовано памяти: {_taskManager.MemoryManager.MemorySize.Physical.MegaBytes:N0} MB | {ch}").ConfigureAwait(false);
            _taskManager.MemoryManagerProgressChar = ch;
        }

        private async Task CallbackDeviceManagerAsync(int wait)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            AsyncControl.Properties.SetText.Sync(fieldCurrentTime, DateTime.Now.ToString(@"dd.MM.yyyy HH:mm:ss"));
        }

        private async Task CallbackPrintManagerAsync(int wait)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            //try
            //{
            //}
            //catch (Exception ex)
            //{
            //    if (CustomMessageBox.Show($"Печатающее устройство недоступно ({_ws.CurrentScale.ZebraPrinter}). {ex.Message}") == DialogResult.OK)
            //    {

            //    }
            //    //throw new Exception(ex.Message);
            //}

            // надо переприсвоить т.к. на CurrentBox сделан Notify чтоб выводить на экран
            _ws.LabelsCurrent = _taskManager.PrintManager.UserLabelCount < _ws.LabelsCount ? _taskManager.PrintManager.UserLabelCount : _ws.LabelsCount;
            // а когда зебра поддергивает ленту то счетчик увеличивается на 1 не может быть что-бы напечатано 3, а на форме 4
            if (_ws.LabelsCurrent == 0)
                _ws.LabelsCurrent = 1;

            char ch = StringUtils.GetProgressChar(_taskManager.PrintManagerProgressChar);
            // TSC printers.
            if (_ws.CurrentScale?.ZebraPrinter != null && _ws.IsTscPrinter)
            {
                //if (_ws.PrintManager.PrintControl != null && !_ws.PrintManager.PrintControl.IsOpen)
                //    await AsyncControl.Properties.SetBackColor.Async(buttonPrint, _ws.PrintManager.PrintControl.IsOpen
                //        ? Color.FromArgb(192, 255, 192) : Color.Transparent).ConfigureAwait(false);
                if (_taskManager.PrintManager.PrintControl != null)
                {
                    //LedPrint.State = _ws.PrintManager.PrintControl.IsOpen;
                    await AsyncControl.Properties.SetText.Async(fieldPrintManager, _taskManager.PrintManager.PrintControl.IsStatusNormal
                        ? $"Принтер: доступен | {ch}" : $"Принтер: недоступен | {ch}").ConfigureAwait(false);
                }
            }
            // Zebra printers.
            else
            {
                Zebra.Sdk.Printer.PrinterStatus state = _taskManager.PrintManager.CurrentStatus;
                // надо переприсвоить т.к. на CurrentBox сделан Notify чтоб выводить на экран
                _ws.LabelsCurrent = _taskManager.PrintManager.UserLabelCount < _ws.LabelsCount ? _taskManager.PrintManager.UserLabelCount : _ws.LabelsCount;
                // а когда зебра поддергивает ленту то счетчик увеличивается на 1 не может быть что-бы напечатано 3, а на форме 4
                if (_ws.LabelsCurrent == 0)
                    _ws.LabelsCurrent = 1;
                if (state != null && !state.isReadyToPrint)
                    await AsyncControl.Properties.SetBackColor.Async(buttonPrint, state.isReadyToPrint
                        ? Color.FromArgb(192, 255, 192) : Color.Transparent).ConfigureAwait(false);
                if (state != null)
                {
                    //LedPrint.State = state.isReadyToPrint;
                    await AsyncControl.Properties.SetText.Async(fieldPrintManager, state.isReadyToPrint
                        ? $"Принтер: доступен | {_taskManager.PrintManager.PrintControl.IpAddress} | {ch}" : $"Принтер: недоступен | {ch}").ConfigureAwait(false);
                }
            }
            _taskManager.PrintManagerProgressChar = ch;
        }

        private async Task CallbackMassaManagerAsync(int wait)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            bool flag = false;
            if (_ws.CurrentPlu != null)
            {
                flag = true;
                await AsyncControl.Properties.SetText.Async(labelPlu, _ws.CurrentPlu.CheckWeight == false
                    ? $"PLU (шт): {_ws.CurrentPlu.PLU}"
                    : $"PLU (вес): {_ws.CurrentPlu.PLU}").ConfigureAwait(false);
                decimal weight = _taskManager.MassaManager.WeightNet - _ws.CurrentPlu.GoodsTareWeight;
                await AsyncControl.Properties.SetText.Async(fieldWeightNetto, $"{weight:0.000} кг").ConfigureAwait(false);
                //await AsyncControl.Properties.SetBackColor.Async(fieldWeightNetto,
                //    _ws.MassaManager.IsStable == 0x01 ? Color.FromArgb(150, 255, 150) : Color.Transparent).ConfigureAwait(false);
                //AsyncControl.Properties.SetText.Async(fieldWeightTare, 
                //    $"{(float)getMassa.Tare / getMassa.ScaleFactor:0.000} кг");
            }

            //LedMassa.State = _ws.MassaManager.IsStable == 1;
            char ch = StringUtils.GetProgressChar(_taskManager.MassaManagerProgressChar);
            await AsyncControl.Properties.SetText.Async(fieldMassaManager, _taskManager.MassaManager.IsReady || _taskManager.MassaManager.IsStable == 1
                ? $"Весы: доступны | Вес брутто: { _taskManager.MassaManager.WeightNet:0.000} кг | {ch}"
                : $"Весы: недоступны | Вес брутто: { _taskManager.MassaManager.WeightNet:0.000} кг | {ch}").ConfigureAwait(false);
            _taskManager.MassaManagerProgressChar = ch;
            if (!flag)
            {
                await AsyncControl.Properties.SetText.Async(labelPlu, "PLU").ConfigureAwait(false);
                await AsyncControl.Properties.SetText.Async(fieldWeightNetto, "0,000 кг").ConfigureAwait(false);
                //await AsyncControl.Properties.SetBackColor.Async(fieldWeightNetto, Color.Transparent).ConfigureAwait(false);
            }
        }

        private void StartTaskManager([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            Application.DoEvents();
            try
            {
                //if (_taskManager.TaskManager == null)
                {
                    _taskManager.MemoryManagerIsExit = false;
                    _taskManager.DeviceManagerIsExit = false;
                    _taskManager.PrintManagerIsExit = false;
                    _taskManager.MassaManagerIsExit = false;

                    //_taskManager.TaskManager = new Task(async () => { 
                    Task.Run(async () => {
                        await _taskManager.TaskRunMemoryManagerAsync(CallbackMemoryManagerAsync, _ws.SqlViewModel)
                            .ConfigureAwait(false); 
                    });
                    Task.Run(async () => {
                        await _taskManager.TaskRunPrintManagerAsync(CallbackPrintManagerAsync, _ws.SqlViewModel, 
                            _ws.IsTscPrinter, _ws.CurrentScale)
                        .ConfigureAwait(false);
                    });
                    Task.Run(async () => {
                        await _taskManager.TaskRunDeviceManagerAsync(CallbackDeviceManagerAsync, _ws.SqlViewModel)
                        .ConfigureAwait(false);
                    });
                    Task.Run(async () => {
                        await _taskManager.TaskRunMassaManagerAsync(CallbackMassaManagerAsync, ButtonSetZero, _ws.SqlViewModel, 
                            _ws.IsTscPrinter, _ws.CurrentScale)
                        .ConfigureAwait(false);
                    });
                    //_taskManager.TaskManager.ConfigureAwait(false);
                    //_taskManager.TaskManager.Start();
                }
            }
            catch (Exception ex)
            {
                _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                if (ex.InnerException != null)
                    _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
            }
        }

        private void StopTaskManager([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                _taskManager.MemoryManagerIsExit = true;
                _taskManager.DeviceManagerIsExit = true;
                _taskManager.PrintManagerIsExit = true;
                _taskManager.MassaManagerIsExit = true;

                _taskManager.MemoryManager?.Close();
                _logUtils.Information("MassaManager is closed", filePath, memberName, lineNumber);
                _taskManager.DeviceManager?.Close();
                _logUtils.Information("MassaManager is closed", filePath, memberName, lineNumber);
                _taskManager.MassaManager?.Close();
                _logUtils.Information("MassaManager is closed", filePath, memberName, lineNumber);
                _taskManager.PrintManager?.Close();
                _logUtils.Information("MassaManager is closed", filePath, memberName, lineNumber);

                if (_taskManager.PrintManager?.PrintControl != null && _ws.IsTscPrinter && !_taskManager.PrintManager.PrintControl.IsStatusNormal)
                {
                    _taskManager.PrintManager.PrintControl.Close();
                    //_ws.PrintManager.PrintControl = null;
                }

                //_taskManager.TaskManager?.Dispose();
                //_taskManager.TaskManager = null;
            }
            catch (Exception ex)
            {
                _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                if (ex.InnerException != null)
                    _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
            }

            Application.DoEvents();
        }

        #endregion

        #region Private methods - Notifications

        //private void NotifyMassa(MassaManagerEntity message)
        //{
        //    var flag = false;
        //    if (message != null)
        //    {
        //        AsyncControl.Properties.SetText.Async(fieldGrossWeight, $"Вес брутто: {message.WeightNet:0.000} кг");
        //        if (_ws.CurrentPlu != null)
        //        {
        //            flag = true;
        //            AsyncControl.Properties.SetText.Async(labelPlu, _ws.CurrentPlu.CheckWeight == false
        //                ? $"PLU (шт): {_ws.CurrentPlu.PLU}" : $"PLU (вес): {_ws.CurrentPlu.PLU}");
        //            var weight = message.WeightNet - _ws.CurrentPlu.GoodsTareWeight;
        //            AsyncControl.Properties.SetText.Async(fieldWeightNetto, $"{weight:0.000} кг");
        //            AsyncControl.Properties.SetBackColor.Async(fieldWeightNetto,
        //                message.IsStable == 0x01 ? Color.FromArgb(150, 255, 150) : Color.Transparent);
        //            //AsyncControl.Properties.SetText.Async(fieldWeightTare, 
        //            //    $"{(float)getMassa.Tare / getMassa.ScaleFactor:0.000} кг");
        //        }
        //        if (message.IsReady)
        //            LedMassa.State = message.IsStable == 1;
        //    }
        //    if (!flag)
        //    {
        //        AsyncControl.Properties.SetText.Async(labelPlu, "PLU");
        //        AsyncControl.Properties.SetText.Async(fieldWeightNetto, "0,000 кг");
        //        AsyncControl.Properties.SetBackColor.Async(fieldWeightNetto, Color.Transparent);
        //    }
        //}

        private void NotifyProductDate(DateTime productDate)
        {
            AsyncControl.Properties.SetText.Async(fieldProductDate, $"{productDate:dd.MM.yyyy}");
        }

        private void NotifyKneading(int kneading)
        {
            AsyncControl.Properties.SetText.Async(fieldKneading, $"{kneading}");
        }

        private void NotifyLabelsCount(int palletSize)
        {
            AsyncControl.Properties.SetText.Async(fieldLabelsCount, $"{_ws.LabelsCurrent}/{_ws.LabelsCount}");
        }

        private void LabelsCurrentRefresh(int labelCurrent)
        {
            AsyncControl.Properties.SetText.Async(fieldLabelsCount, $"{_ws.LabelsCurrent}/{_ws.LabelsCount}");
        }

        private void NotifyPlu(PluDirect plu)
        {
            string strCheckWeight = plu?.CheckWeight == true ? "вес" : "шт";
            AsyncControl.Properties.SetText.Async(fieldPlu, plu != null
                ? $"{plu.PLU} | {strCheckWeight} | {plu.GoodsName}" : string.Empty);
            AsyncControl.Properties.SetEnabled.Async(buttonPrint, plu != null);
            AsyncControl.Properties.SetText.Async(fieldWeightTare, plu != null ? $"{plu.GoodsTareWeight:0.000} кг" : "0,000 кг");
            _log.Info($"Смена PLU: {plu?.GoodsName}");
            _log.Debug($"PLU.GoodsTareWeight: {plu?.GoodsTareWeight}");
        }

        //[Obsolete(@"Use NotifyPrint")]
        //private void NotifyPrintOld(PrintEntity zebraPrinter)
        //{
        //    var state = zebraPrinter.CurrentStatus;
        //    // надо переприсвоить т.к. на CurrentBox сделан Notify чтоб выводить на экран
        //    _ws.CurrentBox = _ws.PrintDevice.UserLabelCount < _ws.PalletSize ? _ws.PrintDevice.UserLabelCount : _ws.PalletSize;
        //    // а когда зебра поддергивает ленту то счетчик увеличивается на 1 не может быть что-бы напечатано 3, а на форме 4
        //    if (_ws.CurrentBox == 0)
        //        _ws.CurrentBox = 1;
        //    if (state != null && !state.isReadyToPrint)  
        //        AsyncControl.Properties.SetBackColor.Async(buttonPrint, state.isReadyToPrint 
        //            ? Color.FromArgb(192, 255, 192) : Color.Transparent);
        //    if (state != null) 
        //        LedPrint.State = state.isReadyToPrint;
        //}

        #endregion

        #region Private methods

        private void ButtonSettings([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                StopTaskManager();

                if (_ws.IsDebug)
                {
                    OpenFormSettings();
                }
                else
                {
                    PasswordForm pinForm = new();
                    if (pinForm.ShowDialog() == DialogResult.OK)
                    {
                        OpenFormSettings();
                    }
                    pinForm.Close();
                }
            }
            catch (Exception ex)
            {
                _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                if (ex.InnerException != null)
                    _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += Environment.NewLine + ex.InnerException.Message;
                CustomMessageBox.Show(this, @"Ошибка вызова формы настроек!" + Environment.NewLine + msg, Messages.Exception);
            }
            finally
            {
                AsyncControl.Select.Invoke(buttonPrint);
                StartTaskManager();
            }
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            ButtonSettings();
        }

        private void OpenFormSettings()
        {
            using SettingsForm settingsForm = new();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void ButtonSetZero(
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                if (_taskManager.MassaManager.WeightNet > Messages.MassaThreshold || _taskManager.MassaManager.WeightNet < -Messages.MassaThreshold)
                {
                    CustomMessageBox messageBox = CustomMessageBox.Show(this, Messages.MassaCheck, Messages.OperationControl,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    messageBox.Wait();
                    if (messageBox.Result != DialogResult.Yes)
                        return;
                }

                _taskManager.MassaManager.SetZero();
                _ws.CurrentPlu = null;
            }
            catch (Exception ex)
            {
                _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                if (ex.InnerException != null)
                    _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += Environment.NewLine + ex.InnerException.Message;
                CustomMessageBox.Show(this, @"Ошибка задания 0!" + Environment.NewLine + msg, Messages.Exception);
            }
            finally
            {
                AsyncControl.Select.Invoke(buttonPrint);
            }
        }

        private void ButtonSetZero_Click(object sender, EventArgs e)
        {
            ButtonSetZero();
        }

        private void ButtonSelectPlu([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                StopTaskManager();

                // Weight check.
                if (_taskManager.MassaManager != null)
                {
                    if (_taskManager.MassaManager.WeightNet > Messages.MassaThreshold || _taskManager.MassaManager.WeightNet < -Messages.MassaThreshold)
                    {
                        CustomMessageBox messageBox = CustomMessageBox.Show(this, Messages.MassaCheck, Messages.OperationControl,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        messageBox.Wait();
                        if (messageBox.Result != DialogResult.Yes)
                            return;
                    }
                }

                // PLU form.
                using PluListForm pluListForm = new() { Owner = this };
                // Commented from 2021-03-05.
                //buttonSetZero_Click(sender, e);
                if (pluListForm.ShowDialog() == DialogResult.OK)
                {
                    _ws.Kneading = 1;
                    _ws.ProductDate = DateTime.Now;
                    _ws.NewPallet();
                    //_mkDevice.SetTareWeight((int) (_ws.CurrentPLU.GoodsTareWeight * _ws.CurrentPLU.Scale.ScaleFactor));

                    // сразу перейдем к форме с замесами, размерами паллет и прочее
                    ButtonSetKneading();
                }
                else if (_ws.CurrentPlu != null)
                {
                    _ws.CurrentPlu = null;
                }
            }
            catch (Exception ex)
            {
                _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                if (ex.InnerException != null)
                    _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += Environment.NewLine + ex.InnerException.Message;
                CustomMessageBox.Show(this, @"Ошибка формы выбора PLU!" + Environment.NewLine + msg, Messages.Exception);
            }
            finally
            {
                AsyncControl.Select.Invoke(buttonPrint);
                StartTaskManager();
            }
        }

        private void ButtonSelectPlu_Click(object sender, EventArgs e)
        {
            ButtonSelectPlu();
        }

        private void ButtonSelectOrder([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                StopTaskManager();

                if (_ws.CurrentOrder == null)
                {
                    using OrderListForm settingsForm = new();
                    if (settingsForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
                else
                {
                    using OrderDetailForm settingsForm = new();
                    DialogResult dialogResult = settingsForm.ShowDialog();

                    if (dialogResult == DialogResult.Retry)
                    {
                        _ws.CurrentOrder = null;
                    }

                    if (dialogResult == DialogResult.OK)
                    {
                        //ws.Kneading = (int)settingsForm.currentKneading;
                    }

                    if (dialogResult == DialogResult.Cancel)
                    {
                        //ws.Kneading = (int)settingsForm.currentKneading;
                    }
                }
                if (_ws.CurrentOrder != null)
                {
                    fieldCountBox.Maximum = _ws.CurrentOrder.PlaneBoxCount;
                    fieldCountBox.Minimum = 0;
                    fieldCountBox.Value = _ws.CurrentOrder.FactBoxCount;
                }
            }
            catch (Exception ex)
            {
                _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                if (ex.InnerException != null)
                    _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += Environment.NewLine + ex.InnerException.Message;
                CustomMessageBox.Show(this, @"Ошибка вызова формы выбора заказа!" + Environment.NewLine + msg, Messages.Exception);
            }
            finally
            {
                AsyncControl.Select.Invoke(buttonPrint);
                StartTaskManager();
            }
        }

        private void ButtonSelectOrder_Click(object sender, EventArgs e)
        {
            ButtonSelectOrder();
        }

        private void ButtonSetKneading([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                StopTaskManager();

                using SetKneadingNumberForm kneadingNumberForm = new()
                { Owner = this };
                if (kneadingNumberForm.ShowDialog() == DialogResult.OK)
                {
                    //_ws.Kneading = settingsForm.CurrentKneading;
                    //_ws.ProductDate = settingsForm.CurrentProductDate;
                }
            }
            catch (Exception ex)
            {
                _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                if (ex.InnerException != null)
                    _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += Environment.NewLine + ex.InnerException.Message;
                CustomMessageBox.Show(this, @"Ошибка формы выбора замеса!" + Environment.NewLine + msg, Messages.Exception);
            }
            finally
            {
                AsyncControl.Select.Invoke(buttonPrint);
                StartTaskManager();
            }
        }

        private void ButtonSetKneading_Click(object sender, EventArgs e)
        {
            ButtonSetKneading();
        }

        private void ButtonPrint([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                _ws?.ProcessWeighingResult(Owner);
            }
            catch (Exception ex)
            {
                _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                if (ex.InnerException != null)
                    _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += Environment.NewLine + ex.InnerException.Message;
                CustomMessageBox.Show(this, @"Ошибка формы печати!" + Environment.NewLine + msg, Messages.Exception);
            }
            finally
            {
                AsyncControl.Select.Invoke(buttonPrint);
            }
        }

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            ButtonPrint();
        }

        private void PictureBoxClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FieldResolution_Selected([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                switch (fieldResolution.SelectedIndex)
                {
                    // 1024х768
                    case 1:
                        WindowState = FormWindowState.Normal;
                        Size = new Size(1024, 768);
                        break;
                    // 1366х768
                    case 2:
                        WindowState = FormWindowState.Normal;
                        Size = new Size(1366, 768);
                        break;
                    // 1920х1080
                    case 3:
                        WindowState = FormWindowState.Normal;
                        Size = new Size(1920, 1080);
                        break;
                    // Максимальное
                    default:
                        WindowState = FormWindowState.Maximized;
                        break;
                }
                CenterToScreen();
            }
            catch (Exception ex)
            {
                _logUtils.Error(ex.Message, filePath, memberName, lineNumber);
                if (ex.InnerException != null)
                    _logUtils.Error(ex.InnerException.Message, filePath, memberName, lineNumber);
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += Environment.NewLine + ex.InnerException.Message;
                CustomMessageBox.Show(this, @"Ошибка изменения разрешения формы!" + Environment.NewLine + msg, Messages.Exception);
            }
        }

        private void FieldResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            FieldResolution_Selected();
        }

        private void FieldDt_DoubleClick(object sender, EventArgs e)
        {
            ServiceMessagesWindow.BuildServiceMessagesWindow(this);
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (Equals(e.Button, MouseButtons.Middle))
            {
                ButtonPrint_Click(sender, e);
            }
        }

        private void ButtonAddKneading_Click(object sender, EventArgs e)
        {
            //_ws.RotateKneading(Direction.forward);
            using NumberInputForm numberInputForm = new()
            { InputValue = 0 };
            // _ws.Kneading;
            if (numberInputForm.ShowDialog() == DialogResult.OK)
            {
                _ws.Kneading = numberInputForm.InputValue;
            }
        }

        private void ButtonNewPallet_Click(object sender, EventArgs e)
        {
            _ws.NewPallet();
        }

        private void fieldTitle_DoubleClick(object sender, EventArgs e)
        {
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.SqlSettings, false) { Width = 400, Height = 400 };
            if (wpfPageLoader.ShowDialog(this) == DialogResult.OK)
            {
                //
            }
        }

        #endregion
    }
}
