using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Database.Core.Common.Commands;

internal interface IDelete<TItem> where TItem : EntityBase, new()
{
    TItem Delete(TItem item);
}