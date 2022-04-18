// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using DataCore.Models;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace DataCore.Helpers
{
    public class DataAccessHelper
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static DataAccessHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static DataAccessHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public DataAccessEntity Dal { get; private set; }
        public string ConnectionString { get; private set; }
        //private IConfiguration? Configuration { get; set; }
        //private IConfigurationBuilder? ConfigurationBuilder { get; set; }

        #endregion

        #region Constructor and destructor

        public DataAccessHelper()
        {
            ConnectionString = string.Empty;
            Dal = new DataAccessEntity(GetJsonSettings(Directory.GetCurrentDirectory()));
        }

        #endregion

        #region Public and private methods

        public JsonSettingsBase? GetJsonSettings(string dir)
        {
            string fileSettings =
#if DEBUG
                $"{dir}\\appsettings.Debug.json"
#else
                $"{(dir}\\appsettings.Release.json"
#endif
                ;
            if (File.Exists(fileSettings))
            {
                using StreamReader streamReader = File.OpenText(fileSettings);
                JsonSerializer serializer = new();
                object? jsonObject = (JsonSettingsBase?)serializer.Deserialize(streamReader, typeof(JsonSettingsBase));
                if (jsonObject is JsonSettingsBase jsonSettings)
                {
                    Microsoft.Data.SqlClient.SqlConnectionStringBuilder sqlConnectionStringBuilder = new();
                    sqlConnectionStringBuilder["Data Source"] = jsonSettings.Sql.Server;
                    sqlConnectionStringBuilder["Initial Catalog"] = jsonSettings.Sql.Db;
                    sqlConnectionStringBuilder["Persist Security Info"] = jsonSettings.Sql.Trusted;
                    sqlConnectionStringBuilder["User ID"] = jsonSettings.Sql.Username;
                    sqlConnectionStringBuilder["Password"] = jsonSettings.Sql.Password;
                    sqlConnectionStringBuilder["TrustServerCertificate"] = jsonSettings.Sql.TrustServerCertificate;
                    //sqlConnectionStringBuilder["Schema"] = jsonSettings.Schema;
                    ConnectionString = sqlConnectionStringBuilder.ConnectionString;
                    return jsonSettings;
                }
            }
            return null;
        }

        #endregion

    }
}
