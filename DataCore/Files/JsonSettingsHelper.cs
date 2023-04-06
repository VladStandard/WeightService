// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Helpers;
using DataCore.Settings.Helpers;
using DataCore.Sql.Core.Helpers;

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

    public bool IsRemote { get; private set; }
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
    private AppVersionHelper AppVersion => AppVersionHelper.Instance;
    private FileLoggerHelper FileLogger => FileLoggerHelper.Instance;
	private string _remoteDir;
	private string RemoteDir
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
    private string FileNameDevelopAleksandrov => "appsettings.DevelopAleksandrov.json";
    private string FileNameDevelopMorozov => "appsettings.DevelopMorozov.json";
    private string FileNameDevelopVs => "appsettings.DevelopVS.json";
    private string FileNameReleaseAleksandrov => "appsettings.ReleaseAleksandrov.json";
    private string FileNameReleaseMorozov => "appsettings.ReleaseMorozov.json";
    private string FileNameReleaseVs => "appsettings.ReleaseVS.json";
    private string JsonFileName =>
        DebugHelper.Instance.Config switch {
            WsConfiguration.DevelopAleksandrov => FileNameDevelopAleksandrov,
            WsConfiguration.DevelopMorozov => FileNameDevelopMorozov,
            WsConfiguration.DevelopVS => FileNameDevelopVs,
            WsConfiguration.ReleaseAleksandrov => FileNameReleaseAleksandrov,
            WsConfiguration.ReleaseMorozov => FileNameReleaseMorozov,
            WsConfiguration.ReleaseVS => FileNameReleaseVs,
            _ => FileNameDevelopVs,
        };
    private string BlazorSubDir => DebugHelper.Instance.IsDevelop ? @"bin\x64\Debug\net7.0\" : @"bin\x64\Release\net7.0\";

    public JsonSettingsHelper()
	{
		_remoteDir = string.Empty;
	}

	#endregion

	#region Public and private methods

    private bool SetupJsonSettings(string dir, bool isRemote, string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("Value must be fill!", nameof(fileName));

        string file = Path.Combine(dir, fileName);
        if (!File.Exists(file))
        {
            throw new(LocaleCore.System.JsonSettingsFileNotFound(file));
        }

        using StreamReader streamReader = File.OpenText(file);
        JsonSerializer serializer = new();
        object? jsonObject = (JsonSettingsModel?)serializer.Deserialize(streamReader, typeof(JsonSettingsModel));
        if (jsonObject is JsonSettingsModel jsonSettings)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new()
            {
                ["Data Source"] = jsonSettings.Sql.DataSource,
                ["Initial Catalog"] = jsonSettings.Sql.InitialCatalog,
                ["Persist Security Info"] = jsonSettings.Sql.PersistSecurityInfo,
                ["User ID"] = jsonSettings.Sql.UserId,
                ["Password"] = jsonSettings.Sql.Password,
                //sqlConnectionStringBuilder["Encrypt"] = jsonSettings.Sql.Encrypt;
                ["Connect Timeout"] = jsonSettings.Sql.ConnectTimeout,
                ["TrustServerCertificate"] = jsonSettings.Sql.TrustServerCertificate
            };

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

    public void SetupScales(string localDir, string appName)
    {
        try
        {
            FileLogger.Setup(localDir, appName);
            CheckUpdates(localDir, JsonFileName);

            if (!SetupJsonSettings(localDir, false, JsonFileName))
            {
                //if (isFileLog)
                //    FileLog.WriteMessage(LocaleCore.System.JsonSettingsLocalFileException);
                throw new(LocaleCore.System.JsonSettingsLocalFileException);
            }

            WsDataAccessHelper.Instance.SetSessionFactory(DebugHelper.Instance.IsDevelop);
            WsDataAccessHelper.Instance.SetupLog(appName);
        }
        catch (Exception ex)
        {
            FileLogger.StoreException(ex);
        }
    }

    private void SetupTests(string localDir, string deviceName, string appName, string fileName, bool isShowSql)
	{
		CheckUpdates(localDir, fileName);

		if (!SetupJsonSettings(localDir, false, fileName))
			throw new(LocaleCore.System.JsonSettingsLocalFileException);
		
		WsDataAccessHelper.Instance.SetSessionFactory(isShowSql);
		WsDataAccessHelper.Instance.SetupLog(deviceName, appName);
	}

	public void SetupTestsDevelopAleksandrov(string localDir, string deviceName, string appName, bool isShowSql) =>
		SetupTests(localDir, deviceName, appName, FileNameDevelopAleksandrov, isShowSql);

	public void SetupTestsDevelopMorozov(string localDir, string deviceName, string appName, bool isShowSql) =>
		SetupTests(localDir, deviceName, appName, FileNameDevelopMorozov, isShowSql);

    public void SetupTestsDevelopVs(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupTests(localDir, deviceName, appName, FileNameDevelopVs, isShowSql);

    public void SetupTestsReleaseAleksandrov(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupTests(localDir, deviceName, appName, FileNameReleaseAleksandrov, isShowSql);

    public void SetupTestsReleaseMorozov(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupTests(localDir, deviceName, appName, FileNameReleaseMorozov, isShowSql);

    public void SetupTestsReleaseVs(string localDir, string deviceName, string appName, bool isShowSql) =>
        SetupTests(localDir, deviceName, appName, FileNameReleaseVs, isShowSql);

    public void SetupWebApp(string localDir, string appName)
    {
        try
        {
            AppVersion.Setup(Assembly.GetExecutingAssembly());
            FileLogger.Setup(localDir, appName);
            string subDir = Path.Combine(localDir, BlazorSubDir);
            if (Directory.Exists(subDir))
            {
                // Local folder.
                FileLogger.Setup(subDir, appName);
                CheckUpdates(subDir, JsonFileName);
                if (!SetupJsonSettings(subDir, false, JsonFileName))
                    throw new(LocaleCore.System.JsonSettingsLocalFileException);
            }
            else
            {
                // IIS publish folder.
                CheckUpdates(localDir, JsonFileName);
                if (!SetupJsonSettings(localDir, false, JsonFileName))
                    throw new(LocaleCore.System.JsonSettingsLocalFileException);
            }

            WsDataAccessHelper.Instance.SetSessionFactory(DebugHelper.Instance.IsDevelop);
            WsDataAccessHelper.Instance.SetupLog(appName);
            WsDataAccessHelper.Instance.SaveLogInformation(LocaleCore.DeviceControl.WebAppIsStarted);
        }
        catch (Exception ex)
        {
            FileLogger.StoreException(ex);
        }
    }

	private void CheckUpdates(string localDir, string fileName)
	{
        switch (DebugHelper.Instance.Config)
        {
            case WsConfiguration.DevelopAleksandrov:
            case WsConfiguration.DevelopMorozov:
            case WsConfiguration.ReleaseAleksandrov:
            case WsConfiguration.ReleaseMorozov:
                return;
            case WsConfiguration.DevelopVS:
            case WsConfiguration.ReleaseVS:
                break;
        }

        if (!Directory.Exists(RemoteDir))
		{
			throw new(LocaleCore.System.JsonSettingsRemoteFolderNotFound);
		}
		string remoteFile = Path.Combine(RemoteDir, fileName);
		if (!Directory.Exists(RemoteDir))
		{
			throw new(LocaleCore.System.JsonSettingsRemoteFileNotFound);
		}

		SetupJsonSettings(RemoteDir, true, fileName);

		ushort version = GetLocalVersion(localDir, fileName);
		if (version == 0 || version < Remote.Version)
		{
			string localFile = Path.Combine(localDir, fileName);
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

			using StreamWriter streamWriter = File.CreateText(Path.Combine(localDir, fileName));
			streamWriter.Write(content);
			streamWriter.Close();
			streamWriter.Dispose();
		}
	}

	private ushort GetLocalVersion(string localDir, string fileName)
	{
		string localFile = Path.Combine(localDir, fileName);
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
			string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
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
