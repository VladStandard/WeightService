using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace EntitiesLib
{

    [Serializable]

    public class ProductionFacilityEntity
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

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProductionFacilityEntity));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
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
                string query = "SELECT * FROM [db_scales].[GetProductionFacility] (@Id);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Id                   = SqlConnectFactory.GetValue<int>(reader, "ID");
                        Name                 = SqlConnectFactory.GetValue<string>(reader, "Name");
                        CreateDate           = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate");
                        ModifiedDate         = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate");
                        RRefID               = SqlConnectFactory.GetValue<string>(reader, "RRefID");

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
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Id = SqlConnectFactory.GetValue<int>(reader, "Id");
                    }

                    reader.Close();

                }

                con.Close();

            }

        }

        public static List<ProductionFacilityEntity> GetList()
        {
            List<ProductionFacilityEntity> res = new List<ProductionFacilityEntity>();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query =
                    "SELECT [Id],[Name],[CreateDate],[ModifiedDate],[1CRRefID] FROM [db_scales].[GetProductionFacility] (default);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
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

                        res.Add(pFacility);

                    }
                    reader.Close();

                }
            }
            return res;

        }



    }
}
