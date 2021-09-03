// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DataProjectsCore.DAL.TableModels
{
    [Serializable]
    public class ZebraPrinterDirect : BaseSerializeEntity<ZebraPrinterDirect>
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Ip { get; set; }
        public virtual short Port { get; set; }
        public virtual string Mac { get; set; }
        public virtual string Password { get; set; }
        public virtual string PrinterType { get; set; }

        [XmlIgnore]
        public Dictionary<string, string> Fonts { get; set; }

        [XmlIgnore]
        public Dictionary<string, string> Logo { get; set; }

        public ZebraPrinterDirect()
        {
            Init();
        }

        public ZebraPrinterDirect(int? id)
        {
            Init();
            if (id != null)
            {
                Id = (int)id;
                Load();
            }
        }

        private void Init()
        {
            Fonts = new Dictionary<string, string>();
            Logo = new Dictionary<string, string>();
        }

        public void Load()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = @"
SELECT x.Id,x.Name,x.Ip,x.Port,x.Password,x.Mac,y.Name as PrinterType
FROM [db_scales].[ZebraPrinter] x
INNER JOIN[db_scales].[ZebraPrinterType] y
ON x.[PrinterTypeId] = y.Id
WHERE x.[Id] = @ID;
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Ip = SqlConnectFactory.GetValue<string>(reader, "Ip");
                                Port = SqlConnectFactory.GetValue<short>(reader, "Port");
                                Mac = SqlConnectFactory.GetValue<string>(reader, "Mac");
                                Name = SqlConnectFactory.GetValue<string>(reader, "Name");
                                Password = SqlConnectFactory.GetValue<string>(reader, "Password");
                                PrinterType = SqlConnectFactory.GetValue<string>(reader, "PrinterType");
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }

            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = @"
select [Name],MAX([ImageData]) [ImageData] 
from [db_scales].[GetPrinterResources] (@ID,@Type)
group by [Name]
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    Logo.Clear();
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("@Type", "GRF");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Logo.Add(reader.GetString(0), reader.GetString(1));
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }

            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = @"
select [Name], [ImageData] 
from [db_scales].[GetPrinterResources] (@ID,@Type)
                    ".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    Fonts.Clear();
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("@Type", "TTF");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Fonts.Add(reader.GetString(0), reader.GetString(1));
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
        }
    }
}