// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore;
using DataShareCore.Utils;
using MvvmHelpers;
using ScalesCore.Models;
using ScalesCore.Properties;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace ScalesCore.Helpers
{
    /// <summary>
    /// Помощник SQL.
    /// </summary>
    public sealed class SqlHelper : BaseViewModel
    {
        #region Design pattern "Singleton"

        private static readonly Lazy<SqlHelper> _instance = new(() => new SqlHelper());
        public static SqlHelper Instance => _instance.Value;
        private SqlHelper()
        {
            Open(ShareEnums.SettingsStorage.UseConfig);
        }

        #endregion

        #region Private fields and properties

        //

        #endregion

        #region Public fields and properties

        public DbProviderFactory ProviderFactory { get; private set; }
        public DbConnection Connection { get; set; }
        public SqlAuthentication Authentication { get; private set; }
        public string ApplicationName { get; private set; }
        public short ConnectTimeout { get; private set; }
        public short PacketSize { get; private set; }
        public string WorkstationId { get; private set; }
        public string ConnectionString { get; private set; }

        private string _status;
        /// <summary>
        /// Статус.
        /// </summary>
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Public methods

        public void Open(string server, string database, bool integratedSecurity, string userId, string password, bool encrypt, 
            string applicationName = null, string workstationId = null, short connectTimeout = 15, short packetSize = 8192)
        {
            Authentication = new SqlAuthentication(server, database, integratedSecurity, userId, password, encrypt, false);
            ApplicationName = !string.IsNullOrEmpty(applicationName) ? applicationName : "ScalesUI";
            ConnectTimeout = connectTimeout;
            if (ConnectTimeout < 15)
                ConnectTimeout = 15;
            PacketSize = packetSize;
            if (PacketSize < 512)
                PacketSize = 512;
            WorkstationId = workstationId;
            SetConnectionString();
        }

        public void Open(ShareEnums.SettingsStorage settingsStorage, string server = "", string database = "", bool integratedSecurity = false,
            string userId = "", string password = "",
            bool encrypt = false, string applicationName = null,
            string workstationId = null, short connectTimeout = 15, short packetSize = 8192)
        {
            //if (settingsStorage == ShareEnums.SettingsStorage.UseRegistry)
            //{
            //    server = string.Empty;
            //    database = string.Empty;
            //    userId = string.Empty;
            //    password = string.Empty;
            //    integratedSecurity = false;
            //    if (Authentication != null)
            //    {
            //        if (_reg.Exists(_reg.Root, Settings.Default.RegScalesUISql))
            //        {
            //            server = _reg.GetValue<string>(_reg.Root, Settings.Default.RegScalesUISql, nameof(Authentication.Server));
            //            database = _reg.GetValue<string>(_reg.Root, Settings.Default.RegScalesUISql, nameof(Authentication.Database));
            //            userId = _reg.GetValue<string>(_reg.Root, Settings.Default.RegScalesUISql, nameof(Authentication.UserId));
            //            password = _reg.GetValue<string>(_reg.Root, Settings.Default.RegScalesUISql, nameof(Authentication.Password));
            //            integratedSecurity = _reg.GetValue<string>(_reg.Root, Settings.Default.RegScalesUISql,
            //                nameof(Authentication.IntegratedSecurity)).Equals("TRUE", StringComparison.InvariantCultureIgnoreCase);
            //        }
            //    }
            //}
            if (settingsStorage == ShareEnums.SettingsStorage.UseConfig)
            {
                server = string.Empty;
                database = string.Empty;
                userId = string.Empty;
                password = string.Empty;
                integratedSecurity = false;
                string[] arr = Settings.Default.ConnectionString.Split(';');
                if (arr.Length > 3)
                {
                    server = arr[0].Contains("Server=") ? arr[0].Substring(7, arr[0].Length - 7) : arr[0];
                    database = arr[1].Contains("Database=") ? arr[1].Substring(9, arr[1].Length - 9) : arr[1];
                    userId = arr[2].Contains("Uid=") ? arr[2].Substring(4, arr[2].Length - 4) : arr[2];
                    password = arr[3].Contains("Pwd=") ? arr[3].Substring(4, arr[3].Length - 4) : arr[3];
                }
            }
            Open(server, database, integratedSecurity, userId, password, encrypt, applicationName, workstationId, connectTimeout, packetSize);
        }

        public void SetConnectionString()
        {
            string workstation = !string.IsNullOrEmpty(WorkstationId) ? $@"Workstation ID={WorkstationId}" : $@"Workstation ID={System.Net.Dns.GetHostName()}";
            ConnectionString = $@"Application Name={ApplicationName}; {Authentication}; Connect Timeout={ConnectTimeout}; Packet Size={PacketSize}; {workstation};";
            Console.WriteLine($@"ConnectionString=""{ConnectionString}""");
        }

        private void SetParameters(DbCommand cmd, Collection<SqlParam> parameters)
        {
            foreach (SqlParam parameter in parameters)
            {
                cmd.Parameters.Add(new SqlParameter(parameter.Name, parameter.Value));
            }
        }

        public Collection<Collection<object>> SelectData(string query, Collection<string> fieldNames, Collection<SqlParam> parameters = null)
        {
            Collection<Collection<object>> result = new();
            if (ProviderFactory == null || string.IsNullOrEmpty(query) || Connection == null || Connection.State != ConnectionState.Open)
                return result;

            using (DbCommand cmd = ProviderFactory.CreateCommand())
            {
                if (cmd != null)
                {
                    cmd.CommandText = query;
                    if (parameters?.Count > 0)
                        SetParameters(cmd, parameters);
                    cmd.Connection = Connection;
                    //cmd.CommandTimeout = 180;
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        using DbDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Collection<object> record = new();
                                foreach (string field in fieldNames)
                                {
                                    record.Add(reader.GetFieldValue<object>(reader.GetOrdinal(field)));
                                }
                                result.Add(record);
                            }
                        }
                        reader.Close();
                    }
                    else
                    {
                        Console.WriteLine(@"Connection is not open.");
                    }
                }
            }
            return result;
        }

        public void OpenConnection(ShareEnums.Lang language = ShareEnums.Lang.Russian)
        {
            try
            {
                if (ProviderFactory == null)
                    //ProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                    ProviderFactory = DbProviderFactories.GetFactory("Microsoft.Data.SqlClient");
                if (ProviderFactory != null && !string.IsNullOrEmpty(ConnectionString))
                {
                    Connection = ProviderFactory.CreateConnection();
                    if (Connection != null)
                    {
                        Connection.ConnectionString = ConnectionString;
                        //MessageBox.Show(Connection.ConnectionString);
                        Connection.Open();
                    }
                }
                Status = language == ShareEnums.Lang.English ?
                    $@"Connecting to SQL server is open." : $"Открыто подключение к SQL-серверу.";
            }
            catch (Exception ex)
            {
                Status = language == ShareEnums.Lang.English ?
                    $@"Error connecting to SQL server! Message: {ex.Message}" : $"Ошибка подключения к SQL-серверу! Сообщение: {ex.Message}";
            }
        }

        /// <summary>
        /// Закрыть SQL-подключение.
        /// </summary>
        public void CloseConnection(ShareEnums.Lang language)
        {
            Status = language == ShareEnums.Lang.English ?
                $@"Connecting to SQL server is close." : $"Закрыто подключение к SQL-серверу.";
            Connection?.Close();
        }

        public ConnectionState GetConnection()
        {
            if (Connection != null)
            {
                return Connection.State;
            }
            return ConnectionState.Closed;
        }

        /// <summary>
        /// Получить значение поля.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public SqlTableField<T> GetValueField<T>(SqlTableField<T> field, SqlDataReader reader) where T : IConvertible
        {
            T value = default;
            int ordinal = -1;
            try
            {
                if (field != null && reader != null)
                {
                    ordinal = reader.GetOrdinal(field.Name);
                    if (!reader.IsDBNull(ordinal))
                    {
                        value = reader.GetFieldValue<T>(ordinal);
                    }
                }
            }
            catch (Exception ex)
            {
                Status = $@"Ошибка поля {field.Name}. Ожидаемый тип данных: {typeof(T)}. Полученный тип данных: {reader.GetFieldType(ordinal)}. Сообщение: " + ex.Message;
                throw;
            }

            if (field != null)
                return new SqlTableField<T>(field.Name, value, field.Default);
            return new SqlTableField<T>(null, value, default);
        }

        /// <summary>
        /// Получить значение поля.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="reader"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public T GetValue<T>(string name, Microsoft.Data.SqlClient.SqlDataReader reader, string description = null) where T : IConvertible
        {
            T value = default;
            int ordinal = -1;
            try
            {
                if (reader != null)
                {
                    if (reader.ExistsField(name))
                        ordinal = reader.GetOrdinal(name);
                    else
                        Status = $@"Поле [{name}] не найдено. {description}";
                    if (ordinal >= 0)
                    {
                        if (!reader.IsDBNull(ordinal))
                        {
                            value = reader.GetFieldValue<T>(ordinal);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Status =
                    $@"Ошибка поля {name}. {description }. Ожидаемый тип данных: {typeof(T)}. Полученный тип данных: {reader.GetFieldType(ordinal)}. Сообщение: {ex.Message}";
                throw;
            }

            return value;
        }

        #endregion
    }
}
