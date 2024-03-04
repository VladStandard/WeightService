using Ws.Domain.Models.Common;

namespace Ws.Database.Core.Common.Commands;

internal interface IDelete<TItem> where TItem : EntityBase, new()
{
    void Delete(TItem item);
}