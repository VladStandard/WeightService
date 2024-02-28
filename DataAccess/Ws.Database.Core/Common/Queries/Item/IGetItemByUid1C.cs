using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Database.Core.Common.Queries.Item;

internal interface IGetItemByUid1C<out TItem> where TItem : Entity1CBase
{
    TItem GetByUid1C(Guid uid1C);
}