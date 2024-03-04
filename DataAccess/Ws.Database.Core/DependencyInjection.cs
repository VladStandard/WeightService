using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Ws.Database.Core.Sessions;

namespace Ws.Database.Core;

public static class DependencyInjection
{
    public static void AddNhibernate(this IServiceCollection services)
    {
        NHibernateHelper.SetSessionFactory();
        AddRepositories(services);
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        IEnumerable<Type> repositoryTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.Name.EndsWith("Repository") && t is { IsAbstract: false, IsInterface: false });

        foreach (Type repositoryType in repositoryTypes)
        {
            services.AddTransient(repositoryType);
        }
    }
}