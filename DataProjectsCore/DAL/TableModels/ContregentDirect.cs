// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataProjectsCore.DAL.TableModels
{
    [Serializable]
    public class ContregentDirect : BaseSerializeEntity<ContregentDirect>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string RRefID { get; set; }
        public bool Marked { get; set; }

        public ContregentDirect()
        {
        }

        public ContregentDirect(int _Id)
        {
            Id = _Id;
            Marked = false;
            Load();
        }

        public void Load()
        {
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
                        Id = SqlConnectFactory.GetValue<int>(reader, "ID");
                        Name = SqlConnectFactory.GetValue<string>(reader, "Name");
                        CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate");
                        ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate");
                        RRefID = SqlConnectFactory.GetValue<string>(reader, "1CRRefID");
                        Marked = SqlConnectFactory.GetValue<bool>(reader, "Marked");
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
                        Id = SqlConnectFactory.GetValue<int>(reader, "Id");
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
                                Id = SqlConnectFactory.GetValue<int>(reader, "Id"),
                                Name = SqlConnectFactory.GetValue<string>(reader, "Name"),
                                CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate"),
                                ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate"),
                                RRefID = SqlConnectFactory.GetValue<string>(reader, "1CRRefID"),
                                Marked = SqlConnectFactory.GetValue<bool>(reader, "Marked")
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
