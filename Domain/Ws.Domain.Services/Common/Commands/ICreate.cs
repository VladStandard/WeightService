namespace Ws.Domain.Services.Common.Commands;

public interface ICreate<TItem>
{
    TItem Create(TItem item);
}