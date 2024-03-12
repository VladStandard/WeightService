using NHibernate;
using PostSharp.Aspects;
using PostSharp.Serialization;
using Ws.Database.Core.Sessions;
using Ws.Database.Core.Sessions.Common;
using Ws.Database.Core.Sessions.Exceptions;
using Ws.Domain.Services.Exceptions;
using Ws.Shared.Utils;

namespace Ws.Domain.Services.Aspects;

[PSerializable]
internal class TransactionalAttribute : OnMethodBoundaryAspect
{
    private ITransactionalSession? UnitOfWork { get; set; }

    public override void OnEntry(MethodExecutionArgs args)
    {
        if (NHibernateHelper.SessionExists()) return;
        UnitOfWork = new TransactionalSession();
    }

    public override void OnExit(MethodExecutionArgs args) => UnitOfWork?.Dispose();
    
    public override void OnSuccess(MethodExecutionArgs args) => UnitOfWork?.Commit();

    public override void OnException(MethodExecutionArgs args)
    {
        UnitOfWork?.Rollback();
        Type exceptionType = args.Exception.GetType();
        if (exceptionType.IsSubclassOf(typeof(HibernateException)))
            throw new DbServiceException();
    }
}