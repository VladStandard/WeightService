// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataProjectsCore.DAL.TableModels
{
    [Serializable]
    public class NomenclatureDirect : BaseSerializeEntity<NomenclatureDirect>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string RRefID { get; set; }
        public string Code { get; set; }
        public bool Marked { get; set; }
        public string NameFull { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Brand { get; set; }
        public string GUID_Mercury { get; set; }
        public string NomenclatureType { get; set; }
        public string VATRate { get; set; }

        public NomenclatureDirect()
        {
        }

        public NomenclatureDirect(int _Id)
        {
            Id = _Id;
            Load();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is NomenclatureDirect item))
            {
                return false;
            }
            return Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void Load()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetNomenclature] (@Id);";
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
                                Code = SqlConnectFactory.GetValue<string>(reader, "Code");
                                Marked = SqlConnectFactory.GetValue<bool>(reader, "Marked");
                                NameFull = SqlConnectFactory.GetValue<string>(reader, "NameFull");
                                Description = SqlConnectFactory.GetValue<string>(reader, "Description");
                                Comment = SqlConnectFactory.GetValue<string>(reader, "Comment");
                                Brand = SqlConnectFactory.GetValue<string>(reader, "Brand");
                                GUID_Mercury = SqlConnectFactory.GetValue<string>(reader, "GUID_Mercury");
                                NomenclatureType = SqlConnectFactory.GetValue<string>(reader, "NomenclatureType");
                                VATRate = SqlConnectFactory.GetValue<string>(reader, "VATRate");
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
                    cmd.Parameters.AddWithValue($"@1CRRefID", RRefID ?? (object)DBNull.Value);  // @1CRRefID
                    cmd.Parameters.AddWithValue($"@Code ", Code ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@Marked", Marked);  // 
                    cmd.Parameters.AddWithValue($"@Name", Name ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@NameFull", NameFull ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@Description", Description ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@Comment", Comment ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@Brand", Brand ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@GUID_Mercury", GUID_Mercury ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@NomenclatureType", NomenclatureType ?? (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@VATRate", VATRate ?? (object)DBNull.Value);  // 
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Id = SqlConnectFactory.GetValue<int>(reader, "ID");
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
        }

        public static List<NomenclatureDirect> GetList()
        {
            List<NomenclatureDirect> result = new List<NomenclatureDirect>();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetNomenclature] (default);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                NomenclatureDirect nomenclature = new NomenclatureDirect()
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
                                result.Add(nomenclature);
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