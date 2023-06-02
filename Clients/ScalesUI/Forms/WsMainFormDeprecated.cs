// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Forms;

public partial class WsMainForm
{
    #region Public and private methods

    /// <summary>
    /// Возврат из контрола смены замеса.
    /// </summary>
    private void ReturnFromKneading()
    {
        using WsFormDigitsForm digitsForm = new() { InputValue = 0 };
        DialogResult result = digitsForm.ShowDialog(this);
        digitsForm.Close();
        if (result == DialogResult.OK)
            LabelSession.WeighingSettings.Kneading = (byte)digitsForm.InputValue;
        UserSession.PluginMassa.Execute();
    }

    ///// <summary>
    ///// Запросить проверку подключения принтера.
    ///// </summary>
    //private void ActionPreparePrintAskDevelopPrinters()
    //{
    //    if (Debug.IsSkipDialogs) return;
    //    if (Debug.IsRelease) return;

    //    // Навигация в контрол диалога Отмена/Да.
    //    WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl,
    //        LocaleCore.Print.QuestionPrintCheckAccess, true, WsEnumLogType.Question, WsEnumDialogType.CancelYes,
    //        new() { () => ActionPrintLabel(false), () => ActionPrintLabel(true) });
    //}

    private void FieldPrintManager_Click(object sender, EventArgs e)
    {
        //using WsWpfPageLoader wpfPageLoader = new(WsEnumPage.MessageBox, false, FormBorderStyle.FixedDialog, 22, 16, 16) { Width = 700, Height = 450 };
        //wpfPageLoader.Text = LocaleCore.Print.InfoCaption;
        //wpfPageLoader.MessageBoxViewModel.Caption = LocaleCore.Print.InfoCaption;
        //wpfPageLoader.MessageBoxViewModel.Message = GetPrintInfo(UserSession.PluginPrintMain, true);
        //if (LabelSession.Scale.IsShipping)
        //{
        //    wpfPageLoader.MessageBoxViewModel.Message += Environment.NewLine + Environment.NewLine +
        //        GetPrintInfo(UserSession.PluginPrintShipping, false);
        //    wpfPageLoader.Height = 700;
        //}
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.ButtonOkVisibility = Visibility.Visible;
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.ButtonCustomVisibility = Visibility.Visible;
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.ButtonCustomContent = LocaleCore.Print.ClearQueue;
        //DialogResult result = wpfPageLoader.ShowDialog(this);
        //wpfPageLoader.Close();
        //if (result == DialogResult.Retry)
        //{
        //    UserSession.PluginPrintMain.ClearPrintBuffer(1);
        //    if (LabelSession.Scale.IsShipping)
        //        UserSession.PluginPrintShipping.ClearPrintBuffer(1);
        //}
    }

    private void FieldTasks_Click(object sender, EventArgs e)
    {
        //string message = string.Empty;
        //foreach (ProcessThread thread in Process.GetCurrentProcess().Threads)
        //{
        //    message += $"{LocaleCore.Scales.ThreadId}: {thread.Id}. " +
        //        $"{LocaleCore.Scales.ThreadPriorityLevel}: {thread.PriorityLevel}. " +
        //        $"{LocaleCore.Scales.ThreadState}: {thread.ThreadState}. " +
        //        $"{LocaleCore.Scales.ThreadStartTime}: {thread.StartTime}. " + Environment.NewLine;
        //}
        //using WsWpfPageLoader wpfPageLoader = new(WsEnumPage.MessageBox, false, FormBorderStyle.FixedDialog,
        //    20, 14, 18, 0, 12)
        //{ Width = Width - 50, Height = Height - 50 };
        //wpfPageLoader.Text = $@"{LocaleCore.Scales.ThreadsCount}: {Process.GetCurrentProcess().Threads.Count}";
        //wpfPageLoader.MessageBoxViewModel.Message = message;
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.ButtonOkVisibility = Visibility.Visible;
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.Localization();
        //wpfPageLoader.ShowDialog(this);
        //wpfPageLoader.Close();
    }

