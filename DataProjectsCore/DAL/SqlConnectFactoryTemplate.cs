using DataProjectsCore.Utils;
using Microsoft.Data.SqlClient;

namespace DataProjectsCore.DAL.TableModels
{
    public class SqlConnectFactoryTemplate
    {
        #region Public and private fields and properties

        public static string Result = string.Empty;

        #endregion

        #region Public and private methods

        public static void ExecuteReaderTemplateReader(SqlDataReader reader)
        {
            Result = string.Empty;
            if (reader.Read())
            {
                Result = reader.GetString(0);
            }
        }

        public static void ExecuteReaderTemplate(int scaleId)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@scale_id", System.Data.SqlDbType.Int) { Value = scaleId },
            };
            SqlConnectFactory.ExecuteReader(SqlQueries.DbScales.Tables.Scales.GetScaleDescription, parameters, ExecuteReaderTemplateReader);
        }

        public static string ExecuteReaderAsStringReader(SqlDataReader reader)
        {
            string result = string.Empty;
            if (reader.Read())
            {
                result = reader.GetString(0);
            }
            return result;
        }

        public static string? ExecuteReaderAsStringTemplate(int scaleId)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@scale_id", System.Data.SqlDbType.Int) { Value = scaleId },
            };
            return SqlConnectFactory.ExecuteReader(SqlQueries.DbScales.Tables.Scales.GetScaleDescription, parameters, ExecuteReaderAsStringReader);
        }

        public static void UpdateTemplate(ScaleDirect scale)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ID", System.Data.SqlDbType.Int) { Value = scale.Id },
                new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 150) { Value = scale.Description },
                new SqlParameter("@Port", System.Data.SqlDbType.SmallInt) { Value = scale.DevicePort },
                new SqlParameter("@SendTimeout", System.Data.SqlDbType.SmallInt) { Value = scale.DeviceSendTimeout },
                new SqlParameter("@ReceiveTimeout", System.Data.SqlDbType.SmallInt) { Value = scale.DeviceReceiveTimeout },
                new SqlParameter("@ComPort", System.Data.SqlDbType.VarChar, 5) { Value = DataShareCore.Utils.StringUtils.GetStringNullValueTrim(scale.DeviceComPort, 5) },
                new SqlParameter("@UseOrder", System.Data.SqlDbType.SmallInt) { Value = scale.UseOrder == true ? 1 : 0 },
                new SqlParameter("@VerScalesUI", System.Data.SqlDbType.VarChar, 30) { Value = DataShareCore.Utils.StringUtils.GetStringNullValueTrim(scale.VerScalesUI, 30) },
                new SqlParameter("@ScaleFactor", System.Data.SqlDbType.Int) { Value = scale.ScaleFactor },
            };
            SqlConnectFactory.ExecuteNonQuery(SqlQueries.DbScales.Tables.Scales.UpdateScaleDirect, parameters);
        }

        #endregion
    }
}
