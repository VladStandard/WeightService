using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Abstractions.Repositories.Queries.Item;

public interface IGetItemByUid1C<out TItem> where TItem : Entity1CBase
{
    TItem GetByUid1C(Guid uid1C);
}