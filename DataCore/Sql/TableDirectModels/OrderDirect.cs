// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableDirectModels;

[Serializable]
public class OrderDirect : BaseSerializeEntity, ISerializable
{
    #region Public and private fields, properties, constructor

    public DateTime CreateDate { get; set; }
    public DateTime PlanePackingOperationBeginDate { get; set; }
    public DateTime PlanePackingOperationEndDate { get; set; }
    public DateTime ProductDate { get; set; }
    public int FactBoxCount { get; set; }
    public int OrderType { get; set; }
    public int PlaneBoxCount { get; set; }
    public int PlanePalletCount { get; set; }
    public long Id { get; set; }
    public long TemplateID { get; set; }
    public PluDirect PLU { get; set; }
    public ProjectsEnums.OrderStatus Status { get; set; }
    public string RRefID { get; set; }
    public ScaleEntity Scale { get; set; }
    public TemplateDirect Template { get; set; }

    #endregion

    #region Constructor and destructor

    public OrderDirect()
    {
        CreateDate = DateTime.MinValue;
        FactBoxCount = 0;
        Id = 0;
        OrderType = 1;
        PlaneBoxCount = 0;
        PlanePackingOperationBeginDate = DateTime.MinValue;
        PlanePackingOperationEndDate = DateTime.MinValue;
        PlanePalletCount = 0;
        PLU = new();
        ProductDate = DateTime.MinValue;
        RRefID = string.Empty;
        Scale = new();
        Status = ProjectsEnums.OrderStatus.New;
        Template = new();
        TemplateID = default;
    }

    public OrderDirect(PluDirect _plu) : this()
    {
        PLU = _plu;
    }

    #endregion

    #region Public and private methods

    public override bool Equals(object obj)
    {
        if (obj is not OrderDirect item)
        {
            return false;
        }

        return Id.Equals(item.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append($"({ProductDate})");
        sb.Append($"{PLU}");
        return sb.ToString();
    }

    public IDictionary<string, object> ObjectToDictionary<T>(T item) where T : class
    {
        Type myObjectType = item.GetType();
        IDictionary<string, object> dict = new Dictionary<string, object>();
        object[] indexer = new object[0];
        System.Reflection.PropertyInfo[] properties = myObjectType.GetProperties();
        foreach (System.Reflection.PropertyInfo info in properties)
        {
            object value = info.GetValue(item, indexer);
            dict.Add(info.Name, value);
        }
        return dict;
    }

    public T ObjectFromDictionary<T>(IDictionary<string, object> dict) where T : class
    {
        Type type = typeof(T);
        T result = (T)Activator.CreateInstance(type);
        foreach (KeyValuePair<string, object> item in dict)
        {
            type.GetProperty(item.Key)?.SetValue(result, item.Value, null);
        }
        return result;
    }

    public void LoadTemplate()
    {
        Template = new(TemplateID);
    }

    public void SetStatus(ProjectsEnums.OrderStatus orderStatus)
    {
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        string query =
            "DECLARE @Status tinyint;" +
            "EXECUTE [db_scales].[SetOrderStatus] @OrderId, @StatusName, @Status OUTPUT" +
            "";
        using (SqlCommand cmd = new(query))
        {
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@OrderId", RRefID);
            cmd.Parameters.AddWithValue("@StatusName", orderStatus.ToString());
            cmd.ExecuteNonQuery();
            Status = orderStatus;
        }
        con.Close();
    }

    public ProjectsEnums.OrderStatus GetStatus()
    {
        using (SqlConnection con = SqlConnect.GetConnection())
        {
            con.Open();
            string query = "SELECT [db_scales].[GetOrderStatus](@OrderId, DEFAULT);";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@OrderId", RRefID);
                string reader = Convert.ToString(cmd.ExecuteScalar());
                Status = (ProjectsEnums.OrderStatus)Enum.Parse(typeof(ProjectsEnums.OrderStatus), reader);
            }
            con.Close();
        }
        return Status;
    }

    public int GetOrderPercentCompletion(OrderDirect order)
    {
        int result = 0;
        using (SqlConnection con = SqlConnect.GetConnection())
        {
            con.Open();
            string query = "SELECT [db_scales].[GetOrderPercentCompletion] (@OrderID)";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@OrderId", order.RRefID);
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            con.Close();
        }
        return result;
    }

