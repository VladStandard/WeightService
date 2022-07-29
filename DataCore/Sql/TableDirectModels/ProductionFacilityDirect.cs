//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore.Sql.Models;
//using Microsoft.Data.SqlClient;
//using System;
//using System.Collections.Generic;
//using static DataCore.Sql.SqlQueries.DbScales.Tables;
//// ReSharper disable MemberCanBePrivate.Global
//// ReSharper disable MissingXmlDoc
//// ReSharper disable UnusedAutoPropertyAccessor.Global

//namespace DataCore.Sql.TableDirectModels;

///// <summary>
///// Table ProductionFacility.
///// </summary>
//public class ProductionFacilityDirect : BaseSerializeEntity
//{
//    #region Public and private fields, properties, constructor

//    public long Id { get; set; }
//    public DateTime CreateDate { get; set; }
//    public DateTime ChangeDt { get; set; }
//    public string Name { get; set; } = string.Empty;

//    /// <summary>
//    /// Constructor.
//    /// </summary>
//    public ProductionFacilityDirect()
//    {
//        Load();
//    }

//    /// <summary>
//    /// Constructor.
//    /// </summary>
//    /// <param name="id"></param>
//    public ProductionFacilityDirect(long id)
//    {
//        Id = id;
//        Load();
//    }

//    #endregion

//    #region Public and private methods

//    public override bool Equals(object obj) => obj is ProductionFacilityDirect item && Id.Equals(item.Id);

//    public override int GetHashCode() => Id.GetHashCode();

//    public void Load()
//    {
//        if (Id == default) return;
//        using SqlConnection con = SqlConnect.GetConnection();
//        con.Open();
//        string query = "SELECT * FROM [db_scales].[GetProductionFacility] (@Id);";
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
//                }
//            }
//            reader.Close();
//        }
//        con.Close();
//    }

//    public void Save()
//    {
//        using SqlConnection con = SqlConnect.GetConnection();
//        con.Open();
//        string query = @"
//DECLARE @ID int; 
//EXECUTE [db_scales].[SetProductionFacility]
//@1CRRefID,
//@Name,
//@ID OUTPUT;
//SELECT @ID";
//        using (SqlCommand cmd = new(query))
//        {
//            cmd.Connection = con;
//            cmd.Parameters.Clear();
//            cmd.Parameters.AddWithValue($"@Name", Name ?? (object)DBNull.Value);  // 
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

//    public List<ProductionFacilityDirect> GetList()
//    {
//        List<ProductionFacilityDirect> result = new();
//        SqlConnect.ExecuteReader(ProductionFacility.GetItems, (reader) =>
//        {
//            while (reader.Read())
//            {
//                ProductionFacilityDirect pFacility = new()
//                {
//                    Id = SqlConnect.GetValueAsNotNullable<long>(reader, "Id"),
//                    //Marked
//                    CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
//                    ChangeDt = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ModifiedDate"),
//                    Name = SqlConnect.GetValueAsString(reader, "Name"),
//                };
//                result.Add(pFacility);
//            }
//        });
//        return result;
//    }

//    #endregion
//}
