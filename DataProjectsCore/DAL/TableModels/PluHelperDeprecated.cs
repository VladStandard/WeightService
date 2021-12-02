//using ScalesLib.Sql.Helpers;
//using System.Collections.Generic;
//using Microsoft.Data.SqlClient;

//namespace DataProjectsCore.DAL.Entities
//{
//    public static class PLUHelper
//    {
//        // Помощник SQL.
//        private static SqlHelper _sql { get; set; } = SqlHelper.Instance;

//        public static PluEntity GetPLU(string scaleGuid, int plu)
//        {
//            PluEntity pluEntity = null;
//            using (SqlConnection con = new SqlConnection(_sql.ConnectionString))
//            {
//                string query = 
//                    "SELECT " +
//                            "[Id]"+                     //0
//                            ",[GoodsName]"+             //1    
//                            ",[GoodsFullName]" +        //2
//                            ",[GoodsDescription]" +     //3  
//                            ",[TemplateID]" +           //4
//                            ",[GTIN]" +                 //5
//                            ",[EAN13]" +                //6
//                            ",[ITF14]" +                //7
//                            ",[GoodsShelfLifeDays]" +   //8
//                            ",[GoodsTareWeight]" +      //9
//                            ",[GoodsBoxQuantly]" +      //10
//                            ",[ConsumerName]" +         //11
//                            ",[GLN]" +                  //12
//                            ",[RRefGoods]" +            //13
//                            ",[PLU]" +                  //14
//                    " FROM [db_scales].[GetPLUByID] (@ScaleID, @PLU);";
//                using (SqlCommand cmd = new SqlCommand(query))
//                {
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@ScaleID", scaleGuid);
//                    cmd.Parameters.AddWithValue("@PLU", plu);
//                    con.Open();
//                    SqlDataReader reader = cmd.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        pluEntity = new PluEntity ()
//                        {
//                            ScaleId         = scaleGuid,
//                            Id              = reader.GetInt32(0),
//                            GoodsName       = reader.GetString(1),
//                            GoodsFullName   = reader.GetString(2),
//                            GoodsDescription = reader.GetString(3),
//                            TemplateID      = reader.GetString(4),
//                            GTIN            = reader.IsDBNull(5)    ? "" : reader.GetString(5),
//                            EAN13           = reader.IsDBNull(6)    ? "" : reader.GetString(6),
//                            ITF14           = reader.IsDBNull(7)    ? "" : reader.GetString(7),
//                            GoodsShelfLifeDays = reader.IsDBNull(8) ? 0  : reader.GetByte(8),
//                            GoodsTareWeight = reader.IsDBNull(9)    ? 0  : reader.GetDecimal(9),
//                            GoodsBoxQuantly = reader.IsDBNull(10)   ? 0  : reader.GetInt32(10),
//                            ConsumerName    = reader.IsDBNull(11)   ? "" : reader.GetString(11),
//                            GLN             = reader.IsDBNull(12)   ? 0  : reader.GetInt32(12),
//                            RRefGoods       = reader.GetString(13),
//                            PLU             = reader.GetInt32(14)

//                        };
//                    }
//                    reader.Close();
//                    con.Close();
//                }
//            }
//            return pluEntity;

//        }


//        public static List<PluEntity> GetPLUList(string scaleGuid)
//        {
//            List<PluEntity> res = new List<PluEntity>();
//            using (SqlConnection con = new SqlConnection(_sql.ConnectionString))
//            {
//                string query =
//                    "SELECT " +
//                            "[Id]" +                    //0
//                            ",[GoodsName]" +            //1    
//                            ",[GoodsFullName]" +        //2
//                            ",[GoodsDescription]" +     //3  
//                            ",[TemplateID]" +           //4
//                            ",[GTIN]" +                 //5
//                            ",[EAN13]" +                //6
//                            ",[ITF14]" +                //7
//                            ",[GoodsShelfLifeDays]" +   //8
//                            ",[GoodsTareWeight]" +      //9
//                            ",[GoodsBoxQuantly]" +      //10
//                            ",[ConsumerName]" +         //11
//                            ",[GLN]" +                  //12
//                            ",[RRefGoods]" +            //13
//                            ",[PLU]" +                  //14
//                    " FROM [db_scales].[GetPLU] (@ScaleID) ORDER BY PLU;";
//                using (SqlCommand cmd = new SqlCommand(query))
//                {
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@ScaleID", scaleGuid);
//                    con.Open();
//                    SqlDataReader reader = cmd.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        PluEntity pluEntity = new PluEntity()
//                        {
//                            ScaleId = scaleGuid,
//                            Id = reader.GetInt32(0),
//                            GoodsName = reader.GetString(1),
//                            GoodsFullName = reader.GetString(2),
//                            GoodsDescription = reader.GetString(3),
//                            TemplateID = reader.GetString(4),
//                            GTIN = reader.IsDBNull(5)  ? "" : reader.GetString(5),
//                            EAN13 = reader.IsDBNull(6) ? "" : reader.GetString(6),
//                            ITF14 = reader.IsDBNull(7) ? "" : reader.GetString(7),
//                            GoodsShelfLifeDays = reader.IsDBNull(8) ? 0 : reader.GetByte(8),
//                            GoodsTareWeight = reader.IsDBNull(9)    ? 0 : reader.GetDecimal(9),
//                            GoodsBoxQuantly = reader.IsDBNull(10)   ? 0 : reader.GetInt32(10),
//                            ConsumerName = reader.IsDBNull(11)      ? "": reader.GetString(11),
//                            GLN = reader.IsDBNull(12)               ? 0 : reader.GetInt32(12),
//                            RRefGoods = reader.GetString(13),
//                            PLU = reader.GetInt32(14)

//                        };

//                        res.Add(pluEntity);

//                    }
//                    reader.Close();
//                    con.Close();
//                }
//            }
//            return res;

//        }
//    }
//}
