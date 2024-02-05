using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Abstractions.Repositories.Commands;

public interface ISave<TItem> where TItem : EntityBase, new()
{
    TItem Save(TItem item);
}