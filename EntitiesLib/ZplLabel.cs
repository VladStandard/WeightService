using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightServices.Common;

namespace EntitiesLib
{
    [Serializable]
    public class ZplLabel
    {
        public int WeighingFactId { get; set; }
        public string Content { get; set; }
        public void Save()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query =
                    "INSERT INTO [db_scales].[Labels] ([WeithingFactId],[Label]) VALUES (@ID, CONVERT(VARBINARY(MAX), @LABEL)) ";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", this.WeighingFactId);
                    cmd.Parameters.AddWithValue("@LABEL", this.Content);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }

            }

        }

    }

}
