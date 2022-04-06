// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using DataCore.Utils;
using Microsoft.Data.SqlClient;
using System;

namespace DataCore.DAL.TableDirectModels
{
    [Serializable]
    public class LogDirect : BaseSerializeEntity<LogDirect>
    {
        #region Public and private fields and properties

        public long? HostId { get; set; } = default!;
        public Guid? AppUid { get; set; } = default;
        public string Version { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public LogDirect()
        {
            HostId = null;
            AppUid = null;
            Version = string.Empty;
        }

        public LogDirect(string host, Guid idRref, string app, string version) : this()
        {
            HostId = GetHostId(host, idRref);
            AppUid = SaveApp(app);
            StringUtils.SetStringValueTrim(ref version, 12);
            Version = version;
        }

        #endregion

        #region Public and private methods

        private void Save(string message, ShareEnums.LogType logType, string filePath, string memberName, int lineNumber)
        {
            StringUtils.SetStringValueTrim(ref filePath, 32, true);
            StringUtils.SetStringValueTrim(ref memberName, 32);
            byte logNumber = (byte)logType;
            StringUtils.SetStringValueTrim(ref message, 1024);
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@hostId", System.Data.SqlDbType.BigInt) { Value = HostId },
                new SqlParameter("@appUid", System.Data.SqlDbType.UniqueIdentifier) { Value = AppUid },
                new SqlParameter("@version", System.Data.SqlDbType.NVarChar, 12) { Value = Version },
                new SqlParameter("@file", System.Data.SqlDbType.NVarChar, 32) { Value = filePath },
                new SqlParameter("@line", System.Data.SqlDbType.Int) { Value = lineNumber },
                new SqlParameter("@member", System.Data.SqlDbType.NVarChar, 32) { Value = memberName },
                new SqlParameter("@logNumber", System.Data.SqlDbType.TinyInt) { Value = logNumber },
                new SqlParameter("@message", System.Data.SqlDbType.NVarChar, 1024) { Value = message },
            };
            SqlConnect.ExecuteNonQuery(SqlQueries.DbServiceManaging.Tables.Logs.AddLog, parameters);
        }

        public void SaveInformation(string message, string filePath, string memberName, int lineNumber)
        {
            Save(message, ShareEnums.LogType.Information, filePath, memberName, lineNumber);
        }

        public void SaveError(string message, string filePath, string memberName, int lineNumber)
        {
            Save(message, ShareEnums.LogType.Error, filePath, memberName, lineNumber);
        }

        public void SaveStop(string message, string filePath, string memberName, int lineNumber)
        {
            Save(message, ShareEnums.LogType.Stop, filePath, memberName, lineNumber);
        }

        public void SaveWarning(string message, string filePath, string memberName, int lineNumber)
        {
            Save(message, ShareEnums.LogType.Warning, filePath, memberName, lineNumber);
        }

        public void SaveQuestion(string message, string filePath, string memberName, int lineNumber)
        {
            Save(message, ShareEnums.LogType.Question, filePath, memberName, lineNumber);
        }

        public Guid? SaveApp(string app)
        {
            StringUtils.SetStringValueTrim(ref app, 32);
            Guid? result = default;
            SqlConnect.ExecuteReader(SqlQueries.DbServiceManaging.Tables.Apps.AddApp,
                new SqlParameter("@app", System.Data.SqlDbType.NVarChar, 32) { Value = app }, (SqlDataReader reader) =>
            {
                if (reader.Read())
                {
                    result = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
                }
            });
            return result;
        }

        public long? GetHostId(string host, Guid idRref)
        {
            StringUtils.SetStringValueTrim(ref host, 150);
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@host", System.Data.SqlDbType.NVarChar, 150) { Value = host },
                new SqlParameter("@idrref", System.Data.SqlDbType.UniqueIdentifier) { Value = idRref },
            };

            int? result = default;
            SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Hosts.GetHostId, parameters, (SqlDataReader reader) =>
            {
                if (reader.Read())
                {
                    result = SqlConnect.GetValueAsNotNullable<int>(reader, "ID");
                }
            });
            return result;
        }

        #endregion
    }
}