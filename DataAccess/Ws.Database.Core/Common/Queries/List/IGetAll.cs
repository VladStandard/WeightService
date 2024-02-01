namespace Ws.Database.Core.Common.Queries.List;

internal interface IGetAll<out TItem>
{
    IEnumerable<TItem> GetAll();
}