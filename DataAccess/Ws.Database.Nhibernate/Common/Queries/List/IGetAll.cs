namespace Ws.Database.Nhibernate.Common.Queries.List;

internal interface IGetAll<out TItem>
{
    IEnumerable<TItem> GetAll();
}