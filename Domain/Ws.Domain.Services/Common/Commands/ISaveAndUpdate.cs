namespace Ws.Domain.Services.Common.Commands;

public interface ISaveAndUpdate<in TItem> : IUpdate<TItem>, ICreate<TItem>
{
    void SaveOrUpdate(TItem item);
}