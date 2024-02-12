using Microsoft.Extensions.DependencyInjection;
using Ws.Database.Core.UnitOfWork;

namespace Ws.Database.Core;

public static class DependencyInjection
{
    public static void AddNhibernate(this IServiceCollection services)
    {
        NHibernateHelper.SetSessionFactory();
    }
}