    public List<OrderDirect> GetOrderList(ScaleEntity scale)
    {
        List<OrderDirect> res = new();
        using (SqlConnection con = SqlConnect.GetConnection())
        {
            con.Open();
            string query = "" +
                        "SELECT " +
                        "[Id]," +                               //0
                        "[RRefID]," +                           //1
                        "[PLU]," +                              //2
                        "[PlaneBoxCount]," +                    //3
                        "[PlanePalletCount]," +                 //4
                        "[PlanePackingOperationBeginDate]," +   //5
                        "[PlanePackingOperationEndDate]," +     //6
                        "[ProductDate]," +                      //7
                        "[TemplateID]," +                       //8
                        "[CreateDate]," +                       //9
                        "[OrderType]," +                        //10
                        "[ScaleID]," +                          //11
                        "[CurrentStatus] " +                    //12
                        "FROM [db_scales].[GetOrderListByScale] (@ScaleId, @StartDate, @EndDate); " +
                        "";
            using (SqlCommand cmd = new(query))
            {
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@ScaleId", scale.IdentityId);
                cmd.Parameters.AddWithValue("@StartDate", DateTime.Now.AddDays(-2));
                cmd.Parameters.AddWithValue("@EndDate", DateTime.Now);
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProjectsEnums.OrderStatus enm = ProjectsEnums.OrderStatus.New;
                        int number = reader.GetByte(12);
                        if (Enum.IsDefined(typeof(ProjectsEnums.OrderStatus), number))
                        {
                            enm = (ProjectsEnums.OrderStatus)number; // преобразование 
                                                                     // или CustomEnum enm = (CustomEnum)Enum.ToObject(typeof(CustomEnum), number);
                        }
                        int pluCode = reader.GetInt32(2);
                        //PluEntity PLU = new PluEntity(SqlConnect.GetValueAsNotNullable<int>(reader, "PLU"), pluCode);
                        PluDirect PLU = new(scale, pluCode);
                        PLU.Load();
                        OrderDirect order = new()
                        {
                            Id = SqlConnect.GetValueAsNotNullable<long>(reader, "Id"),
                            //RRefID = SqlConnect.GetValueAsString(reader, "RRefID"),
                            PlaneBoxCount = SqlConnect.GetValueAsNotNullable<int>(reader, "PlaneBoxCount"),
                            PlanePalletCount = SqlConnect.GetValueAsNotNullable<int>(reader, "PlanePalletCount"),
                            PlanePackingOperationBeginDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "PlanePackingOperationBeginDate"),
                            PlanePackingOperationEndDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "PlanePackingOperationEndDate"),
                            ProductDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ProductDate"),
                            TemplateID = SqlConnect.GetValueAsNotNullable<int>(reader, "TemplateID"),
                            CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
                            OrderType = SqlConnect.GetValueAsNotNullable<int>(reader, "OrderType"),
                            Scale = scale,
                            Status = enm,
                            PLU = PLU
                            //MyEnum myEnum = (MyEnum)myInt;
                            //MyEnum myEnum = (MyEnum)Enum.Parse(typeof(MyEnum), myString);
                        };
                        res.Add(order);
                    }
                }
                reader.Close();
            }
            con.Close();
        }
        return res;
    }

    //public List<OrderDirect> GetOrderList(ScaleDirect scale, DateTime startDate, DateTime endDate)
    //{
    //    List<OrderDirect> res = new();
    //    using (SqlConnection con = SqlConnect.GetConnection())
    //    {
    //        con.Open();
    //        string query = "" +
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
    //        using (SqlCommand cmd = new(query))
    //        {
    //            cmd.Connection = con;
    //            cmd.Parameters.AddWithValue("@ScaleId", scale.Id);
    //            cmd.Parameters.AddWithValue("@StartDate", startDate);
    //            cmd.Parameters.AddWithValue("@EndDate", endDate);
    //            using SqlDataReader reader = cmd.ExecuteReader();
    //            if (reader.HasRows)
    //            {
    //                while (reader.Read())
    //                {
    //                    ProjectsEnums.OrderStatus orderStatus = ProjectsEnums.OrderStatus.New;
    //                    int number = reader.GetByte(12);
    //                    if (Enum.IsDefined(typeof(ProjectsEnums.OrderStatus), number))
    //                    {
    //                        orderStatus = (ProjectsEnums.OrderStatus)number; // преобразование 
    //                                                                         // или CustomEnum enm = (CustomEnum)Enum.ToObject(typeof(CustomEnum), number);
    //                    }
    //                    int pluCode = reader.GetInt32(2);
    //                    PluDirect PLU = new(scale, pluCode);
    //                    PLU.Load();
    //                    OrderDirect order = new()
    //                    {
    //                        Id = SqlConnect.GetValueAsNotNullable<long>(reader, "Id"),
    //                        RRefID = SqlConnect.GetValueAsString(reader, "RRefID"),
    //                        PlaneBoxCount = SqlConnect.GetValueAsNotNullable<int>(reader, "PlaneBoxCount"),
    //                        PlanePalletCount = SqlConnect.GetValueAsNotNullable<int>(reader, "PlanePalletCount"),
    //                        PlanePackingOperationBeginDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "PlanePackingOperationBeginDate"),
    //                        PlanePackingOperationEndDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "PlanePackingOperationEndDate"),
    //                        ProductDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ProductDate"),
    //                        TemplateID = SqlConnect.GetValueAsNotNullable<long>(reader, "TemplateID"),
    //                        CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
    //                        OrderType = SqlConnect.GetValueAsNotNullable<int>(reader, "OrderType"),
    //                        Scale = scale,
    //                        Status = orderStatus,
    //                        PLU = PLU
    //                        //MyEnum myEnum = (MyEnum)myInt;
    //                        //MyEnum myEnum = (MyEnum)Enum.Parse(typeof(MyEnum), myString);
    //                    };
    //                    res.Add(order);
    //                }
    //            }
    //            reader.Close();
    //        }
    //        con.Close();
    //    }
    //    return res;
    //}

    #endregion
}
