// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DataCore.DAL.TableDirectModels
{
    [Serializable]
    public class BarCodeDirect : BaseSerializeEntity<BarCodeDirect>
    {
        #region Public and private fields and properties

        public long Id { get; set; } = default;
        public string Value { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime ChangeDt { get; set; }
        public BarCodeTypeDirect BarCodeType { get; set; } = new BarCodeTypeDirect();
        public NomenclatureDirect Nomenclature { get; set; } = new NomenclatureDirect();
        public NomenclatureUnitDirect NomenclatureUnit { get; set; } = new NomenclatureUnitDirect();
        public ContregentDirect Contragent { get; set; } = new ContregentDirect();
        [XmlIgnore]
        public SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

        #endregion

        #region Constructor and destructor

        public BarCodeDirect()
        {
            Load(default);
        }

        public BarCodeDirect(long id)
        {
            Load(id);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is not BarCodeDirect item)
            {
                return false;
            }
            return Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void Load(long id)
        {
            if (id == default) return;
            Id = id;
            SqlConnect.ExecuteReader("SELECT * FROM [db_scales].[GetBarCode] (default,default,default,default,@ID);", 
                new SqlParameter("@ID", System.Data.SqlDbType.BigInt) { Value = Id }, (SqlDataReader reader) =>
            {
                while (reader.Read())
                {
                    Value = SqlConnect.GetValueAsString(reader, "Value");
                    CreateDate = SqlConnect.GetValueAsNullable<DateTime>(reader, "CreateDate");
                    ChangeDt = SqlConnect.GetValueAsNullable<DateTime>(reader, "ChangeDt");
                    BarCodeType = new BarCodeTypeDirect(SqlConnect.GetValueAsNullable<int>(reader, "BarCodeTypeId"));
                    Nomenclature = new NomenclatureDirect(SqlConnect.GetValueAsNullable<int>(reader, "NomenclatureId"));
                    NomenclatureUnit = new NomenclatureUnitDirect(SqlConnect.GetValueAsNullable<int>(reader, "NomenclatureUnitId"));
                    Contragent = new ContregentDirect(SqlConnect.GetValueAsNullable<int>(reader, "ContragentId"));
                }
            });
        }

        public void Save()
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@BarCodeTypeId", System.Data.SqlDbType.Int) { Value = BarCodeType != null ? BarCodeType.Id : DBNull.Value },
                new SqlParameter("@NomenclatureId", System.Data.SqlDbType.Int) { Value = Nomenclature != null ? Nomenclature.Id : DBNull.Value },
                new SqlParameter("@NomenclatureUnitId", System.Data.SqlDbType.Int) { Value = NomenclatureUnit != null ? NomenclatureUnit.Id : DBNull.Value },
                new SqlParameter("@ContragentId", System.Data.SqlDbType.Int) { Value = Contragent != null ? Contragent.Id : DBNull.Value },
                new SqlParameter("@Value", System.Data.SqlDbType.NVarChar, 150) { Value = Value ?? (object)DBNull.Value },
            };
            SqlConnect.ExecuteReader(SqlQueries.DbScales.StoredProcedures.SetBarCode, parameters, (SqlDataReader reader) =>
            {
                if (reader.Read())
                {
                    Id = SqlConnect.GetValueAsNotNullable<int>(reader, "ID");
                }
            });
        }

        public List<BarCodeDirect> GetList()
        {
            List<BarCodeDirect> result = new();
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@BarCodeTypeId", System.Data.SqlDbType.Int) { Value = BarCodeType != null ? BarCodeType.Id : DBNull.Value },
                new SqlParameter("@NomenclatureId", System.Data.SqlDbType.Int) { Value = Nomenclature != null ? Nomenclature.Id : DBNull.Value },
                new SqlParameter("@NomenclatureUnitId", System.Data.SqlDbType.Int) { Value = NomenclatureUnit != null ? NomenclatureUnit.Id : DBNull.Value },
                new SqlParameter("@ContragentId", System.Data.SqlDbType.Int) { Value = Contragent != null ? Contragent.Id : DBNull.Value },
            };
            SqlConnect.ExecuteReader(SqlQueries.DbScales.StoredProcedures.GetBarCode, parameters, (SqlDataReader reader) =>
            {
                if (reader.Read())
                {
                    BarCodeDirect barCode = new()
                    {
                        Id = SqlConnect.GetValueAsNotNullable<int>(reader, "Id"),
                        Value = SqlConnect.GetValueAsString(reader, "Name"),
                        CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
                        ChangeDt = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ChangeDt"),
                        BarCodeType = new BarCodeTypeDirect(SqlConnect.GetValueAsNotNullable<int>(reader, "BarCodeTypeId")),
                        Nomenclature = new NomenclatureDirect(SqlConnect.GetValueAsNotNullable<int>(reader, "NomenclatureId")),
                        NomenclatureUnit = new NomenclatureUnitDirect(SqlConnect.GetValueAsNotNullable<int>(reader, "NomenclatureUnitId")),
                        Contragent = new ContregentDirect(SqlConnect.GetValueAsNotNullable<int>(reader, "ContragentId")),
                    };
                    result.Add(barCode);
                }
            });
            return result;
        }
    }
}
