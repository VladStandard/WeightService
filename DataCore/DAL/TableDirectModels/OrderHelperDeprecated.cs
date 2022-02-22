//using Microsoft.Data.SqlClient;
//using System;
//using System.Collections.Generic;

//namespace DataCore.DAL.TableDirectModels
//{
//    public class OrderHelper
//    {
//        // Помощник SQL.
//        private readonly SqlHelper _sql { get; set; } = SqlHelper.Instance;

//        public void SetStatus(OrderEntity order, OrderStatus orderStatus)
//        {
//            using (SqlConnection con = new SqlConnection(_sql.ConnectionString))
//            {
//                string query =
//                    "DECLARE @Status tinyint;" +
//                    "EXECUTE [db_scales].[SetOrderStatus] @OrderId, @StatusName, @Status OUTPUT" +
//                    "";
//                using (SqlCommand cmd = new SqlCommand(query))
//                {
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@OrderId", order.RRefID);
//                    cmd.Parameters.AddWithValue("@StatusName", orderStatus.ToString());
//                    con.Open();
//                    cmd.ExecuteNonQuery();
//                    con.Close();
//                    order.Status = orderStatus;
//                }
//            }
//        }

//        public OrderStatus GetStatus(OrderEntity order)
//        {

//            using (SqlConnection con = new SqlConnection(_sql.ConnectionString))
//            {
//                string query = "SELECT [db_scales].[GetOrderStatus](@OrderId, DEFAULT);";
//                using (SqlCommand cmd = new SqlCommand(query))
//                {
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@OrderId", order.RRefID);
//                    con.Open();
//                    string reader = Convert.ToString(cmd.ExecuteScalar());
//                    con.Close();
//                    return (OrderStatus)Enum.Parse(typeof(OrderStatus), reader);
//                }
//            }
//        }

//        public int GetOrderPercentCompletion(OrderEntity order)
//        {
//            using (SqlConnection con = new SqlConnection(_sql.ConnectionString))
//            {
//                string query = "SELECT [db_scales].[GetOrderPercentCompletion] (@OrderID)";
//                using (SqlCommand cmd = new SqlCommand(query))
//                {
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@OrderId", order.RRefID);
//                    con.Open();
//                    int reader = Convert.ToInt32(cmd.ExecuteScalar());
//                    con.Close();
//                    return reader;
//                }
//            }
//        }

//        public List<OrderEntity> GetOrderList(string ScaleId)
//        {
//            List<OrderEntity> res = new List<OrderEntity>();
//            using (SqlConnection con = new SqlConnection(_sql.ConnectionString))
//            {
//                string query = "" +
//                    "SELECT " +
//                    "[Id]," +                               //0
//                    "[RRefID]," +                           //1
//                    "[PLU]," +                              //2
//                    "[PlaneBoxCount]," +                    //3
//                    "[PlanePalletCount]," +                 //4
//                    "[PlanePackingOperationBeginDate]," +    //5
//                    "[PlanePackingOperationEndDate]," +     //6
//                    "[ProductDate]," +                      //7
//                    "[TemplateID]," +                       //8
//                    "[CreateDate]," +                       //9
//                    "[OrderType]," +                        //10
//                    "[ScaleID]," +                          //11
//                    "[CurrentStatus] " +                    //12
//                    "FROM [db_scales].[GetOrderListByScale] (@ScaleId, @StartDate, @EndDate); " +
//                    "";
//                using (SqlCommand cmd = new SqlCommand(query))
//                {
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@ScaleId", ScaleId);
//                    cmd.Parameters.AddWithValue("@StartDate", DateTime.Now.AddDays(-2));
//                    cmd.Parameters.AddWithValue("@EndDate", DateTime.Now);
//                    con.Open();
//                    SqlDataReader reader = cmd.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        OrderStatus enm = OrderStatus.New;
//                        int number = reader.GetByte(12);
//                        if (Enum.IsDefined(typeof(OrderStatus), number))
//                        {
//                            enm = (OrderStatus)number; // преобразование 
//                                                       // или CustomEnum enm = (CustomEnum)Enum.ToObject(typeof(CustomEnum), number);
//                        }

//                        int pluCode = reader.GetInt32(2);


