// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataCore.DAL.TableDirectModels
{
    public class NomenclatureUnitDirect : BaseSerializeEntity
    {
        #region Public and private fields and properties

        public long Id { get; set; } = default;
        public string Name { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime ChangeDt { get; set; }
        public string RRefID { get; set; } = string.Empty;
        public NomenclatureDirect Nomenclature { get; set; } = new NomenclatureDirect();
        public bool IsMarked { get; set; } = false;
        public decimal PackWeight { get; set; }
        public int PackQuantly { get; set; }
        public NomenclatureDirect PackType { get; set; } = new NomenclatureDirect();

        #endregion

        #region Constructor and destructor

        public NomenclatureUnitDirect()
        {
            Load(default);
        }

        public NomenclatureUnitDirect(long id)
        {
            Load(id);
        }

        #endregion

        #region Public and private methods

        public override bool Equals(object obj)
        {
            if (obj is not NomenclatureUnitDirect item)
            {
                return false;
            }
            return Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void Load(long id)
        {
            if (id == default) return;
            Id = id;
            using SqlConnection con = SqlConnect.GetConnection();
            con.Open();
            string query = "SELECT * FROM [db_scales].[GetNomenclatureUnit] (@Id);";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Id", Id);
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Id = SqlConnect.GetValueAsNotNullable<long>(reader, "ID");
                        Name = SqlConnect.GetValueAsString(reader, "Name");
                        CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate");
                        ChangeDt = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ChangeDt");
                        RRefID = SqlConnect.GetValueAsString(reader, "RRefID");
                        IsMarked = SqlConnect.GetValueAsNotNullable<bool>(reader, "Marked");
                        PackWeight = SqlConnect.GetValueAsNotNullable<decimal>(reader, "PackWeight");
                        PackQuantly = SqlConnect.GetValueAsNotNullable<int>(reader, "PackQuantly");
                        PackType = new NomenclatureDirect(SqlConnect.GetValueAsNotNullable<int>(reader, "PackTypeId"));
                        Nomenclature = new NomenclatureDirect(SqlConnect.GetValueAsNotNullable<int>(reader, "NomenclatureId"));
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        public void Save()
        {
            using SqlConnection con = SqlConnect.GetConnection();
            con.Open();
            string query = @"
DECLARE @ID int; 
EXECUTE [db_scales].[SetNomenclatureUnit]
    @1CRRefID,
    @Name,
    @NomenclatureId  ,
    @Marked          ,
    @PackWeight      ,
    @PackQuantly     ,
    @PackTypeId      ,
    @ID OUTPUT;
SELECT @ID";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue($"@1CRRefID", RRefID ?? (object)DBNull.Value);  // @1CRRefID
                cmd.Parameters.AddWithValue($"@Name", Name ?? (object)DBNull.Value);  // 
                cmd.Parameters.AddWithValue($"@NomenclatureId", Nomenclature.Id);  // 
                cmd.Parameters.AddWithValue($"@Marked", IsMarked);  // 
                cmd.Parameters.AddWithValue($"@PackWeight", PackWeight);  // 
                cmd.Parameters.AddWithValue($"@PackQuantly", PackQuantly);  // 
                cmd.Parameters.AddWithValue($"@PackTypeId", PackType != null ? PackType.Id : DBNull.Value);  // 
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        Id = SqlConnect.GetValueAsNotNullable<long>(reader, "Id");
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        public List<NomenclatureUnitDirect> GetList()
        {
            List<NomenclatureUnitDirect> result = new();
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetNomenclatureUnit] (default);";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            NomenclatureUnitDirect pFacility = new()
                            {
                                Id = SqlConnect.GetValueAsNotNullable<int>(reader, "Id"),
                                Name = SqlConnect.GetValueAsString(reader, "Name"),
                                CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
                                ChangeDt = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ChangeDt"),
                                RRefID = SqlConnect.GetValueAsString(reader, "1CRRefID"),
                                IsMarked = SqlConnect.GetValueAsNotNullable<bool>(reader, "Marked"),
                                PackWeight = SqlConnect.GetValueAsNotNullable<decimal>(reader, "PackWeight"),
                                PackQuantly = SqlConnect.GetValueAsNotNullable<int>(reader, "PackQuantly"),
                                PackType = new NomenclatureDirect(SqlConnect.GetValueAsNotNullable<int>(reader, "PackTypeId")),
                                Nomenclature = new NomenclatureDirect(SqlConnect.GetValueAsNotNullable<int>(reader, "NomenclatureId")),
                            };
                            result.Add(pFacility);
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        #endregion
    }
}
