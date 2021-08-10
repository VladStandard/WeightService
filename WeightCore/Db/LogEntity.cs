// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.SqlClient;
using WeightCore.Utils;

namespace WeightCore.Db
{
    [Serializable]
    public class LogEntity : BaseEntity<LogEntity>
    {
        #region Public and private fields and properties

        public int? HostId { get; private set; }
        public Guid? AppUid { get; private set; }
        public string Version { get; private set; }

        #endregion

        #region Constructor and destructor

        public LogEntity(string host, Guid idRref, string app, string version)
        {
            HostId = GetHostId(host, idRref);
            AppUid = SaveApp(app);
            UtilsString.StringValueTrim(ref version, 12);
            Version = version;
        }

        #endregion

        #region Public and private methods

        public void Save(string file, int line, string member, LogType logType, string message)
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                UtilsString.StringValueTrim(ref file, 32, true);
                UtilsString.StringValueTrim(ref member, 32);
                byte logNumber = (byte)logType;
                UtilsString.StringValueTrim(ref message, 1024);
                using (SqlCommand cmd = new SqlCommand(SqlQueries.AddLog))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@hostId", HostId);
                    cmd.Parameters.AddWithValue("@appUid", AppUid);
                    cmd.Parameters.AddWithValue("@version", Version);
                    cmd.Parameters.AddWithValue("@file", file);
                    cmd.Parameters.AddWithValue("@line", line);
                    cmd.Parameters.AddWithValue("@member", member);
                    cmd.Parameters.AddWithValue("@logNumber", logNumber);
                    cmd.Parameters.AddWithValue("@message", message);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public void SaveInfo(string file, int line, string member, string message)
        {
            Save(file, line, member, LogType.Information, message);
        }

        public void SaveError(string file, int line, string member, string message)
        {
            Save(file, line, member, LogType.Error, message);
        }

        public void SaveWarning(string file, int line, string member, string message)
        {
            Save(file, line, member, LogType.Warning, message);
        }

        public void SaveQuestion(string file, int line, string member, string message)
        {
            Save(file, line, member, LogType.Question, message);
        }

        public Guid? SaveApp(string app)
        {
            Guid? result = null;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                UtilsString.StringValueTrim(ref app, 32);
                using (SqlCommand cmd = new SqlCommand(SqlQueries.AddApp))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@app", app);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = SqlConnectFactory.GetValue<Guid>(reader, "UID");
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public int? GetHostId(string host, Guid idRref)
        {
            int? result = null;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                UtilsString.StringValueTrim(ref host, 150);
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetHostId))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@host", host);
                    cmd.Parameters.AddWithValue("@idrref", idRref);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = SqlConnectFactory.GetValue<int>(reader, "ID");
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        #endregion
    }
}