using NHibernate.Persister.Entity;

namespace Ws.Database.Nhibernate.Common;

internal abstract class BaseListener
{
    protected static void Set(IEntityPersister persister, IList<object> state, string propertyName, object value)
    {
        var index = Array.IndexOf(persister.PropertyNames, propertyName);
        if (index == -1)
            return;
        state[index] = value;
    }
}