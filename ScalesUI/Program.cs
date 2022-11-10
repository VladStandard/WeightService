// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Files;
using DataCore.Localizations;
using DataCore.Protocols;
using DataCore.Settings;
using DataCore.Sql.Core;
using DataCore.Sql.TableScaleModels;
using ScalesUI.Forms;
using System;
using System.IO;
using System.Reflection;
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
#nullable enable
	internal static void Main()
	{
		try
		{
			// Setup.
			AppVersion.Setup(Assembly.GetExecutingAssembly());
			//FileLogHelper.Instance.FileName = SqlUtils.FilePathLog;
			JsonSettingsHelper.Instance.SetupForScales(Directory.GetCurrentDirectory());

			// Host.
			string deviceName = NetUtils.GetLocalDeviceName(false);
			DeviceModel device = DataAccess.GetItemDeviceNotNullable(deviceName);
			DeviceTypeFkModel? deviceTypeFk = DataAccess.GetItemDeviceTypeFkNullable(device);
			if (deviceTypeFk is null)
			{
				GuiUtils.WpfForm.ShowNewHostSaveInDb(deviceName, NetUtils.GetLocalIpAddress(), NetUtils.GetLocalMacAddress());
				deviceTypeFk = new() { Device = { Name = deviceName } };
			}
			if (deviceTypeFk.IdentityIsNew)
			{
				string message = LocaleCore.Scales.RegistrationWarningHostNotFound(deviceName);
				GuiUtils.WpfForm.ShowNewRegistration(message + Environment.NewLine + Environment.NewLine + LocaleCore.Scales.CommunicateWithAdmin);
				//DataAccess.LogError(new Exception(message), deviceName, nameof(ScalesUI));
				Application.Exit();
				return;
			}

			// Scale.
			ScaleModel scale = DataAccess.GetItemScaleNotNullable(deviceTypeFk.Device);
			if (scale.IdentityIsNew)
			{
				string message = LocaleCore.Scales.RegistrationWarningScaleNotFound(deviceName);
				GuiUtils.WpfForm.ShowNewRegistration(message + Environment.NewLine + Environment.NewLine + LocaleCore.Scales.CommunicateWithAdmin);
				DataAccess.LogError(new Exception(message), deviceName, nameof(ScalesUI));
				Application.Exit();
				return;
			}

			// Mutex.
			_ = new Mutex(true, Application.ProductName, out bool createdNew);
			if (!createdNew)
			{
				string message = $"{LocaleCore.Strings.Application} {Application.ProductName} {LocaleCore.Scales.AlreadyRunning}!";
				GuiUtils.WpfForm.ShowNewRegistration(message);
				DataAccess.LogError(new Exception(message), deviceName, nameof(ScalesUI));
				Application.Exit();
			}
			else
			{
				DataAccess.LogInformation(LocaleCore.Scales.RegistrationSuccess(deviceTypeFk.Device.Name, scale.Description),
					deviceName, nameof(ScalesUI));
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainForm());
			}
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(null!, ex, false);
			throw new(ex.Message);
		}
	}
#nullable disable

	#endregion
}
