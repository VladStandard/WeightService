// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace MdmControlCore
{
    public class AppSettingsEntity
    {
        #region Public and private fields and properties

        public const string JsonSection = @"LocalSettings";
        public string Server { get; set; }
        public string Db { get; set; }
        public bool Trusted { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionString => ToString();

        #endregion

        #region Constructor and destructor

        /// <summary>
        /// Empty constructor need for HostBuilder.
        /// </summary>
        public AppSettingsEntity()
        {
            //
        }

        public AppSettingsEntity(string server, string db, bool trusted, string username, string password)
        {
            Server = server;
            Db = db;
            Trusted = trusted;
            Username = username;
            Password = password;
        }

        #endregion

        #region Public and private methods

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

        public override string ToString()
        {
            var strTrusted = Trusted ? $"{nameof(Trusted)}: true." : $"{nameof(Username)}: {Username}. {nameof(Password)}: {Password}.";
            return $"{nameof(Server)}: { Server}. " +
                   $"{nameof(Db)}: {Db}. " +
                   $"{strTrusted}";
        }

        #endregion
    }
}