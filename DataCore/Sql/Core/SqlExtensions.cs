// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NHibernate;
using NHibernate.Criterion;

namespace DataCore.Sql.Core;

/// <summary>
/// SQL extensions.
/// </summary>
public static class SqlExtensions
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
		if (filters is null)
			return;

		foreach (SqlFieldFilterModel filter in filters)
		{
			AbstractCriterion? criterion = filter.Comparer switch
			{
				SqlFieldComparerEnum.Equal => Restrictions.Eq(filter.Name, filter.Value),
				SqlFieldComparerEnum.NotEqual => Restrictions.Not(Restrictions.Eq(filter.Name, filter.Value)),
				_ => throw new ArgumentOutOfRangeException()
			};
			if (criterion != null)
				criteria.Add(criterion);
		}
	}

	public static void SetCriteriaOrder(this ICriteria criteria, SqlFieldOrderModel? order)
	{
		if (order is not null)
		{
			Order fieldOrder = order.Direction == SqlFieldOrderDirectionEnum.Asc
				? Order.Asc(order.Name.ToString()) : Order.Desc(order.Name.ToString());
			criteria.AddOrder(fieldOrder);
		}
	}
    
	#endregion
}
