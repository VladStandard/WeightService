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

    public static void SetCriteriaFilters(this ICriteria criteria, List<SqlFieldFilterModel>? filters)
    {
        if (filters is null) return;

        foreach (SqlFieldFilterModel filter in filters)
        {
            AbstractCriterion? criterion = filter.Comparer switch
            {
                WsSqlFieldComparer.Less => Restrictions.Lt(filter.Name, filter.Value),
                WsSqlFieldComparer.More => Restrictions.Gt(filter.Name, filter.Value),
                WsSqlFieldComparer.LessOrEqual => Restrictions.Le(filter.Name, filter.Value),
                WsSqlFieldComparer.MoreOrEqual => Restrictions.Ge(filter.Name, filter.Value),
                WsSqlFieldComparer.Equal => Restrictions.Eq(filter.Name, filter.Value),
                WsSqlFieldComparer.NotEqual => Restrictions.Not(Restrictions.Eq(filter.Name, filter.Value)),
                WsSqlFieldComparer.In => Restrictions.In(filter.Name, filter.Values),
                _ => throw new ArgumentOutOfRangeException()
            };
            if (criterion is not null)
                criteria.Add(criterion);
        }
    }

    public static void SetCriteriaOrder(this ICriteria criteria, List<SqlFieldOrderModel>? orders)
    {
        if (orders is null) return;

        foreach (SqlFieldOrderModel order in orders)
        {
            switch (order.Direction)
            {
                case WsSqlOrderDirection.Asc:
                    criteria.AddOrder(Order.Asc(order.Name));
                    break;
                case WsSqlOrderDirection.Desc:
                    criteria.AddOrder(Order.Desc(order.Name));
                    break;
            }
        }
    }

    #endregion
}