// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;
using System.Windows.Forms;
using DataCore.Enums;
using DataCore.Settings.Helpers;
using WsLocalization.Models;
using WsWeight.Helpers;
using WsWeight.Wpf.Utils;

namespace WsWeight.Managers;

public class ManagerLabels : ManagerBase
{
    #region Public and private fields and properties

    private Button ButtonDevice { get; set; }
    private Button ButtonPackage { get; set; }
    private Button ButtonKneading { get; set; }
    private Button ButtonMore { get; set; }
    private Button ButtonNewPallet { get; set; }
    private Button ButtonPlu { get; set; }
    private Button ButtonPrint { get; set; }
    private Button ButtonScalesInit { get; set; }
    private Button ButtonScalesTerminal { get; set; }
    private Label FieldKneading { get; set; }
    private Label FieldPlu { get; set; }
    private Label FieldProductDate { get; set; }
    private Label FieldSscc { get; set; }
    private Label FieldTitle { get; set; }
    private Label FieldPrintMainManager { get; set; }
    private Label FieldPrintShippingManager { get; set; }
    private Label FieldMassaManager { get; set; }
    private DebugHelper Debug { get; } = DebugHelper.Instance;

    #endregion

    #region Constructor and destructor

    public ManagerLabels() : base()
    {
        Init(Close, ReleaseManaged, ReleaseUnmanaged);
    }

    #endregion

    #region Public and private methods

