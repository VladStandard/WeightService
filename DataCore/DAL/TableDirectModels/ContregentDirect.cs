// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataCore.DAL.TableDirectModels
{
    [Serializable]
    public class ContregentDirect : BaseSerializeEntity<ContregentDirect>
    {
        public int Id { get; set; } = default;
        public string Name { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string RRefID { get; set; } = string.Empty;
        public bool Marked { get; set; }

        public ContregentDirect()
        {
            Load();
        }

        public ContregentDirect(int id)
        {
            Id = id;
            Marked = false;
            Load();
        }

        public void Load()
        {
            if (Id == default) return;
            using SqlConnection con = SqlConnectFactory.GetConnection();
            con.Open();
            string query = "SELECT * FROM [db_scales].[GetContragent](@Id);";
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
                        RRefID = SqlConnectFactory.GetValueAsString(reader, "1CRRefID");
                        Marked = SqlConnectFactory.GetValueAsNotNullable<bool>(reader, "Marked");
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
                    EXECUTE [db_scales].[SetProductionFacility]
                       @1CRRefID,
                      ,@Name
                      ,@Marked
                      ,@ID OUTPUT;
                    SELECT @ID";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue($"@Name", Name ?? (object)DBNull.Value);  // 
                cmd.Parameters.AddWithValue($"@Marked", Marked);  // 
                cmd.Parameters.AddWithValue($"@1CRRefID", RRefID ?? (object)DBNull.Value);  // @1CRRefID
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "Id");
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        public static List<ContregentDirect> GetList()
        {
            List<ContregentDirect> result = new();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetContragent] (default);";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ContregentDirect contregent = new()
                            {
                                Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "Id"),
                                Name = SqlConnectFactory.GetValueAsString(reader, "Name"),
                                CreateDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
                                ModifiedDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "ModifiedDate"),
                                RRefID = SqlConnectFactory.GetValueAsString(reader, "1CRRefID"),
                                Marked = SqlConnectFactory.GetValueAsNotNullable<bool>(reader, "Marked")
                            };
                            result.Add(contregent);
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
