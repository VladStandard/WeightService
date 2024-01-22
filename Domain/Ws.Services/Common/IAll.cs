namespace Ws.Services.Common;

public interface IAll<out TItem>
{
    IEnumerable<TItem> GetAll();
}