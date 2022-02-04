// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DataCore.DAL.TableDirectModels
{
    [Serializable]
    public class BarCodeDirect : BaseSerializeEntity<BarCodeDirect>
    {
        public int Id { get; set; } = default;
        public string Value { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public BarCodeTypeDirect BarCodeType { get; set; } = new BarCodeTypeDirect();
        public NomenclatureDirect Nomenclature { get; set; } = new NomenclatureDirect();
        public NomenclatureUnitDirect NomenclatureUnit { get; set; } = new NomenclatureUnitDirect();
        public ContregentDirect Contragent { get; set; } = new ContregentDirect();

        public BarCodeDirect()
        {
            Load(default);
        }

        public BarCodeDirect(int id)
        {
            Load(id);
        }

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

        public void Load(int id)
        {
            if (id == default) return;
            Id = id;
            string query = "SELECT * FROM [db_scales].[GetBarCode] (default,default,default,default,@ID);";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ID", System.Data.SqlDbType.Int) { Value = Id },
            };
            //SqlConnectFactory.ExecuteReader(SqlQueries.DbScales.Tables.BarCodeTypes.GetItemById, parameters, delegate (SqlDataReader reader)
            SqlConnectFactory.ExecuteReader(query, parameters, delegate (SqlDataReader reader)
            {
                while (reader.Read())
                {
                    Value = SqlConnectFactory.GetValueAsString(reader, "Value");
                    CreateDate = SqlConnectFactory.GetValueAsNullable<DateTime>(reader, "CreateDate");
                    ModifiedDate = SqlConnectFactory.GetValueAsNullable<DateTime>(reader, "ModifiedDate");
                    BarCodeType = new BarCodeTypeDirect(SqlConnectFactory.GetValueAsNullable<int>(reader, "BarCodeTypeId"));
                    Nomenclature = new NomenclatureDirect(SqlConnectFactory.GetValueAsNullable<int>(reader, "NomenclatureId"));
                    NomenclatureUnit = new NomenclatureUnitDirect(SqlConnectFactory.GetValueAsNullable<int>(reader, "NomenclatureUnitId"));
                    Contragent = new ContregentDirect(SqlConnectFactory.GetValueAsNullable<int>(reader, "ContragentId"));
                }
            });
        }

        public void Save()
        {
            using SqlConnection con = SqlConnectFactory.GetConnection();
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
                        Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "ID");
                    }
                }
                reader.Close();
            }
            con.Close();
        }

        public static List<BarCodeDirect> GetList(NomenclatureDirect nomenclature)
        {
            List<BarCodeDirect> result = new();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
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
                                Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "Id"),
                                Value = SqlConnectFactory.GetValueAsString(reader, "Name"),
                                CreateDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
                                ModifiedDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "ModifiedDate"),
                                BarCodeType = new BarCodeTypeDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "BarCodeTypeId")),
                                Nomenclature = new NomenclatureDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "NomenclatureId")),
                                NomenclatureUnit = new NomenclatureUnitDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "NomenclatureUnitId")),
                                Contragent = new ContregentDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "ContragentId")),
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

        public static List<BarCodeDirect> GetList(NomenclatureDirect nomenclature, BarCodeTypeDirect barCodeType)
        {
            List<BarCodeDirect> result = new();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
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
                                Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "Id"),
                                Value = SqlConnectFactory.GetValueAsString(reader, "Name"),
                                CreateDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
                                ModifiedDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "ModifiedDate"),
                                BarCodeType = new BarCodeTypeDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "BarCodeTypeId")),
                                Nomenclature = new NomenclatureDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "NomenclatureId")),
                                NomenclatureUnit = new NomenclatureUnitDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "NomenclatureUnitId")),
                                Contragent = new ContregentDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "ContragentId")),
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

        public static List<BarCodeDirect> GetList(NomenclatureDirect nomenclature, BarCodeTypeDirect barCodeType, NomenclatureUnitDirect nomenclatureUnit)
        {
            List<BarCodeDirect> result = new();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
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
                                Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "Id"),
                                Value = SqlConnectFactory.GetValueAsString(reader, "Name"),
                                CreateDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
                                ModifiedDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "ModifiedDate"),
                                BarCodeType = new BarCodeTypeDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "BarCodeTypeId")),
                                Nomenclature = new NomenclatureDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "NomenclatureId")),
                                NomenclatureUnit = new NomenclatureUnitDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "NomenclatureUnitId")),
                                Contragent = new ContregentDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "ContragentId")),
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

        public static List<BarCodeDirect> GetList(NomenclatureDirect nomenclature, ContregentDirect contregent)
        {
            List<BarCodeDirect> result = new();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
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
                                Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "Id"),
                                Value = SqlConnectFactory.GetValueAsString(reader, "Name"),
                                CreateDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
                                ModifiedDate = SqlConnectFactory.GetValueAsNotNullable<DateTime>(reader, "ModifiedDate"),
                                BarCodeType = new BarCodeTypeDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "BarCodeTypeId")),
                                Nomenclature = new NomenclatureDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "NomenclatureId")),
                                NomenclatureUnit = new NomenclatureUnitDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "NomenclatureUnitId")),
                                Contragent = new ContregentDirect(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "ContragentId")),
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
