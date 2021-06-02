// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace EntitiesLib
{
    public enum OrderStatus
    {
        New = 0,
        InProgress = 1,
        Paused = 2,
        Performed = 3,
        Canceled = 4
    }

    [Serializable]
    public class OrderEntity
    {
        public int Id { get; set; }
        public int OrderType { get; set; } = 1;
        public string RRefID { get; set; }

        public PluEntity PLU { get; set; }
        public int TemplateID { get; set; }
        public TemplateEntity Template { get; set; }

        public int PlaneBoxCount { get; set; }
        public int FactBoxCount { get; set; } = 0;
        public int PlanePalletCount { get; set; }
        public DateTime? PlanePackingOperationBeginDate { get; set; }
        public DateTime? PlanePackingOperationEndDate { get; set; }
        public ScaleEntity Scale { get; set; }
        public DateTime ProductDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public OrderStatus Status { get; set; }


        public override bool Equals(object obj)
        {
            if (!(obj is OrderEntity item))
            {
                return false;
            }

            return Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public OrderEntity()
        {

        }

        public OrderEntity(PluEntity _plu)
        {
            PLU = _plu;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"({ProductDate})");
            sb.Append($"{PLU}");
            return sb.ToString();
        }

        public static IDictionary<string, object> ObjectToDictionary<T>(T item)
            where T : class
        {
            var myObjectType = item.GetType();
            IDictionary<string, object> dict = new Dictionary<string, object>();
            var indexer = new object[0];
            var properties = myObjectType.GetProperties();
            foreach (var info in properties)
            {
                var value = info.GetValue(item, indexer);
                dict.Add(info.Name, value);
            }
            return dict;
        }

        public static T ObjectFromDictionary<T>(IDictionary<string, object> dict)
            where T : class
        {
            var type = typeof(T);
            var result = (T)Activator.CreateInstance(type);
            foreach (var item in dict)
            {
                type.GetProperty(item.Key)?.SetValue(result, item.Value, null);
            }
            return result;
        }

        public string SerializeObject()
        {
            var xmlSerializer = new XmlSerializer(typeof(OrderEntity));
            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }

        public void LoadTemplate()
        {
            Template = new TemplateEntity(TemplateID);
        }


        public void SetStatus(OrderStatus orderStatus)
        {
            using (var con = SqlConnectFactory.GetConnection())
            {
                var query =
                    "DECLARE @Status tinyint;" +
                    "EXECUTE [db_scales].[SetOrderStatus] @OrderId, @StatusName, @Status OUTPUT" +
                    "";
                using (var cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@OrderId", RRefID);
                    cmd.Parameters.AddWithValue("@StatusName", orderStatus.ToString());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Status = orderStatus;
                }
            }
        }

        public OrderStatus GetStatus()
        {
            using (var con = SqlConnectFactory.GetConnection())
            {
                var query = "SELECT [db_scales].[GetOrderStatus](@OrderId, DEFAULT);";
                using (var cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@OrderId", RRefID);
                    con.Open();
                    var reader = Convert.ToString(cmd.ExecuteScalar());
                    con.Close();
                    Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), reader);
                    return Status;
                }
            }
        }

        public static int GetOrderPercentCompletion(OrderEntity order)
        {
            using (var con = SqlConnectFactory.GetConnection())
            {
                var query = "SELECT [db_scales].[GetOrderPercentCompletion] (@OrderID)";
                using (var cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@OrderId", order.RRefID);
                    con.Open();
                    var reader = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    return reader;
                }
            }
        }

        public static List<OrderEntity> GetOrderList(ScaleEntity scale)
        {
            var res = new List<OrderEntity>();
            using (var con = SqlConnectFactory.GetConnection())
            {
                var query = "" +
                            "SELECT " +
                            "[Id]," +                               //0
                            "[RRefID]," +                           //1
                            "[PLU]," +                              //2
                            "[PlaneBoxCount]," +                    //3
                            "[PlanePalletCount]," +                 //4
                            "[PlanePackingOperationBeginDate]," +    //5
                            "[PlanePackingOperationEndDate]," +     //6
                            "[ProductDate]," +                      //7
                            "[TemplateID]," +                       //8
                            "[CreateDate]," +                       //9
                            "[OrderType]," +                        //10
                            "[ScaleID]," +                          //11
                            "[CurrentStatus] " +                    //12
                            "FROM [db_scales].[GetOrderListByScale] (@ScaleId, @StartDate, @EndDate); " +
                            "";
                using (var cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ScaleId", scale.Id);
                    cmd.Parameters.AddWithValue("@StartDate", DateTime.Now.AddDays(-2));
                    cmd.Parameters.AddWithValue("@EndDate", DateTime.Now);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var enm = OrderStatus.New;
                        int number = reader.GetByte(12);
                        if (Enum.IsDefined(typeof(OrderStatus), number))
                        {
                            enm = (OrderStatus)number; // преобразование 
                                                       // или CustomEnum enm = (CustomEnum)Enum.ToObject(typeof(CustomEnum), number);
                        }

                        var pluCode = reader.GetInt32(2);

                        //PluEntity PLU = new PluEntity(SqlConnectFactory.GetValue<int>(reader, "PLU"), pluCode);
                        var PLU = new PluEntity(scale, pluCode);
                        PLU.Load();

                        var order = new OrderEntity
                        {
                            Id = SqlConnectFactory.GetValue<int>(reader, "Id"),
                            //RRefID = SqlConnectFactory.GetValue<string>(reader, "RRefID"),
                            PlaneBoxCount = SqlConnectFactory.GetValue<int>(reader, "PlaneBoxCount"),
                            PlanePalletCount = SqlConnectFactory.GetValue<int>(reader, "PlanePalletCount"),
                            PlanePackingOperationBeginDate = SqlConnectFactory.GetValue<DateTime>(reader, "PlanePackingOperationBeginDate"),
                            PlanePackingOperationEndDate = SqlConnectFactory.GetValue<DateTime>(reader, "PlanePackingOperationEndDate"),
                            ProductDate = SqlConnectFactory.GetValue<DateTime>(reader, "ProductDate"),
                            TemplateID = SqlConnectFactory.GetValue<int>(reader, "TemplateID"),
                            CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate"),
                            OrderType = SqlConnectFactory.GetValue<int>(reader, "OrderType"),
                            Scale = scale,
                            Status = enm,
                            PLU = PLU
                            //MyEnum myEnum = (MyEnum)myInt;
                            //MyEnum myEnum = (MyEnum)Enum.Parse(typeof(MyEnum), myString);
                        };
                        res.Add(order);
                    }
                    reader.Close();
                    con.Close();
                }
            }
            return res;
        }

        public static List<OrderEntity> GetOrderList(ScaleEntity scale, DateTime startDate, DateTime endDate)
        {
            var res = new List<OrderEntity>();
            using (var con = SqlConnectFactory.GetConnection())
            {
                var query = "" +
                            "SELECT " +
                            "[Id]," +                               //0
                            "[RRefID]," +                           //1
                            "[PLU]," +                              //2
                            "[PlaneBoxCount]," +                    //3
                            "[PlanePalletCount]," +                 //4
                            "[PlanePackingOperationBeginDate]," +    //5
                            "[PlanePackingOperationEndDate]," +     //6
                            "[ProductDate]," +                      //7
                            "[TemplateID]," +                       //8
                            "[CreateDate]," +                       //9
                            "[OrderType]," +                        //10
                            "[ScaleID]," +                          //11
                            "[CurrentStatus] " +                    //12
                            "FROM [db_scales].[GetOrderListByScale] (@ScaleId, @StartDate, @EndDate); " +
                            "";
                using (var cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ScaleId", scale.Id);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var enm = OrderStatus.New;
                        int number = reader.GetByte(12);
                        if (Enum.IsDefined(typeof(OrderStatus), number))
                        {
                            enm = (OrderStatus)number; // преобразование 
                                                       // или CustomEnum enm = (CustomEnum)Enum.ToObject(typeof(CustomEnum), number);
                        }
                        var pluCode = reader.GetInt32(2);

                        var PLU = new PluEntity(scale, pluCode);
                        PLU.Load();

                        var order = new OrderEntity
                        {
                            Id = SqlConnectFactory.GetValue<int>(reader, "Id"),
                            RRefID = SqlConnectFactory.GetValue<string>(reader, "RRefID"),
                            PlaneBoxCount = SqlConnectFactory.GetValue<int>(reader, "PlaneBoxCount"),
                            PlanePalletCount = SqlConnectFactory.GetValue<int>(reader, "PlanePalletCount"),
                            PlanePackingOperationBeginDate = SqlConnectFactory.GetValue<DateTime>(reader, "PlanePackingOperationBeginDate"),
                            PlanePackingOperationEndDate = SqlConnectFactory.GetValue<DateTime>(reader, "PlanePackingOperationEndDate"),
                            ProductDate = SqlConnectFactory.GetValue<DateTime>(reader, "ProductDate"),
                            TemplateID = SqlConnectFactory.GetValue<int>(reader, "TemplateID"),
                            CreateDate = SqlConnectFactory.GetValue<DateTime>(reader, "CreateDate"),
                            OrderType = SqlConnectFactory.GetValue<int>(reader, "OrderType"),
                            Scale = scale,
                            Status = enm,
                            PLU = PLU
                            //MyEnum myEnum = (MyEnum)myInt;
                            //MyEnum myEnum = (MyEnum)Enum.Parse(typeof(MyEnum), myString);
                        };
                        res.Add(order);
                    }
                    reader.Close();
                    con.Close();
                }
            }
            return res;
        }
    }
}