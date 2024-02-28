using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Database.Core.Common.Queries.Item;

internal interface IGetItemByUid<out TItem> where TItem : EntityBase, new()
{
    TItem GetByUid(Guid uid);
}