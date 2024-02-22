using PostSharp.Aspects;
using PostSharp.Serialization;
using Ws.Database.Core.Sessions;
using Ws.Database.Core.Sessions.Common;
using Ws.Database.Core.Sessions.Exceptions;
using Ws.Shared.Utils;

namespace Ws.Domain.Services.Aspects;

[PSerializable]
public class TransactionalAttribute : OnMethodBoundaryAspect
{
    private ITransactionalSession? UnitOfWork { get; set; }
    
    public override void OnEntry(MethodExecutionArgs args)
    { 
        bool sessionNotExist = ErrorUtil.Suppress<DataBaseSessionException>(
        () => NHibernateHelper.GetSession()
        );
        if (!sessionNotExist) return;
        
        UnitOfWork = new TransactionalSession();
    }
    
    public override void OnExit(MethodExecutionArgs args)
    {
        UnitOfWork?.Dispose();
    }

    public override void OnSuccess(MethodExecutionArgs args)
    {
        UnitOfWork?.Commit();
    }

    public override void OnException(MethodExecutionArgs args)
    {
        UnitOfWork?.Rollback();
    }
}