using System.Linq.Expressions;

namespace Ws.Shared.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> IfWhere<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        => condition ? query.Where(predicate) : query;
}