// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using DataCore.Helpers;
using DataCore.Models;

namespace WeightCore.Helpers;

/// <summary>
/// Settings helper.
/// </summary>
public class SettingsHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static SettingsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static SettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	public SettingsHelper()
	{
		CurrentLanguage = LangEnum.Russian;
		DirMain = string.Empty;
		DirDocs = string.Empty;
		DirDrivers = string.Empty;
		DirFonts = string.Empty;
		DirManuals = string.Empty;
	}

	#endregion

	#region Public and private fields, properties, constructor

	public AppHelper App { get; } = AppHelper.Instance;
	private CollectionsHelper Collections { get; } = CollectionsHelper.Instance;
	private ProcHelper Proc { get; } = ProcHelper.Instance;
	private RegHelper Reg { get; } = RegHelper.Instance;
	private WinInfoHelper WinInfo { get; } = WinInfoHelper.Instance;

	/// <summary>
	/// Main directory.
	/// </summary>
	public string DirMain { get; private set; }

	/// <summary>
	/// Documentations directory.
	/// </summary>
	public string DirDocs { get; private set; }

	/// <summary>
	/// Drivers directory.
	/// </summary>
	public string DirDrivers { get; private set; }

	/// <summary>
	/// Fonts directory.
	/// </summary>
	public string DirFonts { get; private set; }

	/// <summary>
	/// Manuals directory.
	/// </summary>
	public string DirManuals { get; private set; }

	/// <summary>
	/// Current language.
	/// </summary>
	private LangEnum CurrentLanguage { get; }

	/// <summary>
	/// Исходный каталог документации.
	/// </summary>
	public string DirSourceDocs { get; } = @"Docs\";

	/// <summary>
	/// Исходный каталог драйвера STM.
	/// </summary>
	public string DirSourceDrivers { get; } = @"..\..\Resources\Drivers\";

	/// <summary>
	/// Исходный каталог руководств.
	/// </summary>
	public string DirSourceManuals { get; } = @"..\..\Resources\Manuals\";

	/// <summary>
	/// Исходный каталог ПО.
	/// </summary>
	public string DirSource
	{
		get
		{
#if DEBUG
			return @"..\ScalesUI2\bin\Debug\";
#else
                return @"..\ScalesUI2\bin\Release\";
#endif
		}
	}

	/// <summary>
	/// Файл библиотеки ядра весовой платформы.
	/// </summary>
	/// <returns></returns>
	public string ScalesCoreDll =>
#if DEBUG
		@"..\ScalesCore\bin\Debug\ScalesCore.dll";
#else
            @"..\ScalesCore\bin\Release\ScalesCore.dll";
