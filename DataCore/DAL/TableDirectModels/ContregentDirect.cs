// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DataCore.DAL.TableDirectModels
{
    [Serializable]
    public class ContregentDirect : BaseSerializeEntity<ContregentDirect>
    {
        #region Public and private fields and properties

        public long Id { get; set; } = default;
        public string Name { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string RRefID { get; set; } = string.Empty;
        public bool IsMarked { get; set; } = false;
        [XmlIgnore]
        public SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

        #endregion

        #region Constructor and destructor

        public ContregentDirect()
        {
            Load();
        }

        public ContregentDirect(long id)
        {
            Id = id;
            IsMarked = false;
            Load();
        }

        #endregion

        #region Public and private methods

        public void Load()
        {
            if (Id == default) return;
            using SqlConnection con = SqlConnect.GetConnection();
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
                        Id = SqlConnect.GetValueAsNotNullable<long>(reader, "ID");
                        Name = SqlConnect.GetValueAsString(reader, "Name");
                        CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate");
                        ModifiedDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ModifiedDate");
                        RRefID = SqlConnect.GetValueAsString(reader, "1CRRefID");
                        IsMarked = SqlConnect.GetValueAsNotNullable<bool>(reader, "Marked");
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
                      ,@Name
                      ,@Marked
                      ,@ID OUTPUT;
                    SELECT @ID";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue($"@Name", Name ?? (object)DBNull.Value);  // 
                cmd.Parameters.AddWithValue($"@Marked", IsMarked);  // 
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

        public List<ContregentDirect> GetList()
        {
            List<ContregentDirect> result = new();
            using (SqlConnection con = SqlConnect.GetConnection())
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
                                Id = SqlConnect.GetValueAsNotNullable<long>(reader, "Id"),
                                Name = SqlConnect.GetValueAsString(reader, "Name"),
                                CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
                                ModifiedDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ModifiedDate"),
                                RRefID = SqlConnect.GetValueAsString(reader, "1CRRefID"),
                                IsMarked = SqlConnect.GetValueAsNotNullable<bool>(reader, "Marked")
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

        #endregion
    }
}
