// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.DataModels;
using DataCore.Helpers;
using DataCore.Schedulers;
using DataCore.Utils;
using DataCore.Wmi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;
using WeightCore.Managers;
using LocalizationCore = DataCore.Localization.Core;

namespace ScalesUI.Forms
{
    public partial class MainForm : Form
    {
        #region Private fields and properties - Helpers

        private AppVersionHelper AppVersion { get; set; } = AppVersionHelper.Instance;
        private DebugHelper Debug { get; set; } = DebugHelper.Instance;
        private ExceptionHelper Exception { get; set; } = ExceptionHelper.Instance;
        private LogHelper Log { get; set; } = LogHelper.Instance;
        private ProcHelper Proc { get; set; } = ProcHelper.Instance;
        private QuartzHelper Quartz { get; set; } = QuartzHelper.Instance;
        private SessionStateHelper SessionState { get; set; } = SessionStateHelper.Instance;
        private ShareEnums.ProgramState ProgramState { get; set; } = ShareEnums.ProgramState.Default;

        #endregion

        #region Private fields and properties

        private bool IsProgramStateIsRunVisible { get; set; }
        private bool IsShowInfoLabels { get; set; }
        private Button ButtonKneading  { get; set; }
        private Button ButtonMore { get; set; }
        private Button ButtonNewPallet { get; set; }
        private Button ButtonOrder { get; set; }
        private Button ButtonPlu { get; set; }
        private Button ButtonPrint { get; set; }
        private Button ButtonScalesInit { get; set; }
        private Button ButtonScalesTerminal { get; set; }
        private FontsSettingsEntity FontsSettings { get; set; }
        private readonly object _lockerDays;
        private readonly object _lockerSeconds;
        private Stopwatch StopwatchMain { get; set; }
        private Stopwatch StopwatchMassa { get; set; }
        private Stopwatch StopwatchPrintMain { get; set; }
        private Stopwatch StopwatchPrintShipping { get; set; }
        private string AppName { get; set; }
        private TableLayoutPanel TableLayoutPanelButtons { get; set; }

        #endregion

        #region Public and private methods - MainForm

        public MainForm()
        {
            InitializeComponent();

            _lockerDays = new();
            _lockerSeconds = new();
            FormBorderStyle = Debug.IsDebug ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
            TopMost = !Debug.IsDebug;
            AppName = $"{AppVersion.AppTitle}.  {SessionState.CurrentScale.Description}.";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            bool isCmdSuccess = false;
            StopwatchMain = Stopwatch.StartNew();
            StopwatchPrintMain = Stopwatch.StartNew();
            StopwatchPrintShipping = Stopwatch.StartNew();
            StopwatchMassa = Stopwatch.StartNew();
            try
            {
                FontsSettings = new();
                ButtonsSettingsEntity buttonsSettings = new()
                {
                    IsKneading = false,
                    IsMore = true,
                    IsNewPallet = true,
                    IsOrder = SessionState.CurrentScale.UseOrder,
                    IsPlu = true,
                    IsPrint = true,
                    IsScalesInit = true,
                    IsScalesTerminal = true,
                };
                MainForm_ButtonsCreate(buttonsSettings);

                ProgramState = ShareEnums.ProgramState.IsLoad;
                SessionState.Manager.Close();

                MainForm_LoadResources();
                SessionState.NewPallet();
                SetLabels();

                Quartz.AddJob(QuartzUtils.CronExpression.EveryDays(), delegate { ScheduleEveryDays(); }, 
                    "jobScheduleEveryDays", "triggerScheduleEveryDays", "triggerGroupScheduleEveryDays");
                Quartz.AddJob(QuartzUtils.CronExpression.EverySeconds(), delegate { ScheduleEverySeconds(); }, 
                    "jobScheduleEverySeconds", "triggerScheduleEverySeconds", "triggerGroupScheduleEverySeconds");

                if (Debug.IsDebug)
                    FieldCurrentTime_Click(sender, e);
                isCmdSuccess = true;
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);

                SessionState.Manager.Init(SessionState.CurrentScale, SessionState.PrintBrandMain, SessionState.PrintBrandShipping);
                SessionState.Manager.Labels.Init(labelProductDate, fieldProductDate, labelKneading, fieldKneading, fieldPlu,
                    labelWeightNetto, fieldWeightNetto, labelWeightTare, fieldWeightTare, fieldMassaManager, fieldMassaComPort,
                    fieldMassaQueries, fieldMassaQueriesProgress, fieldMemoryManager, fieldMemoryManagerTotal, fieldTasks, 
                    fieldMemoryProgress, fieldMassaGet, fieldMassaGetCrc, fieldMassaSet, fieldMassaSetCrc);
                SessionState.Manager.Labels.Open();
                if (isCmdSuccess)
                    SessionState.Manager.Open();
                ManagerBase.WaitSync(0_100);
                //ButtonScalesInit_Click(sender, e);
                ProgramState = ShareEnums.ProgramState.IsRun;

                ComboBoxFieldLoad(fieldLang, FieldLang_SelectedIndexChanged, LocalizationCore.Scales.ListLanguages);
                Log.Information(LocalizationData.Program.IsLoaded + $" {nameof(StopwatchMain.Elapsed)}: {StopwatchMain.Elapsed}.");
            }
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
                ProgramState = ShareEnums.ProgramState.IsExit;
                Quartz.Close();
                Quartz.Dispose();
                Quartz = null;
                SetLabels();
                ManagerBase.WaitSync(0_100);
                ScheduleControlsVisible();
                ManagerBase.WaitSync(0_100);
                //SessionState.Dispose(true);
                ManagerBase.WaitSync(0_100);
                SessionState.Manager.Labels.Close();
                SessionState.Manager.Close();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                Log.Information(LocalizationData.Program.IsClosed + $" {nameof(StopwatchMain.Elapsed)}: {StopwatchMain.Elapsed}.");
                StopwatchMain.Stop();
                StopwatchPrintMain.Stop();
                StopwatchPrintShipping.Stop();
                StopwatchMassa.Stop();
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (Equals(e.Button, MouseButtons.Middle))
            {
                ButtonPrint_Click(sender, e);
            }
        }

