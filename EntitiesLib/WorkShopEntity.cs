using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WeightServices.Common;

namespace EntitiesLib
{
    [Serializable]

    public class WorkShopEntity
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

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkShopEntity));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }


        public WorkShopEntity()
        {
        }

        public WorkShopEntity(int _Id)
        {
            this.Id = _Id;
            Load();
        }

        public void Load()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query = "SELECT [Id] ,[Name] ,[ProductionFacilityID] ,[CreateDate] ,[ModifiedDate] ,[1CRRefID]  FROM [db_scales].[GetWorkShop] (default,@Id);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", this.Id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        this.Id             = SqlConnectFactory.GetValue<int>(reader, "ID");
                        this.Name           = SqlConnectFactory.GetValue<string>(reader, "Name");
                        this.CreateDate     = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate");
                        this.ModifiedDate   = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate");
                        this.RRefID         = SqlConnectFactory.GetValue<string>(reader, "RRefID");

                    }

                    this.ProductionFacility = new ProductionFacilityEntity( SqlConnectFactory.GetValue<int>(reader, "ProductionFacilityID"));

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
                    cmd.Parameters.AddWithValue($"@Name", this.Name ?? (object)DBNull.Value);               // 
                    cmd.Parameters.AddWithValue($"@ProductionFacilityID", this.ProductionFacility.Id);      // 
                    cmd.Parameters.AddWithValue($"@1CRRefID", this.RRefID ?? (object)DBNull.Value);         // @1CRRefID
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        this.Id = SqlConnectFactory.GetValue<int>(reader, "Id");
                    }

                    reader.Close();

                }

                con.Close();

            }

        }

        public static List<WorkShopEntity> GetList(ProductionFacilityEntity productionFacility)
        {
            List<WorkShopEntity> res = new List<WorkShopEntity>();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query =
                    "SELECT [Id] ,[Name] ,[ProductionFacilityID] ,[CreateDate] ,[ModifiedDate] ,[1CRRefID]  FROM [db_scales].[GetWorkShop] (@Id,default);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", productionFacility.Id);

                    SqlDataReader reader = cmd.ExecuteReader();
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
                        res.Add(workShop);

                    }
                    reader.Close();

                }
            }
            return res;

        }



    }
}
