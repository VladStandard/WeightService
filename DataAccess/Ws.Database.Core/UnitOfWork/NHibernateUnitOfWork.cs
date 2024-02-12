using System.Data;
using NHibernate.Context;
using Ws.UnitOfWork.Abstractions;

namespace Ws.Database.Core.UnitOfWork;

internal class NHibernateUnitOfWork : IUnitOfWork
{
    private readonly ISession _session;
    private ITransaction? _transaction;

    public NHibernateUnitOfWork(ISession session, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        CurrentSessionContext.Bind(session);

        _session = session;
        _transaction = session.BeginTransaction(isolationLevel);
    }

    #region IUnitOfWork Members

    public void Commit() => _transaction?.Commit();
    
    public void Dispose()
    {
        if (_transaction is { WasCommitted: false, WasRolledBack: false })
            _transaction.Rollback();
        
        _transaction?.Dispose();
        _transaction = null;

        CurrentSessionContext.Unbind(_session.SessionFactory);
        _session.Dispose();
    }
    
    #endregion
}