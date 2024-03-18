using NHibernate.Context;

namespace Ws.Database.Nhibernate.Sessions;

public class ReadSession : IDisposable
{
    private readonly ISession _session;

    public ReadSession()
    {
        _session = NHibernateHelper.SessionFactory.OpenSession();
        _session.FlushMode = FlushMode.Manual;
        CurrentSessionContext.Bind(_session);
    }

    #region IUnitOfWork Members

    public void Dispose()
    {
        CurrentSessionContext.Unbind(_session.SessionFactory);
        _session.Dispose();
    }

    #endregion
}