namespace Ws.Domain.Services.Common.Commands;

public interface IDelete<in TItem>
{
    void Delete(TItem item);
}