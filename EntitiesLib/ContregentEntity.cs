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
    public class ContregentEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string RRefID { get; set; }
        public bool Marked { get; set; }

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ContregentEntity));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }


        public ContregentEntity()
        {
        }


        public ContregentEntity(int _Id)
        {
            this.Id = _Id;
            this.Marked = false;
            Load();
        }


        public void Load()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query = "SELECT * FROM [db_scales].[GetContragent](@Id);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", this.Id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Id = SqlConnectFactory.GetValue<int>(reader, "ID");
                        Name = SqlConnectFactory.GetValue<string>(reader, "Name");
                        CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate");
                        ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate");
                        RRefID = SqlConnectFactory.GetValue<string>(reader, "1CRRefID");
                        Marked = SqlConnectFactory.GetValue<bool>(reader, "Marked");

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
                      ,@Name
                      ,@Marked
                      ,@ID OUTPUT;

                    SELECT @ID";

                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue($"@Name", this.Name ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@Marked", this.Marked );  // 
                    cmd.Parameters.AddWithValue($"@1CRRefID", this.RRefID ?? (object)DBNull.Value);  // @1CRRefID
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

        public static List<ContregentEntity> GetList()
        {
            List<ContregentEntity> res = new List<ContregentEntity>();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query =
                    "SELECT * FROM [db_scales].[GetContragent] (default);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ContregentEntity contregent = new ContregentEntity()
                        {
                            Id = SqlConnectFactory.GetValue<int>(reader, "Id"),
                            Name = SqlConnectFactory.GetValue<string>(reader, "Name"),
                            CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate"),
                            ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate"),
                            RRefID = SqlConnectFactory.GetValue<string>(reader, "1CRRefID"),
                            Marked = SqlConnectFactory.GetValue<bool>(reader, "Marked")

                        };

                        res.Add(contregent);

                    }
                    reader.Close();

                }
            }
            return res;

        }


    }
}
