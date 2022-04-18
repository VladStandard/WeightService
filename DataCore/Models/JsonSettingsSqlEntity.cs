// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Models
{
    public class JsonSettingsSqlEntity
    {
        #region Public and private fields and properties

        public string Server { get; set; }
        public string Db { get; set; }
        public bool Trusted { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Schema { get; set; }
        public bool TrustServerCertificate { get; set; }

        #endregion

        #region Constructor and destructor

        public JsonSettingsSqlEntity()
        {
            Server = string.Empty;
            Db = string.Empty;
            Trusted = default;
            Username = string.Empty;
            Password = string.Empty;
            Schema = string.Empty;
            TrustServerCertificate = false;
        }

        #endregion
    }
}
