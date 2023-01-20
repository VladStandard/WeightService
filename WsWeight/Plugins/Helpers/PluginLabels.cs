// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsWeight.WinForms.Utils;

namespace WsWeight.Plugins.Helpers;

public class PluginLabels : PluginHelperBase
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
    private Label FieldPrintMain { get; set; }
    private Label FieldPrintShipping { get; set; }
    private Label FieldMassa { get; set; }
    private PluginManagerHelper Plugin => PluginManagerHelper.Instance;
    private DebugHelper Debug => DebugHelper.Instance;

    #endregion

    #region Constructor and destructor

    public PluginLabels()
    {
        TskType = TaskType.TaskLabel;
    }

    #endregion

    #region Public and private methods

    public void Init(ConfigModel configReopen, ConfigModel configRequest, ConfigModel configResponse,
        Label fieldTitle, Label fieldPlu, Label fieldSscc, Label fieldProductDate,
        Label fieldKneading, Button buttonDevice, Button buttonPackage, Button buttonKneading, Button buttonMore, Button buttonNewPallet, Button buttonPlu,
        Button buttonPrint, Button buttonScalesInit, Button buttonScalesTerminal, 
        Label fieldPrintMainManager, Label fieldPrintShippingManager, Label fieldMassaManager)
    {
        base.Init();
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
        ActionUtils.ActionTryCatch(() => {
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
            FieldPrintMain = fieldPrintMainManager;
            FieldPrintShipping = fieldPrintShippingManager;
            FieldMassa = fieldMassaManager;
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldTitle, AppVersionHelper.Instance.AppTitle);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPlu, LocaleCore.Scales.Plu);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldSscc, LocaleCore.Scales.FieldSscc);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldProductDate, LocaleCore.Scales.FieldDate);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldKneading, string.Empty);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrintMain, LocaleCore.Print.PrintManager);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrintShipping, LocaleCore.Print.PrintManager);
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassa, LocaleCore.Scales.MassaManager);
        });
    }

    public override void Execute()
    {
        base.Execute();
        ReopenItem.ExecuteInfinity(OpenTitle);
        RequestItem.ExecuteInfinity(() =>
        {
            RequestProductDate();
            RequestPlu();
            RequestKneading();
            RequestMassaDevice();
        });
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
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrintMain,
            $"{Plugin.PrintMain.ReopenCounter} | {Plugin.PrintMain.RequestCounter} | {Plugin.PrintMain.ResponseCounter}");
        if (UserSessionHelper.Instance.Scale.IsShipping)
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrintMain,
                $"{Plugin.PrintShipping.ReopenCounter} | {Plugin.PrintShipping.RequestCounter} | {Plugin.PrintShipping.ResponseCounter}");

        // FieldMassaManager.
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldMassa,
            $"{Plugin.Massa.ReopenCounter} | {Plugin.Massa.RequestCounter} | {Plugin.Massa.ReopenCounter}");
    }

    public override void Close()
    {
        base.Close();
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldTitle, false);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldSscc, false);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldProductDate, false);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldKneading, false);
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

        if (Debug.IsDebug && !FieldPrintMain.Visible)
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldPrintMain, true);
        if (UserSessionHelper.Instance.Scale.IsShipping)
            if (Debug.IsDebug && !FieldPrintShipping.Visible)
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldPrintShipping, true);
        if (Debug.IsDebug && !FieldMassa.Visible)
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldMassa, true);
    }

    #endregion
}