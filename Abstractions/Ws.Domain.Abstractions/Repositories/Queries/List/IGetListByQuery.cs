using NHibernate.Criterion;

namespace Ws.Domain.Abstractions.Repositories.Queries.List;

public interface IGetListByQuery<TItem>
{
    IEnumerable<TItem> GetListByQuery(QueryOver<TItem> query);
}