//                        OrderEntity order = new OrderEntity
//                        {
//                            Id = reader.GetInt32(0),
//                            RRefID = reader.GetString(1),
//                            PlaneBoxCount = reader.GetInt32(3),
//                            PlanePalletCount = reader.GetInt32(4),
//                            PlanePackingOperationBeginDate = reader.IsDBNull(5) ? DateTime.Now : reader.GetDateTime(5),
//                            PlanePackingOperationEndDate = reader.IsDBNull(6) ? DateTime.Now : reader.GetDateTime(6),
//                            ProductDate = reader.IsDBNull(7) ? DateTime.Now : reader.GetDateTime(7),
//                            TemplateID = reader.GetString(8),
//                            CreateDate = reader.IsDBNull(9) ? DateTime.Now : reader.GetDateTime(9),
//                            OrderType = reader.GetInt32(10),
//                            ScaleID = reader.GetString(11),
//                            Status = enm,
//                            PLU = PLUHelper.GetPLU(reader.GetString(11), pluCode)
//                            //MyEnum myEnum = (MyEnum)myInt;
//                            //MyEnum myEnum = (MyEnum)Enum.Parse(typeof(MyEnum), myString);
//                        };
//                        res.Add(order);
//                    }
//                    reader.Close();
//                    con.Close();
//                }
//            }
//            return res;
//        }

//        public List<OrderEntity> GetOrderList(string ScaleId, DateTime startDate, DateTime endDate)
//        {
//            List<OrderEntity> res = new List<OrderEntity>();
//            using (SqlConnection con = new SqlConnection(_sql.ConnectionString))
//            {
//                string query = "" +
//                    "SELECT " +
//                    "[Id]," +                               //0
//                    "[RRefID]," +                           //1
//                    "[PLU]," +                              //2
//                    "[PlaneBoxCount]," +                    //3
//                    "[PlanePalletCount]," +                 //4
//                    "[PlanePackingOperationBeginDate]," +    //5
//                    "[PlanePackingOperationEndDate]," +     //6
//                    "[ProductDate]," +                      //7
//                    "[TemplateID]," +                       //8
//                    "[CreateDate]," +                       //9
//                    "[OrderType]," +                        //10
//                    "[ScaleID]," +                          //11
//                    "[CurrentStatus] " +                    //12
//                    "FROM [db_scales].[GetOrderListByScale] (@ScaleId, @StartDate, @EndDate); " +
//                    "";
//                using (SqlCommand cmd = new SqlCommand(query))
//                {
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@ScaleId", ScaleId);
//                    cmd.Parameters.AddWithValue("@StartDate", startDate);
//                    cmd.Parameters.AddWithValue("@EndDate", endDate);
//                    con.Open();
//                    SqlDataReader reader = cmd.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        var enm = OrderStatus.New;
//                        int number = reader.GetByte(12);
//                        if (Enum.IsDefined(typeof(OrderStatus), number))
//                        {
//                            enm = (OrderStatus)number; // преобразование 
//                                                       // или CustomEnum enm = (CustomEnum)Enum.ToObject(typeof(CustomEnum), number);
//                        }
//                        var pluCode = reader.GetInt32(2);
//                        var order = new OrderEntity
//                        {
//                            Id = reader.GetInt32(0),
//                            RRefID = reader.GetString(1),
//                            PlaneBoxCount = reader.GetInt32(3),
//                            PlanePalletCount = reader.GetInt32(4),
//                            PlanePackingOperationBeginDate = reader.IsDBNull(5) ? DateTime.Now : reader.GetDateTime(5),
//                            PlanePackingOperationEndDate = reader.IsDBNull(6) ? DateTime.Now : reader.GetDateTime(6),
//                            ProductDate = reader.IsDBNull(7) ? DateTime.Now : reader.GetDateTime(7),
//                            TemplateID = reader.GetString(8),
//                            CreateDate = reader.IsDBNull(9) ? DateTime.Now : reader.GetDateTime(9),
//                            OrderType = reader.GetInt32(10),
//                            ScaleID = reader.GetString(11),
//                            Status = enm,
//                            PLU = PLUHelper.GetPLU(reader.GetString(11), pluCode)
//                            //MyEnum myEnum = (MyEnum)myInt;
//                            //MyEnum myEnum = (MyEnum)Enum.Parse(typeof(MyEnum), myString);
//                        };
//                        res.Add(order);
//                    }
//                    reader.Close();
//                    con.Close();
//                }
//            }
//            return res;
//        }
//    }
//}
