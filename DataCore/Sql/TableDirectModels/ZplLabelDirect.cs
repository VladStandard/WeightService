// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataCore.Sql.TableDirectModels
{
    public class ZplLabelDirect : BaseSerializeEntity
    {
        #region Public and private fields, properties, constructor

        public long WeighingFactId { get; set; }
        public string? Zpl { get; set; }

        #endregion

        #region Constructor and destructor

        public ZplLabelDirect()
        {
            WeighingFactId = default;
            Zpl = default;
        }

        #endregion

        #region Public and private methods

        public void SaveZpl()
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new("@ID", SqlDbType.Int) { Value = WeighingFactId },
                new("@Zpl", SqlDbType.NVarChar) { Value = Zpl },
            };
            SqlConnect.ExecuteNonQuery(SqlQueries.DbScales.Tables.Labels.SaveZpl, parameters);
        }

        #endregion
    }
}
