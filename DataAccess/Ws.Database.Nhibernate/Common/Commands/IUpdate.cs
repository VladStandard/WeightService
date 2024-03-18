using Ws.Domain.Models.Common;

namespace Ws.Database.Nhibernate.Common.Commands;

internal interface IUpdate<TItem> where TItem : EntityBase, new()
{
    TItem Update(TItem item);
}