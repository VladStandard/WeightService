using System.Data;

namespace Ws.UnitOfWork.Abstractions;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();
    IUnitOfWork Create(IsolationLevel isolationLevel);
}