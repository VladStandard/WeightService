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

        public Guid Uid { get; set; } = Guid.Empty;
        public DateTime CreateDt { get; set; } = default;
        public string File { get; set; } = string.Empty;
        public int Line { get; set; } = -1;
        public string Member { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public LogEntity()
        {
            //
        }

        public LogEntity(DateTime dt)
        {
            CreateDt = dt;
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

        public void Load(Guid uid)
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query = "select [UID],[CREATE_DT],[FILE],[LINE],[MEMBER],[ICON],[MESSAGE] from [db_scales].[LOGS] where [UID]=@UID";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@UID", uid);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Uid = uid;
                        CreateDt = SqlConnectFactory.GetValue<DateTime>(reader, "CREATE_DT");
                        File = SqlConnectFactory.GetValue<string>(reader, "FILE");
                        Line = SqlConnectFactory.GetValue<int>(reader, "LINE");
                        Member = SqlConnectFactory.GetValue<string>(reader, "MEMBER");
                        Icon = SqlConnectFactory.GetValue<string>(reader, "ICON");
                        Message = SqlConnectFactory.GetValue<string>(reader, "MESSAGE");
                    }
                    reader.Close();
                    con.Close();
                }
            }
        }

        public void Save()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query = @"insert into [db_scales].[LOGS]([FILE],[LINE],[MEMBER],[ICON],[MESSAGE]) values (@File,@Line,@Member,@Icon,@Message)";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@File", File);
                    cmd.Parameters.AddWithValue("@Line", Line);
                    cmd.Parameters.AddWithValue("@Member", Member);
                    cmd.Parameters.AddWithValue("@Icon", Icon);
                    cmd.Parameters.AddWithValue("@Message", Message);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public static void Save(string file, int line, string member, string icon, string message)
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query = @"insert into [db_scales].[LOGS]([METHOD],[ICON],[MESSAGE]) values (@File,@Line,@Member,@Icon,@Message)";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
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

        public static void SaveInfo(string file, int line, string member, string message)
        {
            Save(file, line, member, "Information", message);
        }

        public static void SaveError(string file, int line, string member, string message)
        {
            Save(file, line, member, "Error", message);
        }

        public static void SaveWarning(string file, int line, string member, string message)
        {
            Save(file, line, member, "Warning", message);
        }

        public static void SaveQuestion(string file, int line, string member, string message)
        {
            Save(file, line, member, "Question", message);
        }

        #endregion
    }
}