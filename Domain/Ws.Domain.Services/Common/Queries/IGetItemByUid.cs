namespace Ws.Domain.Services.Common.Queries;

public interface IGetItemByUid<out TItem>
{
    TItem GetItemByUid(Guid uid);
}