//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore.Sql.Models;
//using Microsoft.Data.SqlClient;
//using System;
//using System.Collections.Generic;
//// ReSharper disable MemberCanBePrivate.Global
//// ReSharper disable MissingXmlDoc

//namespace DataCore.Sql.TableDirectModels;

///// <summary>
///// WorkShop table.
///// </summary>
//public class WorkShopDirect : BaseSerializeEntity
//{
//    #region Public and private fields, properties, constructor

//    public long Id { get; set; }
//    public string Name { get; set; } = string.Empty;

//    public ProductionFacilityDirect ProductionFacility { get; set; } = new();
//    public DateTime CreateDate { get; set; }
//    public DateTime ChangeDt { get; set; }
//    public string RRefID { get; set; } = string.Empty;

//    #endregion

//    #region Constructor and destructor

//    public WorkShopDirect()
//    {
//        Load();
//    }

//    public WorkShopDirect(long id)
//    {
//        Id = id;
//        Load();
//    }

//    #endregion

//    #region Public and private methods

//    public override bool Equals(object obj)
//    {
//        if (obj is not WorkShopDirect item)
//        {
//            return false;
//        }
//        return Id.Equals(item.Id);
//    }

//    public override int GetHashCode()
//    {
//        return Id.GetHashCode();
//    }

//    public void Load()
//    {
//        using SqlConnection con = SqlConnect.GetConnection();
//        con.Open();
//        string query =
//            "SELECT [Id],[Name],[ProductionFacilityID],[CreateDate],[ModifiedDate],[IdRRef] FROM [db_scales].[GetWorkShop] (default,@Id);";
//        using (SqlCommand cmd = new(query))
//        {
//            cmd.Connection = con;
//            cmd.Parameters.AddWithValue("@Id", Id);
//            using SqlDataReader reader = cmd.ExecuteReader();
//            if (reader.HasRows)
//            {
//                while (reader.Read())
//                {
//                    Id = SqlConnect.GetValueAsNotNullable<long>(reader, "ID");
//                    Name = SqlConnect.GetValueAsString(reader, "Name");
//                    CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate");
//                    ChangeDt = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ChangeDt");
//                    RRefID = SqlConnect.GetValueAsString(reader, "RRefID");
//                }
//            }
//            ProductionFacility = new(SqlConnect.GetValueAsNotNullable<int>(reader, "ProductionFacilityID"));
//            reader.Close();
//        }
//        con.Close();
//    }

//    public void Save()
//    {

//        using SqlConnection con = SqlConnect.GetConnection();
//        con.Open();
//        string query = @"
//                DECLARE @ID int; 
//                EXECUTE @RC = [db_scales].[SetWorkShop] 
//                   @Name
//                  ,@ProductionFacilityID
//                  ,@1CRRefID
//                   @ID OUTPUT;
//                SELECT @ID";
//        using (SqlCommand cmd = new(query))
//        {
//            cmd.Connection = con;
//            cmd.Parameters.Clear();
//            cmd.Parameters.AddWithValue($"@Name", Name ?? (object)DBNull.Value);               // 
//            cmd.Parameters.AddWithValue($"@ProductionFacilityID", ProductionFacility.Id);      // 
//            cmd.Parameters.AddWithValue($"@1CRRefID", RRefID ?? (object)DBNull.Value);         // @1CRRefID
//            using SqlDataReader reader = cmd.ExecuteReader();
//            if (reader.HasRows)
//            {
//                while (reader.Read())
//                {
//                    Id = SqlConnect.GetValueAsNotNullable<long>(reader, "Id");
//                }
//            }
//            reader.Close();
//        }
//        con.Close();

//    }

//    public List<WorkShopDirect> GetList(ProductionFacilityDirect productionFacility)
//    {
//        List<WorkShopDirect> result = new();
//        using SqlConnection con = SqlConnect.GetConnection();
//        con.Open();
//        string query =
//            "SELECT [Id],[Name],[ProductionFacilityID],[CreateDate],[ModifiedDate],[IdRRef] FROM [db_scales].[GetWorkShop](@ID, DEFAULT);";
//        using (SqlCommand cmd = new(query))
//        {
//            cmd.Connection = con;
//            cmd.Parameters.AddWithValue("@Id", productionFacility.Id);
//            using SqlDataReader reader = cmd.ExecuteReader();
//            if (reader.HasRows)
//            {
//                while (reader.Read())
//                {
//                    WorkShopDirect workShop = new()
//                    {
//                        Id = SqlConnect.GetValueAsNotNullable<long>(reader, "Id"),
//                        Name = SqlConnect.GetValueAsString(reader, "Name"),
//                        CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
//                        ChangeDt = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ChangeDt"),
//                        RRefID = SqlConnect.GetValueAsString(reader, "1CRRefID")
//                    };
//                    workShop.ProductionFacility = productionFacility;
//                    result.Add(workShop);
//                }
//            }
//            reader.Close();
//        }
//        con.Close();
//        return result;
//    }

//    #endregion
//}
