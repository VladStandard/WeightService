// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;

namespace DataProjectsCore.DAL.TableModels
{
    [Serializable]
    public class ZplLabelDirect : BaseSerializeEntity<ZplLabelDirect>
    {
        #region Public and private fields and properties

        public int WeighingFactId { get; set; }
        public string Content { get; set; }

        #endregion

        #region Public and private methods

        public void Save()
        {
            using SqlConnection con = SqlConnectFactory.GetConnection();
            con.Open();
            string query = "INSERT INTO [db_scales].[Labels] ([WeithingFactId],[Label]) VALUES (@ID, CONVERT(VARBINARY(MAX), @LABEL)) ";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@ID", WeighingFactId);
                cmd.Parameters.AddWithValue("@LABEL", Content);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        #endregion
    }
}
