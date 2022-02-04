// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCore.Models
{
    public class JsonSettingsSql
    {
        public string Server { get; set; } = string.Empty;
        public string Db { get; set; } = string.Empty;
        public bool Trusted { get; set; } = default;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Schema { get; set; } = string.Empty;
        public bool TrustServerCertificate { get; set; }
    }

    public class JsonSettingsBase
    {
        #region Public and private fields and properties

        public JsonSettingsSql Sql { get; set; } = new JsonSettingsSql();
        public bool IsDebug { get; set; } = default;
        public int SectionRowCount { get; set; } = default;
        public int ItemRowCount { get; set; } = default;

        #endregion

        #region Constructor and destructor

        public JsonSettingsBase()
        {
            CheckProperties();
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strTrusted = Sql.Trusted ? $"{nameof(Sql.Trusted)}: true." : $"{nameof(Sql.Username)}: {Sql.Username}. {nameof(Sql.Password)}: {Sql.Password}.";
            return $"{nameof(Sql.Server)}: {Sql.Server}. " +
                   $"{nameof(Sql.Db)}: {Sql.Db}. " +
                   $"{strTrusted} " +
                   $"{nameof(Sql.TrustServerCertificate)}: {Sql.TrustServerCertificate}. " +
                   $"{nameof(IsDebug)}: {IsDebug}. " +
                   $"{nameof(SectionRowCount)}: {SectionRowCount}. " +
                   $"{nameof(ItemRowCount)}: {ItemRowCount}. ";
        }

        public bool CheckProperties()
        {
            if (string.IsNullOrEmpty(Sql.Server))
                throw new ArgumentNullException(Sql.Server, $"{nameof(Sql.Server)} must be fill!");
            if (string.IsNullOrEmpty(Sql.Db))
                throw new ArgumentNullException(Sql.Db, $"{nameof(Sql.Db)} must be fill!");
            if (!Sql.Trusted)
            {
                if (string.IsNullOrEmpty(Sql.Username))
                    throw new ArgumentNullException(Sql.Username, $"{nameof(Sql.Username)} must be fill!");
                if (string.IsNullOrEmpty(Sql.Password))
                    throw new ArgumentNullException(Sql.Password, $"{nameof(Sql.Password)} must be fill!");
            }
            return true;
        }

        #endregion
    }
}
