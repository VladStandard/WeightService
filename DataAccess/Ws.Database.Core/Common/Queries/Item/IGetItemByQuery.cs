namespace Ws.Database.Core.Common.Queries.Item;

internal interface IGetItemByQuery<TItem>
{
    TItem GetItemByQuery(QueryOver<TItem> query);
}