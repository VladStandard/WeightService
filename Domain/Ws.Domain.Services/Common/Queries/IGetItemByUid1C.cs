using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Services.Common.Queries;

public interface IGetItemByUid1C<out TItem> where TItem : Entity1CBase
{
    TItem GetItemByUid1С(Guid uid);
}