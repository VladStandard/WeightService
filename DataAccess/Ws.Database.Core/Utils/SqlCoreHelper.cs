#pragma warning disable CS8618// Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using NHibernate.Transform;
using Ws.Database.Core.Sessions;
using Ws.Domain.Models.Common;

namespace Ws.Database.Core.Utils;

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

    #region GetList

    public static IEnumerable<TObject> GetEnumerableBySql<TObject>(string sqlQuery)
    {
        IEnumerable<TObject> items = Enumerable.Empty<TObject>();

        using ISession session = NHibernateHelper.SessionFactory.OpenSession();
        session.FlushMode = FlushMode.Manual;

        try
        {
            ISQLQuery query = session.CreateSQLQuery(sqlQuery);
            query.SetResultTransformer(Transformers.AliasToBean<TObject>());
            items = query.List<TObject>();
        }
        catch (Exception)
        {
            // ignored
        }

        return items;
    }

    #endregion

    #region CRUD

    public static T SaveOrUpdate<T>(T item) where T : EntityBase
    {
        ExecuteTransactionCore(session => session.SaveOrUpdate(item));
        return item;
    }

    public static void Delete<T>(T item) where T : EntityBase
    {
        ExecuteTransactionCore(session => session.Delete(item));
    }

    #endregion
}