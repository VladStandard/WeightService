namespace Ws.Domain.Abstractions.Repositories.Queries.List;

public interface IGetAll<out TItem>
{
    IEnumerable<TItem> GetAll();
}