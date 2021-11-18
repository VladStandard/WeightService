// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;

namespace DataProjectsCore.DAL.TableModels
{
    [Serializable]
    public class ZebraPrinterHelper : BaseSerializeEntity<ZebraPrinterHelper>
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static ZebraPrinterHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static ZebraPrinterHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public int Id { get; set; }
        public virtual string Name { get; set; } = "";
        public virtual string Ip { get; set; } = "";
        public virtual short Port { get; set; } = default;
        public virtual string Mac { get; set; } = "";
        public virtual string Password { get; set; } = "";
        public virtual string PrinterType { get; set; } = "";
        [XmlIgnore]
        public Dictionary<string, string> Fonts { get; set; } = new Dictionary<string, string>();
        [XmlIgnore]
        public Dictionary<string, string> Logo { get; set; } = new Dictionary<string, string>();

        #endregion

        public void Setup(int? id)
        {
            if (id != null)
            {
                Id = (int)id;
                Load();
            }
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
                    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    using SqlDataReader reader = cmd.ExecuteReader();
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
                con.Close();
            }

            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = @"
select [Name],MAX([ImageData]) [ImageData] 
from [db_scales].[GetPrinterResources] (@ID,@Type)
group by [Name]
                    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                using (SqlCommand cmd = new(query))
                {
                    Logo.Clear();
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("@Type", "GRF");
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Logo.Add(reader.GetString(0), reader.GetString(1));
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }

            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = @"
select [Name], [ImageData] 
from [db_scales].[GetPrinterResources] (@ID,@Type)
                    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                using (SqlCommand cmd = new(query))
                {
                    Fonts.Clear();
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("@Type", "TTF");
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Fonts.Add(reader.GetString(0), reader.GetString(1));
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
        }
    }
}
