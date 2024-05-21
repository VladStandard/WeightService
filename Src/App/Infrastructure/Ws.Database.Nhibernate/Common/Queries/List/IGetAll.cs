namespace Ws.Database.Nhibernate.Common.Queries.List;

internal interface IGetAll<TItem>
{
    IList<TItem> GetAll();
}