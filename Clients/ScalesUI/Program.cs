// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
            // SetCompatibleTextRenderingDefault is the first.
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Setup.
            AppVersion.Setup(Assembly.GetExecutingAssembly(), LocaleCore.Scales.AppTitle);
            ContextManager.SetupJsonScales(Directory.GetCurrentDirectory(), typeof(Program).Assembly.GetName().Name);

            // User
            WsUserSessionHelper.Instance.SetMain();
            if (WsUserSessionHelper.Instance.DeviceScaleFk.IsNew)
            {
                string message = LocaleCore.Scales.RegistrationWarningScaleNotFound(WsUserSessionHelper.Instance.DeviceName);
                WpfUtils.ShowNewRegistration(message + Environment.NewLine + Environment.NewLine + LocaleCore.Scales.CommunicateWithAdmin);
                ContextManager.ContextItem.SaveLogError(new Exception(message));
                System.Windows.Forms.Application.Exit();
                return;
            }

            // Mutex for check run app.
            _ = new Mutex(true, System.Windows.Forms.Application.ProductName, out bool createdNew);
            if (!createdNew)
            {
                string message = $"{LocaleCore.Strings.Application} {System.Windows.Forms.Application.ProductName} {LocaleCore.Scales.AlreadyRunning}!";
                WpfUtils.ShowNewRegistration(message);
                ContextManager.ContextItem.SaveLogError(new Exception(message));
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                ContextManager.ContextItem.SaveLogInformation(
                    LocaleCore.Scales.RegistrationSuccess(WsUserSessionHelper.Instance.DeviceName, WsUserSessionHelper.Instance.DeviceScaleFk.Scale.Description));
                // Run app.
                System.Windows.Forms.Application.Run(new MainForm());
            }
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, true, true);
        }
    }
}