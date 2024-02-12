using Ws.Database.Core.UnitOfWork;

namespace Ws.Database.Core.Common;

public class BaseRepository
{
    protected static ISession Session => NHibernateHelper.GetSession();
}