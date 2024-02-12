namespace Ws.UnitOfWork.Abstractions;

public interface IUnitOfWork : IDisposable
{
    void Commit();
}