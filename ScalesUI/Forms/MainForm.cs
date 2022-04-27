// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Localizations;
using DataCore.Schedulers;
using DataCore.Settings;
using DataCore.Wmi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;
using WeightCore.Managers;
using static DataCore.ShareEnums;

namespace ScalesUI.Forms
{
    public partial class MainForm : Form
    {
        #region Private fields and properties - Helpers

        private AppVersionHelper AppVersion { get; set; } = AppVersionHelper.Instance;
        private DebugHelper Debug { get; set; } = DebugHelper.Instance;
        private ExceptionHelper Exception { get; set; } = ExceptionHelper.Instance;
        private ProcHelper Proc { get; set; } = ProcHelper.Instance;
        private QuartzHelper Quartz { get; set; } = QuartzHelper.Instance;
        private SessionStateHelper SessionState { get; set; } = SessionStateHelper.Instance;

        #endregion

        #region Private fields and properties

        private Button ButtonKneading { get; set; }
        private Button ButtonMore { get; set; }
        private Button ButtonNewPallet { get; set; }
        private Button ButtonOrder { get; set; }
        private Button ButtonPlu { get; set; }
        private Button ButtonPrint { get; set; }
        private Button ButtonScalesInit { get; set; }
        private Button ButtonScalesTerminal { get; set; }
        private FontsSettingsEntity FontsSettings { get; set; }
        private readonly object _lockerDays;
        private TableLayoutPanel TableLayoutPanelButtons { get; set; }

        #endregion

        #region Public and private methods - MainForm

        public MainForm()
        {
            InitializeComponent();
            _lockerDays = new();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                SessionState.StopwatchMain = Stopwatch.StartNew();
                SessionState.StopwatchMain.Restart();
                SessionState.DataAccess.Log.Setup(SessionState.Scale.Host.Name, typeof(Program).Assembly.GetName().Name);
                FormBorderStyle = Debug.IsDebug ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
                TopMost = !Debug.IsDebug;
                FontsSettings = new();
                MainForm_ButtonsCreate();
                MainForm_LoadResources();
                SessionState.NewPallet();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
                SetComboBoxItems(fieldLang, FieldLang_SelectedIndexChanged, LocaleCore.Scales.ListLanguages);
                SessionState.StopwatchMain.Stop();
            }

