// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataCore.DAL.TableDirectModels
{
    [Serializable]
    public class NomenclatureDirect : BaseSerializeEntity<NomenclatureDirect>
    {
        public int Id { get; set; } = default;
        public string? Name { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = default;
        public DateTime ModifiedDate { get; set; } = default;
        public string? RRefID { get; set; } = default;
        public string? Code { get; set; } = default;
        public bool Marked { get; set; } = default;
        public string NameFull { get; set; } = "";
        public string Description { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string GUID_Mercury { get; set; } = string.Empty;
        public string NomenclatureType { get; set; } = string.Empty;
        public string VATRate { get; set; } = string.Empty;

        public NomenclatureDirect()
        {
            Load();
        }

        public NomenclatureDirect(int id)
        {
            Id = id;
            Load();
        }

        public override bool Equals(object obj)
        {
            if (obj is not NomenclatureDirect item)
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
            if (Id == default) return;
            using SqlConnection con = SqlConnectFactory.GetConnection();
            con.Open();
            string query = "SELECT * FROM [db_scales].[GetNomenclature] (@Id);";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Id", Id);
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "ID");
                        Name = SqlConnectFactory.GetValueAsString(reader, "Name");
                        CreateDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "CreateDate");
                        ModifiedDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "ModifiedDate");
                        RRefID = SqlConnectFactory.GetValueAsString(reader, "RRefID");
                        Code = SqlConnectFactory.GetValueAsString(reader, "Code");
                        Marked = SqlConnectFactory.GetValueAsNotNullable<bool>(reader, "Marked");
                        NameFull = SqlConnectFactory.GetValueAsString(reader, "NameFull");
                        Description = SqlConnectFactory.GetValueAsString(reader, "Description");
                        Comment = SqlConnectFactory.GetValueAsString(reader, "Comment");
                        Brand = SqlConnectFactory.GetValueAsString(reader, "Brand");
                        GUID_Mercury = SqlConnectFactory.GetValueAsString(reader, "GUID_Mercury");
                        NomenclatureType = SqlConnectFactory.GetValueAsString(reader, "NomenclatureType");
                        VATRate = SqlConnectFactory.GetValueAsString(reader, "VATRate");
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        public void Save()
        {

            using SqlConnection con = SqlConnectFactory.GetConnection();
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
            using (SqlCommand cmd = new(query))
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
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "ID");
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        public static List<NomenclatureDirect> GetList()
        {
            List<NomenclatureDirect> result = new();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetNomenclature] (default);";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            NomenclatureDirect nomenclature = new()
                            {
                                Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "Id"),
                                Name = SqlConnectFactory.GetValueAsString(reader, "Name"),
                                CreateDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
                                ModifiedDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "ModifiedDate"),
                                RRefID = SqlConnectFactory.GetValueAsString(reader, "1CRRefID"),
                                Code = SqlConnectFactory.GetValueAsString(reader, "Code"),
                                Marked = SqlConnectFactory.GetValueAsNotNullable<bool>(reader, "Marked"),
                                NameFull = SqlConnectFactory.GetValueAsString(reader, "NameFull"),
                                Description = SqlConnectFactory.GetValueAsString(reader, "Description"),
                                Comment = SqlConnectFactory.GetValueAsString(reader, "Comment"),
                                Brand = SqlConnectFactory.GetValueAsString(reader, "Brand"),
                                GUID_Mercury = SqlConnectFactory.GetValueAsString(reader, "GUID_Mercury"),
                                NomenclatureType = SqlConnectFactory.GetValueAsString(reader, "NomenclatureType"),
                                VATRate = SqlConnectFactory.GetValueAsString(reader, "VATRate")
                            };
                            result.Add(nomenclature);
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }
    }
}