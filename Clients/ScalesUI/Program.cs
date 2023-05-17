// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLabelCore.Utils;

namespace ScalesUI;

internal static class Program
{
    private static AppVersionHelper AppVersion => AppVersionHelper.Instance;
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;

    [STAThread]
    internal static void Main()
    {
        try
        {
            // Это должно быть выполнено в первую очередь.
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Настройка.
            AppVersion.Setup(Assembly.GetExecutingAssembly(), LocaleCore.Scales.AppTitle);
            ContextManager.SetupJsonScales(Directory.GetCurrentDirectory(), typeof(Program).Assembly.GetName().Name);

            // Линия.
            WsUserSessionHelper.Instance.SetMain();
            if (WsUserSessionHelper.Instance.DeviceScaleFk.IsNew)
            {
                string message = LocaleCore.Scales.RegistrationWarningScaleNotFound(WsUserSessionHelper.Instance.DeviceName);
                WsWpfUtils.ShowNewRegistration(message + Environment.NewLine + Environment.NewLine + LocaleCore.Scales.CommunicateWithAdmin);
                ContextManager.ContextItem.SaveLogError(new Exception(message));
                System.Windows.Forms.Application.Exit();
                return;
            }

            // Проверка повторного запуска.
            _ = new Mutex(true, System.Windows.Forms.Application.ProductName, out bool createdNew);
            if (!createdNew)
            {
                string message = $"{LocaleCore.Strings.Application} {System.Windows.Forms.Application.ProductName} {LocaleCore.Scales.AlreadyRunning}!";
                WsWpfUtils.ShowNewRegistration(message);
                ContextManager.ContextItem.SaveLogWarning(message);
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                ContextManager.ContextItem.SaveLogInformation(
                    LocaleCore.Scales.RegistrationSuccess(WsUserSessionHelper.Instance.DeviceName, WsUserSessionHelper.Instance.DeviceScaleFk.Scale.Description));
                // Запуск.
                System.Windows.Forms.Application.Run(new MainForm());
            }
        }
        catch (Exception ex)
        {
            WsWpfUtils.CatchException(ex, true, true);
        }
    }
}