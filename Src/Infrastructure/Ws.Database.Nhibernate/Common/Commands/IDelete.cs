using Ws.Domain.Models.Common;

namespace Ws.Database.Nhibernate.Common.Commands;

internal interface IDelete<in TItem> where TItem : EntityBase, new()
{
    void Delete(TItem item);
}