using NHibernate.Criterion;

namespace Ws.Domain.Abstractions.Repositories.Queries.Item;

public interface IGetItemByQuery<TItem>
{
    TItem GetItemByQuery(QueryOver<TItem> query);
}