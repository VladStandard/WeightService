using DataProjectsCore.Utils;
using Microsoft.Data.SqlClient;

namespace DataProjectsCore.DAL.TableModels
{
    public class _TemplateSqlExecute
    {
        public static string Result = string.Empty;

        public static void DelegateExecuteReaderTemplate(SqlDataReader reader)
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
            SqlConnectFactory.ExecuteReader(SqlQueries.DbScales.Tables.Scales.GetScaleDescription, parameters, DelegateExecuteReaderTemplate);
        }
    }
}
