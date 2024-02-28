using Ws.Domain.Models.Common;

namespace Ws.Database.Core.Common.Commands;

internal interface ISave<TItem> where TItem : EntityBase, new()
{
    TItem Save(TItem item);
}