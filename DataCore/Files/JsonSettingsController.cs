// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql;
using DataCore.Localizations;
using DataCore.Settings;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace DataCore.Files
{
    public class JsonSettingsController
    {
        #region Public and private fields, properties, constructor

        public AppVersionHelper AppVersion { get; private set; } = AppVersionHelper.Instance;
        public DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;
        //public FileLogHelper FileLog { get; private set; } = FileLogHelper.Instance;
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

        public const string FileName =
#if DEBUG
            "appsettings.Debug.json";
#else
            "appsettings.Release.json";
#endif
        public string ExceptionFileName(string localDir) => Path.Combine(localDir, $"{FileName}.log");
        public const string BlazorSubDir =
#if DEBUG
            @"bin\x64\Debug\net6.0\";
#else
            @"bin\x64\Release\net6.0\";
#endif

        #endregion

        #region Constructor and destructor

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
                    CheckUpdates(subDir, false);
                    if (!Setup(subDir))
                        throw new Exception(LocaleCore.System.JsonSettingsLocalFileException);
                }
                else
                {
                    // IIS publish folder.
                    CheckUpdates(localDir, false);
                    if (!Setup(localDir))
                        throw new Exception(LocaleCore.System.JsonSettingsLocalFileException);
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

        public void SetupForTests(string localDir, string? hostName, string? appName)
        {
            CheckUpdates(localDir, false);

            if (!Setup(localDir))
                throw new Exception(LocaleCore.System.JsonSettingsLocalFileException);

            DataAccess.Log.Setup(hostName, appName);
        }

        public void SetupForScales(string localDir, bool isFileLog)
        {
            CheckUpdates(localDir, isFileLog);

            if (!Setup(localDir, false, isFileLog))
            {
                //if (isFileLog)
                //    FileLog.WriteMessage(LocaleCore.System.JsonSettingsLocalFileException);
                throw new Exception(LocaleCore.System.JsonSettingsLocalFileException);
            }
        }

        private bool Setup(string dir, bool isRemote = false, bool isFileLog = false)
        {
            string file = Path.Combine(dir, FileName);
            if (!File.Exists(file))
            {
                //if (isFileLog)
                //    FileLog.WriteMessage(LocaleCore.System.JsonSettingsFileNotFound(file));
                throw new Exception(LocaleCore.System.JsonSettingsFileNotFound(file));
            }

            using StreamReader streamReader = File.OpenText(file);
            JsonSerializer serializer = new();
            object? jsonObject = (JsonSettingsEntity?)serializer.Deserialize(streamReader, typeof(JsonSettingsEntity));
            if (jsonObject is JsonSettingsEntity getJjsonSettings)
            {
                Microsoft.Data.SqlClient.SqlConnectionStringBuilder sqlConnectionStringBuilder = new();
                sqlConnectionStringBuilder["Data Source"] = getJjsonSettings.Sql.DataSource;
                sqlConnectionStringBuilder["Initial Catalog"] = getJjsonSettings.Sql.InitialCatalog;
                sqlConnectionStringBuilder["Persist Security Info"] = getJjsonSettings.Sql.PersistSecurityInfo;
                sqlConnectionStringBuilder["User ID"] = getJjsonSettings.Sql.UserId;
                sqlConnectionStringBuilder["Password"] = getJjsonSettings.Sql.Password;
                sqlConnectionStringBuilder["Encrypt"] = getJjsonSettings.Sql.Encrypt;
                sqlConnectionStringBuilder["Connect Timeout"] = getJjsonSettings.Sql.ConnectTimeout;
                sqlConnectionStringBuilder["TrustServerCertificate"] = getJjsonSettings.Sql.TrustServerCertificate;
                if (!isRemote)
                {
                    DataAccess.JsonSettingsLocal = getJjsonSettings;
                    DataAccess.JsonSettingsLocal.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
                }
                else
                {
                    DataAccess.JsonSettingsRemote = getJjsonSettings;
                    DataAccess.JsonSettingsRemote.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
                }
            }
            return jsonObject != null;
        }

        public void CheckUpdates(string localDir, bool isFileLog)
        {
            //if (isFileLog)
            //    FileLog.WriteMessage($"{nameof(localDir)}: {localDir}");
            if (!Directory.Exists(RemoteDir))
            {
                //if (isFileLog)
                //{
                //    FileLog.WriteMessage(LocaleCore.System.JsonSettingsRemoteFolderNotFound);
                //    FileLog.WriteMessage(RemoteDir);
                //}
                throw new Exception(LocaleCore.System.JsonSettingsRemoteFolderNotFound);
            }
            string remoteFile = Path.Combine(RemoteDir, FileName);
            if (!File.Exists(remoteFile))
            {
                //if (isFileLog)
                //    FileLog.WriteMessage(LocaleCore.System.JsonSettingsRemoteFileNotFound);
                throw new Exception(LocaleCore.System.JsonSettingsRemoteFileNotFound);
            }

            Setup(RemoteDir, true);

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
                    throw new Exception(LocaleCore.System.JsonSettingsFileIsEmpty(remoteFile));
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

            if (content.Contains(nameof(JsonSettingsEntity.Version)))
            {
                string[] lines = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if (line.Contains($"\"{nameof(JsonSettingsEntity.Version)}\""))
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
}