    private void FieldSscc_Click(object sender, EventArgs e)
    {
        //using WsWpfPageLoader wpfPageLoader = new(WsEnumPage.MessageBox, false, FormBorderStyle.FixedDialog, 26, 20, 18) { Width = 700, Height = 400 };
        //wpfPageLoader.Text = LocaleCore.Scales.FieldSsccShort;
        //wpfPageLoader.MessageBoxViewModel.Caption = LocaleCore.Scales.FieldSscc;
        //wpfPageLoader.MessageBoxViewModel.Message =
        //    $"{LocaleCore.Scales.FieldSscc}: {UserSession.ProductSeries.Sscc.Sscc}" + Environment.NewLine +
        //    $"{LocaleCore.Scales.FieldSsccGln}: {UserSession.ProductSeries.Sscc.Gln}" + Environment.NewLine +
        //    $"{LocaleCore.Scales.FieldSsccUnitId}: {UserSession.ProductSeries.Sscc.UnitId}" + Environment.NewLine +
        //    $"{LocaleCore.Scales.FieldSsccUnitType}: {UserSession.ProductSeries.Sscc.UnitType}" + Environment.NewLine +
        //    $"{LocaleCore.Scales.Field•SsccSynonym}: {UserSession.ProductSeries.Sscc.SynonymSscc}" + Environment.NewLine +
        //    $"{LocaleCore.Scales.FieldSsccControlNumber}: {UserSession.ProductSeries.Sscc.Check}";
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.ButtonOkVisibility = Visibility.Visible;
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.Localization();
        //wpfPageLoader.ShowDialog(this);
        //wpfPageLoader.Close();
    }

    //private string GetPrintInfo(WsPluginPrintModel pluginPrint, bool isMain)
    //{
    //    string peeler = isMain
    //        ? LabelSession.PluginPrintMain.ZebraPeelerStatus : LabelSession.PluginPrintShipping.ZebraPeelerStatus;
    //    string printMode = isMain
    //        ? LabelSession.PluginPrintMain.GetZebraPrintMode() :
    //        LabelSession.PluginPrintShipping.GetZebraPrintMode();
    //    PrintBrand printBrand = isMain ? LabelSession.PluginPrintMain.PrintBrand : LabelSession.PluginPrintShipping.PrintBrand;
    //    MdWmiWinPrinterModel wmiPrinter = pluginPrint.TscWmiPrinter;
    //    return
    //        $"{LabelSession.WeighingSettings.GetPrintName(isMain, printBrand)}" + Environment.NewLine +
    //        $"{LocaleCore.Print.DeviceCommunication} ({pluginPrint.Printer.Ip}): {pluginPrint.Printer.PingStatus}" + Environment.NewLine +
    //        $"{LocaleCore.Print.PrinterStatus}: {pluginPrint.GetDeviceStatus()}" + Environment.NewLine +
    //        Environment.NewLine +
    //        $"{LocaleCore.Print.Name}: {wmiPrinter.Name}" + Environment.NewLine +
    //        $"{LocaleCore.Print.Driver}: {wmiPrinter.DriverName}" + Environment.NewLine +
    //        $"{LocaleCore.Print.Port}: {wmiPrinter.PortName}" + Environment.NewLine +
    //        $"{LocaleCore.Print.StateCode}: {wmiPrinter.PrinterState}" + Environment.NewLine +
    //        $"{LocaleCore.Print.StatusCode}: {wmiPrinter.PrinterStatus}" + Environment.NewLine +
    //        $"{LocaleCore.Print.Status}: {pluginPrint.GetPrinterStatusDescription(LocaleCore.Lang, wmiPrinter.PrinterStatus)}" + Environment.NewLine +
    //        $"{LocaleCore.Print.State} (ENG): {wmiPrinter.Status}" + Environment.NewLine +
    //        $"{LocaleCore.Print.State}: {MdWmiHelper.Instance.GetStatusDescription(
    //            LocaleCore.Lang == Lang.English ? MDSoft.Wmi.Enums.MdLang.English : MDSoft.Wmi.Enums.MdLang.Russian, wmiPrinter.Status)}" + Environment.NewLine +
    //        $"{LocaleCore.Print.SensorPeeler}: {peeler}" + Environment.NewLine +
    //        $"{LocaleCore.Print.Mode}: {printMode}" + Environment.NewLine;
    //}

    #endregion
}