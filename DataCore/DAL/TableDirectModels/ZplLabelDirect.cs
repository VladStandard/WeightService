// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Xml.Serialization;

namespace DataCore.DAL.TableDirectModels
{
    [Serializable]
    public class ZplLabelDirect : BaseSerializeEntity<ZplLabelDirect>
    {
        #region Public and private fields and properties

        public long WeighingFactId { get; set; }
        public string? Label { get; set; }
        public string? Zpl { get; set; }
        [XmlIgnore]
        public SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

        #endregion

        #region Constructor and destructor

        public ZplLabelDirect()
        {
            WeighingFactId = default;
            Label = default;
            Zpl = default;
        }

        #endregion

        #region Public and private methods

        public void Save()
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ID", SqlDbType.BigInt) { Value = WeighingFactId },
                new SqlParameter("@LABEL", SqlDbType.NVarChar) { Value = Label },
            };
            SqlConnect.ExecuteNonQuery(SqlQueries.DbScales.Tables.Labels.Save, parameters);
        }

        public void SaveZpl()
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ID", SqlDbType.Int) { Value = WeighingFactId },
                new SqlParameter("@Zpl", SqlDbType.NVarChar) { Value = Zpl },
            };
            SqlConnect.ExecuteNonQuery(SqlQueries.DbScales.Tables.Labels.SaveZpl, parameters);
        }

        #endregion
    }
}
