//using ScalesLib.Sql.Helpers;
//using System;
//using Microsoft.Data.SqlClient;
//using System.Xml.Linq;

//namespace DataCore.DAL.TableDirectModels
//{
//    /// <summary>
//    /// Помощник весов.
//    /// </summary>
//    public static class WeightingHelper
//    {
//        /// <summary>
//        /// Помощник SQL.
//        /// </summary>
//        private static SqlHelper _sql { get; set; } = SqlHelper.Instance;

//        /// <summary>
//        /// Выполнить ХП SetWeithingFact.
//        /// Редактировал: 2020-06-23 Морозов Дамиан.
//        /// </summary>
//        /// <param name="weighingFact"></param>
//        public static void SaveWeighingFact(WeighingFactEntity weighingFact)
//        {
//            using (SqlConnection con = new SqlConnection(_sql.ConnectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(SaveWeighingFactGetQuery()))
//                {
//                    cmd.Connection = con;
//                    cmd.Parameters.Clear();
//                    SqlParameter orderId = new SqlParameter("OrderID", weighingFact.Order == null ? DBNull.Value : (object)weighingFact.Order.RRefID);
//                    SqlParameter scaleID = new SqlParameter("ScaleID", weighingFact.Scale.RRefID);
//                    SqlParameter plu = new SqlParameter("PLU", weighingFact.PLU.PLU);
//                    SqlParameter netWeight = new SqlParameter("NetWeight", weighingFact.WeightNetto);
//                    SqlParameter tareWeight = new SqlParameter("TareWeight", weighingFact.WeightTare);
//                    SqlParameter productDate = new SqlParameter("ProductDate", weighingFact.ProductDate);
//                    cmd.Parameters.Add(orderId);
//                    cmd.Parameters.Add(scaleID);
//                    cmd.Parameters.Add(plu);
//                    cmd.Parameters.Add(netWeight);
//                    cmd.Parameters.Add(tareWeight);
//                    cmd.Parameters.Add(productDate);
//                    con.Open();
//                    SqlDataReader reader = cmd.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        //string sscc = reader.GetString(0);
//                        weighingFact.RegDate = reader.GetDateTime(1);
//                        XDocument xDoc = XDocument.Parse(reader.GetString(2));
//                        SsccEntity sscc = new SsccEntity
//                        {
//                            SSCC = xDoc.Root.Element("Item").Attribute("SSCC").Value,
//                            GLN = xDoc.Root.Element("Item").Attribute("GLN").Value,
//                            UnitID = int.Parse(xDoc.Root.Element("Item").Attribute("UnitID").Value),
//                            UnitType = byte.Parse(xDoc.Root.Element("Item").Attribute("UnitType").Value),
//                            SynonymSSCC = xDoc.Root.Element("Item").Attribute("SynonymSSCC").Value,
//                            Check = int.Parse(xDoc.Root.Element("Item").Attribute("Check").Value)
//                        };
//                        weighingFact.Sscc = sscc;
//                    }
//                    con.Close();
//                }
//            }
//        }

//        /// <summary>
//        /// SQL-запрос.
//        /// Редактировал: 2020-06-23 Морозов Дамиан.
//        /// </summary>
//        /// <returns></returns>
//        public static string SaveWeighingFactGetQuery()
//        {
//            return @"
//DECLARE @SSCC varchar(50)
//DECLARE @WeithingDate datetime
//DECLARE @xmldata xml;
//EXECUTE [db_scales].[SetWeithingFact] @OrderID,@ScaleID,@PLU,@NetWeight,@TareWeight,@ProductDate,@SSCC OUTPUT,@WeithingDate OUTPUT,@xmldata OUTPUT
//SELECT @SSCC [SSCC], @WEITHINGDATE [WEITHINGDATE], CONVERT(VARCHAR(MAX), @XMLDATA) [XMLDATA]
//                    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
//        }
//    }
//}
