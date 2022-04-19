// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.TableDirectModels;
using DataCore.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeightCore.Helpers;
using LocalizationCore = DataCore.Localization.Core;

namespace WeightCore.Managers
{
    public class ManagerLabels : ManagerBase
    {
        #region Public and private fields and properties

        private Label FieldKneading { get; set; }
        private Label FieldPlu { get; set; }
        private Label FieldProductDate { get; set; }
        private Label FieldSscc { get; set; }
        private Label FieldTitle { get; set; }
        private Label LabelKneading { get; set; }
        private Label LabelProductDate { get; set; }
        private PluDirect CurrentPlu => SessionStateHelper.Instance.CurrentPlu;
        public Label FieldPrintMain { get; private set; }
        public Label FieldPrintShipping { get; private set; }

        #endregion

        #region Constructor and destructor

        public ManagerLabels() : base()
        {
            Init(Close, ReleaseManaged, ReleaseUnmanaged);
        }

        #endregion

        #region Public and private methods

        public void Init(Label fieldTitle, Label fieldPlu, Label fieldSscc, 
            Label labelProductDate, Label fieldProductDate, Label labelKneading, Label fieldKneading)
        {
            Init(ProjectsEnums.TaskType.LabelManager,
            () =>
            {
                FieldTitle = fieldTitle;
                FieldPlu = fieldPlu;
                FieldSscc = fieldSscc;
                LabelProductDate = labelProductDate;
                FieldProductDate = fieldProductDate;
                LabelKneading = labelKneading;
                FieldKneading = fieldKneading;
                
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle, SessionStateHelper.Instance.AppName);
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu, LocalizationCore.Scales.Plu);
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldSscc, LocalizationCore.Scales.FieldSscc);
                MDSoft.WinFormsUtils.InvokeControl.SetText(LabelProductDate, LocalizationCore.Scales.FieldTime);
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldProductDate, LocalizationCore.Scales.FieldDate);
                MDSoft.WinFormsUtils.InvokeControl.SetText(LabelKneading, LocalizationCore.Scales.FieldKneading);
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldKneading, string.Empty);
                
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldTitle, true);
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldPlu, true);
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldSscc, true);
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelProductDate, true);
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldProductDate, true);
                if (SessionStateHelper.Instance.CurrentScale?.IsKneading == true)
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelKneading, true);
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldKneading, true);
                }
            },
            1_000, 1_000, 1_000, 1_000, 1_000);
        }

        public new void Open()
        {
            try
            {
                Open(
                () =>
                {
                    OpenTitle();
                },
                () =>
                {
                    RequestProductDate();
                    RequestPlu();
                    RequestKneading();
                },
                null);
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
        }

        private void OpenTitle()
        {
            switch (SessionStateHelper.Instance.SqlViewModel.PublishType)
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
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldSscc, $"{LocalizationCore.Scales.FieldSscc}: {SessionStateHelper.Instance.ProductSeries.Sscc.SSCC}");
        }

        private void SetTitleSwitchDev()
        {
            switch (SessionStateHelper.Instance.ProgramState)
            {
                case ShareEnums.ProgramState.IsLoad:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle, SessionStateHelper.Instance.AppName +
                        $" SQL: {SessionStateHelper.Instance.SqlViewModel.PublishDescription} | {LocalizationCore.Scales.ProgramLoad}");
                    break;
                case ShareEnums.ProgramState.IsRun:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle, SessionStateHelper.Instance.AppName +
                        $" SQL: {SessionStateHelper.Instance.SqlViewModel.PublishDescription}");
                    break;
                case ShareEnums.ProgramState.IsExit:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle, SessionStateHelper.Instance.AppName +
                        $" SQL: {SessionStateHelper.Instance.SqlViewModel.PublishDescription} | {LocalizationCore.Scales.ProgramExit}");
                    break;
            }
            MDSoft.WinFormsUtils.InvokeControl.SetBackColor(FieldTitle, Color.LightYellow);
        }

        private void SetTitleSwitchRelease()
        {
            switch (SessionStateHelper.Instance.ProgramState)
            {
                case ShareEnums.ProgramState.IsLoad:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle, SessionStateHelper.Instance.AppName + $"  {LocalizationCore.Scales.ProgramLoad}");
                    break;
                case ShareEnums.ProgramState.IsRun:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle, SessionStateHelper.Instance.AppName);
                    break;
                case ShareEnums.ProgramState.IsExit:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle, SessionStateHelper.Instance.AppName + $"  {LocalizationCore.Scales.ProgramExit}");
                    break;
            }
            MDSoft.WinFormsUtils.InvokeControl.SetBackColor(FieldTitle, Color.LightGreen);
        }

        private void SetTitleSwitchDefault()
        {
            switch (SessionStateHelper.Instance.ProgramState)
            {
                case ShareEnums.ProgramState.IsLoad:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle,
                        $@"{AppVersionHelper.Instance.AppTitle}.  {SessionStateHelper.Instance.CurrentScale.Description}. SQL: {SessionStateHelper.Instance.SqlViewModel.PublishDescription}." +
                        $"  {LocalizationCore.Scales.ProgramLoad}");
                    break;
                case ShareEnums.ProgramState.IsRun:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle,
                        $@"{AppVersionHelper.Instance.AppTitle}.  {SessionStateHelper.Instance.CurrentScale.Description}. SQL: {SessionStateHelper.Instance.SqlViewModel.PublishDescription}.");
                    break;
                case ShareEnums.ProgramState.IsExit:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle,
                        $@"{AppVersionHelper.Instance.AppTitle}.  {SessionStateHelper.Instance.CurrentScale.Description}. SQL: {SessionStateHelper.Instance.SqlViewModel.PublishDescription}." +
                        $"  {LocalizationCore.Scales.ProgramExit}");
                    break;
            }
            MDSoft.WinFormsUtils.InvokeControl.SetBackColor(FieldTitle, Color.IndianRed);
        }

        private void RequestProductDate()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(LabelProductDate,
                $"{LocalizationCore.Scales.FieldTime}: {DateTime.Now:HH:mm:ss}");
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldProductDate,
                $"{LocalizationCore.Scales.FieldDate}: {SessionStateHelper.Instance.ProductDate:dd.MM.yyyy}");
        }

        private void RequestPlu()
        {
            if (CurrentPlu == null)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu, LocalizationCore.Scales.Plu);
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

        private void RequestKneading()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(LabelKneading, $"{LocalizationCore.Scales.FieldKneading}");
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldKneading, $"{SessionStateHelper.Instance.WeighingSettings.Kneading}");
        }

        public new void Close()
        {
            base.Close();
        }

        public new void ReleaseManaged()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldTitle, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldSscc, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelProductDate, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldProductDate, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelKneading, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldKneading, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldPlu, false);
            
            base.ReleaseManaged();
        }

        public new void ReleaseUnmanaged()
        {
            base.ReleaseUnmanaged();
        }

        #endregion
    }
}
