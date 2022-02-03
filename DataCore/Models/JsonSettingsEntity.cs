// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.Configuration;

namespace DataCore.Models
{
    /// <summary>
    /// Data model for appsettings.json.
    /// </summary>
    public class JsonSettingsEntity
    {
        #region Public and private fields and properties

        public IConfiguration Configuration { get; }
        public string Server
        {
            get => Configuration[$"Sql:{nameof(Server)}"];
            set => Configuration[$"Sql:{nameof(Server)}"] = value;
        }
        public string Db
        {
            get => Configuration[$"Sql:{nameof(Db)}"];
            set => Configuration[$"Sql:{nameof(Db)}"] = value;
        }
        public bool Trusted
        {
            get => Convert.ToBoolean(Configuration[$"Sql:{nameof(Trusted)}"]);
            set => Configuration[$"Sql:{nameof(Trusted)}"] = value.ToString();
        }
        public string Username
        {
            get => Configuration[$"Sql:{nameof(Username)}"];
            set => Configuration[$"Sql:{nameof(Username)}"] = value;
        }
        public string Password
        {
            get => Configuration[$"Sql:{nameof(Password)}"];
            set => Configuration[$"Sql:{nameof(Password)}"] = value;
        }
        public string Schema
        {
            get => Configuration[$"Sql:{nameof(Schema)}"];
            set => Configuration[$"Sql:{nameof(Schema)}"] = value;
        }
        public bool TrustServerCertificate
        {
            get => Convert.ToBoolean(Configuration[$"Sql:{nameof(TrustServerCertificate)}"]);
            set => Configuration[$"Sql:{nameof(TrustServerCertificate)}"] = value.ToString();
        }
        public bool IsDebug
        {
            get => Convert.ToBoolean(Configuration[$"{nameof(IsDebug)}"]);
            set => Configuration[$"{nameof(IsDebug)}"] = value.ToString();
        }
        public int SectionRowCount
        {
            get => Convert.ToInt32(Configuration[$"{nameof(SectionRowCount)}"]);
            set => Configuration[$"{nameof(SectionRowCount)}"] = value.ToString();
        }
        public int ItemRowCount
        {
            get => Convert.ToInt32(Configuration[$"{nameof(ItemRowCount)}"]);
            set => Configuration[$"{nameof(ItemRowCount)}"] = value.ToString();
        }

        #endregion

        #region Constructor and destructor

        public JsonSettingsEntity(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strTrusted = Trusted ? $"{nameof(Trusted)}: true." : $"{nameof(Username)}: {Username}. {nameof(Password)}: {Password}.";
            return $"{nameof(Server)}: {Server}. " +
                   $"{nameof(Db)}: {Db}. " +
                   $"{strTrusted} " +
                   $"{nameof(TrustServerCertificate)}: {TrustServerCertificate}. " +
                   $"{nameof(IsDebug)}: {IsDebug}. " +
                   $"{nameof(SectionRowCount)}: {SectionRowCount}. " +
                   $"{nameof(ItemRowCount)}: {ItemRowCount}. ";
        }

        public bool CheckProperties()
        {
            if (string.IsNullOrEmpty(Server))
                throw new ArgumentNullException(Server, $"{nameof(Server)} must be fill!");
            if (string.IsNullOrEmpty(Db))
                throw new ArgumentNullException(Db, $"{nameof(Db)} must be fill!");
            if (!Trusted)
            {
                if (string.IsNullOrEmpty(Username))
                    throw new ArgumentNullException(Username, $"{nameof(Username)} must be fill!");
                if (string.IsNullOrEmpty(Password))
                    throw new ArgumentNullException(Password, $"{nameof(Password)} must be fill!");
            }
            return true;
        }

        #endregion
    }
}
