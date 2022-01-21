// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore;
using DataShareCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;

namespace DataProjectsCore.DAL.TableModels
{
    [Serializable]
    public class LogDirect : BaseSerializeEntity<LogDirect>
    {
        #region Public and private fields and properties

        public int? HostId { get; set; }
        public Guid? AppUid { get; set; }
        public string Version { get; set; }

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
            DataShareCore.Utils.StringUtils.SetStringValueTrim(ref version, 12);
            Version = version;
        }

        #endregion

        #region Public and private methods

        private void Save(string message, ShareEnums.LogType logType, string filePath, string memberName, int lineNumber)
        {
            DataShareCore.Utils.StringUtils.SetStringValueTrim(ref filePath, 32, true);
            DataShareCore.Utils.StringUtils.SetStringValueTrim(ref memberName, 32);
            byte logNumber = (byte)logType;
            DataShareCore.Utils.StringUtils.SetStringValueTrim(ref message, 1024);
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@hostId", System.Data.SqlDbType.Int) { Value = HostId },
                new SqlParameter("@appUid", System.Data.SqlDbType.UniqueIdentifier) { Value = AppUid },
                new SqlParameter("@version", System.Data.SqlDbType.NVarChar, 12) { Value = Version },
                new SqlParameter("@file", System.Data.SqlDbType.NVarChar, 32) { Value = filePath },
                new SqlParameter("@line", System.Data.SqlDbType.Int) { Value = lineNumber },
                new SqlParameter("@member", System.Data.SqlDbType.NVarChar, 32) { Value = memberName },
                new SqlParameter("@logNumber", System.Data.SqlDbType.TinyInt) { Value = logNumber },
                new SqlParameter("@message", System.Data.SqlDbType.NVarChar, 1024) { Value = message },
            };
            SqlConnectFactory.ExecuteNonQuery(SqlQueries.DbServiceManaging.Tables.Logs.AddLog, parameters);
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

        public static Guid? SaveAppReader(SqlDataReader reader)
        {
            Guid? result = default;
            if (reader.Read())
            {
                result = SqlConnectFactory.GetValue<Guid>(reader, "UID");
            }
            return result;
        }

        public Guid? SaveApp(string app)
        {
            DataShareCore.Utils.StringUtils.SetStringValueTrim(ref app, 32);
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@app", System.Data.SqlDbType.NVarChar, 32) { Value = app },
            };
            return SqlConnectFactory.ExecuteReader(SqlQueries.DbServiceManaging.Tables.Apps.AddApp, parameters, SaveAppReader);
        }

        public static int? GetHostIdReader(SqlDataReader reader)
        {
            int? result = default;
            if (reader.Read())
            {
                result = SqlConnectFactory.GetValue<int>(reader, "ID");
            }
            return result;
        }

        public int? GetHostId(string host, Guid idRref)
        {
            DataShareCore.Utils.StringUtils.SetStringValueTrim(ref host, 150);
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@host", System.Data.SqlDbType.NVarChar, 150) { Value = host },
                new SqlParameter("@idrref", System.Data.SqlDbType.UniqueIdentifier) { Value = idRref },
            };
            return SqlConnectFactory.ExecuteReader(SqlQueries.DbScales.Tables.Hosts.GetHostId, parameters, GetHostIdReader);
        }

        #endregion
    }
}