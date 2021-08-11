// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WeightCore.DAL.TableModels
{
    [Serializable]
    public class BarCodeEntity : BaseEntity<BarCodeEntity>
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public BarCodeTypeEntity BarCodeType { get; set; }
        public NomenclatureEntity Nomenclature { get; set; }
        public NomenclatureUnitEntity NomenclatureUnit { get; set; }
        public ContregentEntity Contragent { get; set; }

        public BarCodeEntity()
        {
        }

        public BarCodeEntity(int _Id)
        {
            Id = _Id;
            Load();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BarCodeEntity item))
            {
                return false;
            }
            return Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void Load()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetBarCode] (default,default,default,default,@Id);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Id = SqlConnectFactory.GetValue<int>(reader, "ID");
                                Value = SqlConnectFactory.GetValue<string>(reader, "Value");
                                CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate");
                                ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate");
                                BarCodeType = new BarCodeTypeEntity(SqlConnectFactory.GetValue<int>(reader, "BarCodeTypeId"));
                                Nomenclature = new NomenclatureEntity(SqlConnectFactory.GetValue<int>(reader, "NomenclatureId"));
                                NomenclatureUnit = new NomenclatureUnitEntity(SqlConnectFactory.GetValue<int>(reader, "NomenclatureUnitId"));
                                Contragent = new ContregentEntity(SqlConnectFactory.GetValue<int>(reader, "ContragentId"));
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
        }

        public void Save()
        {

            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
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
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue($"@BarCodeTypeId", BarCodeType != null ? BarCodeType.Id : (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@NomenclatureId", Nomenclature != null ? Nomenclature.Id : (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@NomenclatureUnitId", NomenclatureUnit != null ? NomenclatureUnit.Id : (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@ContragentId", Contragent != null ? Contragent.Id : (object)DBNull.Value);  // 
                    cmd.Parameters.AddWithValue($"@Value", Value ?? (object)DBNull.Value);  // 
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Id = SqlConnectFactory.GetValue<int>(reader, "ID");
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
        }

        public static List<BarCodeEntity> GetList(NomenclatureEntity nomenclature)
        {
            List<BarCodeEntity> result = new List<BarCodeEntity>();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetBarCode] (@NomenclatureId,default,default,default,default);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@NomenclatureId", nomenclature.Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                BarCodeEntity barCode = new BarCodeEntity()
                                {
                                    Id = SqlConnectFactory.GetValue<int>(reader, "Id"),
                                    Value = SqlConnectFactory.GetValue<string>(reader, "Name"),
                                    CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate"),
                                    ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate"),
                                    BarCodeType = new BarCodeTypeEntity(SqlConnectFactory.GetValue<int>(reader, "BarCodeTypeId")),
                                    Nomenclature = new NomenclatureEntity(SqlConnectFactory.GetValue<int>(reader, "NomenclatureId")),
                                    NomenclatureUnit = new NomenclatureUnitEntity(SqlConnectFactory.GetValue<int>(reader, "NomenclatureUnitId")),
                                    Contragent = new ContregentEntity(SqlConnectFactory.GetValue<int>(reader, "ContragentId")),
                                };
                                result.Add(barCode);
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }

        public static List<BarCodeEntity> GetList(NomenclatureEntity nomenclature, BarCodeTypeEntity barCodeType)
        {
            List<BarCodeEntity> result = new List<BarCodeEntity>();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetBarCode] (@NomenclatureId,@BarCodeTypeId,default,default,default);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@NomenclatureId", nomenclature.Id);
                    cmd.Parameters.AddWithValue("@BarCodeTypeId", barCodeType.Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                BarCodeEntity barCode = new BarCodeEntity()
                                {
                                    Id = SqlConnectFactory.GetValue<int>(reader, "Id"),
                                    Value = SqlConnectFactory.GetValue<string>(reader, "Name"),
                                    CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate"),
                                    ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate"),
                                    BarCodeType = new BarCodeTypeEntity(SqlConnectFactory.GetValue<int>(reader, "BarCodeTypeId")),
                                    Nomenclature = new NomenclatureEntity(SqlConnectFactory.GetValue<int>(reader, "NomenclatureId")),
                                    NomenclatureUnit = new NomenclatureUnitEntity(SqlConnectFactory.GetValue<int>(reader, "NomenclatureUnitId")),
                                    Contragent = new ContregentEntity(SqlConnectFactory.GetValue<int>(reader, "ContragentId")),
                                };
                                result.Add(barCode);
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }

        public static List<BarCodeEntity> GetList(NomenclatureEntity nomenclature, BarCodeTypeEntity barCodeType, NomenclatureUnitEntity nomenclatureUnit)
        {
            List<BarCodeEntity> result = new List<BarCodeEntity>();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetBarCode] (@NomenclatureId,@BarCodeTypeId,@NomenclatureUnitId,default,default);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@NomenclatureId", nomenclature.Id);
                    cmd.Parameters.AddWithValue("@BarCodeTypeId", barCodeType.Id);
                    cmd.Parameters.AddWithValue("@NomenclatureUnitId", nomenclatureUnit.Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                BarCodeEntity barCode = new BarCodeEntity()
                                {
                                    Id = SqlConnectFactory.GetValue<int>(reader, "Id"),
                                    Value = SqlConnectFactory.GetValue<string>(reader, "Name"),
                                    CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate"),
                                    ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate"),
                                    BarCodeType = new BarCodeTypeEntity(SqlConnectFactory.GetValue<int>(reader, "BarCodeTypeId")),
                                    Nomenclature = new NomenclatureEntity(SqlConnectFactory.GetValue<int>(reader, "NomenclatureId")),
                                    NomenclatureUnit = new NomenclatureUnitEntity(SqlConnectFactory.GetValue<int>(reader, "NomenclatureUnitId")),
                                    Contragent = new ContregentEntity(SqlConnectFactory.GetValue<int>(reader, "ContragentId")),
                                };
                                result.Add(barCode);
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }

        public static List<BarCodeEntity> GetList(NomenclatureEntity nomenclature, ContregentEntity contregent)
        {
            List<BarCodeEntity> result = new List<BarCodeEntity>();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT * FROM [db_scales].[GetBarCode] (@NomenclatureId,default,default,@ContragentId,default);";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@NomenclatureId", nomenclature.Id);
                    cmd.Parameters.AddWithValue("@BarCodeTypeId", contregent.Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                BarCodeEntity barCode = new BarCodeEntity()
                                {
                                    Id = SqlConnectFactory.GetValue<int>(reader, "Id"),
                                    Value = SqlConnectFactory.GetValue<string>(reader, "Name"),
                                    CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate"),
                                    ModifiedDate = SqlConnectFactory.GetValue<DateTime>(reader, "ModifiedDate"),
                                    BarCodeType = new BarCodeTypeEntity(SqlConnectFactory.GetValue<int>(reader, "BarCodeTypeId")),
                                    Nomenclature = new NomenclatureEntity(SqlConnectFactory.GetValue<int>(reader, "NomenclatureId")),
                                    NomenclatureUnit = new NomenclatureUnitEntity(SqlConnectFactory.GetValue<int>(reader, "NomenclatureUnitId")),
                                    Contragent = new ContregentEntity(SqlConnectFactory.GetValue<int>(reader, "ContragentId")),
                                };
                                result.Add(barCode);
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }
    }
}
