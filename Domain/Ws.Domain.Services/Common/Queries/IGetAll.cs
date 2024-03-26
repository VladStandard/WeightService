namespace Ws.Domain.Services.Common.Queries;

public interface IGetAll<out TItem>
{
    IEnumerable<TItem> GetAll();
}