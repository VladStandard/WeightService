using PostSharp.Aspects;
using PostSharp.Serialization;
using Ws.Database.Core.UnitOfWork;
using Ws.UnitOfWork.Abstractions;

namespace Ws.Domain.Services.Aspects;

[PSerializable]
public class SessionAttribute : OnMethodBoundaryAspect
{
    private IUnitOfWork? UnitOfWork { get; set; }
    
    public override void OnEntry(MethodExecutionArgs args)
    {
        UnitOfWork = new NHibernateUnitOfWorkFactory().Create();
    }
    
    public override void OnExit(MethodExecutionArgs args)
    {
        UnitOfWork?.Dispose();
    }
}