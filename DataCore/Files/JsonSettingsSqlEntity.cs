// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace DataCore.Files
{
    [Serializable]
    public class JsonSettingsSqlEntity : ISerializable
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

        public JsonSettingsSqlEntity(SerializationInfo info, StreamingContext context)
        {
            Server = info.GetString(nameof(Server));
            Db = info.GetString(nameof(Db));
            Trusted = info.GetBoolean(nameof(Trusted));
            Username = info.GetString(nameof(Username));
            Password = info.GetString(nameof(Password));
            Schema = info.GetString(nameof(Schema));
            TrustServerCertificate = info.GetBoolean(nameof(TrustServerCertificate));
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strTrusted = Trusted ? $"{nameof(Trusted)}: {Trusted}. " : $"{nameof(Username)}: {Username}. {nameof(Password)}: {Password}. ";
            return $"{nameof(Server)}: {Server}. " +
                $"{nameof(Db)}: {Db}. " +
                $"{strTrusted} " +
                $"{nameof(TrustServerCertificate)}: {TrustServerCertificate}. ";
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Server), Server);
            info.AddValue(nameof(Db), Db);
            info.AddValue(nameof(Trusted), Trusted);
            info.AddValue(nameof(Username), Username);
            info.AddValue(nameof(Password), Password);
            info.AddValue(nameof(Schema), Schema);
            info.AddValue(nameof(TrustServerCertificate), TrustServerCertificate);
        }

        #endregion
    }
}
