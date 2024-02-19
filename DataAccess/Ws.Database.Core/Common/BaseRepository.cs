using System.Diagnostics.CodeAnalysis;
using Ws.Database.Core.UnitOfWork;

namespace Ws.Database.Core.Common;

[SuppressMessage("Performance", "CA1822:Пометьте члены как статические")]
public class BaseRepository
{
    // ReSharper disable once MemberCanBeMadeStatic.Global
    protected ISession Session => NHibernateHelper.GetSession();
}