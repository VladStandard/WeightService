// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI;

internal static class Program
{
    private static AppVersionHelper AppVersion => AppVersionHelper.Instance;
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private static WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;

    [STAThread]
    internal static void Main()
    {
        // В первую очередь.
        System.Windows.Forms.Application.EnableVisualStyles();
        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        // Настройка.
        AppVersion.Setup(Assembly.GetExecutingAssembly(), LocaleCore.Scales.AppTitle);
        ContextManager.SetupJsonScales(Directory.GetCurrentDirectory(), typeof(Program).Assembly.GetName().Name);
        // Запуск.
        ContextManager.ContextItem.SaveLogInformation(
            LocaleCore.Scales.RegistrationSuccess(LabelSession.DeviceName, LabelSession.DeviceScaleFk.Scale.Description));
        System.Windows.Forms.Application.Run(new WsMainForm());
    }
}