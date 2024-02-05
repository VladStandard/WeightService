namespace Ws.Domain.Services.Common.Commands;

public interface IUpdate<in TItem>
{
    void Update(TItem item);
}