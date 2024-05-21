namespace Ws.Domain.Services.Common.Queries;

public interface IGetAll<TItem>
{
    IList<TItem> GetAll();
}