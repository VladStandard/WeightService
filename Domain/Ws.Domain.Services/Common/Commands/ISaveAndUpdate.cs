namespace Ws.Domain.Services.Common.Commands;

public interface ISaveAndUpdate<in TItem> : IUpdate<TItem>, ISave<TItem>
{
    void SaveOrUpdate(TItem item);
}