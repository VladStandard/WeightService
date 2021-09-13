// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;

namespace ScalesCore.Models
{
    /// <summary>
    /// SQL-авторизация.
    /// </summary>
    public class SqlAuthentication
    {
        #region Constructor

        public SqlAuthentication(string server, string database, bool persistSecurityInfo, bool integratedSecurity, string userId, string password, bool encrypt, bool usePort, ushort port)
        {
            Server = server;
            Database = database;
            PersistSecurityInfo = persistSecurityInfo;
            IntegratedSecurity = integratedSecurity;
            UserId = userId;
            Password = password;
            Encrypt = encrypt;
            UsePort = usePort;
            Port = port;
        }

        public SqlAuthentication(string server, string database, string userId, string password, bool usePort = false, ushort port = 1433) :
            this(server, database, false, false, userId, password, false, usePort, port)
        {
        }

        public SqlAuthentication(string server, string database, bool integratedSecurity, string userId, string password, bool encrypt, bool usePort = false, ushort port = 1433) : this(server, database, false, integratedSecurity, userId, password, encrypt, usePort, port)
        {
        }

        public SqlAuthentication(string server, string database, bool encrypt) : this(server, database, false, true, string.Empty, string.Empty, encrypt, false, 1433)
        {
        }

        #endregion

        #region Public fields and properties

        public string Server { get; set; }
        public string Database { get; set; }
        public bool PersistSecurityInfo { get; }
        public bool IntegratedSecurity { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool Encrypt { get; }
        public bool UsePort { get; }
        public ushort Port { get; }

        #endregion

        #region Public methods

        /// <summary>
        /// Строка с аутентификацией Windows.
        /// </summary>
        /// <param name="useCrypt"></param>
        /// <returns></returns>
        public string GetStringIntegratedSecurity(bool useCrypt = false)
        {
            return IntegratedSecurity
                ? @"Integrated Security=True;"
                : $@"Integrated Security=False; User ID={UserId}; Password={(useCrypt ? "**********" : Password)}";
        }

        public override string ToString()
        {
            return
                $@"Data Source={Server}{(UsePort ? $",{Port}" : "")}; Initial Catalog={Database}; Persist Security Info={PersistSecurityInfo}; Encrypt={Encrypt}; {GetStringIntegratedSecurity()}";
        }

        /// <summary>
        /// Настраиваемое представление как строки.
        /// </summary>
        /// <param name="conStringLevel"></param>
        /// <returns></returns>
        public string AsString(ProjectsEnums.ConStringLevel conStringLevel)
        {
            if (!UsePort)
            {
                if (conStringLevel == ProjectsEnums.ConStringLevel.Full)
                    return $@"Data Source={Server}; Initial Catalog={Database}; Persist Security Info={PersistSecurityInfo}; Encrypt={Encrypt}; {GetStringIntegratedSecurity()}";
                if (conStringLevel == ProjectsEnums.ConStringLevel.Middle)
                    return $@"Server={Server}; Database={Database}; {GetStringIntegratedSecurity()}";
                if (conStringLevel == ProjectsEnums.ConStringLevel.Low)
                    return $@"Server={Server}; Database={Database}; {GetStringIntegratedSecurity(true)}";
                if (conStringLevel == ProjectsEnums.ConStringLevel.Basic)
                    return $@"Server={Server}; Database={Database}";
            }
            else
            {
                if (conStringLevel == ProjectsEnums.ConStringLevel.Full)
                    return $@"Data Source={Server},{Port}; Initial Catalog={Database}; Persist Security Info={PersistSecurityInfo}; Encrypt={Encrypt}; {GetStringIntegratedSecurity()}";
                if (conStringLevel == ProjectsEnums.ConStringLevel.Middle)
                    return $@"Server={Server},{Port}; Database={Database}; {GetStringIntegratedSecurity()}";
                if (conStringLevel == ProjectsEnums.ConStringLevel.Low)
                    return $@"Server={Server}; Database={Database}; {GetStringIntegratedSecurity(true)}";
                if (conStringLevel == ProjectsEnums.ConStringLevel.Basic)
                    return $@"Server={Server}; Database={Database}";
            }
            return string.Empty;
        }

        /// <summary>
        /// Существует.
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            if (!string.IsNullOrEmpty(Server) && !string.IsNullOrEmpty(Database))
            {
                if (IntegratedSecurity)
                    return true;
                if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(Password))
                    return true;
            }
            return false;
        }

        #endregion
    }
}
