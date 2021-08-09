// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace WeightCore.Db
{
    [Serializable]
    public class LogTypeEntity
    {
        #region Public and private fields and properties

        public Guid Uid { get; private set; }
        public byte Number { get; private set; }
        public string Icon { get; private set; }

        #endregion

        #region Constructor and destructor

        public LogTypeEntity(Guid uid, byte number, string icon)
        {
            Uid = uid;
            Number = number;
            Icon = icon;
        }

        #endregion

        #region Public and private methods

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LogTypeEntity));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }

        public void Save(byte number, string icon)
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                Utils.StringValueTrim(ref icon, 32);
                string query = @"
insert into [db_scales].[LOG_TYPES]([NUMBER],[ICON]) 
values (@number,@icon)
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n').Replace(Environment.NewLine, " ");
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@number", number);
                    cmd.Parameters.AddWithValue("@icon", icon);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public void Save()
        {
            Save(Number, Icon);
        }

        #endregion
    }
}