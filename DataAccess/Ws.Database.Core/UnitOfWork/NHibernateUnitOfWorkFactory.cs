using System.Data;
using Ws.UnitOfWork.Abstractions;

namespace Ws.Database.Core.UnitOfWork;

public class NHibernateUnitOfWorkFactory : IUnitOfWorkFactory
{
    public IUnitOfWork Create(IsolationLevel isolationLevel) => 
        new NHibernateUnitOfWork(NHibernateHelper.SessionFactory.OpenSession(), isolationLevel);

    public IUnitOfWork Create() => Create(IsolationLevel.ReadCommitted);
}