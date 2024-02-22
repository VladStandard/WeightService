#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using NHibernate.Transform;
using Ws.Database.Core.Sessions;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Database.Core.Helpers;

public static class SqlCoreHelper
{

    private static void ExecuteSelectCore(Action<ISession> action)
    {
        using ISession session = NHibernateHelper.SessionFactory.OpenSession();
        session.FlushMode = FlushMode.Manual;
        
        try
        {
            action(session);
        }
        catch (Exception)
        {
            // ignored
        }
    }

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
        
        ExecuteSelectCore(session => {
            ISQLQuery query = session.CreateSQLQuery(sqlQuery);
            query.SetResultTransformer(Transformers.AliasToBean<TObject>()); 
            items = query.List<TObject>();
        });
        
        return items;
    }
    
    #endregion
    
    #region CRUD

    public static T SaveOrUpdate<T>(T item) where T : EntityBase
    {
        ExecuteTransactionCore(session => session.SaveOrUpdate(item));
        return item;
    }

    public static T Save<T>(T item) where T : EntityBase
    {
        ExecuteTransactionCore(session => session.Save(item));
        return item;
    }

    public static T Update<T>(T item) where T : EntityBase
    {
        ExecuteTransactionCore(session => session.Update(item));
        return item;
    }

    public static void Delete<T>(T item) where T : EntityBase
    {
        ExecuteTransactionCore(session => session.Delete(item));
    }

    #endregion
}