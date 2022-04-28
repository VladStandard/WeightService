// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels;
using DataCore.Sql.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Xml.Serialization;

namespace DataCore.Sql.TableDirectModels
{
    public class ProductSeriesDirect : BaseSerializeEntity
    {
        #region Public and private fields and properties

        public long Id { get; set; }
        public Guid UUID { get; set; }
        public ScaleEntity Scale { get; set; }
        public DateTime CreateDate { get; set; }
        public SsccDirect Sscc { get; set; }
        public PluDirect Plu { get; set; }
        public int CountUnit { get; set; }
        public decimal TotalNetWeight { get; set; }
        public decimal TotalTareWeight { get; set; }
        public bool IsMarked { get; set; }
        [XmlIgnore] public TemplateDirect Template { get; set; }

        #endregion

        #region Constructor and destructor

        public ProductSeriesDirect()
        {
            Id = 0;
            IsMarked = false;
            UUID = Guid.Empty;
            CreateDate = DateTime.MinValue;
            CountUnit = 0;
            TotalNetWeight = 0;
            TotalTareWeight = 0;
            Sscc = new();
            Plu = new();
            Template = new();
            Scale = new();
        }

        public ProductSeriesDirect(ScaleEntity scale) : this()
        {
            Scale = scale;
            Load();
        }

        #endregion

        #region Public and private methods

        public void LoadTemplate(long id)
        {
            Template = new(id);
        }

        public void Load()
        {
            if (Scale == null || Scale.IdentityId == default) 
            {
                throw new Exception("Equipment instance not identified. Set [Scale].");
            }

            SqlConnect.ExecuteReader(SqlQueries.DbScales.Functions.GetCurrentProductSeriesV2,
                new SqlParameter("@SCALE_ID", System.Data.SqlDbType.VarChar, 38) { Value = Scale.IdentityId }, (SqlDataReader reader) =>
                {
                    byte count = 0;
                    while (reader.Read())
                    {
                        if (count > 0)
                            throw new Exception($"{nameof(count)} > 0 ({count})");
                        count++;
                        if (reader[0] is long longId)
                            Id = longId;
                        else if (reader[0] is int intId)
                            Id = intId;
                        CreateDate = reader.GetDateTime(1);
                        UUID = reader.GetGuid(2);
                        Sscc = new SsccDirect(reader.GetString(3));
                        CountUnit = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                        TotalNetWeight = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5);
                        TotalTareWeight = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6);
                    }
                });
        }

        #endregion
    }
}
