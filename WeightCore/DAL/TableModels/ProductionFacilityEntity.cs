// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WeightCore.DAL.TableModels
{
    [Serializable]
    public class ProductionFacilityEntity : BaseEntity<ProductionFacilityEntity>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string RRefID { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is ProductionFacilityEntity item))
            {
                return false;
            }
            return Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public ProductionFacilityEntity()
        {
        }

        public ProductionFacilityEntity(int _Id)
        {
            Id = _Id;
            Load();
        }

        public void Load()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetProductionFacility] (@Id);";
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
                                CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate");
                                ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate");
                                RRefID = SqlConnectFactory.GetValue<string>(reader, "RRefID");
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
        }

        public void Save()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = @"
                    DECLARE @ID int; 
                    EXECUTE [db_scales].[SetProductionFacility]
                       @1CRRefID,
                       @Name,
                       @ID OUTPUT;
                    SELECT @ID";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue($"@Name", Name ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@1CRRefID", RRefID ?? (object)DBNull.Value);  // @1CRRefID
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Id = SqlConnectFactory.GetValue<int>(reader, "Id");
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
        }

        public static List<ProductionFacilityEntity> GetList()
        {
            List<ProductionFacilityEntity> result = new List<ProductionFacilityEntity>();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT [Id],[Name],[CreateDate],[ModifiedDate],[1CRRefID] FROM [db_scales].[GetProductionFacility] (default);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductionFacilityEntity pFacility = new ProductionFacilityEntity()
                                {
                                    Id = SqlConnectFactory.GetValue<int>(reader, "Id"),
                                    Name = SqlConnectFactory.GetValue<string>(reader, "Name"),
                                    CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate"),
                                    ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate"),
                                    RRefID = SqlConnectFactory.GetValue<string>(reader, "1CRRefID")
                                };
                                result.Add(pFacility);
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }
    }
}