            _ = Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                    Quartz.AddJob(QuartzUtils.CronExpression.EveryDays(), delegate { ScheduleEveryDays(); },
                        "jobScheduleEveryDays", "triggerScheduleEveryDays", "triggerGroupScheduleEveryDays");
                    //Quartz.AddJob(QuartzUtils.CronExpression.EverySeconds(), delegate { ScheduleEverySeconds(); },
                    //    "jobScheduleEverySeconds", "triggerScheduleEverySeconds", "triggerGroupScheduleEverySeconds");
                    ManagersLoad();
                }
                catch (Exception ex)
                {
                    Exception.Catch(this, ref ex, true);
                }
                finally
                {
                    MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
                    SetComboBoxItems(fieldLang, FieldLang_SelectedIndexChanged, LocaleCore.Scales.ListLanguages);
                    SessionState.DataAccess.Log.LogInformation($"{LocaleCore.Scales.ScreenResolution}: {Width} x {Height}");
                    SessionState.DataAccess.Log.LogInformation(LocaleData.Program.IsLoaded + $" {nameof(SessionState.StopwatchMain.Elapsed)}: {SessionState.StopwatchMain.Elapsed}.");
                    SessionState.StopwatchMain.Stop();
                }
            }).ConfigureAwait(false);
        }

        private void ManagersLoad()
        {
            // Labels.
            SessionState.Manager.Labels.Init(fieldTitle, fieldPlu, fieldSscc,
                labelProductDate, fieldProductDate, labelKneading, fieldKneading, fieldResolution, fieldLang,
                ButtonKneading, ButtonMore, ButtonNewPallet, ButtonOrder, ButtonPlu, ButtonPrint,
                ButtonScalesInit, ButtonScalesTerminal, pictureBoxClose);
            SessionState.Manager.Labels.Open();
            // Memory.
            SessionState.Manager.Memory.Init(fieldMemory, fieldTasks);
            SessionState.Manager.Memory.Open();
            // Massa.
            SessionState.Manager.Massa.Init(labelWeightNetto, fieldWeightNetto, labelWeightTare, fieldWeightTare,
                fieldMassaProgress, fieldThreshold, fieldMassaGet,
                fieldMassaGetCrc, fieldMassaSet, fieldMassaSetCrc, fieldMassaScalePar);
            SessionState.Manager.Massa.Open();
            // PrintMain.
            SessionState.Manager.PrintMain.Init(SessionState.PrintBrandMain, SessionState.Scale.PrinterMain,
                SessionState.Scale.PrinterMain.Name, SessionState.Scale.PrinterMain.Ip,
                SessionState.Scale.PrinterMain.Port, fieldPrintMain, true);
            SessionState.Manager.PrintMain.Open(true);
            // PrintShipping.
            if (SessionState.Scale.IsShipping)
            {
                SessionState.Manager.PrintShipping.Init(SessionState.PrintBrandShipping, SessionState.Scale.PrinterShipping,
                    SessionState.Scale.PrinterShipping.Name, SessionState.Scale.PrinterShipping.Ip,
                    SessionState.Scale.PrinterShipping.Port, fieldPrintShipping, false);
                SessionState.Manager.PrintShipping.Open(false);
            }
            //ButtonScalesInit_Click(sender, e);
            SessionState.Manager.Labels.SetControlsVisible();
        }

        private void MainForm_LoadResources([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                System.Resources.ResourceManager resourceManager = new("ScalesUI.Properties.Resources", Assembly.GetExecutingAssembly());
                object exit = resourceManager.GetObject("exit_1");
                if (exit != null)
                {
                    Bitmap bmpExit = new((Bitmap)exit);
                    pictureBoxClose.Image = bmpExit;
                }

                MDSoft.WinFormsUtils.InvokeControl.SetText(this, AppVersion.AppTitle);
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldProductDate, string.Empty);
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true, filePath, lineNumber, memberName);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
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
                SessionState.StopwatchMain.Restart();
                if (Quartz != null)
                {
                    Quartz.Close();
                    Quartz.Dispose();
                    Quartz = null;
                }
                ManagerBase.WaitSync(0_100);
                SessionState.Manager.Labels.Close();
                SessionState.Manager.Massa.Close();
                SessionState.Manager.Memory.Close();
                SessionState.Manager.PrintMain.Close();
                SessionState.Manager.PrintShipping.Close();
                SessionState.Manager.Close();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                SessionState.DataAccess.Log.LogInformation(LocaleData.Program.IsClosed + $" {nameof(SessionState.StopwatchMain.Elapsed)}: {SessionState.StopwatchMain.Elapsed}.");
                SessionState.StopwatchMain.Stop();
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (Equals(e.Button, MouseButtons.Middle))
            {
                ActionPrint_Click(sender, e);
            }
        }

        private void MainForm_FontsSet()
        {
            fieldResolution.Font = FontsSettings.FontMinimum;
            fieldLang.Font = FontsSettings.FontMinimum;

            fieldTitle.Font = FontsSettings.FontLabelsTitle;

            fieldWeightNetto.Font = FontsSettings.FontLabelsMaximum;
            fieldWeightTare.Font = FontsSettings.FontLabelsMaximum;
            fieldPlu.Font = FontsSettings.FontLabelsMaximum;
            fieldProductDate.Font = FontsSettings.FontLabelsMaximum;

            fieldThreshold.Font = FontsSettings.FontLabelsGray;
            fieldSscc.Font = FontsSettings.FontLabelsGray;
            fieldTasks.Font = FontsSettings.FontLabelsGray;
            fieldPrintMain.Font = FontsSettings.FontLabelsGray;
            fieldPrintShipping.Font = FontsSettings.FontLabelsGray;
            fieldPrintProgressMain.Font = FontsSettings.FontLabelsGray;
            fieldPrintProgressShipping.Font = FontsSettings.FontLabelsGray;
            fieldMassaScalePar.Font = FontsSettings.FontLabelsGray;
            fieldMassaGetCrc.Font = FontsSettings.FontLabelsGray;
            fieldMassaGet.Font = FontsSettings.FontLabelsGray;
            fieldMassaSetCrc.Font = FontsSettings.FontLabelsGray;
            fieldMassaSet.Font = FontsSettings.FontLabelsGray;
            fieldMassaProgress.Font = FontsSettings.FontLabelsGray;
            fieldMemory.Font = FontsSettings.FontLabelsGray;

            labelWeightNetto.Font = FontsSettings.FontLabelsBlack;
            labelWeightTare.Font = FontsSettings.FontLabelsBlack;
            labelKneading.Font = FontsSettings.FontLabelsBlack;
            fieldKneading.Font = FontsSettings.FontLabelsBlack;
            labelProductDate.Font = FontsSettings.FontLabelsBlack;

            if (ButtonScalesTerminal != null)
                ButtonScalesTerminal.Font = FontsSettings.FontButtons;
            if (ButtonScalesInit != null)
                ButtonScalesInit.Font = FontsSettings.FontButtons;
            if (ButtonOrder != null)
                ButtonOrder.Font = FontsSettings.FontButtons;
            if (ButtonNewPallet != null)
                ButtonNewPallet.Font = FontsSettings.FontButtons;
            if (ButtonKneading != null)
                ButtonKneading.Font = FontsSettings.FontButtons;
            if (ButtonPlu != null)
                ButtonPlu.Font = FontsSettings.FontButtons;
            if (ButtonMore != null)
                ButtonMore.Font = FontsSettings.FontButtons;
            if (ButtonPrint != null)
                ButtonPrint.Font = FontsSettings.FontButtons;
        }

        private void MainForm_ButtonsCreate()
        {
            ButtonsSettingsEntity buttonsSettings = new()
            {
                IsKneading = false,
                IsMore = true,
                IsNewPallet = true,
                IsOrder = SessionState.Scale.UseOrder,
                IsPlu = true,
                IsPrint = true,
                IsScalesInit = true,
                IsScalesTerminal = true,
            };

            int column = 0;

            TableLayoutPanelButtons = GuiUtils.WinForm.NewTableLayoutPanel(tableLayoutPanelMain, nameof(TableLayoutPanelButtons),
                0, tableLayoutPanelMain.RowCount - 1, tableLayoutPanelMain.ColumnCount);

            if (buttonsSettings.IsScalesTerminal)
            {
                ButtonScalesTerminal = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonScalesTerminal), column++);
                ButtonScalesTerminal.Click += new EventHandler(ActionScalesTerminal_Click);
                ButtonScalesTerminal.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsScalesInit)
            {
                ButtonScalesInit = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonScalesInit), column++);
                ButtonScalesInit.Click += new EventHandler(ActionScalesInit_Click);
                ButtonScalesInit.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsOrder)
            {
                ButtonOrder = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonOrder), column++);
                ButtonOrder.Click += new EventHandler(ActionOrder_Click);
                ButtonOrder.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsNewPallet)
            {
                ButtonNewPallet = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonNewPallet), column++);
                ButtonNewPallet.Click += new EventHandler(ActionNewPallet_Click);
                ButtonNewPallet.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsKneading)
            {
                ButtonKneading = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonKneading), column++);
                ButtonKneading.Click += new EventHandler(ActionKneading_Click);
                ButtonKneading.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsPlu)
            {
                ButtonPlu = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonPlu), column++);
                ButtonPlu.Click += new EventHandler(ActionPlu_Click);
                ButtonPlu.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsMore)
            {
                ButtonMore = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonMore), column++);
                ButtonMore.Click += new EventHandler(ActionMore_Click);
                ButtonMore.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsPrint)
            {
                ButtonPrint = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonPrint), column++);
                ButtonPrint.Click += new EventHandler(ActionPrint_Click);
                ButtonPrint.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            GuiUtils.WinForm.SetTableLayoutPanelColumnStyles(TableLayoutPanelButtons, column);
        }

        #endregion

        #region Public and private methods - Schedulers

        private void ScheduleEveryDays()
        {
            lock (_lockerDays)
            {
                if (Quartz == null)
                    return;
                SessionState.ProductDate = DateTime.Now;
                SessionState.DataAccess.Log.LogInformation("ScheduleIsNextDay");
            }
        }

        #endregion

        #region Private methods

        public void SetComboBoxItems(ComboBox comboBox, EventHandler eventHandler, List<string> sourceList, int selectedIndex = 0)
        {
            if (comboBox == null || sourceList == null || sourceList.Count == 0 || selectedIndex < 0)
                return;

            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke((Action)delegate
                {
                    Work(comboBox, eventHandler, sourceList, selectedIndex);
                });
            }
            else
            {
                Work(comboBox, eventHandler, sourceList, selectedIndex);
            }

            static void Work(ComboBox comboBox, EventHandler eventHandler, List<string> sourceList, int selectedIndex = 0)
            {
                comboBox.SelectedIndexChanged -= eventHandler;

                int backupIndex = comboBox.SelectedIndex;
                comboBox.Items.Clear();
                comboBox.Items.AddRange(sourceList.ToArray());
                comboBox.SelectedIndex = selectedIndex <= 0
                    ? backupIndex <= 0 ? 0 : backupIndex
                    : selectedIndex < comboBox.Items.Count ? selectedIndex : 0;

                comboBox.SelectedIndexChanged += eventHandler;
                eventHandler.Invoke(comboBox, null);
            }
        }

        private void FieldPrintManager_Click(object sender, EventArgs e)
        {
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false, FormBorderStyle.FixedDialog,
                22, 16, 16)
            { Width = 700, Height = 650 };
            wpfPageLoader.Text = LocaleCore.Print.InfoCaption;
            wpfPageLoader.MessageBox.Caption = LocaleCore.Print.InfoCaption;
            wpfPageLoader.MessageBox.Message =
                GetPrintInfo(SessionState.Manager.PrintMain.Win32Printer(), true) + Environment.NewLine + Environment.NewLine +
                GetPrintInfo(SessionState.Manager.PrintShipping.Win32Printer(), false);
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonCustomVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonCustomContent = LocaleCore.Print.ClearQueue;
            DialogResult result = wpfPageLoader.ShowDialog(this);
            wpfPageLoader.Close();
            wpfPageLoader.Dispose();
            if (result == DialogResult.Retry)
            {
                SessionState.Manager.PrintMain.ClearPrintBuffer(true, SessionState.Manager.PrintMain.CurrentLabels);
                SessionState.Manager.PrintShipping.ClearPrintBuffer(true, SessionState.Manager.PrintShipping.CurrentLabels);
            }
        }

        private string GetPrintInfo(WmiWin32PrinterEntity win32Printer, bool isMain)
        {
            string caption = isMain
                ? SessionState.Manager.PrintMain.PrintBrand == WeightCore.Print.PrintBrand.Zebra
                    ? LocaleCore.Print.NameMainZebra : LocaleCore.Print.NameMainTsc
                : SessionState.Manager.PrintShipping.PrintBrand == WeightCore.Print.PrintBrand.Zebra
                    ? LocaleCore.Print.NameShippingZebra : LocaleCore.Print.NameShippingTsc;
            string peeler = isMain
                ? SessionState.Manager.PrintMain.ZebraPeelerStatus : SessionState.Manager.PrintShipping.ZebraPeelerStatus;
            string printMode = isMain
                ? SessionState.Manager.PrintMain.GetZebraPrintMode() :
                SessionState.Manager.PrintShipping.GetZebraPrintMode();
            return $"{caption}: {win32Printer.Name}" + Environment.NewLine +
                $"{LocaleCore.Print.Driver}: {win32Printer.DriverName}" + Environment.NewLine +
                $"{LocaleCore.Print.Port}: {win32Printer.PortName}" + Environment.NewLine +
                $"{LocaleCore.Print.StateCode}: {win32Printer.PrinterState}" + Environment.NewLine +
                $"{LocaleCore.Print.StatusCode}: {win32Printer.PrinterStatus}" + Environment.NewLine +
                $"{LocaleCore.Print.Status}: {win32Printer.PrinterStatusDescription}" + Environment.NewLine +
                $"{LocaleCore.Print.State} (ENG): {win32Printer.Status}" + Environment.NewLine +
                $"{LocaleCore.Print.State}: {win32Printer.StatusDescription}" + Environment.NewLine +
                $"{LocaleCore.Print.SensorPeeler}: {peeler}" + Environment.NewLine +
                $"{LocaleCore.Print.Mode}: {printMode}" + Environment.NewLine;
        }

        private void FieldSscc_Click(object sender, EventArgs e)
        {
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false, FormBorderStyle.FixedDialog, 26, 20, 18) { Width = 700, Height = 400 };
            wpfPageLoader.Text = LocaleCore.Scales.FieldSsccShort;
            wpfPageLoader.MessageBox.Caption = LocaleCore.Scales.FieldSscc;
            wpfPageLoader.MessageBox.Message =
                $"{LocaleCore.Scales.FieldSscc}: {SessionState.ProductSeries.Sscc.SSCC}" + Environment.NewLine +
                $"{LocaleCore.Scales.FieldSsccGln}: {SessionState.ProductSeries.Sscc.GLN}" + Environment.NewLine +
                $"{LocaleCore.Scales.FieldSsccUnitId}: {SessionState.ProductSeries.Sscc.UnitID}" + Environment.NewLine +
                $"{LocaleCore.Scales.FieldSsccUnitType}: {SessionState.ProductSeries.Sscc.UnitType}" + Environment.NewLine +
                $"{LocaleCore.Scales.FieldSsccSynonym}: {SessionState.ProductSeries.Sscc.SynonymSSCC}" + Environment.NewLine +
                $"{LocaleCore.Scales.FieldSsccControlNumber}: {SessionState.ProductSeries.Sscc.Check}";
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.VisibilitySettings.Localization();
            wpfPageLoader.ShowDialog(this);
            wpfPageLoader.Close();
            wpfPageLoader.Dispose();
        }

        private void FieldTasks_DoubleClick(object sender, EventArgs e)
        {
            string message = string.Empty;
            foreach (ProcessThread thread in Process.GetCurrentProcess().Threads)
            {
                message += $"{LocaleCore.Scales.ThreadId}: {thread.Id}. " +
                    $"{LocaleCore.Scales.ThreadPriorityLevel}: {thread.PriorityLevel}. " +
                    $"{LocaleCore.Scales.ThreadState}: {thread.ThreadState}. " +
                    $"{LocaleCore.Scales.ThreadStartTime}: {thread.StartTime}. " + Environment.NewLine;
            }
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false, FormBorderStyle.FixedDialog,
                20, 14, 18, 0, 12, 1)
            { Width = Width - 50, Height = Height - 50 };
            wpfPageLoader.Text = $"{LocaleCore.Scales.ThreadsCount}: {Process.GetCurrentProcess().Threads.Count}";
            wpfPageLoader.MessageBox.Message = message;
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.VisibilitySettings.Localization();
            wpfPageLoader.ShowDialog(this);
            wpfPageLoader.Close();
            wpfPageLoader.Dispose();
        }

        private void FieldResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (fieldResolution.Items[fieldResolution.SelectedIndex])
                {
                    case "800x600":
                        WindowState = FormWindowState.Normal;
                        Size = new System.Drawing.Size(800, 600);
                        break;
                    case "1024x768":
                        WindowState = FormWindowState.Normal;
                        Size = new System.Drawing.Size(1024, 768);
                        break;
                    case "1366x768":
                        WindowState = FormWindowState.Normal;
                        Size = new System.Drawing.Size(1366, 768);
                        break;
                    case "1600x1024":
                        WindowState = FormWindowState.Normal;
                        Size = new System.Drawing.Size(1600, 1024);
                        break;
                    case "1920x1080":
                        WindowState = FormWindowState.Normal;
                        Size = new System.Drawing.Size(1920, 1080);
                        break;
                    default:
                        WindowState = FormWindowState.Maximized;
                        break;
                }
                CenterToScreen();
                FontsSettings.Transform(Width, Height);
                MainForm_FontsSet();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void FieldLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LocaleCore.Lang = LocaleData.Lang = fieldLang.SelectedIndex switch { 1 => ShareEnums.Lang.English, _ => ShareEnums.Lang.Russian, };
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonScalesTerminal, LocaleCore.Scales.ButtonRunScalesTerminal);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonScalesInit, LocaleCore.Scales.ButtonScalesInitShort);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonOrder, LocaleCore.Scales.ButtonSelectOrder);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonNewPallet, LocaleCore.Scales.ButtonNewPallet);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonKneading, LocaleCore.Scales.ButtonAddKneading);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonPlu, LocaleCore.Scales.ButtonSelectPlu);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonMore, LocaleCore.Scales.ButtonSetKneading);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonPrint, LocaleCore.Print.ActionPrint);
                SetComboBoxItems(fieldResolution, FieldResolution_SelectedIndexChanged, LocaleCore.Scales.ListResolutions,
                    Debug.IsDebug ? 2 : LocaleCore.Scales.ListResolutions.Count - 1);
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void FieldTitle_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                SessionState.Manager.Massa.Close();

                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.SqlSettings, false) { Width = 400, Height = 400 };
                wpfPageLoader.ShowDialog(this);
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();

                SessionState.Manager.Massa.Open();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        #endregion

        #region Public and private methods - Actions

        private void ActionClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ActionScalesTerminal_Click(object sender, EventArgs e)
        {
            try
            {
                //if (GuiUtils.WpfForm.ShowNewPinCode(this))
                //{
                DialogResult result = GuiUtils.WpfForm.ShowNewOperationControl(this,
                    $"{LocaleCore.Scales.QuestionRunApp} ScalesTerminal?",
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (result != DialogResult.Yes)
                    return;

                // Run app.
                if (File.Exists(LocaleData.Paths.ScalesTerminal))
                {
                    SessionState.Manager.Massa.Close();
                    Proc.Run(LocaleData.Paths.ScalesTerminal, string.Empty, false, ProcessWindowStyle.Normal, true);
                }
                else
                {
                    GuiUtils.WpfForm.ShowNewOperationControl(this,
                        LocaleCore.Scales.ProgramNotFound(LocaleData.Paths.ScalesTerminal));
                }
                //}
                SessionState.Manager.Open();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionScalesInit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SessionState.IsPluCheckWeight)
                {
                    GuiUtils.WpfForm.ShowNewOperationControl(this, LocaleCore.Scales.PluNotSelectWeight);
                    return;
                }
                if (!SessionState.Manager.Massa.MassaDevice.IsConnected)
                {
                    GuiUtils.WpfForm.ShowNewOperationControl(this, LocaleCore.Scales.MassaNotRespond);
                    return;
                }

                DialogResult result = GuiUtils.WpfForm.ShowNewOperationControl(this, LocaleCore.Scales.QuestionPerformOperation,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (result != DialogResult.Yes)
                    return;

                SessionState.Manager.Massa.Close();

                // Fix negative weight.
                if (SessionState.Manager.Massa.WeightNet < 0)
                {
                    SessionState.Manager.Massa.ResetMassa();
                }

                SessionState.CheckWeightMassaDeviceExists(this);
                SessionState.SetCurrentPlu(null);

                SessionState.Manager.Massa.Open();
                SessionState.Manager.Massa.GetInit();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionOrder_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.Manager.Massa.Close();

                if (SessionState.Order == null)
                {
                    using OrderListForm settingsForm = new();
                    settingsForm.ShowDialog(this);
                    settingsForm.Close();
                    settingsForm.Dispose();
                }
                else
                {
                    using OrderDetailForm settingsForm = new();
                    DialogResult result = settingsForm.ShowDialog(this);
                    settingsForm.Close();
                    settingsForm.Dispose();
                    if (result == DialogResult.Retry)
                    {
                        SessionState.Order = null;
                    }
                    if (result == DialogResult.OK)
                    {
                        //ws.Kneading = (int)settingsForm.currentKneading;
                    }
                    if (result == DialogResult.Cancel)
                    {
                        //ws.Kneading = (int)settingsForm.currentKneading;
                    }
                }
                if (SessionState.Order != null)
                {
                    MDSoft.WinFormsUtils.InvokeProgressBar.SetMaximum(fieldPrintProgressMain, SessionState.Order.PlaneBoxCount);
                    MDSoft.WinFormsUtils.InvokeProgressBar.SetMinimum(fieldPrintProgressMain, 0);
                    MDSoft.WinFormsUtils.InvokeProgressBar.SetValue(fieldPrintProgressMain, SessionState.Order.FactBoxCount);
                }

                SessionState.Manager.Massa.Open();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionNewPallet_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.NewPallet();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionKneading_Click(object sender, EventArgs e)
        {
            try
            {
                using NumberInputForm numberInputForm = new() { InputValue = 0 };
                DialogResult result = numberInputForm.ShowDialog(this);
                numberInputForm.Close();
                numberInputForm.Dispose();
                if (result == DialogResult.OK)
                    SessionState.WeighingSettings.Kneading = (byte)numberInputForm.InputValue;
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionPlu_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.SetCurrentPlu(null);
                if (SessionState.CheckWeightMassaDeviceExists(this))
                {
                    if (!SessionState.CheckWeightIsNegative(this) || !SessionState.CheckWeightIsPositive(this))
                        return;
                }
                SessionState.Manager.Massa.Close();

                // PLU form.
                using PluListForm pluListForm = new() { Owner = this };
                DialogResult result = pluListForm.ShowDialog(this);
                pluListForm.Close();
                pluListForm.Dispose();
                if (result == DialogResult.OK)
                {
                    SessionState.WeighingSettings.Kneading = 1;
                    SessionState.ProductDate = DateTime.Now;
                    SessionState.NewPallet();
                    //_mkDevice.SetTareWeight((int) (_sessionState.CurrentPLU.GoodsTareWeight * _sessionState.CurrentPLU.Scale.ScaleFactor));

                    // сразу перейдем к форме с замесами, размерами паллет и прочее
                    ActionMore_Click(null, null);
                }
                else
                {
                    SessionState.SetCurrentPlu(null);
                }

                SessionState.Manager.Massa.Open();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionMore_Click(object sender, EventArgs e)
        {
            try
            {
                if (SessionState.Plu == null)
                {
                    GuiUtils.WpfForm.ShowNewOperationControl(this, LocaleCore.Scales.PluNotSelect);
                    return;
                }

                SessionState.Manager.Massa.Close();

                using KneadingForm kneadingForm = new() { Owner = this };
                DialogResult result = kneadingForm.ShowDialog();
                kneadingForm.Close();
                kneadingForm.Dispose();
                if (result == DialogResult.OK)
                {
                    //_sessionState.Kneading = settingsForm.CurrentKneading;
                    //_sessionState.ProductDate = settingsForm.CurrentProductDate;
                }

                SessionState.Manager.Massa.Open();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SessionState.CheckPluIsEmpty(this))
                    return;
                if (!SessionState.CheckWeightMassaDeviceExists(this))
                    return;
                SessionState.SetCurrentWeighingFact();
                if (!SessionState.CheckWeightIsNegative(this))
                    return;
                if (!SessionState.CheckWeightThresholds(this))
                    return;
                SessionState.PrintLabel(false);

                SessionState.Manager.Massa.Open();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
                //_sessionState.TaskManager.OpenPrintManager(CallbackPrintManagerClose, _sessionState.SqlViewModel,
                //_sessionState.PrintBrand, _sessionState.CurrentScale);
            }
        }

        #endregion
    }
}
