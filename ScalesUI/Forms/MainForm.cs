// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.Utils;
using DataShareCore;
using DataShareCore.Helpers;
using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Managers;
using WeightCore.Models;

namespace ScalesUI.Forms
{
    public partial class MainForm : Form
    {
        #region Private fields and properties

        private readonly AppVersionHelper _appVersion = AppVersionHelper.Instance;
        private readonly SessionState _ws = SessionState.Instance;
        private readonly TaskManagerEntity _taskManager = TaskManagerEntity.Instance;
        private readonly LogUtils _logUtils = LogUtils.Instance;

        #endregion

        #region MainForm methods

        public MainForm()
        {
            InitializeComponent();

            FormBorderStyle = _ws.IsDebug ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
            TopMost = !_ws.IsDebug;
            fieldResolution.Visible = _ws.IsDebug;
            fieldResolution.SelectedIndex = _ws.IsDebug ? 1 : 0;
        }

        private void ActionFormLoad([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (_ws.CurrentScale != null)
            {
                // _ws.CurrentScale.Load(_app.GuidToString());
                buttonSelectOrder.Visible = !(buttonSelectPlu.Visible = !(_ws.CurrentScale.UseOrder==true));
            }

            // Загрузить ресурсы.
            LoadResources();

            // Callbacks.
            _ws.ProductDateRefresh = ProductDateRefresh;
            _ws.PluRefresh = PluRefresh;
            _ws.LabelsCurrentRefresh = LabelsCurrentRefresh;
            _ws.KneadingRefresh = KneadingRefresh;

            _ws.NewPallet();

            // Manager tasks.
            _taskManager.Open(CallbackDeviceManagerAsync, CallbackMemoryManagerAsync, CallbackPrintManagerAsync, CallbackMassaManagerAsync,
                ButtonSetZero, _ws.SqlViewModel, _ws.IsTscPrinter, _ws.CurrentScale);

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

                // Text = _appVersionHelper.AppTitle;
                MDSoft.WinFormsUtils.InvokeControl.SetText(this, _appVersion.AppTitle);
                switch (_ws.SqlViewModel.PublishType)
                {
                    case ShareEnums.PublishType.Debug:
                    case ShareEnums.PublishType.Dev:
                        //fieldTitle.Text = $@"{_ws.AppVersion}.  {_ws.CurrentScale.Description}. SQL: {_ws.SqlViewModel.PublishDescription}.";
                        MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle, $@"{_appVersion.AppTitle}.  {_ws.CurrentScale.Description}. SQL: {_ws.SqlViewModel.PublishDescription}.");
                        fieldTitle.BackColor = Color.Yellow;
                        break;
                    case ShareEnums.PublishType.Release:
                        //fieldTitle.Text = $@"{_ws.AppVersion}.  {_ws.CurrentScale.Description}.";
                        MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle, $@"{_appVersion.AppTitle}.  {_ws.CurrentScale.Description}.");
                        fieldTitle.BackColor = Color.LightGreen;
                        break;
                    case ShareEnums.PublishType.Default:
                    default:
                        //fieldTitle.Text = $@"{_ws.AppVersion}.  {_ws.CurrentScale.Description}. SQL: {_ws.SqlViewModel.PublishDescription}.";
                        MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle, $@"{_appVersion.AppTitle}.  {_ws.CurrentScale.Description}. SQL: {_ws.SqlViewModel.PublishDescription}.");
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
                if (isClose)
                {
                    _taskManager.Close();
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
            finally
            {
                Application.DoEvents();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ActionFormClosing(e);
        }

        #endregion

        #region Public and private methods - Tasks

        private async Task CallbackMemoryManagerAsync(int wait, bool isTaskEnabled)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            if (isTaskEnabled)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetEnabled(fieldMemoryManager, true);
                char ch = StringUtils.GetProgressChar(_taskManager.MemoryManagerProgressChar);
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMemoryManager,
                    $"Использовано памяти: {_taskManager.MemoryManager.MemorySize.Physical.MegaBytes:N0} MB | {ch}");
                _taskManager.MemoryManagerProgressChar = ch;
            }
            else {
                MDSoft.WinFormsUtils.InvokeControl.SetEnabled(fieldMemoryManager, false);
            }
        }

        private async Task CallbackDeviceManagerAsync(int wait)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldCurrentTime, DateTime.Now.ToString(@"dd.MM.yyyy HH:mm:ss"));
        }

        private async Task CallbackPrintManagerAsync(int wait)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

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
                //    await MDSoft.WinFormsUtils.InvokeControl.SetBackColor.Async(buttonPrint, _ws.PrintManager.PrintControl.IsOpen
                //        ? Color.FromArgb(192, 255, 192) : Color.Transparent).ConfigureAwait(false);
                if (_taskManager.PrintManager.PrintControl != null)
                {
                    //LedPrint.State = _ws.PrintManager.PrintControl.IsOpen;
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPrintManager, _taskManager.PrintManager.PrintControl.IsStatusNormal
                        ? $"Принтер: доступен | {ch}" : $"Принтер: недоступен | {ch}");
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
                    MDSoft.WinFormsUtils.InvokeControl.SetBackColor(buttonPrint, state.isReadyToPrint
                        ? Color.FromArgb(192, 255, 192) : Color.Transparent);
                if (state != null)
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPrintManager, state.isReadyToPrint
                        ? $"Принтер: доступен | {_taskManager.PrintManager.PrintControl.IpAddress} | {ch}" : $"Принтер: недоступен | {ch}");
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
                MDSoft.WinFormsUtils.InvokeControl.SetText(labelPlu, _ws.CurrentPlu.CheckWeight == false
                    ? $"PLU (шт): {_ws.CurrentPlu.PLU}"
                    : $"PLU (вес): {_ws.CurrentPlu.PLU}");
                decimal weight = _taskManager.MassaManager.WeightNet - _ws.CurrentPlu.GoodsTareWeight;
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldWeightNetto, $"{weight:0.000} кг");
                //await MDSoft.WinFormsUtils.InvokeControl.SetBackColor.Async(fieldWeightNetto,
                //    _ws.MassaManager.IsStable == 0x01 ? Color.FromArgb(150, 255, 150) : Color.Transparent).ConfigureAwait(false);
                //MDSoft.WinFormsUtils.InvokeControl.SetText.Async(fieldWeightTare, 
                //    $"{(float)getMassa.Tare / getMassa.ScaleFactor:0.000} кг");
            }

            //LedMassa.State = _ws.MassaManager.IsStable == 1;
            char ch = StringUtils.GetProgressChar(_taskManager.MassaManagerProgressChar);
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaManager, _taskManager.MassaManager.IsReady || _taskManager.MassaManager.IsStable == 1
                ? $"Весы: доступны | Вес брутто: { _taskManager.MassaManager.WeightNet:0.000} кг | {ch}"
                : $"Весы: недоступны | Вес брутто: { _taskManager.MassaManager.WeightNet:0.000} кг | {ch}");
            _taskManager.MassaManagerProgressChar = ch;
            if (!flag)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(labelPlu, "PLU");
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldWeightNetto, "0,000 кг");
                //await MDSoft.WinFormsUtils.InvokeControl.SetBackColor.Async(fieldWeightNetto, Color.Transparent).ConfigureAwait(false);
            }
        }

        #endregion

        #region Private methods - Callbacks

        //private void NotifyMassa(MassaManagerEntity message)
        //{
        //    var flag = false;
        //    if (message != null)
        //    {
        //        MDSoft.WinFormsUtils.InvokeControl.SetText.Async(fieldGrossWeight, $"Вес брутто: {message.WeightNet:0.000} кг");
        //        if (_ws.CurrentPlu != null)
        //        {
        //            flag = true;
        //            MDSoft.WinFormsUtils.InvokeControl.SetText.Async(labelPlu, _ws.CurrentPlu.CheckWeight == false
        //                ? $"PLU (шт): {_ws.CurrentPlu.PLU}" : $"PLU (вес): {_ws.CurrentPlu.PLU}");
        //            var weight = message.WeightNet - _ws.CurrentPlu.GoodsTareWeight;
        //            MDSoft.WinFormsUtils.InvokeControl.SetText.Async(fieldWeightNetto, $"{weight:0.000} кг");
        //            MDSoft.WinFormsUtils.InvokeControl.SetBackColor.Async(fieldWeightNetto,
        //                message.IsStable == 0x01 ? Color.FromArgb(150, 255, 150) : Color.Transparent);
        //            //MDSoft.WinFormsUtils.InvokeControl.SetText.Async(fieldWeightTare, 
        //            //    $"{(float)getMassa.Tare / getMassa.ScaleFactor:0.000} кг");
        //        }
        //        if (message.IsReady)
        //            LedMassa.State = message.IsStable == 1;
        //    }
        //    if (!flag)
        //    {
        //        MDSoft.WinFormsUtils.InvokeControl.SetText.Async(labelPlu, "PLU");
        //        MDSoft.WinFormsUtils.InvokeControl.SetText.Async(fieldWeightNetto, "0,000 кг");
        //        MDSoft.WinFormsUtils.InvokeControl.SetBackColor.Async(fieldWeightNetto, Color.Transparent);
        //    }
        //}

        private void ProductDateRefresh(DateTime productDate)
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldProductDate, $"{productDate:dd.MM.yyyy}");
        }

        private void KneadingRefresh(int kneading)
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldKneading, $"{kneading}");
        }

        private void LabelsCurrentRefresh(int labelCurrent)
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldLabelsCount, $"{_ws.LabelsCurrent}/{_ws.LabelsCount}");
        }

        private void PluRefresh(PluDirect plu)
        {
            string strCheckWeight = plu?.CheckWeight == true ? "вес" : "шт";
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPlu, plu != null
                ? $"{plu.PLU} | {strCheckWeight} | {plu.GoodsName}" : string.Empty);
            MDSoft.WinFormsUtils.InvokeControl.SetEnabled(buttonPrint, plu != null);
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldWeightTare, plu != null ? $"{plu.GoodsTareWeight:0.000} кг" : "0,000 кг");
            _logUtils.Information($"Смена PLU: {plu?.GoodsName}");
            _logUtils.Information($"PLU.GoodsTareWeight: {plu?.GoodsTareWeight}");
        }

        #endregion

        #region Private methods

        private void ButtonSettings([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                _taskManager.Close();

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
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                _taskManager.Open(CallbackDeviceManagerAsync, CallbackMemoryManagerAsync, CallbackPrintManagerAsync, CallbackMassaManagerAsync,
                    ButtonSetZero, _ws.SqlViewModel, _ws.IsTscPrinter, _ws.CurrentScale);
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
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
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
                _taskManager.Close();

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
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                _taskManager.Open(CallbackDeviceManagerAsync, CallbackMemoryManagerAsync, CallbackPrintManagerAsync, CallbackMassaManagerAsync,
                    ButtonSetZero, _ws.SqlViewModel, _ws.IsTscPrinter, _ws.CurrentScale);
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
                _taskManager.Close();

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
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                _taskManager.Open(CallbackDeviceManagerAsync, CallbackMemoryManagerAsync, CallbackPrintManagerAsync, CallbackMassaManagerAsync,
                    ButtonSetZero, _ws.SqlViewModel, _ws.IsTscPrinter, _ws.CurrentScale);
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
                _taskManager.Close();

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
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                _taskManager.Open(CallbackDeviceManagerAsync, CallbackMemoryManagerAsync, CallbackPrintManagerAsync, CallbackMassaManagerAsync,
                    ButtonSetZero, _ws.SqlViewModel, _ws.IsTscPrinter, _ws.CurrentScale);
            }
        }

        private void ButtonSetKneading_Click(object sender, EventArgs e)
        {
            ButtonSetKneading();
        }

        private void ButtonPrint(
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                _ws?.PrintLabel(Owner);
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
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
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
