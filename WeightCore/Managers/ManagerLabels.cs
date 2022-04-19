// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.TableDirectModels;
using DataCore.Utils;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using WeightCore.Helpers;
using LocalizationCore = DataCore.Localization.Core;

namespace WeightCore.Managers
{
    public class ManagerLabels : ManagerBase
    {
        #region Public and private fields and properties

        private Label FieldKneading { get; set; }
        private Label FieldMassaComPort { get; set; }
        private Label FieldMassaGet { get; set; }
        private Label FieldMassaGetCrc { get; set; }
        private Label FieldMassaManager { get; set; }
        private Label FieldMassaQueries { get; set; }
        private Label FieldMassaSet { get; set; }
        private Label FieldMassaSetCrc { get; set; }
        private Label FieldPlu { get; set; }
        private Label FieldProductDate { get; set; }
        private Label FieldWeightNetto { get; set; }
        private Label FieldWeightTare { get; set; }
        private Label LabelKneading { get; set; }
        private Label LabelProductDate { get; set; }
        private Label LabelWeightNetto { get; set; }
        private Label LabelWeightTare { get; set; }
        private PluDirect CurrentPlu => SessionStateHelper.Instance.CurrentPlu;
        private ProgressBar FieldMassaQueriesProgress { get; set; }
        public Label FieldPrintMain { get; private set; }
        public Label FieldPrintShipping { get; private set; }
        public Label LabelPrintMain { get; private set; }
        public Label LabelPrintShipping { get; private set; }

        #endregion

        #region Constructor and destructor

        public ManagerLabels() : base()
        {
            Init(CloseMethod, ReleaseManaged, ReleaseUnmanaged);
        }

        #endregion

        #region Public and private methods

        public void Init(Label labelProductDate, Label fieldProductDate, Label labelKneading, Label fieldKneading,
            Label fieldPlu, Label labelWeightNetto, Label fieldWeightNetto, Label labelWeightTare, Label fieldWeightTare,
            Label fieldMassaManager, Label fieldMassaComPort, Label fieldMassaQueries, ProgressBar fieldMassaQueriesProgress,
            Label fieldMassaGet, Label fieldMassaGetCrc, Label fieldMassaSet, Label fieldMassaSetCrc,
            Label labelPrintMain, Label fieldPrintMain, Label labelPrintShipping, Label fieldPrintShipping)
        {
            Init(ProjectsEnums.TaskType.LabelManager,
            () =>
            {
                LabelProductDate = labelProductDate;
                FieldProductDate = fieldProductDate;
                LabelKneading = labelKneading;
                FieldKneading = fieldKneading;
                FieldPlu = fieldPlu;
                LabelWeightNetto = labelWeightNetto;
                FieldWeightNetto = fieldWeightNetto;
                LabelWeightTare = labelWeightTare;
                FieldWeightTare = fieldWeightTare;
                FieldMassaManager = fieldMassaManager;
                FieldMassaComPort = fieldMassaComPort;
                FieldMassaQueries = fieldMassaQueries;
                FieldMassaQueriesProgress = fieldMassaQueriesProgress;
                FieldMassaGet = fieldMassaGet;
                FieldMassaGetCrc = fieldMassaGetCrc;
                FieldMassaSet = fieldMassaSet;
                FieldMassaSetCrc = fieldMassaSetCrc;
                LabelPrintMain = labelPrintMain;
                FieldPrintMain = fieldPrintMain;
                LabelPrintShipping = labelPrintShipping;
                FieldPrintShipping = fieldPrintShipping;
            },
            5_000, 250, 250, 250, 1_000);
        }

