namespace Ws.Domain.Services.Common.Commands;

public interface IUpdate<TItem>
{
    TItem Update(TItem item);
}