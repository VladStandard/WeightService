// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Files;
using DataCore.Localizations;
using Newtonsoft.Json;
using System;
using System.IO;

namespace DataCore.DAL.Models
{
    public class JsonController
    {
        #region Public and private fields and properties

        public DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;
        public const string RemoteDir = @"\\palych\Install\VSSoft\appsettings\";
        public const string FileName =
#if DEBUG
                "appsettings.Debug.json";
#else
                "appsettings.Release.json";
#endif
        public const string BlazorSubDir =
#if DEBUG
                @"bin\x64\Debug\net6.0\";
#else
                @"bin\x64\Release\net6.0\";
#endif

        #endregion

        #region Constructor and destructor

        public JsonController()
        {
            //
        }

        #endregion

        #region Public and private methods

        public void SetupForBlazorApp(string localDir)
        {
            CheckUpdates(localDir);
            // IIS folder.
            if (Setup(localDir))
                return;
            // Local folder.
            if (!Setup(Path.Combine(localDir, BlazorSubDir)))
                throw new Exception(LocaleCore.System.JsonSettingsLocalFileException);
        }

        public void SetupForTests(string localDir)
        {
            CheckUpdates(localDir);

            if (!Setup(localDir))
                throw new Exception(LocaleCore.System.JsonSettingsLocalFileException);
        }

        public void SetupForScales(string localDir)
        {
            CheckUpdates(localDir);

            if (!Setup(localDir))
                throw new Exception(LocaleCore.System.JsonSettingsLocalFileException);
        }

        private bool Setup(string dir, bool isRemote = false)
        {
            string file = Path.Combine(dir, FileName);
            if (!File.Exists(file))
                throw new Exception(LocaleCore.System.JsonSettingsFileNotFound(file));

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
            //if (DataAccess.Crud != null && DataAccess.JsonSettingsLocal != null)
            //    DataAccess.Crud = new();
            return jsonObject != null;
        }

        public void CheckUpdates(string localDir)
        {
            if (!Directory.Exists(RemoteDir))
                throw new Exception(LocaleCore.System.JsonSettingsRemoteFolderNotFound);
            string remoteFile = Path.Combine(RemoteDir, FileName);
            if (!File.Exists(remoteFile))
                throw new Exception(LocaleCore.System.JsonSettingsRemoteFileNotFound);

            Setup(RemoteDir, true);
            if (DataAccess.JsonSettingsRemote == null)
                throw new Exception(LocaleCore.System.JsonSettingsRemoteFileException);

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
                    throw new Exception(LocaleCore.System.JsonSettingsFileIsEmpty(remoteFile));

                using StreamWriter streamWriter = File.CreateText(Path.Combine(localDir, FileName));
                streamWriter.Write(content);
                streamWriter.Close();
                streamWriter.Dispose();
            }
        }

        private ushort GetLocalVersion(string localDir)
        {
            string file = Path.Combine(localDir, FileName);
            using StreamReader streamReader = File.OpenText(file);
            string content = streamReader.ReadToEnd();
            streamReader.Close();
            streamReader.Dispose();
            if (string.IsNullOrEmpty(content))
                throw new Exception(LocaleCore.System.JsonSettingsFileIsEmpty(file));
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
                        throw new Exception(LocaleCore.System.JsonSettingsParseVersionException(file));
                    }
                }
            }

            return 0;
        }

        #endregion
    }
}
