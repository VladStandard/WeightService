// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Data.SqlClient;

namespace DataCore.DAL
{
    public class SqlConnectFactoryTemplate
    {
        #region Public and private fields and properties

        public static string Result = string.Empty;
        public SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

        #endregion

        #region Public and private methods

        public void ExecuteReaderTemplate(long scaleId)
        {
            Result = string.Empty;
            SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Scales.GetScaleDescription,
                new SqlParameter("@scale_id", System.Data.SqlDbType.BigInt) { Value = scaleId }, (SqlDataReader reader) =>
            {
                if (reader.Read())
                {
                    Result = reader.GetString(0);
                }
            });
        }

        public string? ExecuteReaderAsStringTemplate(long scaleId)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@scale_id", System.Data.SqlDbType.BigInt) { Value = scaleId },
            };
            string result = string.Empty;
            SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Scales.GetScaleDescription, parameters, (SqlDataReader reader) =>
            {
                if (reader.Read())
                {
                    result = reader.GetString(0);
                }
            });
            return result;
        }

        //public void UpdateTemplate(ScaleDirect scale)
        //{
        //    SqlParameter[] parameters = new SqlParameter[] {
        //        new SqlParameter("@ID", System.Data.SqlDbType.BigInt) { Value = scale.Id },
        //        new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 150) { Value = scale.Description },
        //        new SqlParameter("@Port", System.Data.SqlDbType.SmallInt) { Value = scale.DevicePort },
        //        new SqlParameter("@SendTimeout", System.Data.SqlDbType.SmallInt) { Value = scale.DeviceWriteTimeout },
        //        new SqlParameter("@ReceiveTimeout", System.Data.SqlDbType.SmallInt) { Value = scale.DeviceReadTimeout },
        //        new SqlParameter("@ComPort", System.Data.SqlDbType.VarChar, 5) { Value = StringUtils.GetStringNullValueTrim(scale.DeviceComPort, 5) },
        //        new SqlParameter("@UseOrder", System.Data.SqlDbType.SmallInt) { Value = scale.UseOrder == true ? 1 : 0 },
        //        new SqlParameter("@VerScalesUI", System.Data.SqlDbType.VarChar, 30) { Value = StringUtils.GetStringNullValueTrim(scale.VerScalesUI, 30) },
        //        new SqlParameter("@ScaleFactor", System.Data.SqlDbType.Int) { Value = scale.ScaleFactor },
        //    };
        //    SqlConnect.ExecuteNonQuery(SqlQueries.DbScales.Tables.Scales.UpdateScaleDirect, parameters);
        //}

        #endregion
    }
}
