namespace Ws.Database.Nhibernate.Sessions.Common;

public interface ITransactionalSession : IDisposable
{
    void Commit();
    void Rollback();
}