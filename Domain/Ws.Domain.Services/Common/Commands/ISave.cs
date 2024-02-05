namespace Ws.Domain.Services.Common.Commands;

public interface ISave<in TItem>
{
    void Create(TItem item);
}