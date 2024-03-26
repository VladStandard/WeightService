#pragma warning disable CS8618// Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using Ws.Database.Nhibernate.Sessions;
using Ws.Domain.Models.Common;

namespace Ws.Database.Nhibernate.Utils;

public static class SqlCoreHelper
{
    private static void ExecuteTransactionCore(Action<ISession> action)
    {
        using ISession session = NHibernateHelper.SessionFactory.OpenSession();
        session.FlushMode = FlushMode.Commit;

        using ITransaction transaction = session.BeginTransaction();
        try
        {
            action(session);
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }

    #region CRUD

    public static T SaveOrUpdate<T>(T item) where T : EntityBase
    {
        ExecuteTransactionCore(session => session.SaveOrUpdate(item));
        return item;
    }

    #endregion
}