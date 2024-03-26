using Ws.Domain.Models.Common;

namespace Ws.Database.Nhibernate.Common.Queries.Item;

internal interface IGetItemByUid<out TItem> where TItem : EntityBase, new()
{
    TItem GetByUid(Guid uid);
}