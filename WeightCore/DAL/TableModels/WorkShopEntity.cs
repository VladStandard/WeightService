// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace WeightCore.DAL.TableModels
{
    [Serializable]
    public class WorkShopEntity : BaseEntity<WorkShopEntity>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ProductionFacilityEntity ProductionFacility { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string RRefID { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is WorkShopEntity item))
            {
                return false;
            }
            return Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public WorkShopEntity()
        {
        }

        public WorkShopEntity(int _Id)
        {
            Id = _Id;
            Load();
        }

        public void Load()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT [Id] ,[Name] ,[ProductionFacilityID] ,[CreateDate] ,[ModifiedDate] ,[1CRRefID]  FROM [db_scales].[GetWorkShop] (default,@Id);";
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
                                Id             = SqlConnectFactory.GetValue<int>(reader, "ID");
                                Name           = SqlConnectFactory.GetValue<string>(reader, "Name");
                                CreateDate     = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate");
                                ModifiedDate   = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate");
                                RRefID         = SqlConnectFactory.GetValue<string>(reader, "RRefID");
                            }
                        }
                        ProductionFacility = new ProductionFacilityEntity(SqlConnectFactory.GetValue<int>(reader, "ProductionFacilityID"));
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
                    EXECUTE @RC = [db_scales].[SetWorkShop] 
                       @Name
                      ,@ProductionFacilityID
                      ,@1CRRefID
                       @ID OUTPUT;
                    SELECT @ID";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue($"@Name", Name ?? (object)DBNull.Value);               // 
                    cmd.Parameters.AddWithValue($"@ProductionFacilityID", ProductionFacility.Id);      // 
                    cmd.Parameters.AddWithValue($"@1CRRefID", RRefID ?? (object)DBNull.Value);         // @1CRRefID
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

        public static List<WorkShopEntity> GetList(ProductionFacilityEntity productionFacility)
        {
            List<WorkShopEntity> result = new List<WorkShopEntity>();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT [Id] ,[Name] ,[ProductionFacilityID] ,[CreateDate] ,[ModifiedDate] ,[1CRRefID]  FROM [db_scales].[GetWorkShop] (@Id,default);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", productionFacility.Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                WorkShopEntity workShop = new WorkShopEntity()
                                {
                                    Id = SqlConnectFactory.GetValue<int>(reader, "Id"),
                                    Name = SqlConnectFactory.GetValue<string>(reader, "Name"),
                                    CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate"),
                                    ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate"),
                                    RRefID = SqlConnectFactory.GetValue<string>(reader, "1CRRefID")
                                };
                                workShop.ProductionFacility = productionFacility;
                                result.Add(workShop);
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