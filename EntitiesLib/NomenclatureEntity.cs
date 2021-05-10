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
    public class NomenclatureEntity
    {

        public Int32 Id         { get; set; }
        public string Name          { get; set; }
        public DateTime CreateDate  { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string RRefID        { get; set; }
        public string  Code         { get; set; }
        public Boolean  Marked      { get; set; }
        public string  NameFull     { get; set; }
        public string  Description  { get; set; }
        public string  Comment      { get; set; }
        public string  Brand        { get; set; }
        public string  GUID_Mercury { get; set; }
        public string  NomenclatureType { get; set; }
        public string  VATRate       { get; set; }


        public NomenclatureEntity()
        {
        }

        public NomenclatureEntity(int _Id)
        {
            this.Id = _Id;
            Load();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is NomenclatureEntity item))
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
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(NomenclatureEntity));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }


        public void Load()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query = "SELECT * FROM [db_scales].[GetNomenclature] (@Id);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", this.Id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Id                      = SqlConnectFactory.GetValue<int>(reader, "ID");
                        Name                    = SqlConnectFactory.GetValue<string>(reader, "Name");
                        CreateDate              = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate");
                        ModifiedDate            = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate");
                        RRefID                  = SqlConnectFactory.GetValue<string>(reader, "RRefID");
                        Code                    = SqlConnectFactory.GetValue<string>(reader, "Code");
                        Marked                  = SqlConnectFactory.GetValue<bool>(reader, "Marked");
                        NameFull                = SqlConnectFactory.GetValue<string>(reader, "NameFull");
                        Description             = SqlConnectFactory.GetValue<string>(reader, "Description");
                        Comment                 = SqlConnectFactory.GetValue<string>(reader, "Comment");
                        Brand                   = SqlConnectFactory.GetValue<string>(reader, "Brand");
                        GUID_Mercury            = SqlConnectFactory.GetValue<string>(reader, "GUID_Mercury");
                        NomenclatureType        = SqlConnectFactory.GetValue<string>(reader, "NomenclatureType");
                        VATRate                 = SqlConnectFactory.GetValue<string>(reader, "VATRate");

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
                    
                    EXECUTE  [db_scales].[SetNomenclature] 
                       @1CRRefID
                      ,@Code
                      ,@Marked
                      ,@Name
                      ,@NameFull
                      ,@Description
                      ,@Comment
                      ,@Brand
                      ,@GUID_Mercury
                      ,@NomenclatureType
                      ,@VATRate
                      ,@ID OUTPUT

                    SELECT @ID as ID";

                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue($"@1CRRefID", this.RRefID ?? (object)DBNull.Value);  // @1CRRefID
                    cmd.Parameters.AddWithValue($"@Code "            , this.Code ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@Marked", this.Marked);  // 
                    cmd.Parameters.AddWithValue($"@Name", this.Name ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@NameFull", this.NameFull ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@Description", this.Description ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@Comment", this.Comment ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@Brand", this.Brand ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@GUID_Mercury", this.GUID_Mercury ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@NomenclatureType", this.NomenclatureType ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@VATRate", this.VATRate ?? (object)DBNull.Value);  // 

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        this.Id = SqlConnectFactory.GetValue<int>(reader, "ID");
                    }

                    reader.Close();

                }

                con.Close();

            }

        }

        public static List<NomenclatureEntity> GetList()
        {
            List<NomenclatureEntity> res = new List<NomenclatureEntity>();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query =
                    "SELECT * FROM [db_scales].[GetNomenclature] (default);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        NomenclatureEntity nomenclature = new NomenclatureEntity()
                        {
                            Id = SqlConnectFactory.GetValue<int>(reader, "Id"),
                            Name = SqlConnectFactory.GetValue<string>(reader, "Name"),
                            CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate"),
                            ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate"),
                            RRefID = SqlConnectFactory.GetValue<string>(reader, "1CRRefID"),
                            Code = SqlConnectFactory.GetValue<string>(reader, "Code"),
                            Marked = SqlConnectFactory.GetValue<bool>(reader, "Marked"),
                            NameFull = SqlConnectFactory.GetValue<string>(reader, "NameFull"),
                            Description = SqlConnectFactory.GetValue<string>(reader, "Description"),
                            Comment = SqlConnectFactory.GetValue<string>(reader, "Comment"),
                            Brand = SqlConnectFactory.GetValue<string>(reader, "Brand"),
                            GUID_Mercury = SqlConnectFactory.GetValue<string>(reader, "GUID_Mercury"),
                            NomenclatureType = SqlConnectFactory.GetValue<string>(reader, "NomenclatureType"),
                            VATRate = SqlConnectFactory.GetValue<string>(reader, "VATRate")


                    };

                        res.Add(nomenclature);

                    }
                    reader.Close();

                }
            }
            return res;
        }


    }
}
