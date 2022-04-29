// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Settings;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeightCore.Helpers;

namespace WeightCore.Managers
{
    public class ManagerLabels : ManagerBase
    {
        #region Public and private fields and properties

        private Button ButtonKneading { get; set; }
        private Button ButtonMore { get; set; }
        private Button ButtonNewPallet { get; set; }
        private Button ButtonOrder { get; set; }
        private Button ButtonPlu { get; set; }
        private Button ButtonPrint { get; set; }
        private Button ButtonScalesInit { get; set; }
        private Button ButtonScalesTerminal { get; set; }
        private PictureBox PictureBoxClose { get; set; }
        private ComboBox FieldLang { get; set; }
        private ComboBox FieldResolution { get; set; }
        private Label FieldKneading { get; set; }
        private Label FieldPlu { get; set; }
        private Label FieldProductDate { get; set; }
        private Label FieldSscc { get; set; }
        private Label FieldTitle { get; set; }
        private Label LabelKneading { get; set; }
        private Label LabelProductDate { get; set; }

        #endregion

        #region Constructor and destructor

        public ManagerLabels() : base()
        {
            Init(Close, ReleaseManaged, ReleaseUnmanaged);
        }

        #endregion

        #region Public and private methods

        public void Init(Label fieldTitle, Label fieldPlu, Label fieldSscc, Label labelProductDate, Label fieldProductDate, 
            Label labelKneading, Label fieldKneading, ComboBox fieldResolution, ComboBox fieldLang,
            Button buttonKneading, Button buttonMore, Button buttonNewPallet, Button buttonOrder, Button buttonPlu, 
            Button buttonPrint, Button buttonScalesInit, Button buttonScalesTerminal, PictureBox pictureBoxClose)
        {
            try
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
                        FieldResolution = fieldResolution;
                        FieldLang = fieldLang;
                        ButtonKneading = buttonKneading;
                        ButtonMore = buttonMore;
                        ButtonNewPallet = buttonNewPallet;
                        ButtonOrder = buttonOrder;
                        ButtonPlu = buttonPlu;
                        ButtonPrint = buttonPrint;
                        ButtonScalesInit = buttonScalesInit;
                        ButtonScalesTerminal = buttonScalesTerminal;
                        PictureBoxClose = pictureBoxClose;

                        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle, AppVersionHelper.Instance.AppName);
                        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu, LocaleCore.Scales.Plu);
                        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldSscc, LocaleCore.Scales.FieldSscc);
                        MDSoft.WinFormsUtils.InvokeControl.SetText(LabelProductDate, LocaleCore.Scales.FieldTime);
                        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldProductDate, LocaleCore.Scales.FieldDate);
                        MDSoft.WinFormsUtils.InvokeControl.SetText(LabelKneading, LocaleCore.Scales.FieldKneading);
                        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldKneading, string.Empty);
                    },
                    new(1_000, 0_500, 0_500, 0_050, 5_000));
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
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
            switch (UserSessionHelper.Instance.SqlViewModel.PublishType)
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
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldSscc, $"{LocaleCore.Scales.FieldSscc}: {UserSessionHelper.Instance.ProductSeries.Sscc.SSCC}");
        }

        private void SetTitleSwitchDev()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle, AppVersionHelper.Instance.AppName +
                $" SQL: {UserSessionHelper.Instance.SqlViewModel.PublishDescription}");
            MDSoft.WinFormsUtils.InvokeControl.SetBackColor(FieldTitle, Color.LightYellow);
        }

        private void SetTitleSwitchRelease()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle, AppVersionHelper.Instance.AppName);
            MDSoft.WinFormsUtils.InvokeControl.SetBackColor(FieldTitle, Color.LightGreen);
        }

        private void SetTitleSwitchDefault()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle,
                $@"{AppVersionHelper.Instance.AppTitle}.  {UserSessionHelper.Instance.Scale.Description}. SQL: {UserSessionHelper.Instance.SqlViewModel.PublishDescription}.");
            MDSoft.WinFormsUtils.InvokeControl.SetBackColor(FieldTitle, Color.IndianRed);
        }

        private void RequestProductDate()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(LabelProductDate,
                $"{LocaleCore.Scales.FieldTime}: {DateTime.Now:HH:mm:ss}");
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldProductDate,
                $"{LocaleCore.Scales.FieldDate}: {UserSessionHelper.Instance.ProductDate:dd.MM.yyyy}");
        }

        private void RequestPlu()
        {
            if (UserSessionHelper.Instance.Plu == null)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu, LocaleCore.Scales.Plu);
            }
            else
            {
                if (UserSessionHelper.Instance.Plu.IsCheckWeight == true)
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu,
                        $"{LocaleCore.Scales.PluWeight}: " +
                        $"{UserSessionHelper.Instance.Plu.PLU} | {UserSessionHelper.Instance.Plu.GoodsName}");
                }
                else
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu,
                        $"{LocaleCore.Scales.PluCount}: " +
                        $"{UserSessionHelper.Instance.Plu.PLU} | {UserSessionHelper.Instance.Plu.GoodsName}");

                }
            }
        }

        private void RequestKneading()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(LabelKneading, $"{LocaleCore.Scales.FieldKneading}");
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldKneading, $"{UserSessionHelper.Instance.WeighingSettings.Kneading}");
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
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(PictureBoxClose, false);
            
            base.ReleaseManaged();
        }

        public new void ReleaseUnmanaged()
        {
            base.ReleaseUnmanaged();
        }

        public void SetControlsVisible()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldTitle, true);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldPlu, true);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldSscc, true);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelProductDate, true);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldProductDate, true);
            if (UserSessionHelper.Instance.Scale?.IsKneading == true)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(LabelKneading, true);
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldKneading, true);
            }

            MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonKneading, true);
            MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonMore, true);
            MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonNewPallet, true);
            MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonOrder, true);
            MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonPlu, true);
            MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonPrint, true);
            MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonScalesInit, true);
            MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonScalesTerminal, true);
            
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonKneading, true);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonMore, true);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonNewPallet, true);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonOrder, false);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonPlu, true);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonPrint, true);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonScalesInit, true);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonScalesTerminal, true);

            MDSoft.WinFormsUtils.InvokeControl.SetVisible(PictureBoxClose, true);
            MDSoft.WinFormsUtils.InvokeControl.SetEnabled(PictureBoxClose, true);

            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldResolution, Debug.IsDebug);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldLang, Debug.IsDebug);
            MDSoft.WinFormsUtils.InvokeControl.SetEnabled(FieldResolution, Debug.IsDebug);
            MDSoft.WinFormsUtils.InvokeControl.SetEnabled(FieldLang, Debug.IsDebug);
        }

        #endregion
    }
}
