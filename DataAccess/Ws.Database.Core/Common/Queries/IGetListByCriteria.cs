namespace Ws.Database.Core.Common.Queries;

internal interface IGetListByCriteria<out TItem>
{
    IEnumerable<TItem> GetListByCriteria(DetachedCriteria criteria);
}