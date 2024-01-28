namespace Ws.Database.Core.Common.Queries;

internal interface IGetAll<out TItem>
{
    IEnumerable<TItem> GetAll();
}