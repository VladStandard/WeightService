// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Helpers;

namespace ScalesUI;

internal static class Program
{
    #region Public and private fields and properties

    private static AppVersionHelper AppVersion => AppVersionHelper.Instance;
    private static WsDataAccessHelper DataAccess => WsDataAccessHelper.Instance;

    #endregion

    #region Public and private methods

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
            WsJsonSettingsHelper.Instance.SetupScales(Directory.GetCurrentDirectory(), typeof(Program).Assembly.GetName().Name);

            // User
            UserSessionHelper.Instance.SetMain();
            if (UserSessionHelper.Instance.DeviceScaleFk.IsNew)
            {
                string message = LocaleCore.Scales.RegistrationWarningScaleNotFound(UserSessionHelper.Instance.DeviceName);
                WpfUtils.ShowNewRegistration(message + Environment.NewLine + Environment.NewLine + LocaleCore.Scales.CommunicateWithAdmin);
                DataAccess.SaveLogError(new Exception(message));
                System.Windows.Forms.Application.Exit();
                return;
            }

            // Mutex for check run app.
            _ = new Mutex(true, System.Windows.Forms.Application.ProductName, out bool createdNew);
            if (!createdNew)
            {
                string message = $"{LocaleCore.Strings.Application} {System.Windows.Forms.Application.ProductName} {LocaleCore.Scales.AlreadyRunning}!";
                WpfUtils.ShowNewRegistration(message);
                DataAccess.SaveLogError(new Exception(message));
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                DataAccess.SaveLogInformation(
                    LocaleCore.Scales.RegistrationSuccess(UserSessionHelper.Instance.DeviceName, UserSessionHelper.Instance.DeviceScaleFk.Scale.Description));
                // Run app.
                System.Windows.Forms.Application.Run(new MainForm());
            }
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, true, true);
        }
    }

    #endregion
}