#endif

	/// <summary>
	/// Windows menu.
	/// </summary>
	public string MenuScalesUI { get; } = @"%ProgramMenu%\VladimirStandardCorp\ScalesUI";

	#endregion

	#region Public methods

	/// <summary>
	/// Настроить и проверить каталоги.
	/// </summary>
	/// <param name="installDir"></param>
	/// <param name="silentUI"></param>
	/// <param name="language"></param>
	/// <returns></returns>
	public bool SetupAndCheckDirs(string installDir, SilentUiEnum silentUI, LangEnum language)
	{
		if (string.IsNullOrEmpty(installDir))
			return false;

		DirDocs = string.Empty;
		DirDrivers = string.Empty;
		DirFonts = string.Empty;
		DirManuals = string.Empty;
		DirMain = installDir + (installDir.EndsWith(@"\") ? "" : @"\");

		if (!Directory.Exists(DirMain))
		{
			string message = language == LangEnum.English ? $@"Directory '{DirMain}' not exists!" : $@"Каталог '{DirMain}' не существует!";
			Console.WriteLine(message);
			if (silentUI == SilentUiEnum.False)
				MessageBox.Show(message);
			DirMain = string.Empty;
			return false;
		}

		Environment.CurrentDirectory = DirMain;
		DirDocs = DirMain + @"Docs";
		DirDrivers = DirMain + @"Drivers";
		DirFonts = DirMain + @"frx";
		DirManuals = DirMain + @"Manuals";

		return true;
	}

	/// <summary>
	/// Установить.
	/// </summary>
	public ResultEnum DirCreate()
	{
		try
		{
			// Создать каталоги и перенести файлы.
			DirCreateAndMoveFiles(DirDocs, Collections.Docs);
			DirCreateAndMoveFiles(DirManuals, Collections.Manuals);
			DirCreateAndMoveFiles(DirDrivers, Collections.DriversArchives);

			Console.WriteLine(@"Install complete.");
			return ResultEnum.Good;
		}
		catch (Exception ex)
		{
			Console.WriteLine(@"Install error: " + ex.Message);
		}
		return ResultEnum.Error;
	}

	/// <summary>
	/// Создать каталоги и перенести файлы.
	/// </summary>
	/// <param name="dir"></param>
	/// <param name="files"></param>
	public void DirCreateAndMoveFiles(string dir, Collection<string> files)
	{
		if (!Directory.Exists(dir))
		{
			Directory.CreateDirectory(dir);
			Console.WriteLine(@"Directory created.");
		}

		foreach (string file in files)
		{
			if (File.Exists(DirMain + @"\" + file))
			{
				File.Move(DirMain + @"\" + file, dir + @"\" + file);
				Console.WriteLine($@"File '{file}' moved to '{dir}'.");
			}
		}
	}

	/// <summary>
	/// Распаковать архивы драйверов.
	/// </summary>
	public void ExtractDrivers()
	{
		foreach (string arch in Collections.DriversArchives)
		{
			if (arch.EndsWith(".zip"))
			{
				if (!File.Exists(DirDrivers + @"\" + arch))
					return;
				// Распаковать архивы.
				ZipFile.ExtractToDirectory(DirDrivers + @"\" + arch, DirDrivers);
				// Удалить архивы.
				File.Delete(DirDrivers + @"\" + arch);
			}
		}
	}

	/// <summary>
	/// Установить драйвера.
	/// </summary>
	/// <param name="assembly"></param>
	public void InstallDrivers(Assembly assembly)
	{
		// Определить имя файла драйвера.
		string driverFileName;
		// Windows 8 - 10.
		if (WinInfo.MajorVersion == 6 && WinInfo.MinorVersion >= 2 || WinInfo.MajorVersion > 6)
		{
			driverFileName = Collections.GetDriverFileName(Environment.Is64BitOperatingSystem ? WinVersionEnum.Win10x64 : WinVersionEnum.Win10x32);
		}
		// Windows 7.
		//else if (_winInfo.MajorVersion == 6 && _winInfo.MinorVersion == 1)
		else
		{
			driverFileName = Collections.GetDriverFileName(Environment.Is64BitOperatingSystem ? WinVersionEnum.Win7x64 : WinVersionEnum.Win7x32);
		}

		// Проверить установку драйвера.
		if (Reg.SearchingSoftware(WinProviderEnum.Registry, "Virtual Comport Driver",
			    StringTemplateEnum.Equals).Vendor.
		    Equals("STMicroelectronics", StringComparison.InvariantCultureIgnoreCase))
			return;

		// Запустить установку драйвера.
		if (MessageBox.Show(@"Драйвер весов не обнаружен. Установить?", App.GetDescription(assembly), MessageBoxButtons.YesNo, MessageBoxIcon.Question,
			    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
		{
			//MessageBox.Show(DirDrivers + @"\" + driverFileName);
			Proc.Run(DirDrivers + @"\" + driverFileName, string.Empty, true, ProcessWindowStyle.Normal, true);
		}
	}

	/// <summary>
	/// Удалить.
	/// </summary>
	public ResultEnum DirClear()
	{
		try
		{
			// Документация.
			DirClear(DirDocs);
			// Драйвера.
			DirClear(DirDrivers);
			// Шрифты.
			DirClear(DirFonts);
			// Руководства.
			DirClear(DirManuals);

			Console.WriteLine(@"Uninstall complete.");
			return 0;
		}
		catch (Exception ex)
		{
			Console.WriteLine(@"Uninstall error: " + ex.Message);
		}
		return ResultEnum.Error;
	}

	/// <summary>
	/// Очистка и удаление каталога.
	/// </summary>
	/// <param name="dir"></param>
	public void DirClear(string dir)
	{
		if (Directory.Exists(dir))
		{
			foreach (string dirIn in Directory.GetDirectories(dir))
			{
				DirClear(dirIn);
			}
			foreach (string file in Directory.GetFiles(dir))
			{
				Console.WriteLine($@"File '{file}' deleted from '{dir}'.");
				File.Delete(file);
			}
			Directory.Delete(dir);
			Console.WriteLine($@"Directory '{dir}' cleared.");
		}
	}

	/// <summary>
	/// Полное имя конфигурационного файла в "C:\Program Files (x86)\".
	/// </summary>
	/// <returns></returns>
	public string GetScalesConfigFileName()
	{
		return $@"{DirMain}ScalesUI.exe.config";
	}

	#endregion
}
