// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataProjectsCore;
using DataProjectsCore.Utils;
using DataShareCore;
using DataShareCore.Helpers;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;
using WeightCore.Managers;
using WeightCore.Models;

namespace ScalesUI.Forms
{
    public partial class MainForm : Form
    {
        #region Private fields and properties

        private readonly AppVersionHelper _appVersion = AppVersionHelper.Instance;
        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                _taskManager.Close();

                if (_ws.CurrentScale != null)
                {
                    // _ws.CurrentScale.Load(_app.GuidToString());
                    buttonSelectOrder.Visible = !(buttonSelectPlu.Visible = !(_ws.CurrentScale.UseOrder == true));
                }

                LoadResources();

                _ws.NewPallet();

                _logUtils.Information("The program is runned");
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                _taskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackPrintManager, CallbackMassaManager,
                    ButtonSetZero_Click, _ws.SqlViewModel, _ws.IsTscPrinter, _ws.CurrentScale);
            }
        }

        private void LoadResources()
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
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
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
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
                _logUtils.Information("The program is closed");
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
            }
        }

        #endregion

        #region Public and private methods - Callbacks

        private void CheckEnabledManager(ProjectsEnums.TaskType taskType, Control fieldManager)
        {
            if (_ws.SqlViewModel.IsTaskEnabled(taskType))
            {
                if (!fieldManager.Visible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldManager, true);
            }
            else
            {
                if (fieldManager.Visible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldManager, false);
            }
        }

        private void CallbackDeviceManager()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldCurrentTime, DateTime.Now.ToString(@"dd.MM.yyyy HH:mm:ss"));
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldProductDate, $"{_ws.ProductDate:dd.MM.yyyy}");
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldKneading, $"{_ws.Kneading}");

            if (!Equals(buttonPrint.Enabled, _ws.CurrentPlu != null))
                MDSoft.WinFormsUtils.InvokeControl.SetEnabled(buttonPrint, _ws.CurrentPlu != null);

            string strCheckWeight = _ws.CurrentPlu?.CheckWeight == true ? "вес" : "шт";
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPlu, _ws.CurrentPlu != null
                ? $"{_ws.CurrentPlu.PLU} | {strCheckWeight} | {_ws.CurrentPlu.GoodsName}" : string.Empty);
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldWeightTare, _ws.CurrentPlu != null
                ? $"{_ws.CurrentPlu.GoodsTareWeight:0.000} кг" : "0,000 кг");
        }

        private void CallbackMemoryManager()
        {
            CheckEnabledManager(ProjectsEnums.TaskType.MemoryManager, fieldMemoryManager);
            if (_ws.SqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MemoryManager))
            {
                char ch = StringUtils.GetProgressChar(_taskManager.MemoryManagerProgressChar);
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMemoryManager,
                    $"Использовано памяти: {_taskManager.MemoryManager.MemorySize.Physical.MegaBytes:N0} MB | {ch}");
                _taskManager.MemoryManagerProgressChar = ch;
            }
        }

        private void CallbackPrintManager()
        {
            CheckEnabledManager(ProjectsEnums.TaskType.PrintManager, fieldPrintManager);
            CheckEnabledManager(ProjectsEnums.TaskType.PrintManager, labelLabelsTitle);
            CheckEnabledManager(ProjectsEnums.TaskType.PrintManager, fieldLabelsCount);
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldLabelsCount, $"{_ws.LabelsCurrent}/{_ws.LabelsCount}");

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
                //Zebra.Sdk.Printer.PrinterStatus state = _taskManager.PrintManager.CurrentStatus;
                // надо переприсвоить т.к. на CurrentBox сделан Notify чтоб выводить на экран
                _ws.LabelsCurrent = _taskManager.PrintManager.UserLabelCount < _ws.LabelsCount ? _taskManager.PrintManager.UserLabelCount : _ws.LabelsCount;
                // а когда зебра поддергивает ленту то счетчик увеличивается на 1 не может быть что-бы напечатано 3, а на форме 4
                if (_ws.LabelsCurrent == 0)
                    _ws.LabelsCurrent = 1;
                //MDSoft.WinFormsUtils.InvokeControl.SetBackColor(buttonPrint,
                //    _taskManager.PrintManager.CurrentStatus != null && _taskManager.PrintManager.CurrentStatus.isReadyToPrint
                //    ? Color.FromArgb(192, 255, 192) : Color.Transparent);
                if (_taskManager.PrintManager.CurrentStatus != null)
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPrintManager, _taskManager.PrintManager.CurrentStatus.isReadyToPrint
                        ? $"Принтер: доступен | {_taskManager.PrintManager.PrintControl.IpAddress} | {ch}" : $"Принтер: недоступен | {ch}");
                }
            }
            _taskManager.PrintManagerProgressChar = ch;
        }

        private void CallbackMassaManager()
        {
            CheckEnabledManager(ProjectsEnums.TaskType.MassaManager, fieldMassaManager);
            CheckEnabledManager(ProjectsEnums.TaskType.MassaManager, buttonSetZero);
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

        #endregion

        #region Private methods

        private void ButtonSettings_Click(object sender, EventArgs e)
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
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                _taskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackPrintManager, CallbackMassaManager,
                    ButtonSetZero_Click, _ws.SqlViewModel, _ws.IsTscPrinter, _ws.CurrentScale);
            }
        }

        private void OpenFormSettings()
        {
            using SettingsForm settingsForm = new();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void ButtonSetZero_Click(object sender, EventArgs e)
        {
            try
            {
                if (_taskManager.MassaManager.WeightNet > LocalizationData.ScalesUI.MassaThreshold || _taskManager.MassaManager.WeightNet < -LocalizationData.ScalesUI.MassaThreshold)
                {
                    CustomMessageBox messageBox = CustomMessageBox.Show(this, LocalizationData.ScalesUI.MassaCheck, LocalizationData.ScalesUI.OperationControl,
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
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
            }
        }

        private void ButtonSelectPlu_Click(object sender, EventArgs e)
        {
            try
            {
                _taskManager.Close();

                // Weight check.
                if (_taskManager.MassaManager != null)
                {
                    if (_taskManager.MassaManager.WeightNet > LocalizationData.ScalesUI.MassaThreshold || _taskManager.MassaManager.WeightNet < -LocalizationData.ScalesUI.MassaThreshold)
                    {
                        CustomMessageBox messageBox = CustomMessageBox.Show(this, LocalizationData.ScalesUI.MassaCheck, LocalizationData.ScalesUI.OperationControl,
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
                    ButtonSetKneading_Click(null, null);
                }
                else if (_ws.CurrentPlu != null)
                {
                    _ws.CurrentPlu = null;
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                _taskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackPrintManager, CallbackMassaManager,
                    ButtonSetZero_Click, _ws.SqlViewModel, _ws.IsTscPrinter, _ws.CurrentScale);
            }
        }

        private void ButtonSelectOrder_Click(object sender, EventArgs e)
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
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                _taskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackPrintManager, CallbackMassaManager,
                    ButtonSetZero_Click, _ws.SqlViewModel, _ws.IsTscPrinter, _ws.CurrentScale);
            }
        }

        private void ButtonSetKneading_Click(object sender, EventArgs e)
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
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                _taskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackPrintManager, CallbackMassaManager,
                    ButtonSetZero_Click, _ws.SqlViewModel, _ws.IsTscPrinter, _ws.CurrentScale);
            }
        }

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                _ws?.PrintLabel(Owner);
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
            }
        }

        private void PictureBoxClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FieldResolution_SelectedIndexChanged(object sender, EventArgs e)
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
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
            }
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
            try
            {
                _taskManager.Close();

                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.SqlSettings, false) { Width = 400, Height = 400 };
                wpfPageLoader.ShowDialog(this);
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                _taskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackPrintManager, CallbackMassaManager,
                    ButtonSetZero_Click, _ws.SqlViewModel, _ws.IsTscPrinter, _ws.CurrentScale);
            }
        }

        #endregion

        #region Public and private methods - Share

        private void TemplateJobWithTaskManager()
        {
            try
            {
                _taskManager.Close();

                // .. methods
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                _taskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackPrintManager, CallbackMassaManager,
                    ButtonSetZero_Click, _ws.SqlViewModel, _ws.IsTscPrinter, _ws.CurrentScale);
            }
        }

        #endregion
    }
}
