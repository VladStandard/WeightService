// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace EntitiesLib
{
    [Serializable]
    public class LogEntity
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
            StringValueTrim(ref version, 12);
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

        public void Save(string file, int line, string member, string icon, string message)
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                StringValueTrim(ref file, 128);
                StringValueTrim(ref member, 64);
                StringValueTrim(ref icon, 64);
                StringValueTrim(ref message, 1024);
                string query = @"
insert into [db_scales].[LOGS]([HOST_ID],[APP_UID],[VERSION],[FILE],[LINE],[MEMBER],[ICON],[MESSAGE]) 
values (@HostId,@AppUid,@Version,@File,@Line,@Member,@Icon,@Message)
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n').Replace(Environment.NewLine, " ");
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@HostId", HostId);
                    cmd.Parameters.AddWithValue("@AppUid", AppUid);
                    cmd.Parameters.AddWithValue("@Version", Version);
                    cmd.Parameters.AddWithValue("@File", file);
                    cmd.Parameters.AddWithValue("@Line", line);
                    cmd.Parameters.AddWithValue("@Member", member);
                    cmd.Parameters.AddWithValue("@Icon", icon);
                    cmd.Parameters.AddWithValue("@Message", message);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public void SaveInfo(string file, int line, string member, string message)
        {
            Save(file, line, member, "Information", message);
        }

        public void SaveError(string file, int line, string member, string message)
        {
            Save(file, line, member, "Error", message);
        }

        public void SaveWarning(string file, int line, string member, string message)
        {
            Save(file, line, member, "Warning", message);
        }

        public void SaveQuestion(string file, int line, string member, string message)
        {
            Save(file, line, member, "Question", message);
        }

        public Guid? SaveApp(string app)
        {
            Guid? result = null;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                StringValueTrim(ref app, 32);
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
                StringValueTrim(ref host, 150);
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

        public void StringValueTrim(ref string value, int length)
        {
            if (value.Length > length)
                value = value.Substring(0, length);
        }

        #endregion
    }
}