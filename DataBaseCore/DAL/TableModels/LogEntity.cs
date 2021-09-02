// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataBaseCore.Utils;
using Microsoft.Data.SqlClient;
using System;

namespace DataBaseCore.DAL.TableModels
{
    [Serializable]
    public class LogEntity : BaseEntity<LogEntity>
    {
        #region Public and private fields and properties

        public int? HostId { get; set; }
        public Guid? AppUid { get; set; }
        public string Version { get; set; }

        #endregion

        #region Constructor and destructor

        public LogEntity()
        {

        }

        public LogEntity(string host, Guid idRref, string app, string version) : this()
        {
            HostId = GetHostId(host, idRref);
            AppUid = SaveApp(app);
            StringUtils.StringValueTrim(ref version, 12);
            Version = version;
        }

        #endregion

        #region Public and private methods

        public void Save(string file, int line, string member, LogType logType, string message)
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                StringUtils.StringValueTrim(ref file, 32, true);
                StringUtils.StringValueTrim(ref member, 32);
                byte logNumber = (byte)logType;
                StringUtils.StringValueTrim(ref message, 1024);
                using (SqlCommand cmd = new SqlCommand(SqlQueries.Logs.AddLog))
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
                con.Open();
                StringUtils.StringValueTrim(ref app, 32);
                using (SqlCommand cmd = new SqlCommand(SqlQueries.Apps.AddApp))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@app", app);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                result = SqlConnectFactory.GetValue<Guid>(reader, "UID");
                            }
                        }
                        reader.Close();
                    }
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
                con.Open();
                StringUtils.StringValueTrim(ref host, 150);
                using (SqlCommand cmd = new SqlCommand(SqlQueries.Hosts.GetHostId))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@host", host);
                    cmd.Parameters.AddWithValue("@idrref", idRref);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                result = SqlConnectFactory.GetValue<int>(reader, "ID");
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }

        #endregion
    }
}