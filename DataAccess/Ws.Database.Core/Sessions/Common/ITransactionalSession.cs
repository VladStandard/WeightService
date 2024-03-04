namespace Ws.Database.Core.Sessions.Common;

public interface ITransactionalSession : IDisposable
{
    void Commit();
    void Rollback();
}