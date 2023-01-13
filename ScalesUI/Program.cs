// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI;

internal static class Program
{
    #region Public and private fields and properties

    private static AppVersionHelper AppVersion { get; } = AppVersionHelper.Instance;
    private static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;

    #endregion

    #region Public and private methods

    [STAThread]
    internal static void Main()
    {
        try
        {
            // Setup.
            AppVersion.Setup(Assembly.GetExecutingAssembly(), LocaleCore.Scales.AppTitle);
            //FileLogHelper.Instance.FileName = SqlUtils.FilePathLog;
            JsonSettingsHelper.Instance.SetupScales(Directory.GetCurrentDirectory(), typeof(Program).Assembly.GetName().Name);

            // User
            UserSessionHelper.Instance.SetMain();
            if (UserSessionHelper.Instance.DeviceScaleFk.IsNew)
            {
                string message = LocaleCore.Scales.RegistrationWarningScaleNotFound(UserSessionHelper.Instance.DeviceName);
                WpfUtils.ShowNewRegistration(message + Environment.NewLine + Environment.NewLine + LocaleCore.Scales.CommunicateWithAdmin);
                DataAccess.LogError(new Exception(message), UserSessionHelper.Instance.DeviceName, nameof(ScalesUI));
                System.Windows.Forms.Application.Exit();
                return;
            }

            // Mutex.
            _ = new Mutex(true, System.Windows.Forms.Application.ProductName, out bool createdNew);
            if (!createdNew)
            {
                string message = $"{LocaleCore.Strings.Application} {System.Windows.Forms.Application.ProductName} {LocaleCore.Scales.AlreadyRunning}!";
                WpfUtils.ShowNewRegistration(message);
                DataAccess.LogError(new Exception(message), UserSessionHelper.Instance.DeviceName, nameof(ScalesUI));
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                DataAccess.LogInformation(
                    LocaleCore.Scales.RegistrationSuccess(UserSessionHelper.Instance.DeviceName, UserSessionHelper.Instance.DeviceScaleFk.Scale.Description),
                    UserSessionHelper.Instance.DeviceName, nameof(ScalesUI));
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                System.Windows.Forms.Application.Run(new MainForm());
            }
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, true, true, true);
        }
    }

    #endregion
}