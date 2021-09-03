// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;

namespace DataProjectsCore.DAL.TableModels
{
    [Serializable]
    public class BarCodeTypeDirect : BaseSerializeEntity<BarCodeTypeDirect>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BarCodeTypeDirect()
        {
        }

        public BarCodeTypeDirect(int _Id)
        {
            Id = _Id;
            Load();
        }

        public void Load()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetBarCodeType](@Id);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Id = SqlConnectFactory.GetValue<int>(reader, "ID");
                                Name = SqlConnectFactory.GetValue<string>(reader, "Name");
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
