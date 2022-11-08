// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Files;

public class JsonSettingsHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static JsonSettingsHelper _instance;
#pragma warning restore CS8618
    public static JsonSettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public bool IsRemote { get; set; }
    private JsonSettingsModel? _local;
    public JsonSettingsModel Local
    {
        get
        {
            if (_local is null)
                throw new ArgumentNullException(nameof(Local));

            _local.CheckProperties(true);
            return _local;
        }
        private set => _local = value;
    }

    private JsonSettingsModel? _remote;
    public JsonSettingsModel Remote
    {
        get
        {
            if (_remote is null)
                throw new ArgumentNullException(nameof(Remote));

            _remote.CheckProperties(true);
            return _remote;
        }
        private set => _remote = value;
    }

    private AppVersionHelper AppVersion { get; } = AppVersionHelper.Instance;
    private string _remoteDir;
    public string RemoteDir
    {
	    get
	    {
		    if (!string.IsNullOrEmpty(_remoteDir) && Directory.Exists(_remoteDir))
			    return _remoteDir;
		    string tempDir = @"\\palych\Install\VSSoft\appsettings\";
		    if (Directory.Exists(tempDir))
			    _remoteDir = tempDir;
		    else
		    {
			    tempDir = @"h:\Install\VSSoft\appsettings\";
			    if (Directory.Exists(tempDir))
				    _remoteDir = tempDir;
		    }
		    return _remoteDir;
	    }
    }

    private string FileName =>
#if DEBUG
	    FileNameDebug;
#else
        FileNameRelease;
#endif
    public string FileNameDebug => "appsettings.Debug.json";
    public string FileNameRelease => "appsettings.Release.json";
    public string ExceptionFileName(string localDir) => Path.Combine(localDir, $"{FileName}.log");
    public const string BlazorSubDir =
#if DEBUG
	    @"bin\x64\Debug\net6.0\";
#else
        @"bin\x64\Release\net6.0\";
