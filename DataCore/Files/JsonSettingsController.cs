// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace DataCore.Files;

public class JsonSettingsController
{
    #region Public and private fields, properties, constructor

    private AppVersionHelper AppVersion { get; } = AppVersionHelper.Instance;
    private DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
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

    public static string FileName =>
#if DEBUG
	    FileNameDebug;
#else
        FileNameRelease;
#endif
	public static string FileNameDebug => "appsettings.Debug.json";
    public static string FileNameRelease => "appsettings.Release.json";
    public string ExceptionFileName(string localDir) => Path.Combine(localDir, $"{FileName}.log");
    public const string BlazorSubDir =
#if DEBUG
        @"bin\x64\Debug\net6.0\";
#else
        @"bin\x64\Release\net6.0\";
#endif

	/// <summary>
	/// Constructor.
	/// </summary>
	public JsonSettingsController()
    {
        _remoteDir = string.Empty;
        FileLogHelper.Instance.Recreate();
    }

    #endregion

    #region Public and private methods

    public void SetupForBlazorApp(string localDir, string? hostName, string? appName)
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

            DataAccess.Log.Setup(hostName, appName);
            DataAccess.Log.LogInformation(LocaleCore.DeviceControl.WebAppIsStarted);
        }
        catch (Exception ex)
        {
            DataAccess.Log.LogToFile(ExceptionFileName(localDir), ex.Message);
            if (ex.InnerException != null)
                DataAccess.Log.LogToFile(ExceptionFileName(localDir), $"{FileName}.log", ex.InnerException.Message);
        }
    }

    public void SetupForTests(string localDir, string? hostName, string? appName, string fileName)
    {
        CheckUpdates(localDir);

        if (!Setup(localDir, false, fileName))
            throw new(LocaleCore.System.JsonSettingsLocalFileException);

        DataAccess.Log.Setup(hostName, appName);
    }

    public void SetupForScales(string localDir)
    {
        CheckUpdates(localDir);

        if (!Setup(localDir, false, ""))
        {
            //if (isFileLog)
            //    FileLog.WriteMessage(LocaleCore.System.JsonSettingsLocalFileException);
            throw new(LocaleCore.System.JsonSettingsLocalFileException);
        }
    }

    private bool Setup(string dir, bool isRemote, string fileName)
    {
	    string file;
	    if (!string.IsNullOrEmpty(fileName))
	    {
		    file = Path.Combine(dir, fileName);
	    }
	    else
	    {
			file = Path.Combine(dir, FileName);
	    }
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
            sqlConnectionStringBuilder["Encrypt"] = jsonSettings.Sql.Encrypt;
            sqlConnectionStringBuilder["Connect Timeout"] = jsonSettings.Sql.ConnectTimeout;
            sqlConnectionStringBuilder["TrustServerCertificate"] = jsonSettings.Sql.TrustServerCertificate;
            switch (isRemote)
            {
	            case false:
		            DataAccess.JsonSettingsLocal = jsonSettings;
		            DataAccess.JsonSettingsLocal.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
		            break;
	            default:
		            DataAccess.JsonSettingsRemote = jsonSettings;
		            DataAccess.JsonSettingsRemote.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
		            break;
            }
		    DataAccess.InitSessionFactoryWithJson(isRemote);
        }
		return jsonObject != null;
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
        if (version == 0 || version < DataAccess.JsonSettingsRemote.Version)
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
