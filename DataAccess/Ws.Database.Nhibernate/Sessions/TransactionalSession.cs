using System.Data;
using NHibernate.Context;
using Ws.Database.Nhibernate.Sessions.Common;

namespace Ws.Database.Nhibernate.Sessions;

public class TransactionalSession : ITransactionalSession
{
    private readonly ISession _session;
    private readonly ITransaction _transaction;

    public TransactionalSession(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        _session = NHibernateHelper.SessionFactory.OpenSession();
        _transaction = _session.BeginTransaction(isolationLevel);

        CurrentSessionContext.Bind(_session);
    }

    #region IUnitOfWork Members

    public void Commit() => _transaction.Commit();

    public void Rollback()
    {
        if (_transaction is { WasCommitted: false, WasRolledBack: false })
            _transaction.Rollback();
    }

    public void Dispose()
    {
        CurrentSessionContext.Unbind(_session.SessionFactory);
        _session.Dispose();
        _transaction.Dispose();
    }

    #endregion
}