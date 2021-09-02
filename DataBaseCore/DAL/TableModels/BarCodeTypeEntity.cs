// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Data.SqlClient;
using System;

namespace DataBaseCore.DAL.TableModels
{
    [Serializable]
    public class BarCodeTypeEntity : BaseEntity<BarCodeTypeEntity>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BarCodeTypeEntity()
        {
        }

        public BarCodeTypeEntity(int _Id)
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
