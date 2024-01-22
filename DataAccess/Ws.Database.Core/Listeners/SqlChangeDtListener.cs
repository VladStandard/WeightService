using NHibernate.Event;
using Ws.Domain.Models.Common;

namespace Ws.Database.Core.Listeners;

internal class SqlChangeDtListener : BaseListener, IPreUpdateEventListener
{
    public bool OnPreUpdate(PreUpdateEvent @event)
    {
        if (@event.Entity is not EntityBase entity)
            return false;
            
        DateTime now = DateTime.Now;
        entity.ChangeDt = now;
            
        Set(@event.Persister, @event.State, nameof(entity.ChangeDt), entity.ChangeDt);
            
        return false;
    }
    
    public Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
    {
        return Task.FromResult(OnPreUpdate(@event));
    }
}