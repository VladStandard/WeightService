﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataProjectsCore.DAL.TableModels
{
    [Serializable]
    public class NomenclatureUnitDirect : BaseSerializeEntity<NomenclatureUnitDirect>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string RRefID { get; set; }

        public NomenclatureDirect Nomenclature { get; set; }
        public bool Marked { get; set; }
        public decimal PackWeight { get; set; }
        public int PackQuantly { get; set; }
        public NomenclatureDirect PackType { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is NomenclatureUnitDirect item))
            {
                return false;
            }
            return Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public NomenclatureUnitDirect()
        {
        }

        public NomenclatureUnitDirect(int _Id)
        {
            Id = _Id;
            Load();
        }

        public void Load()
        {
            using SqlConnection con = SqlConnectFactory.GetConnection();
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
                        Id = SqlConnectFactory.GetValue<int>(reader, "ID");
                        Name = SqlConnectFactory.GetValue<string>(reader, "Name");
                        CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate");
                        ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate");
                        RRefID = SqlConnectFactory.GetValue<string>(reader, "RRefID");
                        Marked = SqlConnectFactory.GetValue<bool>(reader, "Marked");
                        PackWeight = SqlConnectFactory.GetValue<decimal>(reader, "PackWeight");
                        PackQuantly = SqlConnectFactory.GetValue<int>(reader, "PackQuantly");
                        PackType = new NomenclatureDirect(SqlConnectFactory.GetValue<int>(reader, "PackTypeId"));
                        Nomenclature = new NomenclatureDirect(SqlConnectFactory.GetValue<int>(reader, "NomenclatureId"));
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
                cmd.Parameters.AddWithValue($"@Marked", Marked);  // 
                cmd.Parameters.AddWithValue($"@PackWeight", PackWeight);  // 
                cmd.Parameters.AddWithValue($"@PackQuantly", PackQuantly);  // 
                cmd.Parameters.AddWithValue($"@PackTypeId", PackType == null ? PackType.Id : (object)DBNull.Value);  // 
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        Id = SqlConnectFactory.GetValue<int>(reader, "Id");
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        public static List<NomenclatureUnitDirect> GetList()
        {
            List<NomenclatureUnitDirect> result = new();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
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
                                Id = SqlConnectFactory.GetValue<int>(reader, "Id"),
                                Name = SqlConnectFactory.GetValue<string>(reader, "Name"),
                                CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate"),
                                ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate"),
                                RRefID = SqlConnectFactory.GetValue<string>(reader, "1CRRefID"),
                                Marked = SqlConnectFactory.GetValue<bool>(reader, "Marked"),
                                PackWeight = SqlConnectFactory.GetValue<decimal>(reader, "PackWeight"),
                                PackQuantly = SqlConnectFactory.GetValue<int>(reader, "PackQuantly"),
                                PackType = new NomenclatureDirect(SqlConnectFactory.GetValue<int>(reader, "PackTypeId")),
                                Nomenclature = new NomenclatureDirect(SqlConnectFactory.GetValue<int>(reader, "NomenclatureId")),
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
    }
}