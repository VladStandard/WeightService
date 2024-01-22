using Ws.Domain.Models.Common;

namespace Ws.Database.Core.Common;

internal interface IUidRepo<out TItem> where TItem : EntityBase, new()
{
    TItem GetByUid(Guid uid);
}