using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Abstractions.Repositories.Commands;

public interface IDelete<TItem> where TItem : EntityBase, new()
{
    TItem Delete(TItem item);
}