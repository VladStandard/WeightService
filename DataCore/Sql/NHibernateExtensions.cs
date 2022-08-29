// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Fields;
using NHibernate;
using NHibernate.Criterion;

namespace DataCore.Sql;

public static class NHibernateExtensions
{
	public static void SetCriteriaFilters(this ICriteria criteria, List<FieldFilterModel>? filters)
	{
		if (filters is null)
			return;

		foreach (FieldFilterModel filter in filters)
		{
			AbstractCriterion? criterion = filter.Comparer switch
			{
				ShareEnums.DbComparer.Equal => Restrictions.Eq(filter.Name, filter.Value),
				ShareEnums.DbComparer.NotEqual => Restrictions.Not(Restrictions.Eq(filter.Name, filter.Value)),
				_ => throw new ArgumentOutOfRangeException()
			};
			if (criterion != null)
				criteria.Add(criterion);
		}
	}

	public static void SetCriteriaOrder(this ICriteria criteria, FieldOrderModel? order)
	{
		if (order is not null)
		{
			Order fieldOrder = order.Direction == ShareEnums.DbOrderDirection.Asc
				? Order.Asc(order.Name.ToString()) : Order.Desc(order.Name.ToString());
			criteria.AddOrder(fieldOrder);
		}
	}

}
