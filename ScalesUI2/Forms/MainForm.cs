// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using EntitiesLib;
using Hardware;
using Hardware.MassaK;
using Hardware.Print;
using log4net;
using ScalesUI.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hardware.Memory;
using ScalesUI.Utils;
using UICommon;
using UICommon.WinForms.Utils;
using UtilsLib;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ScalesUI.Forms
{
    public partial class MainForm : Form
    {
        #region Private fields and properties

        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // Состояние устройства.
        private readonly SessionState _ws = SessionState.Instance;
        // Помощник мыши.
        //private readonly MouseHookHelper _mouse = MouseHookHelper.Instance;
        public Task TaskManager { get; set; }

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

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (_ws.CurrentScale != null)
            {
                // _ws.CurrentScale.Load(_app.GuidToString());
                buttonSelectOrder.Visible = !(buttonSelectPlu.Visible = !_ws.CurrentScale.UseOrder);
            }

            //_mouse.Init(progressBarCountBox);

            // Загрузить ресурсы.
            LoadResources();

            // Подписка.
            _ws.NotifyProductDate += NotifyProductDate;
            _ws.NotifyPlu += NotifyPlu;
            _ws.NotifyPalletSize += NotifyPalletSize;
            _ws.NotifyCurrentBox += NotifyCurrentBox;
            _ws.NotifyKneading += NotifyKneading;
            //_ws.PrintManager.Notify += NotifyPrint;
            //_ws.MassaManager.Notify += NotifyMassa;
            
            _ws.NewPallet();

            // Manager tasks.
            StartTaskManager();
        }

        private void LoadResources([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                var resourceManager = new System.Resources.ResourceManager("ScalesUI.Properties.Resources", Assembly.GetExecutingAssembly());
                var exit = resourceManager.GetObject("exit_2");
                if (exit != null)
                {
                    var bmpExit = new Bitmap((Bitmap)exit);
                    pictureBoxClose.Image = bmpExit;
                }

                Text = _ws.AppVersion;
                var sqlInstance = GetSqlInstance();
                if (sqlInstance.Equals("DVLP"))
                {
                    fieldTitle.Text = $@"{_ws.AppVersion}.  {_ws.CurrentScale.Description}. SQL: Тестовый сервер.";
                    fieldTitle.BackColor = Color.Yellow;
                }
                else if (sqlInstance.Equals("LUTON"))
                {
                    fieldTitle.Text = $@"{_ws.AppVersion}.  {_ws.CurrentScale.Description}.";
                    fieldTitle.BackColor = Color.LightGreen;
                }
                else
                {
                    fieldTitle.Text = $@"{_ws.AppVersion}.  {_ws.CurrentScale.Description}. SQL: {sqlInstance}";
                    fieldTitle.BackColor = Color.DarkRed;
                }

                fieldKneading.Text = string.Empty;
                fieldProductDate.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogEntity.SaveError(filePath, lineNumber, memberName, ex.Message);
                if (ex.InnerException != null)
                    LogEntity.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
                var msg = ex.Message;
                if (ex.InnerException != null)
                    msg += Environment.NewLine + ex.InnerException.Message;
                CustomMessageBox.Show(this, @"Ошибка загрузки ресурсов!" + Environment.NewLine + msg, Messages.Exception);
            }
        }

        public SqlCommand GetSqlInstanceCmd(SqlConnection con)
        {
            var query = @"select serverproperty('InstanceName') [InstanceName]";
            var cmd = new SqlCommand(query, con) { CommandType = CommandType.Text };
            cmd.Prepare();
            return cmd;
        }

        public string GetSqlInstance()
        {
            var result = string.Empty;
            using (var con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                var cmd = GetSqlInstanceCmd(con);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = SqlConnectFactory.GetValue<string>(reader, "InstanceName");
                    }
                }
                reader.Close();
            }
            return result;
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
                    using (var pinForm = new PasswordForm() { TopMost = !_ws.IsDebug })
                    {
                        isClose = pinForm.ShowDialog() == DialogResult.OK;
                        pinForm.Close();
                    }
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
            }
            catch (Exception ex)
            {
                LogEntity.SaveError(filePath, lineNumber, memberName, ex.Message);
                if (ex.InnerException != null)
                    LogEntity.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
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

            var ch = UtilsDt.GetProgressChar(_ws.MemoryManagerProgressChar);
            await AsyncControl.Properties.SetText.Async(fieldMemoryManager, 
                $"Использовано памяти: {_ws.MemoryManager.MemorySize.Physical.MegaBytes:N0} MB | {ch}").ConfigureAwait(false);
            _ws.MemoryManagerProgressChar = ch;
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
            _ws.CurrentBox = _ws.PrintManager.UserLabelCount < _ws.PalletSize ? _ws.PrintManager.UserLabelCount : _ws.PalletSize;
            // а когда зебра поддергивает ленту то счетчик увеличивается на 1 не может быть что-бы напечатано 3, а на форме 4
            if (_ws.CurrentBox == 0)
                _ws.CurrentBox = 1;

            var ch = UtilsDt.GetProgressChar(_ws.PrintManagerProgressChar);
            // TSC printers.
            if (_ws.CurrentScale?.ZebraPrinter != null && _ws.IsTscPrinter)
            {
                //if (_ws.PrintManager.PrintControl != null && !_ws.PrintManager.PrintControl.IsOpen)
                //    await AsyncControl.Properties.SetBackColor.Async(buttonPrint, _ws.PrintManager.PrintControl.IsOpen
                //        ? Color.FromArgb(192, 255, 192) : Color.Transparent).ConfigureAwait(false);
                if (_ws.PrintManager.PrintControl != null)
                {
                    //LedPrint.State = _ws.PrintManager.PrintControl.IsOpen;
                    await AsyncControl.Properties.SetText.Async(fieldPrintManager, _ws.PrintManager.PrintControl.IsStatusNormal
                        ? $"Принтер: доступен | {ch}" : $"Принтер: недоступен | {ch}").ConfigureAwait(false);
                }
            }
            // Zebra printers.
            else
            {
                var state = _ws.PrintManager.CurrentStatus;
                // надо переприсвоить т.к. на CurrentBox сделан Notify чтоб выводить на экран
                _ws.CurrentBox = _ws.PrintManager.UserLabelCount < _ws.PalletSize ? _ws.PrintManager.UserLabelCount : _ws.PalletSize;
                // а когда зебра поддергивает ленту то счетчик увеличивается на 1 не может быть что-бы напечатано 3, а на форме 4
                if (_ws.CurrentBox == 0)
                    _ws.CurrentBox = 1;
                if (state != null && !state.isReadyToPrint)
                    await AsyncControl.Properties.SetBackColor.Async(buttonPrint, state.isReadyToPrint
                        ? Color.FromArgb(192, 255, 192) : Color.Transparent).ConfigureAwait(false);
                if (state != null)
                {
                    //LedPrint.State = state.isReadyToPrint;
                    await AsyncControl.Properties.SetText.Async(fieldPrintManager, state.isReadyToPrint
                        ? $"Принтер: доступен | {_ws.PrintManager.PrintControl.IpAddress} | {ch}" : $"Принтер: недоступен | {ch}").ConfigureAwait(false);
                }
            }
            _ws.PrintManagerProgressChar = ch;
        }

        private async Task CallbackMassaManagerAsync(int wait)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            var flag = false;
            if (_ws.CurrentPlu != null)
            {
                flag = true;
                await AsyncControl.Properties.SetText.Async(labelPlu, _ws.CurrentPlu.CheckWeight == false
                    ? $"PLU (шт): {_ws.CurrentPlu.PLU}"
                    : $"PLU (вес): {_ws.CurrentPlu.PLU}").ConfigureAwait(false);
                var weight = _ws.MassaManager.WeightNet - _ws.CurrentPlu.GoodsTareWeight;
                await AsyncControl.Properties.SetText.Async(fieldWeightNetto, $"{weight:0.000} кг").ConfigureAwait(false);
                //await AsyncControl.Properties.SetBackColor.Async(fieldWeightNetto,
                //    _ws.MassaManager.IsStable == 0x01 ? Color.FromArgb(150, 255, 150) : Color.Transparent).ConfigureAwait(false);
                //AsyncControl.Properties.SetText.Async(fieldWeightTare, 
                //    $"{(float)getMassa.Tare / getMassa.ScaleFactor:0.000} кг");
            }

            //LedMassa.State = _ws.MassaManager.IsStable == 1;
            var ch = UtilsDt.GetProgressChar(_ws.MassaManagerProgressChar);
            await AsyncControl.Properties.SetText.Async(fieldMassaManager, _ws.MassaManager.IsReady || _ws.MassaManager.IsStable == 1
                ? $"Весы: доступны | Вес брутто: { _ws.MassaManager.WeightNet:0.000} кг | {ch}" 
                : $"Весы: недоступны | Вес брутто: { _ws.MassaManager.WeightNet:0.000} кг | {ch}").ConfigureAwait(false);
            _ws.MassaManagerProgressChar = ch;
            if (!flag)
            {
                await AsyncControl.Properties.SetText.Async(labelPlu, "PLU").ConfigureAwait(false);
                await AsyncControl.Properties.SetText.Async(fieldWeightNetto, "0,000 кг").ConfigureAwait(false);
                //await AsyncControl.Properties.SetBackColor.Async(fieldWeightNetto, Color.Transparent).ConfigureAwait(false);
            }
        }

        private async Task TaskDeviceManagerAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            // MemoryManager.
            var taskMemory = new Task(() =>
            {
                while (!_ws.MemoryManagerIsExit)
                {
                    if (_ws.MemoryManager == null)
                    {
                        _ws.MemoryManager = new MemoryManagerEntity(1_000, 5_000, 5_000);
                    }
                    _ws.MemoryManager.Open(CallbackMemoryManagerAsync);
                }
            });
            taskMemory.Start();

            // PrintManager.
            var taskPrint = new Task(() =>
            {
                while (!_ws.PrintManagerIsExit)
                {
                    if (_ws.PrintManager == null)
                    {
                        _ws.PrintManager = new PrintManagerEntity(_ws.CurrentScale.ZebraPrinter.Ip, _ws.CurrentScale.ZebraPrinter.Port, 1_000, 5_000, 5_000);
                    }
                    _ws.PrintManager.Open(_ws.IsTscPrinter, CallbackPrintManagerAsync);
                    // STOP
                    //if (_ws.PrintManager.PrintControl != null && _ws.IsTscPrinter && !_ws.PrintManager.PrintControl.IsStatusNormal)
                    //{
                    //    _ws.PrintManager.PrintControl.Close();
                    //    _ws.PrintManager.PrintControl = null;
                    //}
                }
            });
            taskPrint.Start();

            // DeviceManager.
            var taskDevice = new Task(() =>
            {
                while (!_ws.DeviceManagerIsExit)
                {
                    if (_ws.DeviceManager == null)
                    {
                        _ws.DeviceManager = new DeviceManagerEntity(1_000, 5_000, 5_000);
                    }
                    _ws.DeviceManager.Open(CallbackDeviceManagerAsync);
                }
            });
            taskDevice.Start();

            // MassaManager.
            var taskMassa = new Task(() =>
            {
                while (!_ws.MassaManagerIsExit)
                {
                    if (_ws.MassaManager == null)
                    {
                        var deviceSocketRs232 = new DeviceSocketRs232(_ws.CurrentScale.DeviceComPort);
                        _ws.MassaManager = new MassaManagerEntity(deviceSocketRs232, 1_000, 5_000, 5_000);
                        ButtonSetZero_Click(null, null);
                    }
                    _ws.MassaManager.Open(CallbackMassaManagerAsync);
                }
            });
            taskMassa.Start();
        }

        private void StartTaskManager()
        {
            Application.DoEvents();
            if (TaskManager == null)
            {
                _ws.MemoryManagerIsExit = false;
                _ws.DeviceManagerIsExit = false;
                _ws.PrintManagerIsExit = false;
                _ws.MassaManagerIsExit = false;

                TaskManager = new Task(async () => { await TaskDeviceManagerAsync().ConfigureAwait(false); });
                TaskManager.ConfigureAwait(false);
                TaskManager.Start();
            }
        }

        private void StopTaskManager()
        {
            _ws.MemoryManagerIsExit = true;
            _ws.DeviceManagerIsExit = true;
            _ws.PrintManagerIsExit = true;
            _ws.MassaManagerIsExit = true;

            _ws.MemoryManager?.Close();
            _ws.DeviceManager?.Close();
            _ws.MassaManager?.Close();
            _ws.PrintManager?.Close();

            if (_ws.PrintManager?.PrintControl != null && _ws.IsTscPrinter && !_ws.PrintManager.PrintControl.IsStatusNormal)
            {
                _ws.PrintManager.PrintControl.Close();
                //_ws.PrintManager.PrintControl = null;
            }

            TaskManager?.Dispose();
            TaskManager = null;

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

        private void NotifyPalletSize(int palletSize)
        {
            AsyncControl.Properties.SetText.Async(fieldPalletSize, $"{_ws.CurrentBox}/{_ws.PalletSize}");
        }
        
        private void NotifyCurrentBox(int currentBox)
        {
            AsyncControl.Properties.SetText.Async(fieldPalletSize, $"{_ws.CurrentBox}/{_ws.PalletSize}");
        }

        private void NotifyPlu(PluEntity plu)
        {
            var strCheckWeight = plu?.CheckWeight == true ? "вес" : "шт";
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
                    var pinForm = new PasswordForm();
                    if (pinForm.ShowDialog() == DialogResult.OK)
                    {
                        OpenFormSettings();
                    }
                    pinForm.Close();
                }
            }
            catch (Exception ex)
            {
                LogEntity.SaveError(filePath, lineNumber, memberName, ex.Message);
                if (ex.InnerException != null)
                    LogEntity.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
                var msg = ex.Message;
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
            using (var settingsForm = new SettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }

        private void ButtonSetZero([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                if (_ws.MassaManager.WeightNet > Messages.MassaThreshold || _ws.MassaManager.WeightNet < -Messages.MassaThreshold)
                {
                    var messageBox = CustomMessageBox.Show(this, Messages.MassaCheck, Messages.OperationControl,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    messageBox.Wait();
                    if (messageBox.Result != DialogResult.Yes)
                        return;
                }

                _ws.MassaManager.SetZero();
                _ws.CurrentPlu = null;
            }
            catch (Exception ex)
            {
                LogEntity.SaveError(filePath, lineNumber, memberName, ex.Message);
                if (ex.InnerException != null)
                    LogEntity.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
                var msg = ex.Message;
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
                if (_ws.MassaManager.WeightNet > Messages.MassaThreshold || _ws.MassaManager.WeightNet < -Messages.MassaThreshold)
                {
                    var messageBox = CustomMessageBox.Show(this, Messages.MassaCheck, Messages.OperationControl,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    messageBox.Wait();
                    if (messageBox.Result != DialogResult.Yes)
                        return;
                }

                // PLU form.
                using (var pluListForm = new PluListForm() { Owner = this })
                {
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
            }
            catch (Exception ex)
            {
                LogEntity.SaveError(filePath, lineNumber, memberName, ex.Message);
                if (ex.InnerException != null)
                    LogEntity.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
                var msg = ex.Message;
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
                    using (OrderListForm settingsForm = new OrderListForm())
                    {
                        if (settingsForm.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                }
                else
                {
                    using (OrderDetailForm settingsForm = new OrderDetailForm())
                    {
                        var dialogResult = settingsForm.ShowDialog();

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
                LogEntity.SaveError(filePath, lineNumber, memberName, ex.Message);
                if (ex.InnerException != null)
                    LogEntity.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
                var msg = ex.Message;
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
                
                using (var kneadingNumberForm = new SetKneadingNumberForm { Owner = this })
                {
                    if (kneadingNumberForm.ShowDialog() == DialogResult.OK)
                    {
                        //_ws.Kneading = settingsForm.CurrentKneading;
                        //_ws.ProductDate = settingsForm.CurrentProductDate;
                    }
                }
            }
            catch (Exception ex)
            {
                LogEntity.SaveError(filePath, lineNumber, memberName, ex.Message);
                if (ex.InnerException != null)
                    LogEntity.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
                var msg = ex.Message;
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
                LogEntity.SaveError(filePath, lineNumber, memberName, ex.Message);
                if (ex.InnerException != null)
                    LogEntity.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
                var msg = ex.Message;
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
                LogEntity.SaveError(filePath, lineNumber, memberName, ex.Message);
                if (ex.InnerException != null)
                    LogEntity.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
                var msg = ex.Message;
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
            using (var numberInputForm = new NumberInputForm {InputValue = 0})
            {
                // _ws.Kneading;
                if (numberInputForm.ShowDialog() == DialogResult.OK)
                {
                    _ws.Kneading = numberInputForm.InputValue;
                }
            }
        }

        private void ButtonNewPallet_Click(object sender, EventArgs e)
        {
            _ws.NewPallet();
        }

        #endregion
    }
}
