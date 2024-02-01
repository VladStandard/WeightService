namespace Ws.Database.Core.Common.Queries.List;

public interface IGetListByQuery<TItem>
{
    IEnumerable<TItem> GetListByQuery(QueryOver<TItem> query);
}