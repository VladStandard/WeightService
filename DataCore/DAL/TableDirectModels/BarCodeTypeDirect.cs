// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;

namespace DataCore.DAL.TableDirectModels
{
    [Serializable]
    public class BarCodeTypeDirect : BaseSerializeEntity<BarCodeTypeDirect>
    {
        #region Public and private fields and properties

        public int Id { get; set; } = default;
        public string Name { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public BarCodeTypeDirect()
        {
            Load(default);
        }

        public BarCodeTypeDirect(int id)
        {
            Load(id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return
                $"{nameof(Id)}: {Id}. " +
                $"{nameof(Name)}: {Name}. ";
        }

        public void Load(int id)
        {
            if (id == default) return;
            Id = id;

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ID", System.Data.SqlDbType.Int) { Value = Id },
            };
            SqlConnectFactory.ExecuteReader(SqlQueries.DbScales.Tables.BarCodeTypes.GetItemById, parameters, delegate (SqlDataReader reader)
            {
                while (reader.Read())
                {
                    Name = SqlConnectFactory.GetValueAsString(reader, "NAME");
                }
            });

            //using SqlConnection con = SqlConnectFactory.GetConnection();
            //con.Open();
            //string query = "SELECT * FROM [db_scales].[GetBarCodeType](@Id);";
            //using (SqlCommand cmd = new(query))
            //{
            //    cmd.Connection = con;
            //    cmd.Parameters.AddWithValue("@Id", Id);
            //    using SqlDataReader reader = cmd.ExecuteReader();
            //    if (reader.HasRows)
            //    {
            //        while (reader.Read())
            //        {
            //            Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "ID");
            //            Name = SqlConnectFactory.GetValueAsString(reader, "Name");
            //        }
            //    }
            //    reader.Close();
            //}
            //con.Close();
        }

        #endregion
    }
}
