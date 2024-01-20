using NHibernate.Event;
using Ws.Domain.Models.Common;

namespace Ws.StorageCore.Listeners;

internal class SqlCreateDtListener : BaseListener, IPreInsertEventListener
{
    public bool OnPreInsert(PreInsertEvent @event)
    {
        if (@event.Entity is not EntityBase entity)
            return false;
        
        DateTime now = DateTime.Now;
        entity.CreateDt = now;
        entity.ChangeDt = now;

        Set(@event.Persister, @event.State, nameof(entity.CreateDt), entity.CreateDt);
        Set(@event.Persister, @event.State, nameof(entity.ChangeDt), entity.ChangeDt);
            
        return false;
    }

    public Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
    {
        return Task.FromResult(OnPreInsert(@event));
    }
}