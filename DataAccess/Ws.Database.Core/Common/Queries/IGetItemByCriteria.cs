namespace Ws.Database.Core.Common.Queries;

internal interface IGetItemByCriteria<out TItem>
{
    TItem GetItemByCriteria(DetachedCriteria criteria);
}