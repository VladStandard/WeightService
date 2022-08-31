// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Files;
using DataCore.Localizations;
using DataCore.Protocols;
using DataCore.Settings;
using DataCore.Sql;
using DataCore.Sql.TableScaleModels;
using ScalesUI.Forms;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using WeightCore.Gui;

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
        MainInside();
    }

    private static void MainInside([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            // Setup.
            AppVersion.Setup(Assembly.GetExecutingAssembly());
            FileLogHelper.Instance.FileName = SqlUtils.FilePathLog;
            DataAccess.JsonControl.SetupForScales(Directory.GetCurrentDirectory());

            // Host.
            string hostName = NetUtils.GetLocalHostName(false);
            HostModel host = SqlUtils.GetHost(hostName);
            if (host.Identity.Id == 0)
            {
                GuiUtils.WpfForm.ShowNewHostSaveInDb(hostName, NetUtils.GetLocalIpAddress(), NetUtils.GetLocalMacAddress());
                host = SqlUtils.GetHost(hostName);
            }
            if (host.Identity.Id == 0)
            {
                string message = LocaleCore.Scales.RegistrationWarningHostNotFound(hostName);
                GuiUtils.WpfForm.ShowNewRegistration(message + Environment.NewLine + Environment.NewLine + LocaleCore.Scales.CommunicateWithAdmin);
                //DataAccess.Log.LogError(new Exception(message), hostName, nameof(ScalesUI));
                Application.Exit();
                return;
            }

            // Scale.
            ScaleModel scale = SqlUtils.GetScaleFromHost(host.Identity.Id);
            if (scale.Identity.Id == 0)
            {
                string message = LocaleCore.Scales.RegistrationWarningScaleNotFound(hostName);
                GuiUtils.WpfForm.ShowNewRegistration(message + Environment.NewLine + Environment.NewLine + LocaleCore.Scales.CommunicateWithAdmin);
                DataAccess.Log.LogError(new Exception(message), hostName, nameof(ScalesUI));
                Application.Exit();
                return;
            }

            // Mutex.
            _ = new Mutex(true, Application.ProductName, out bool createdNew);
            if (!createdNew)
            {
                string message = $"{LocaleCore.Strings.Application} {Application.ProductName} {LocaleCore.Scales.AlreadyRunning}!";
                GuiUtils.WpfForm.ShowNewRegistration(message);
                DataAccess.Log.LogError(new Exception(message), hostName, nameof(ScalesUI));
                Application.Exit();
            }
            else
            {
                DataAccess.Log.LogInformation(LocaleCore.Scales.RegistrationSuccess(host.Name, scale.Description), 
                    hostName, nameof(ScalesUI));
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
        catch (Exception ex)
        {
            GuiUtils.WpfForm.CatchException(null!, ex, false, true, filePath, lineNumber, memberName);
            throw new(ex.Message);
        }
    }

    #endregion
}
