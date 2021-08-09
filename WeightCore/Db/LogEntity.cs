// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace WeightCore.Db
{
    [Serializable]
    public class LogEntity
    {
        #region Public and private fields and properties

        public int? HostId { get; private set; }
        public Guid? AppUid { get; private set; }
        public string Version { get; private set; }
        //public LogTypeEntity LogType { get; private set; }

        #endregion

        #region Constructor and destructor

        public LogEntity(string host, Guid idRref, string app, string version)
        {
            HostId = GetHostId(host, idRref);
            AppUid = SaveApp(app);
            Utils.StringValueTrim(ref version, 12);
            Version = version;
        }

        #endregion

        #region Public and private methods

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LogEntity));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }

        [Obsolete(@"Deprecated method")]
        private void Save(string file, int line, string member, string icon, string message)
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                Utils.StringValueTrim(ref file, 32, true);
                Utils.StringValueTrim(ref member, 32);
                Utils.StringValueTrim(ref icon, 32);
                Utils.StringValueTrim(ref message, 1024);
                string query = @"
insert into [db_scales].[LOGS]([HOST_ID],[APP_UID],[VERSION],[FILE],[LINE],[MEMBER],[MESSAGE]) 
values (@hostId,@appUid,@version,@file,@line,@member,@message)
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n').Replace(Environment.NewLine, " ");
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@hostId", HostId);
                    cmd.Parameters.AddWithValue("@appUid", AppUid);
                    cmd.Parameters.AddWithValue("@version", Version);
                    cmd.Parameters.AddWithValue("@file", file);
                    cmd.Parameters.AddWithValue("@line", line);
                    cmd.Parameters.AddWithValue("@member", member);
                    cmd.Parameters.AddWithValue("@message", message);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public void Save(string file, int line, string member, Enums.LogType logType, string message)
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                Utils.StringValueTrim(ref file, 32, true);
                Utils.StringValueTrim(ref member, 32);
                byte logNumber = (byte)logType;
                Utils.StringValueTrim(ref message, 1024);
                string query = @"
declare @log_type_uid uniqueidentifier = (select [UID] from [db_scales].[LOG_TYPES] where [NUMBER]=@logNumber)
insert into [db_scales].[LOGS]([HOST_ID],[APP_UID],[VERSION],[FILE],[LINE],[MEMBER],[LOG_TYPE_UID],[MESSAGE]) 
values (@hostId,@appUid,@version,@file,@line,@member,@log_type_uid,@message)
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n').Replace(Environment.NewLine, " ");
                using (SqlCommand cmd = new SqlCommand(query))
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
            Save(file, line, member, Enums.LogType.Information, message);
        }

        public void SaveError(string file, int line, string member, string message)
        {
            Save(file, line, member, Enums.LogType.Error, message);
        }

        public void SaveWarning(string file, int line, string member, string message)
        {
            Save(file, line, member, Enums.LogType.Warning, message);
        }

        public void SaveQuestion(string file, int line, string member, string message)
        {
            Save(file, line, member, Enums.LogType.Question, message);
        }

        public Guid? SaveApp(string app)
        {
            Guid? result = null;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                Utils.StringValueTrim(ref app, 32);
                string query = @"
if not exists (select 1 from [db_scales].[APPS] where [NAME]=@app) begin
	insert into [db_scales].[APPS]([NAME]) values(@app)
end
select [UID]
from [db_scales].[APPS]
where [NAME]=@app
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n').Replace(Environment.NewLine, " ");
                using (SqlCommand cmd = new SqlCommand(query))
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
                Utils.StringValueTrim(ref host, 150);
                string query = @"
select [ID]
from [db_scales].[Hosts] 
where [Name]=@host and [IdRRef]=@idrref
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n').Replace(Environment.NewLine, " ");
                using (SqlCommand cmd = new SqlCommand(query))
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