        private void MainForm_FontsTransform()
        {
            float emSize;
            if (Width >= 1920 && Height >= 1080)
            {
                emSize = 16.00f;
            }
            else if (Width >= 1600 && Height >= 1024)
            {
                emSize = 14.00f;
            }
            else if (Width >= 1366 && Height >= 768)
            {
                emSize = 12.00f;
            }
            else if (Width >= 1024 && Height >= 768)
            {
                emSize = 10.00f;
            }
            else
            {
                emSize = 9.00f;
            }
            FontsSettings.Setup(emSize);
        }

        private void MainForm_FontsSet()
        {
            // FontMaximum.
            fieldWeightNetto.Font = FontsSettings.FontLabelsTitle;

            // FontTitle.
            fieldTitle.Font = FontsSettings.FontLabelsTitle;
            fieldPlu.Font = FontsSettings.FontLabelsTitle;
            fieldProductDate.Font = FontsSettings.FontLabelsTitle;

            // FontLabelsGray.
            labelThreshold.Font = FontsSettings.FontLabelsGray;
            fieldThreshold.Font = FontsSettings.FontLabelsGray;
            labelSscc.Font = FontsSettings.FontLabelsGray;
            fieldSscc.Font = FontsSettings.FontLabelsGray;
            fieldTasks.Font = FontsSettings.FontLabelsGray;
            labelPrintLabelsMain.Font = FontsSettings.FontLabelsGray;
            labelPrintLabelsShipping.Font = FontsSettings.FontLabelsGray;
            fieldPrintLabelsMain.Font = FontsSettings.FontLabelsGray;
            fieldPrintLabelsShipping.Font = FontsSettings.FontLabelsGray;
            fieldPrintProgressMain.Font = FontsSettings.FontLabelsGray;
            fieldPrintProgressShipping.Font = FontsSettings.FontLabelsGray;
            fieldMassaManager.Font = FontsSettings.FontLabelsGray;
            fieldMassaScalePar.Font = FontsSettings.FontLabelsGray;
            fieldMassaGetCrc.Font = FontsSettings.FontLabelsGray;
            fieldMassaComPort.Font = FontsSettings.FontLabelsGray;
            fieldMassaGet.Font = FontsSettings.FontLabelsGray;
            fieldMassaSetCrc.Font = FontsSettings.FontLabelsGray;
            fieldMassaQueries.Font = FontsSettings.FontLabelsGray;
            fieldMassaSet.Font = FontsSettings.FontLabelsGray;
            fieldMassaQueriesProgress.Font = FontsSettings.FontLabelsGray;
            fieldMemoryManager.Font = FontsSettings.FontLabelsGray;
            fieldMemoryManagerTotal.Font = FontsSettings.FontLabelsGray;
            fieldMemoryProgress.Font = FontsSettings.FontLabelsGray;

            // FontMedium.
            labelWeightNetto.Font = FontsSettings.FontLabelsBlack;
            labelWeightTare.Font = FontsSettings.FontLabelsBlack;
            labelKneading.Font = FontsSettings.FontLabelsBlack;
            fieldKneading.Font = FontsSettings.FontLabelsBlack;
            labelProductDate.Font = FontsSettings.FontLabelsBlack;
            fieldResolution.Font = FontsSettings.FontLabelsBlack;

            // FontButtons.
            if (ButtonScalesTerminal != null)
                ButtonScalesTerminal.Font = FontsSettings.FontButtons;
            if (ButtonScalesInit != null)
                ButtonScalesInit.Font = FontsSettings.FontButtons;
            if (ButtonOrder != null)
                ButtonOrder.Font = FontsSettings.FontButtons;
            if (ButtonNewPallet != null)
                ButtonNewPallet.Font = FontsSettings.FontButtons;
            if (ButtonKneading  != null)
                ButtonKneading .Font = FontsSettings.FontButtons;
            if (ButtonPlu != null)
                ButtonPlu.Font = FontsSettings.FontButtons;
            if (ButtonMore != null)
                ButtonMore.Font = FontsSettings.FontButtons;
            if (ButtonPrint != null)
                ButtonPrint.Font = FontsSettings.FontButtons;
            
            // FontComboBox.
            fieldResolution.Font = FontsSettings.FontComboBox;
            fieldLang.Font = FontsSettings.FontComboBox;
        }

