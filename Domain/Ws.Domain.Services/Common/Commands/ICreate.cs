namespace Ws.Domain.Services.Common.Commands;

public interface ICreate<in TItem>
{
    void Create(TItem item);
}