    public void Init(Label fieldTitle, Label fieldPlu, Label fieldSscc, Label fieldProductDate,
        Label fieldKneading, Button buttonDevice, Button buttonPackage, Button buttonKneading, Button buttonMore, Button buttonNewPallet, Button buttonPlu,
        Button buttonPrint, Button buttonScalesInit, Button buttonScalesTerminal, 
        Label fieldPrintMainManager, Label fieldPrintShippingManager, Label fieldMassaManager)
    {
        try
        {
            Init(TaskType.TaskLabel,
                () =>
                {
                    FieldTitle = fieldTitle;
                    FieldPlu = fieldPlu;
                    FieldSscc = fieldSscc;
                    FieldProductDate = fieldProductDate;
                    FieldKneading = fieldKneading;
                    ButtonDevice = buttonDevice;
                    ButtonPackage = buttonPackage;
                    ButtonKneading = buttonKneading;
                    ButtonMore = buttonMore;
                    ButtonNewPallet = buttonNewPallet;
                    ButtonPlu = buttonPlu;
                    ButtonPrint = buttonPrint;
                    ButtonScalesInit = buttonScalesInit;
                    ButtonScalesTerminal = buttonScalesTerminal;
                    FieldPrintMainManager = fieldPrintMainManager;
                    FieldPrintShippingManager = fieldPrintShippingManager;
                    FieldMassaManager = fieldMassaManager;

                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle, AppVersionHelper.Instance.AppTitle);
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu, LocaleCore.Scales.Plu);
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldSscc, LocaleCore.Scales.FieldSscc);
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldProductDate, LocaleCore.Scales.FieldDate);
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldKneading, string.Empty);
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrintMainManager, LocaleCore.Print.PrintManager);
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrintShippingManager, LocaleCore.Print.PrintManager);
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaManager, LocaleCore.Scales.MassaManager);
                },
                new(waitReopen: 0_250, waitRequest: 0_250, waitResponse: 0_250, waitClose: 0_250, waitException: 0_250));
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex);
        }
    }

    public new void Open()
    {
        try
        {
            Open(OpenTitle,
                () =>
                {
                    RequestProductDate();
                    RequestPlu();
                    RequestKneading();
                    RequestMassaDevice();
                },
                null);
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex);
        }
    }

    private void OpenTitle()
    {
        MDSoft.WinFormsUtils.InvokeControl.SetText(
            FieldTitle, AppVersionHelper.Instance.AppTitle + //$". {UserSessionHelper.Instance.Scale.Description}" +
            $". {UserSessionHelper.Instance.PublishDescription}.");
        MDSoft.WinFormsUtils.InvokeControl.SetBackColor(FieldTitle,
            UserSessionHelper.Instance.PublishType == PublishType.Unknown ? Color.IndianRed : Color.Transparent);
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldSscc, $"{LocaleCore.Scales.FieldSscc}: {UserSessionHelper.Instance.ProductSeries.Sscc.Sscc}");
    }

    private void RequestProductDate()
    {
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldProductDate, $"{UserSessionHelper.Instance.ProductDate:dd.MM.yyyy}");
    }

    private void RequestPlu()
    {
        if (UserSessionHelper.Instance.PluScale.IsNew)
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu, LocaleCore.Scales.Plu);
        }
        else
        {
            if (UserSessionHelper.Instance.PluScale.Plu.IsCheckWeight)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu,
                    $"{LocaleCore.Scales.PluWeight}: " +
                    $"{UserSessionHelper.Instance.PluScale.Plu.Number} | {UserSessionHelper.Instance.PluScale.Plu.Name}");
            }
            else
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu,
                    $"{LocaleCore.Scales.PluCount}: " +
                    $"{UserSessionHelper.Instance.PluScale.Plu.Number} | {UserSessionHelper.Instance.PluScale.Plu.Name}");
            }
        }
    }

    private void RequestKneading()
    {
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldKneading, $"{UserSessionHelper.Instance.WeighingSettings.Kneading}");
    }

    private void RequestMassaDevice()
    {
        // FieldPrintManager.
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrintMainManager,
            $"{ManagerControllerHelper.Instance.PrintMain.ReopenCount} | " +
            $"{ManagerControllerHelper.Instance.PrintMain.RequestCount} | " +
            $"{ManagerControllerHelper.Instance.PrintMain.ResponseCount}"
        );
        if (UserSessionHelper.Instance.Scale.IsShipping)
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrintMainManager,
                $"{ManagerControllerHelper.Instance.PrintMain.ReopenCount} | " +
                $"{ManagerControllerHelper.Instance.PrintMain.RequestCount} | " +
                $"{ManagerControllerHelper.Instance.PrintMain.ResponseCount}"
            );

        // FieldMassaManager.
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassaManager,
            $"{ManagerControllerHelper.Instance.Massa.ReopenCount} | " +
            $"{ManagerControllerHelper.Instance.Massa.RequestCount} | " +
            $"{ManagerControllerHelper.Instance.Massa.ResponseCount}"
        );
    }

    public new void Close()
    {
        base.Close();
    }

    public new void ReleaseManaged()
    {
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldTitle, false);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldSscc, false);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldProductDate, false);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldKneading, false);

        base.ReleaseManaged();
    }

    public new void ReleaseUnmanaged()
    {
        base.ReleaseUnmanaged();
    }

    public void SetControlsVisible()
    {
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldTitle, true);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldSscc, true);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldProductDate, true);
        if (UserSessionHelper.Instance.Scale.IsKneading)
        {
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldKneading, true);
        }

        MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonDevice, true);
        MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonPackage, true);
        MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonKneading, true);
        MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonMore, true);
        MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonNewPallet, true);
        MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonPlu, true);
        MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonPrint, true);
        MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonScalesInit, true);
        MDSoft.WinFormsUtils.InvokeControl.SetEnabled(ButtonScalesTerminal, true);

        MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonDevice, true);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonPackage, true);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonKneading, true);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonMore, true);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonNewPallet, true);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonPlu, true);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonPrint, true);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonScalesInit, true);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(ButtonScalesTerminal, true);

        if (Debug.IsDebug && !FieldPrintMainManager.Visible)
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldPrintMainManager, true);
        if (UserSessionHelper.Instance.Scale.IsShipping)
            if (Debug.IsDebug && !FieldPrintShippingManager.Visible)
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldPrintShippingManager, true);
        if (Debug.IsDebug && !FieldMassaManager.Visible)
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassaManager, true);
    }

    #endregion
}