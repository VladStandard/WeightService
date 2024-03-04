using NHibernate.Event;
using Ws.Domain.Models.Common;

namespace Ws.Database.Core.Listeners;

internal class SqlChangeDtListener : BaseListener, IPreUpdateEventListener
{
    public bool OnPreUpdate(PreUpdateEvent @event)
    {
        if (@event.Entity is not EntityBase entity)
            return false;

        entity.ChangeDt = DateTime.Now;

        Set(@event.Persister, @event.State, nameof(entity.ChangeDt), entity.ChangeDt);

        return false;
    }

    public Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
    {
        return Task.FromResult(OnPreUpdate(@event));
    }
}