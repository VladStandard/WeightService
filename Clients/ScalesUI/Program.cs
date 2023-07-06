// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI;

internal static class Program
{
    private static WsAppVersionHelper AppVersion => WsAppVersionHelper.Instance;
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private static WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;

    [STAThread]
    internal static void Main()
    {
        // В первую очередь.
        System.Windows.Forms.Application.EnableVisualStyles();
        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        // Проверить каталог и файлы локализации.
        WsLocalizationUtils.CheckDirectoryWithFiles();
        // Настройка.
        AppVersion.Setup(Assembly.GetExecutingAssembly(), LabelSession.Localization.LabelPrint.App);
        ContextManager.SetupJsonScales(Directory.GetCurrentDirectory(), typeof(Program).Assembly.GetName().Name);
        // Лог.
        StringBuilder log = new();
        log.AppendLine(WsLocaleCore.LabelPrint.RegistrationIsComplete);
        log.AppendLine(WsLocaleCore.LabelPrint.RegistrationSuccess(LabelSession.DeviceName, LabelSession.DeviceScaleFk.Scale.Description));
        log.AppendLine($"{WsLocaleCore.LabelPrint.ClickOnceIntallDirectory}: {WsAssemblyUtils.GetClickOnceNetworkInstallDirectory()}");
        ContextManager.ContextItem.SaveLogInformation(log);
        // Режим работы.
        WsDebugHelper.Instance.IsSkipDialogs = false;
        // Запуск.
        System.Windows.Forms.Application.Run(new WsMainForm());
    }
}