        public new void Open()
        {
            try
            {
                Open(
                () =>
                {
                    OpenProductDate();
                    OpenPlu();
                    OpenWeights();
                    OpenResponseGetMassa();
                    OpenKneading();
                    OpenMasseSet();
                    OpenPrint();
                }, null, null);
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
        }

        private void OpenPrint()
        {
            OpenPrint(LabelPrintMain, LocalizationCore.Print.NameMain, FieldPrintMain,
                $"{LocalizationCore.Scales.Labels}: {SessionStateHelper.Instance.CurrentLabelsMain} / " +
                $"{SessionStateHelper.Instance.WeighingSettings.CurrentLabelsCountMain}",
                SessionStateHelper.Instance.Manager.PrintMain);
            OpenPrint(LabelPrintShipping, LocalizationCore.Print.NameShipping, FieldPrintShipping,
                $"{LocalizationCore.Scales.Labels}: {SessionStateHelper.Instance.CurrentLabelsShipping} / " +
                $"{SessionStateHelper.Instance.WeighingSettings.CurrentLabelsCountShipping}",
                SessionStateHelper.Instance.Manager.PrintShipping);
        }

        private void OpenMasseSet()
        {
            if (SessionStateHelper.Instance.Manager.Massa.ResponseParseSet == null)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSet, $"{LocalizationCore.Scales.WeightingScaleCmd}: ...");
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSetCrc, $"{LocalizationCore.Scales.Crc}: ...");
            }
            else
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSet,
                    $"{SessionStateHelper.Instance.Manager.Massa.ResponseParseSet.DtCreated:HH:mm:ss}  " +
                    $"{LocalizationCore.Scales.WeightingScaleCmd}: " +
                    SessionStateHelper.Instance.Manager.Massa.ResponseParseSet.Message);
                //MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSetCrc, $"{LocalizationCore.Scales.Crc}: " +
                //    (SessionStateHelper.Instance.Manager.Massa.ResponseParseSet.IsValidAll
                //    ? $"{LocalizationCore.Scales.StateCorrect} {SessionStateHelper.Instance.Manager.Massa.ProgressStringResponse}"
                //    : $"{LocalizationCore.Scales.StateError}! {SessionStateHelper.Instance.Manager.Massa.ProgressStringResponse}"));
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaSetCrc, $"{LocalizationCore.Scales.Crc}: " +
                    (SessionStateHelper.Instance.Manager.Massa.ResponseParseSet.IsValidAll
                    ? $"{LocalizationCore.Scales.StateCorrect}" : $"{LocalizationCore.Scales.StateError}!"));
                //SessionStateHelper.Instance.Manager.Massa.ProgressStringResponse = StringUtils.GetProgressString(SessionStateHelper.Instance.Manager.Massa.ProgressStringResponse);
            }
        }

        private void OpenResponseGetMassa()
        {
            if (SessionStateHelper.Instance.Manager.Massa.ResponseParseGet == null)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet, $"{LocalizationCore.Scales.WeightingMessage}: ...");
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGetCrc, $"{LocalizationCore.Scales.Crc}: ...");
            }
            else
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGet,
                    $"{SessionStateHelper.Instance.Manager.Massa.ResponseParseGet.DtCreated:HH:mm:ss)}  " +
                    $"{LocalizationCore.Scales.WeightingMessage}: " +
                    SessionStateHelper.Instance.Manager.Massa.ResponseParseGet.Message);
                //MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGetCrc, $"{LocalizationCore.Scales.Crc}: " +
                //    (SessionStateHelper.Instance.Manager.Massa.ResponseParseGet.IsValidAll
                //    ? $"{LocalizationCore.Scales.StateCorrect} {SessionStateHelper.Instance.Manager.Massa.ProgressStringRequest}"
                //    : $"{LocalizationCore.Scales.StateError}! {SessionStateHelper.Instance.Manager.Massa.ProgressStringRequest}"));
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaGetCrc, $"{LocalizationCore.Scales.Crc}: " +
                    (SessionStateHelper.Instance.Manager.Massa.ResponseParseGet.IsValidAll
                    ? $"{LocalizationCore.Scales.StateCorrect}" : $"{LocalizationCore.Scales.StateError}!"));
                //SessionStateHelper.Instance.Manager.Massa.ProgressStringRequest = StringUtils.GetProgressString(SessionStateHelper.Instance.Manager.Massa.ProgressStringRequest);
            }
        }

        private void OpenProductDate()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(LabelProductDate,
                $"{LocalizationCore.Scales.FieldTime}: {DateTime.Now:HH:mm:ss}");
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldProductDate,
                $"{LocalizationCore.Scales.FieldDate}: {SessionStateHelper.Instance.ProductDate:dd.MM.yyyy}");
        }

        private void OpenPlu()
        {
            if (CurrentPlu == null)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu, $"{@LocalizationCore.Scales.Plu}");
            }
            else
            {
                if (CurrentPlu.IsCheckWeight == true)
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu,
                        $"{LocalizationCore.Scales.PluWeight}: " +
                        $"{CurrentPlu.PLU} | {CurrentPlu.GoodsName}");
                }
                else
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu,
                        $"{LocalizationCore.Scales.PluCount}: " +
                        $"{CurrentPlu.PLU} | {CurrentPlu.GoodsName}");

                }
            }
        }

        private void OpenKneading()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(LabelKneading, LocalizationCore.Scales.FieldKneading);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldKneading,
                $"{SessionStateHelper.Instance.WeighingSettings.Kneading}");
        }

        private void OpenWeights()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(LabelWeightTare, LocalizationCore.Scales.FieldWeightTare);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldWeightTare, CurrentPlu != null
                ? $"{CurrentPlu.GoodsTareWeight:0.000} {LocalizationCore.Scales.UnitKg}"
                : $"0,000 {LocalizationCore.Scales.UnitKg}");
            MDSoft.WinFormsUtils.InvokeControl.SetText(LabelWeightNetto, LocalizationCore.Scales.FieldWeightNetto);

            if (SessionStateHelper.Instance.Manager.Massa != null)
            {
                SessionStateHelper.Instance.Manager.Massa.ProgressString = StringUtils.GetProgressString(SessionStateHelper.Instance.Manager.Massa.ProgressString);
                if (CurrentPlu == null)
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldWeightNetto, $"{0:0.000} {LocalizationCore.Scales.UnitKg}");
                    //MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaManager,
                    //    $"{0:0.000} {LocalizationCore.Scales.UnitKg} {SessionStateHelper.Instance.Manager.Massa.ProgressString}");
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaManager, $"{0:0.000} {LocalizationCore.Scales.UnitKg}");
                }
                else
                {
                    decimal weight = SessionStateHelper.Instance.Manager.Massa.WeightNet - CurrentPlu.GoodsTareWeight;
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldWeightNetto, $"{weight:0.000} {LocalizationCore.Scales.UnitKg}");
                    //MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaManager, SessionStateHelper.Instance.Manager.Massa.IsStable == 0
                    //    ? $"{@LocalizationCore.Scales.WeightingProcess}: {SessionStateHelper.Instance.Manager.Massa.WeightNet:0.000} " +
                    //      $"{LocalizationCore.Scales.UnitKg} {SessionStateHelper.Instance.Manager.Massa.ProgressString}"
                    //    : $"{@LocalizationCore.Scales.WeightingStable}: {SessionStateHelper.Instance.Manager.Massa.WeightNet:0.000} " +
                    //      $"{LocalizationCore.Scales.UnitKg} {SessionStateHelper.Instance.Manager.Massa.ProgressString}");
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaManager, SessionStateHelper.Instance.Manager.Massa.IsStable == 0
                        ? $"{@LocalizationCore.Scales.WeightingProcess}: {SessionStateHelper.Instance.Manager.Massa.WeightNet:0.000} " +
                          $"{LocalizationCore.Scales.UnitKg}"
                        : $"{@LocalizationCore.Scales.WeightingStable}: {SessionStateHelper.Instance.Manager.Massa.WeightNet:0.000} " +
                          $"{LocalizationCore.Scales.UnitKg}");
                }
                if (SessionStateHelper.Instance.Manager.Massa.MassaDevice != null)
                {
                    //MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaComPort, SessionStateHelper.Instance.Manager.Massa.MassaDevice.IsConnected
                    //    ? $"{LocalizationCore.Scales.ComPortState}: {LocalizationCore.Scales.StateResponsed} " +
                    //      $"{SessionStateHelper.Instance.Manager.Massa.ProgressString}"
                    //    : $"{LocalizationCore.Scales.ComPortState}: {LocalizationCore.Scales.StateNotResponsed} " +
                    //      $"{SessionStateHelper.Instance.Manager.Massa.ProgressString}");
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaComPort, SessionStateHelper.Instance.Manager.Massa.MassaDevice.IsConnected
                        ? $"{LocalizationCore.Scales.ComPortState}: {LocalizationCore.Scales.StateResponsed}"
                        : $"{LocalizationCore.Scales.ComPortState}: {LocalizationCore.Scales.StateNotResponsed}");
                }
                // Очередь сообщений весов.
                //MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaQueries,
                //    $"{LocalizationCore.Scales.ScaleQueue}: {SessionStateHelper.Instance.Manager.Massa.Requests?.Count} {SessionStateHelper.Instance.Manager.Massa.ProgressStringQueries}");
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaQueries,
                    $"{LocalizationCore.Scales.ScaleQueue}: {SessionStateHelper.Instance.Manager.Massa.Requests?.Count}");
                //SessionStateHelper.Instance.Manager.Massa.ProgressStringQueries = StringUtils.GetProgressString(SessionStateHelper.Instance.Manager.Massa.ProgressStringQueries);
                MDSoft.WinFormsUtils.InvokeProgressBar.SetValue(FieldMassaQueriesProgress,
                    SessionStateHelper.Instance.Manager.Massa.Requests != null
                        ? SessionStateHelper.Instance.Manager.Massa.Requests.Count : 0);
            }
        }

        private void OpenPrint(Label labelPrint, string caption, Label fieldPrint, string value, ManagerPrint managerPrint)
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(labelPrint, caption);
            //SessionStateHelper.Instance.LabelsCurrent = SessionStateHelper.Instance.Manager.Print.UserLabelCount < SessionStateHelper.Instance.LabelsCount
            //    ? SessionStateHelper.Instance.Manager.Print.UserLabelCount : SessionStateHelper.Instance.LabelsCount;

            // LabelsCurrent
            if (SessionStateHelper.Instance.CurrentLabelsMain < 1)
                SessionStateHelper.Instance.CurrentLabelsMain = 1;

            switch (SessionStateHelper.Instance.PrintBrandMain)
            {
                case Print.PrintBrand.Zebra:
                    if (SessionStateHelper.Instance.ProgramState == ShareEnums.ProgramState.IsRun &&
                        SessionStateHelper.Instance.StopwatchPrintMain.Elapsed.TotalSeconds > 5)
                    {
                        //SessionStateHelper.Instance.Manager.PrintMain.SetCurrentStatus();
                        SessionStateHelper.Instance.StopwatchPrintMain.Restart();
                    }
                    if (managerPrint.CurrentStatus != null)
                    {
                        MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPrint,
                            $"{LocalizationCore.Print.ModelZebra} {SessionStateHelper.Instance.CurrentScale.PrinterMain.Ip}: " +
                            (managerPrint.CurrentStatus.isReadyToPrint
                                ? $"{LocalizationCore.Print.Available}"
                                : $"{LocalizationCore.Print.Unavailable}"
                            ) + $" | {LocalizationCore.Print.SensorPeeler}: {managerPrint.SensorPeelerStatus} | {value} | {SessionStateHelper.Instance.Manager.PrintMain.ProgressString}"
                        );
                    }
                    break;
                case Print.PrintBrand.TSC:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPrint,
                        $"{LocalizationCore.Print.ModelTsc}: {SessionStateHelper.Instance.Manager.PrintMain.Win32Printer()?.PrinterStatusDescription} " +
                        $"{SessionStateHelper.Instance.Manager.PrintMain.ProgressString}");
                    break;
                case Print.PrintBrand.Default:
                default:
                    break;
            }
            SessionStateHelper.Instance.Manager.PrintMain.ProgressString = StringUtils.GetProgressString(SessionStateHelper.Instance.Manager.PrintMain.ProgressString);
        }

        public new void CloseMethod()
        {
            base.Close();
        }

        public new void ReleaseManaged()
        {
            base.ReleaseManaged();
        }

        public new void ReleaseUnmanaged()
        {
            base.ReleaseUnmanaged();
        }

        #endregion
    }
}
