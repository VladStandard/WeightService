namespace WsStorageCore.Utils;

public static class WsSqlExtensions
{
    #region Public methods

    /// <summary>
    /// Check field exists.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public static bool IsFieldExists(this SqlDataReader reader, string fieldName)
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (reader.GetName(i).Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
                return true;
        }
        return false;
    }

    public static void SetCriteriaOrder(this ICriteria criteria, List<WsSqlFieldOrderModel>? orders)
    {
        if (orders is null) return;

        foreach (WsSqlFieldOrderModel order in orders)
        {
            switch (order.Direction)
            {
                case WsSqlEnumOrder.Asc:
                    criteria.AddOrder(Order.Asc(order.Name));
                    break;
                case WsSqlEnumOrder.Desc:
                    criteria.AddOrder(Order.Desc(order.Name));
                    break;
            }
        }
    }

    #endregion
}