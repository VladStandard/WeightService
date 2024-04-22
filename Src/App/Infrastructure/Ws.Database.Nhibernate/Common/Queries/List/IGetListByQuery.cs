namespace Ws.Database.Nhibernate.Common.Queries.List;

internal interface IGetListByQuery<TItem>
{
    IEnumerable<TItem> GetListByQuery(QueryOver<TItem> query);
}