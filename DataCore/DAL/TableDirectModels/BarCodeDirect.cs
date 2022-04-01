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
            string query = "SELECT * FROM [db_scales].[GetBarCode] (default,default,default,default,@ID);";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ID", System.Data.SqlDbType.BigInt) { Value = Id },
            };
            //SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.BarCodeTypes.GetItemById, parameters, delegate (SqlDataReader reader)
            SqlConnect.ExecuteReader(query, parameters, delegate (SqlDataReader reader)
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
            using SqlConnection con = SqlConnect.GetConnection();
            con.Open();
            string query = @"
                    DECLARE @ID int; 
                    
                    EXECUTE [db_scales].[SetBarCode] 
                        @BarCodeTypeId
                        ,@NomenclatureId
                        ,@NomenclatureUnitId
                        ,@ContragentId
                        ,@Value
                        ,@ID OUTPUT
                      ,@ID OUTPUT

                    SELECT @ID as ID";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue($"@BarCodeTypeId", BarCodeType != null ? BarCodeType.Id : DBNull.Value);  // 
                cmd.Parameters.AddWithValue($"@NomenclatureId", Nomenclature != null ? Nomenclature.Id : DBNull.Value);  // 
                cmd.Parameters.AddWithValue($"@NomenclatureUnitId", NomenclatureUnit != null ? NomenclatureUnit.Id : DBNull.Value);  // 
                cmd.Parameters.AddWithValue($"@ContragentId", Contragent != null ? Contragent.Id : DBNull.Value);  // 
                cmd.Parameters.AddWithValue($"@Value", Value ?? (object)DBNull.Value);  // 
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Id = SqlConnect.GetValueAsNotNullable<int>(reader, "ID");
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        public List<BarCodeDirect> GetList(NomenclatureDirect nomenclature)
        {
            List<BarCodeDirect> result = new();
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetBarCode] (@NomenclatureId,default,default,default,default);";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@NomenclatureId", nomenclature.Id);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
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
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public List<BarCodeDirect> GetList(NomenclatureDirect nomenclature, BarCodeTypeDirect barCodeType)
        {
            List<BarCodeDirect> result = new();
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetBarCode] (@NomenclatureId,@BarCodeTypeId,default,default,default);";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@NomenclatureId", nomenclature.Id);
                    cmd.Parameters.AddWithValue("@BarCodeTypeId", barCodeType.Id);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
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
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public List<BarCodeDirect> GetList(NomenclatureDirect nomenclature, BarCodeTypeDirect barCodeType, NomenclatureUnitDirect nomenclatureUnit)
        {
            List<BarCodeDirect> result = new();
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetBarCode] (@NomenclatureId,@BarCodeTypeId,@NomenclatureUnitId,default,default);";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@NomenclatureId", nomenclature.Id);
                    cmd.Parameters.AddWithValue("@BarCodeTypeId", barCodeType.Id);
                    cmd.Parameters.AddWithValue("@NomenclatureUnitId", nomenclatureUnit.Id);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
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
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public List<BarCodeDirect> GetList(NomenclatureDirect nomenclature, ContregentDirect contregent)
        {
            List<BarCodeDirect> result = new();
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetBarCode] (@NomenclatureId,default,default,@ContragentId,default);";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@NomenclatureId", nomenclature.Id);
                    cmd.Parameters.AddWithValue("@BarCodeTypeId", contregent.Id);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
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
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }
    }
}
