// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Utils;

/// <summary>
/// SQL extensions.
/// </summary>
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

    public static void SetCriteriaFilters(this ICriteria criteria, List<WsSqlFieldFilterModel> filters)
    {
        if (!filters.Any()) return;
        foreach (WsSqlFieldFilterModel filter in filters)
        {
            criteria.Add(filter.Comparer switch
            {
                WsSqlEnumFieldComparer.Less => Restrictions.Lt(filter.Name, filter.Value),
                WsSqlEnumFieldComparer.More => Restrictions.Gt(filter.Name, filter.Value),
                WsSqlEnumFieldComparer.LessOrEqual => Restrictions.Le(filter.Name, filter.Value),
                WsSqlEnumFieldComparer.MoreOrEqual => Restrictions.Ge(filter.Name, filter.Value),
                WsSqlEnumFieldComparer.Equal => Restrictions.Eq(filter.Name, filter.Value),
                WsSqlEnumFieldComparer.NotEqual => Restrictions.Not(Restrictions.Eq(filter.Name, filter.Value)),
                WsSqlEnumFieldComparer.In => Restrictions.In(filter.Name, filter.Values),
                _ => throw new ArgumentOutOfRangeException(nameof(filter.Comparer), filter.Comparer.ToString())
            });
        }
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