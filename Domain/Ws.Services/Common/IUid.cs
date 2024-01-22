namespace Ws.Services.Common;

public interface IUid<out TItem>
{
    TItem GetByUid(Guid uid);
}