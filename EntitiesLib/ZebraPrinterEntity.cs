using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace EntitiesLib
{
    [Serializable]
    public class ZebraPrinterEntity
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Ip { get; set; }
        public virtual short  Port { get; set; }
        public virtual string Mac { get; set; }
        public virtual string Password { get; set; }
        
        public virtual string PrinterType { get; set; }

        [XmlIgnore]
        public Dictionary<string, string> Fonts { get; set; }

        [XmlIgnore]
        public Dictionary<string, string> Logo { get; set; }

        public ZebraPrinterEntity()
        {
            Init();
        }

        public ZebraPrinterEntity(int? id)
        {
            Init();
            if (id != null) {
                Id = (int)id;
                Load();
            }
        }

        private void Init()
        {
            Fonts = new Dictionary<string, string>();
            Logo = new Dictionary<string, string>();
        }

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ScaleEntity));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }

        }

        public void Load()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query = @"
SELECT x.Id,x.Name,x.Ip,x.Port,x.Password,x.Mac,y.Name as PrinterType
FROM[ScalesDB].[db_scales].[ZebraPrinter] x
INNER JOIN[db_scales].[ZebraPrinterType] y
ON x.[PrinterTypeId] = y.Id
WHERE x.[Id] = @ID;
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Ip = SqlConnectFactory.GetValue<string>(reader, "Ip");
                        Port = SqlConnectFactory.GetValue<short>(reader, "Port");
                        Mac = SqlConnectFactory.GetValue<string>(reader, "Mac");
                        Name = SqlConnectFactory.GetValue<string>(reader, "Name");
                        Password = SqlConnectFactory.GetValue<string>(reader, "Password");
                        PrinterType = SqlConnectFactory.GetValue<string>(reader, "PrinterType");

                    }

                    reader.Close();
                    con.Close();
                }

            }

            using (var con = SqlConnectFactory.GetConnection())
            {
                var query = @"
select [Name],MAX([ImageData]) [ImageData] 
from [db_scales].[GetPrinterResources] (@ID,@Type)
group by [Name]
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
                using (var cmd = new SqlCommand(query))
                {
                    Logo.Clear();
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("@Type", "GRF");
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Logo.Add(reader.GetString(0), reader.GetString(1));
                    }
                    reader.Close();
                }
            }

            using (var con = SqlConnectFactory.GetConnection())
            {
                var query = @"
select [Name], [ImageData] 
from [db_scales].[GetPrinterResources] (@ID,@Type)
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
                using (var cmd = new SqlCommand(query))
                {
                    Fonts.Clear();
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("@Type", "TTF");
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Fonts.Add(reader.GetString(0), reader.GetString(1));
                    }
                    reader.Close();
                }
            }
        }
    }
}