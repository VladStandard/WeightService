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
    public class BarCodeTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BarCodeTypeEntity));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }


        public BarCodeTypeEntity()
        {
        }


        public BarCodeTypeEntity(int _Id)
        {
            this.Id = _Id;
            Load();
        }


        public void Load()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query = "SELECT * FROM [db_scales].[GetBarCodeType](@Id);";
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

                    }

                    reader.Close();
                    con.Close();
                }
            }
        }


    }
}