#endif

	public JsonSettingsHelper()
	{
		_remoteDir = string.Empty;
		//FileLogHelper.Instance.Recreate();
	}

	#endregion

	#region Public and private methods

	public void SetupForBlazorApp(string localDir, string deviceName, string appName)
	{
		try
		{
			AppVersion.Setup(Assembly.GetExecutingAssembly());
			string subDir = Path.Combine(localDir, BlazorSubDir);
			if (Directory.Exists(subDir))
			{
				// Local folder.
				CheckUpdates(subDir);
				if (!Setup(subDir, false, ""))
					throw new(LocaleCore.System.JsonSettingsLocalFileException);
			}
			else
			{
				// IIS publish folder.
				CheckUpdates(localDir);
				if (!Setup(localDir, false, ""))
					throw new(LocaleCore.System.JsonSettingsLocalFileException);
			}

			DataAccessHelper.Instance.SetSessionFactory();
			DataAccessHelper.Instance.SetupLog(deviceName, appName);
			DataAccessHelper.Instance.LogInformation(LocaleCore.DeviceControl.WebAppIsStarted);
		}
		catch (Exception ex)
		{
			DataAccessHelper.Instance.LogToFile(ExceptionFileName(localDir), ex.Message);
			if (ex.InnerException is not null)
				DataAccessHelper.Instance.LogToFile(ExceptionFileName(localDir), ex.InnerException.Message);
		}
	}

	private void SetupForTests(string localDir, string deviceName, string appName, string fileName)
	{
		CheckUpdates(localDir);

		if (!Setup(localDir, false, fileName))
			throw new(LocaleCore.System.JsonSettingsLocalFileException);
		
		DataAccessHelper.Instance.SetSessionFactory();
		DataAccessHelper.Instance.SetupLog(deviceName, appName);
	}

	public void SetupForTestsDebug(string localDir, string deviceName, string appName) =>
		SetupForTests(localDir, deviceName, appName, FileNameDebug);

	public void SetupForTestsRelease(string localDir, string deviceName, string appName) =>
		SetupForTests(localDir, deviceName, appName, FileNameRelease);

	public void SetupForScales(string localDir)
	{
		CheckUpdates(localDir);

		if (!Setup(localDir, false, ""))
		{
			//if (isFileLog)
			//    FileLog.WriteMessage(LocaleCore.System.JsonSettingsLocalFileException);
			throw new(LocaleCore.System.JsonSettingsLocalFileException);
		}
		
		DataAccessHelper.Instance.SetSessionFactory();
	}

	private bool Setup(string dir, bool isRemote, string fileName)
	{
		var file = Path.Combine(dir, !string.IsNullOrEmpty(fileName) ? fileName : FileName);
		if (!File.Exists(file))
		{
			throw new(LocaleCore.System.JsonSettingsFileNotFound(file));
		}

		using StreamReader streamReader = File.OpenText(file);
		JsonSerializer serializer = new();
		object? jsonObject = (JsonSettingsModel?)serializer.Deserialize(streamReader, typeof(JsonSettingsModel));
		if (jsonObject is JsonSettingsModel jsonSettings)
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new();
			sqlConnectionStringBuilder["Data Source"] = jsonSettings.Sql.DataSource;
			sqlConnectionStringBuilder["Initial Catalog"] = jsonSettings.Sql.InitialCatalog;
			sqlConnectionStringBuilder["Persist Security Info"] = jsonSettings.Sql.PersistSecurityInfo;
			sqlConnectionStringBuilder["User ID"] = jsonSettings.Sql.UserId;
			sqlConnectionStringBuilder["Password"] = jsonSettings.Sql.Password;
			//sqlConnectionStringBuilder["Encrypt"] = jsonSettings.Sql.Encrypt;
			sqlConnectionStringBuilder["Connect Timeout"] = jsonSettings.Sql.ConnectTimeout;
			sqlConnectionStringBuilder["TrustServerCertificate"] = jsonSettings.Sql.TrustServerCertificate;

			switch (isRemote)
			{
				case false:
					Local = jsonSettings;
					Local.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
					break;
				default:
					Remote = jsonSettings;
					Remote.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
					break;
			}
			IsRemote = isRemote;
			//DataAccessHelper.Instance.SetupSessionFactory();
		}
		return jsonObject is not null;
	}

	private void CheckUpdates(string localDir)
	{
		if (!Directory.Exists(RemoteDir))
		{
			throw new(LocaleCore.System.JsonSettingsRemoteFolderNotFound);
		}
		string remoteFile = Path.Combine(RemoteDir, FileName);
		if (!File.Exists(remoteFile))
		{
			throw new(LocaleCore.System.JsonSettingsRemoteFileNotFound);
		}

		Setup(RemoteDir, true, "");

		ushort version = GetLocalVersion(localDir);
		if (version == 0 || version < Remote.Version)
		{
			string localFile = Path.Combine(localDir, FileName);
			if (File.Exists(localFile))
				File.Delete(localFile);

			using StreamReader streamReader = File.OpenText(remoteFile);
			string content = streamReader.ReadToEnd();
			streamReader.Close();
			streamReader.Dispose();
			if (string.IsNullOrEmpty(content))
			{
				//if (isFileLog)
				//    FileLog.WriteMessage(LocaleCore.System.JsonSettingsFileIsEmpty(remoteFile));
				throw new(LocaleCore.System.JsonSettingsFileIsEmpty(remoteFile));
			}

			using StreamWriter streamWriter = File.CreateText(Path.Combine(localDir, FileName));
			streamWriter.Write(content);
			streamWriter.Close();
			streamWriter.Dispose();
		}
	}

	private ushort GetLocalVersion(string localDir)
	{
		string localFile = Path.Combine(localDir, FileName);
		if (!File.Exists(localFile))
			return 0;

		using StreamReader streamReader = File.OpenText(localFile);
		string content = streamReader.ReadToEnd();
		streamReader.Close();
		streamReader.Dispose();
		if (string.IsNullOrEmpty(content))
		{
			//throw new Exception(LocaleCore.System.JsonSettingsFileIsEmpty(localFile));
			return 0;
		}

		if (content.Contains(nameof(JsonSettingsModel.Version)))
		{
			string[] lines = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
			foreach (string line in lines)
			{
				if (line.Contains($"\"{nameof(JsonSettingsModel.Version)}\""))
				{
					int posStart = line.IndexOf(": ") + 2;
					int posEnd = line.IndexOf(",");
					int length = posEnd - posStart;
					string strVersion = line.Substring(posStart, length);
					if (ushort.TryParse(strVersion, out ushort result))
						return result;
					//throw new Exception(LocaleCore.System.JsonSettingsParseVersionException(localFile));
				}
			}
		}
		return 0;
	}

	#endregion
}