using System;
using System.Data.SqlClient;

namespace EntitiesLib
{
    [Serializable]
    public class ZplLabel
    {
        #region Public and private fields and properties

        public int WeighingFactId { get; set; }
        public string Content { get; set; }

        #endregion

        #region Public and private methods

        public void Save()
        {
            using (var con = SqlConnectFactory.GetConnection())
            {
                var query = "INSERT INTO [db_scales].[Labels] ([WeithingFactId],[Label]) VALUES (@ID, CONVERT(VARBINARY(MAX), @LABEL)) ";
                using (var cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", WeighingFactId);
                    cmd.Parameters.AddWithValue("@LABEL", Content);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        #endregion
    }
}
