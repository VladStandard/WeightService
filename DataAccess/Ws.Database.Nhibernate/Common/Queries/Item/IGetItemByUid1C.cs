using Ws.Domain.Models.Common;

namespace Ws.Database.Nhibernate.Common.Queries.Item;

internal interface IGetItemByUid1C<out TItem> where TItem : Entity1CBase
{
    TItem GetByUid1C(Guid uid1C);
}