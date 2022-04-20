﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataCore.DAL.TableDirectModels
{
    public class ProductionFacilityDirect : BaseSerializeEntity
    {
        #region Public and private fields and properties

        public long Id { get; set; } = default;
        public string Name { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime ChangeDt { get; set; }
        public string RRefID { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public ProductionFacilityDirect()
        {
            Load();
        }

        public ProductionFacilityDirect(long id)
        {
            Id = id;
            Load();
        }

        #endregion

        #region Public and private methods

        public override bool Equals(object obj)
        {
            if (obj is not ProductionFacilityDirect item)
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
            using SqlConnection con = SqlConnect.GetConnection();
            con.Open();
            string query = "SELECT * FROM [db_scales].[GetProductionFacility] (@Id);";
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
EXECUTE [db_scales].[SetProductionFacility]
    @1CRRefID,
    @Name,
    @ID OUTPUT;
SELECT @ID";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue($"@Name", Name ?? (object)DBNull.Value);  // 
                cmd.Parameters.AddWithValue($"@1CRRefID", RRefID ?? (object)DBNull.Value);  // @1CRRefID
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Id = SqlConnect.GetValueAsNotNullable<long>(reader, "Id");
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        public List<ProductionFacilityDirect> GetList()
        {
            List<ProductionFacilityDirect> result = new();
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                string query = "SELECT [Id],[Name],[CreateDate],[ModifiedDate],[IdRRef] FROM [db_scales].[GetProductionFacility] (default);";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ProductionFacilityDirect pFacility = new()
                            {
                                Id = SqlConnect.GetValueAsNotNullable<long>(reader, "Id"),
                                Name = SqlConnect.GetValueAsString(reader, "Name"),
                                CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
                                ChangeDt = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ChangeDt"),
                                RRefID = SqlConnect.GetValueAsString(reader, "1CRRefID")
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