        private void MainForm_ButtonsCreate(ButtonsSettingsEntity buttonsSettings)
        {
            int column = 0;

            TableLayoutPanelButtons = GuiUtils.WinForm.NewTableLayoutPanel(tableLayoutPanelMain, nameof(TableLayoutPanelButtons),
                0, tableLayoutPanelMain.RowCount - 1, tableLayoutPanelMain.ColumnCount);

            if (buttonsSettings.IsScalesTerminal)
            {
                ButtonScalesTerminal = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonScalesTerminal), column++);
                ButtonScalesTerminal.Click += new EventHandler(ButtonScalesTerminal_Click);
                ButtonScalesTerminal.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsScalesInit)
            {
                ButtonScalesInit = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonScalesInit), column++);
                ButtonScalesInit.Click += new EventHandler(ButtonScalesInit_Click);
                ButtonScalesInit.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsOrder)
            {
                ButtonOrder = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonOrder), column++);
                ButtonOrder.Click += new EventHandler(ButtonOrder_Click);
                ButtonOrder.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsNewPallet)
            {
                ButtonNewPallet = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonNewPallet), column++);
                ButtonNewPallet.Click += new EventHandler(ButtonNewPallet_Click);
                ButtonNewPallet.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsKneading)
            {
                ButtonKneading = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonKneading), column++);
                ButtonKneading.Click += new EventHandler(ButtonKneading_Click);
                ButtonKneading.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsPlu)
            {
                ButtonPlu = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonPlu), column++);
                ButtonPlu.Click += new EventHandler(ButtonPlu_Click);
                ButtonPlu.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsMore)
            {
                ButtonMore = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonMore), column++);
                ButtonMore.Click += new EventHandler(ButtonMore_Click);
                ButtonMore.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsPrint)
            {
                ButtonPrint = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonPrint), column++);
                ButtonPrint.Click += new EventHandler(ButtonPrint_Click);
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
                Log.Information("ScheduleIsNextDay");
            }
        }

        private void ScheduleEverySeconds()
        {
            lock (_lockerSeconds)
            {
                if (Quartz == null)
                    return;
                ScheduleMassaManager();
                SchedulePrint(labelPrintLabelsMain, LocalizationCore.Print.NameMain, fieldPrintLabelsMain, 
                    $"{LocalizationCore.Scales.Labels}: {SessionState.CurrentLabelsMain} / {SessionState.WeighingSettings.CurrentLabelsCountMain}",
                    SessionState.Manager.PrintMain);
                SchedulePrint(labelPrintLabelsShipping, LocalizationCore.Print.NameShipping, fieldPrintLabelsShipping,
                    $"{LocalizationCore.Scales.Labels}: {SessionState.CurrentLabelsShipping} / {SessionState.WeighingSettings.CurrentLabelsCountShipping}",
                    SessionState.Manager.PrintShipping);
                ScheduleControlsVisible();
                SetLabels();
            }
        }

        private void SetLabels()
        {
            switch (SessionState.SqlViewModel.PublishType)
            {
                case ShareEnums.PublishType.Debug:
                case ShareEnums.PublishType.Dev:
                    SetTitleSwitchDev();
                    break;
                case ShareEnums.PublishType.Release:
                    SetTitleSwitchRelease();
                    break;
                case ShareEnums.PublishType.Default:
                default:
                    SetTitleSwitchDefault();
                    break;
            }
            MDSoft.WinFormsUtils.InvokeControl.SetText(labelSscc, LocalizationCore.Scales.FieldSscc);
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldSscc, SessionState.ProductSeries.Sscc.SSCC);
            MDSoft.WinFormsUtils.InvokeControl.SetText(labelThreshold, LocalizationCore.Scales.FieldThresholds);
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldThreshold, SessionState.CurrentPlu == null ? string.Empty :
                $"{LocalizationCore.Scales.FieldThresholdLower}: {SessionState.CurrentPlu.LowerWeightThreshold:0.000} {LocalizationCore.Scales.UnitKg} | " +
                $"{LocalizationCore.Scales.FieldThresholdNominal}: {SessionState.CurrentPlu.NominalWeight:0.000} {LocalizationCore.Scales.UnitKg} | " +
                $"{LocalizationCore.Scales.FieldThresholdUpper}: {SessionState.CurrentPlu.UpperWeightThreshold:0.000} {LocalizationCore.Scales.UnitKg}");
        }

        private void SetTitleSwitchDev()
        {
            switch (ProgramState)
            {
                case ShareEnums.ProgramState.IsLoad:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle, AppName +
                        $" SQL: {SessionState.SqlViewModel.PublishDescription} | {LocalizationCore.Scales.ProgramLoad}");
                    break;
                case ShareEnums.ProgramState.IsRun:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle, AppName + 
                        $" SQL: {SessionState.SqlViewModel.PublishDescription}");
                    break;
                case ShareEnums.ProgramState.IsExit:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle, AppName +
                        $" SQL: {SessionState.SqlViewModel.PublishDescription} | {LocalizationCore.Scales.ProgramExit}");
                    break;
            }
            fieldTitle.BackColor = Color.LightYellow;
        }

        private void SetTitleSwitchRelease()
        {
            switch (ProgramState)
            {
                case ShareEnums.ProgramState.IsLoad:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle, AppName + $"  {LocalizationCore.Scales.ProgramLoad}");
                    break;
                case ShareEnums.ProgramState.IsRun:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle, AppName);
                    break;
                case ShareEnums.ProgramState.IsExit:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle, AppName + $"  {LocalizationCore.Scales.ProgramExit}");
                    break;
            }
            fieldTitle.BackColor = Color.LightGreen;
        }

        private void SetTitleSwitchDefault()
        {
            switch (ProgramState)
            {
                case ShareEnums.ProgramState.IsLoad:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle,
                        $@"{AppVersion.AppTitle}.  {SessionState.CurrentScale.Description}. SQL: {SessionState.SqlViewModel.PublishDescription}." +
                        $"  {LocalizationCore.Scales.ProgramLoad}");
                    break;
                case ShareEnums.ProgramState.IsRun:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle,
                        $@"{AppVersion.AppTitle}.  {SessionState.CurrentScale.Description}. SQL: {SessionState.SqlViewModel.PublishDescription}.");
                    break;
                case ShareEnums.ProgramState.IsExit:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle,
                        $@"{AppVersion.AppTitle}.  {SessionState.CurrentScale.Description}. SQL: {SessionState.SqlViewModel.PublishDescription}." +
                        $"  {LocalizationCore.Scales.ProgramExit}");
                    break;
            }
            fieldTitle.BackColor = Color.IndianRed;
        }

        private void ScheduleControlsVisible()
        {
            switch (ProgramState)
            {
                case ShareEnums.ProgramState.IsRun:
                    if (!IsProgramStateIsRunVisible)
                    {
                        MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldPlu, true);
                        MDSoft.WinFormsUtils.InvokeControl.SetVisible(labelWeightNetto, true);
                        MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldWeightTare, true);
                        //MDSoft.WinFormsUtils.InvokeControl.SetVisible(labelKneading, true);
                        //MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldKneading, true);
                        MDSoft.WinFormsUtils.InvokeControl.SetVisible(labelProductDate, true);
                        MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldProductDate, true);
                        IsProgramStateIsRunVisible = true;
                    }
                    break;
                case ShareEnums.ProgramState.IsExit:
                    IsProgramStateIsRunVisible = false;
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldPlu, false);
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(labelWeightNetto, false);
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldWeightTare, false);
                    //MDSoft.WinFormsUtils.InvokeControl.SetVisible(labelKneading, false);
                    //MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldKneading, false);
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(labelProductDate, false);
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldProductDate, false);
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldResolution, false);
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(pictureBoxClose, false);
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldTasks, false);
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldLang, false);
                    break;
                case ShareEnums.ProgramState.Default:
                case ShareEnums.ProgramState.IsLoad:
                default:
                    IsProgramStateIsRunVisible = false;
                    break;
            }
        }

        private void SchedulePrint(Label labelPrint, string caption, Label fieldPrint, string value, ManagerPrint managerPrint)
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(labelPrint, caption);
            //SessionState.LabelsCurrent = SessionState.Manager.Print.UserLabelCount < SessionState.LabelsCount
            //    ? SessionState.Manager.Print.UserLabelCount : SessionState.LabelsCount;

            // LabelsCurrent
            if (SessionState.CurrentLabelsMain < 1)
                SessionState.CurrentLabelsMain = 1;

            switch (SessionState.PrintBrandMain)
            {
                case WeightCore.Print.PrintBrand.Zebra:
                    if (ProgramState == ShareEnums.ProgramState.IsRun && StopwatchPrintMain.Elapsed.TotalSeconds > 5)
                    {
                        //SessionState.Manager.PrintMain.SetCurrentStatus();
                        StopwatchPrintMain.Restart();
                    }
                    if (managerPrint.CurrentStatus != null)
                    {
                        MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPrint,
                            $"{LocalizationCore.Print.ModelZebra} {SessionState.CurrentScale.PrinterMain.Ip}: " +
                            (managerPrint.CurrentStatus.isReadyToPrint
                                ? $"{LocalizationCore.Print.Available}" 
                                : $"{LocalizationCore.Print.Unavailable}"
                            ) + $" | {LocalizationCore.Print.SensorPeeler}: {managerPrint.SensorPeelerStatus} | {value} | {SessionState.Manager.PrintMain.ProgressString}"
                        );
                    }
                    break;
                case WeightCore.Print.PrintBrand.TSC:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPrint,
                        $"{LocalizationCore.Print.ModelTsc}: {SessionState.Manager.PrintMain.Win32Printer()?.PrinterStatusDescription} " +
                        $"{SessionState.Manager.PrintMain.ProgressString}");
                    break;
                case WeightCore.Print.PrintBrand.Default:
                default:
                    break;
            }
            SessionState.Manager.PrintMain.ProgressString = StringUtils.GetProgressString(SessionState.Manager.PrintMain.ProgressString);
        }

        private void ScheduleMassaManager()
        {
            if (SessionState.Manager.Massa == null)
                return;

            // Состояние COM-порта.
            if (ProgramState == ShareEnums.ProgramState.IsRun && SessionState.IsCurrentPluCheckWeight && 
                StopwatchMassa.Elapsed.TotalSeconds > 5)
            {
                SessionState.Manager.Massa.Open();
                StopwatchMassa.Restart();
            }

            // Schedule - Request parameters.
            ScheduleMassaManagerRequestGetScalePar();
        }

        private void ScheduleMassaManagerRequestGetScalePar()
        {
            if (SessionState.Manager.Massa.ResponseParseScalePar == null)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaScalePar, $"{LocalizationCore.Scales.RequestParameters}: ...");
            }
            else
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaScalePar,
                    SessionState.Manager.Massa.ResponseParseScalePar.DtCreated.ToString(@"HH:mm:ss") + 
                    $"  {LocalizationCore.Scales.RequestParameters}: " +
                    SessionState.Manager.Massa.ResponseParseScalePar.Message);
            }
        }

        #endregion

        #region Private methods

        private void ComboBoxFieldLoad(ComboBox comboBox, EventHandler eventHandler, List<string> sourceList, int selectedIndex = 0)
        {
            if (comboBox == null || sourceList == null || sourceList.Count == 0 || selectedIndex < 0)
                return;
            int backupIndex = comboBox.SelectedIndex;
            comboBox.SelectedIndexChanged -= eventHandler;
            comboBox.Items.Clear();
            comboBox.Items.AddRange(sourceList.ToArray());
            comboBox.SelectedIndex = selectedIndex <= 0
                ? backupIndex <= 0 ? 0 : backupIndex
                : selectedIndex < comboBox.Items.Count ? selectedIndex : 0;
            comboBox.SelectedIndexChanged += eventHandler;
            eventHandler.Invoke(comboBox, null);
        }

        private void FieldPrintManager_Click(object sender, EventArgs e)
        {
            WmiWin32PrinterEntity win32Printer = SessionState.Manager.PrintMain.Win32Printer();
            if (win32Printer == null)
                return;
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false, FormBorderStyle.FixedDialog, 26, 20, 18) { Width = 700, Height = 400 };
            wpfPageLoader.Text = LocalizationCore.Print.InfoCaption;
            wpfPageLoader.MessageBox.Caption = LocalizationCore.Print.InfoCaption;
            wpfPageLoader.MessageBox.Message =
                $"{LocalizationCore.Print.Name}: {win32Printer.Name}" + Environment.NewLine +
                $"{LocalizationCore.Print.Driver}: {win32Printer.DriverName}" + Environment.NewLine +
                $"{LocalizationCore.Print.Port}: {win32Printer.PortName}" + Environment.NewLine +
                $"{LocalizationCore.Print.StateCode}: {win32Printer.PrinterState}" + Environment.NewLine +
                $"{LocalizationCore.Print.StatusCode}: {win32Printer.PrinterStatus}" + Environment.NewLine +
                $"{LocalizationCore.Print.Status}: {win32Printer.PrinterStatusDescription}" + Environment.NewLine +
                $"{LocalizationCore.Print.State} (ENG): {win32Printer.Status}" + Environment.NewLine +
                $"{LocalizationCore.Print.State}: {win32Printer.StatusDescription}";
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonCustomVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonCustomContent = LocalizationCore.Print.ClearQueue;
            //wpfPageLoader.MessageBox.VisibilitySettings.Localization();
            DialogResult result = wpfPageLoader.ShowDialog(this);
            wpfPageLoader.Close();
            wpfPageLoader.Dispose();
            if (result == DialogResult.Retry)
            {
                SessionState.Manager.PrintMain.ClearPrintBuffer(true, SessionState.CurrentLabelsMain);
            }
        }

        private void FieldSscc_DoubleClick(object sender, EventArgs e)
        {
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false, FormBorderStyle.FixedDialog, 26, 20, 18) { Width = 700, Height = 400 };
            wpfPageLoader.Text = LocalizationCore.Scales.FieldSsccShort;
            wpfPageLoader.MessageBox.Caption = LocalizationCore.Scales.FieldSscc;
            wpfPageLoader.MessageBox.Message =
                $"{LocalizationCore.Scales.FieldSscc}: {SessionState.ProductSeries.Sscc.SSCC}" + Environment.NewLine +
                $"{LocalizationCore.Scales.FieldSsccGln}: {SessionState.ProductSeries.Sscc.GLN}" + Environment.NewLine +
                $"{LocalizationCore.Scales.FieldSsccUnitId}: {SessionState.ProductSeries.Sscc.UnitID}" + Environment.NewLine +
                $"{LocalizationCore.Scales.FieldSsccUnitType}: {SessionState.ProductSeries.Sscc.UnitType}" + Environment.NewLine +
                $"{LocalizationCore.Scales.FieldSsccSynonym}: {SessionState.ProductSeries.Sscc.SynonymSSCC}" + Environment.NewLine +
                $"{LocalizationCore.Scales.FieldSsccControlNumber}: {SessionState.ProductSeries.Sscc.Check}";
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
                message += $"{LocalizationCore.Scales.ThreadId}: {thread.Id}. " +
                    $"{LocalizationCore.Scales.ThreadPriorityLevel}: {thread.PriorityLevel}. " +
                    $"{LocalizationCore.Scales.ThreadState}: {thread.ThreadState}. " +
                    $"{LocalizationCore.Scales.ThreadStartTime}: {thread.StartTime}. " + Environment.NewLine;
            }
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false, FormBorderStyle.FixedDialog, 
                20, 14, 18, 0, 12, 1) { Width = Width - 50, Height = Height - 50 };
            wpfPageLoader.Text = $"{LocalizationCore.Scales.ThreadsCount}: {Process.GetCurrentProcess().Threads.Count}";
            wpfPageLoader.MessageBox.Message = message;
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.VisibilitySettings.Localization();
            wpfPageLoader.ShowDialog(this);
            wpfPageLoader.Close();
            wpfPageLoader.Dispose();
        }

        private void PictureBoxClose_Click(object sender, EventArgs e)
        {
            Close();
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
                MainForm_FontsTransform();
                MainForm_FontsSet();
                Log.Information($"Screen resolution: {Width} x {Height}");
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
                LocalizationCore.Lang = LocalizationData.Lang = fieldLang.SelectedIndex switch { 1 => ShareEnums.Lang.English, _ => ShareEnums.Lang.Russian, };
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonScalesTerminal, LocalizationCore.Scales.ButtonRunScalesTerminal);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonScalesInit, LocalizationCore.Scales.ButtonScalesInitShort);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonOrder, LocalizationCore.Scales.ButtonSelectOrder);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonNewPallet, LocalizationCore.Scales.ButtonNewPallet);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonKneading , LocalizationCore.Scales.ButtonAddKneading);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonPlu, LocalizationCore.Scales.ButtonSelectPlu);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonMore, LocalizationCore.Scales.ButtonSetKneading);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonPrint, LocalizationCore.Print.ActionPrint);
                ComboBoxFieldLoad(fieldResolution, FieldResolution_SelectedIndexChanged, LocalizationCore.Scales.ListResolutions,
                    Debug.IsDebug ? 2 : LocalizationCore.Scales.ListResolutions.Count - 1);
                SetLabels();
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

        private void FieldCurrentTime_Click(object sender, EventArgs e)
        {
            IsShowInfoLabels = Debug.IsDebug && ProgramState == ShareEnums.ProgramState.IsRun && !IsShowInfoLabels;
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldResolution, IsShowInfoLabels);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldLang, IsShowInfoLabels);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldTasks, IsShowInfoLabels);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldMemoryManager, IsShowInfoLabels);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldMemoryManagerTotal, IsShowInfoLabels);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldMemoryProgress, IsShowInfoLabels);
        }

        private void FieldTitle_DoubleClick(object sender, EventArgs e)
        {
            bool isCmdSuccess = false;
            try
            {
                SessionState.Manager.Close();
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.SqlSettings, false) { Width = 400, Height = 400 };
                wpfPageLoader.ShowDialog(this);
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
                isCmdSuccess = true;
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
                if (isCmdSuccess)
                    SessionState.Manager.Open();
            }
        }

        #endregion

        #region Public and private methods - Buttons
        
        private void ButtonScalesTerminal_Click(object sender, EventArgs e)
        {
            bool isCmdSuccess = false;
            try
            {
                if (GuiUtils.WpfForm.ShowNewPinCode(this))
                {
                    DialogResult result = GuiUtils.WpfForm.ShowNewOperationControl(this, 
                        $"{LocalizationCore.Scales.QuestionRunApp} ScalesTerminal?",
                        new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                    if (result != DialogResult.Yes)
                        return;

                    SessionState.Manager.Close();

                    // Run app.
                    if (File.Exists(LocalizationData.Paths.ScalesTerminal))
                    {
                        SessionState.Manager.Close();
                        Proc.Run(LocalizationData.Paths.ScalesTerminal, string.Empty, false, ProcessWindowStyle.Normal, true);
                    }
                    else
                    {
                        GuiUtils.WpfForm.ShowNewOperationControl(this,
                            LocalizationCore.Scales.ProgramNotFound(LocalizationData.Paths.ScalesTerminal));
                    }
                }
                isCmdSuccess = true;
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
                if (isCmdSuccess)
                    SessionState.Manager.Open();
            }
        }
        
        private void ButtonScalesInit_Click(object sender, EventArgs e)
        {
            bool isCmdSuccess = false;
            int lineNumber = 0;
            try
            {
                // ShowNewOperationControl.
                if (ProgramState == ShareEnums.ProgramState.IsRun && 
                    (!SessionState.Manager.Massa.MassaDevice.IsConnected || !SessionState.IsCurrentPluCheckWeight))
                {
                    GuiUtils.WpfForm.ShowNewOperationControl(this, LocalizationCore.Scales.ButtonSelectPluWeight);
                    return;
                }

                lineNumber = 3;
                SessionState.Manager.Close();
                lineNumber = 4;

                // Fix negative weight.
                if (SessionState.Manager.Massa.WeightNet < 0)
                {
                    lineNumber = 5;
                    SessionState.Manager.Massa.ResetMassa();
                    lineNumber = 6;
                }

                lineNumber = 7;
                SessionState.CheckWeightMassaDeviceExists(this);
                lineNumber = 8;
                SessionState.ClearCurrentPlu();
                lineNumber = 9;

                isCmdSuccess = true;
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true, "", lineNumber);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
                if (isCmdSuccess)
                {
                    SessionState.Manager.Open();
                    SessionState.Manager.Massa.GetInit();
                }
            }
        }

        private void ButtonOrder_Click(object sender, EventArgs e)
        {
            bool isCmdSuccess = false;
            try
            {
                SessionState.Manager.Close();

                if (SessionState.CurrentOrder == null)
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
                        SessionState.CurrentOrder = null;
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
                if (SessionState.CurrentOrder != null)
                {
                    MDSoft.WinFormsUtils.InvokeProgressBar.SetMaximum(fieldPrintProgressMain, SessionState.CurrentOrder.PlaneBoxCount);
                    MDSoft.WinFormsUtils.InvokeProgressBar.SetMinimum(fieldPrintProgressMain, 0);
                    MDSoft.WinFormsUtils.InvokeProgressBar.SetValue(fieldPrintProgressMain, SessionState.CurrentOrder.FactBoxCount);
                }
                isCmdSuccess = true;
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
                if (isCmdSuccess)
                    SessionState.Manager.Open();
            }
        }

        private void ButtonNewPallet_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.NewPallet();
                SetLabels();
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

        private void ButtonKneading_Click(object sender, EventArgs e)
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

        private void ButtonPlu_Click(object sender, EventArgs e)
        {
            bool isCmdSuccess = false;
            try
            {
                SessionState.ClearCurrentPlu();
                if (SessionState.CheckWeightMassaDeviceExists(this))
                {
                    if (!SessionState.CheckWeightIsNegative(this) || !SessionState.CheckWeightIsPositive(this))
                        return;
                }
                SessionState.Manager.Close();

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
                    ButtonMore_Click(null, null);
                }
                else
                {
                    SessionState.ClearCurrentPlu();
                }
                isCmdSuccess = true;
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
                if (isCmdSuccess)
                    SessionState.Manager.Open();
            }
        }

        private void ButtonMore_Click(object sender, EventArgs e)
        {
            bool isCmdSuccess = false;
            try
            {
                if (SessionState.CurrentPlu == null)
                {
                    GuiUtils.WpfForm.ShowNewOperationControl(this, LocalizationCore.Scales.ChoosePlu);
                    return;
                }

                SessionState.Manager.Close();

                using KneadingForm kneadingForm = new() { Owner = this };
                DialogResult result = kneadingForm.ShowDialog();
                kneadingForm.Close();
                kneadingForm.Dispose();
                if (result == DialogResult.OK)
                {
                    //_sessionState.Kneading = settingsForm.CurrentKneading;
                    //_sessionState.ProductDate = settingsForm.CurrentProductDate;
                }
                isCmdSuccess = true;
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
                if (isCmdSuccess)
                    SessionState.Manager.Open();
            }
        }

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            bool isCmdSuccess = false;
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
                isCmdSuccess = true;
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
                if (isCmdSuccess)
                    SessionState.Manager.Open();
            }
        }

        #endregion

        #region Public and private methods - Template

        // Template - don't remove this method.
        //private void TemplateJobWithTaskManager(
        //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        //{
        //    bool isCmdSuccess = false;
        //    try
        //    {
        //        SessionState.Manager.Close();

        //        // .. methods
        //        isCmdSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception.Catch(this, ref ex, true, filePath, lineNumber, memberName);
        //    }
        //    finally
        //    {
        //        MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        //        if (isCmdSuccess)
        //            SessionState.Manager.Open();
        //    }
        //}

        #endregion
    }
}
