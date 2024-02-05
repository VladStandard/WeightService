using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Abstractions.Repositories.Queries.Item;

public interface IGetItemByUid<out TItem> where TItem : EntityBase, new()
{
    TItem GetByUid(Guid uid);
}