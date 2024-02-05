using Microsoft.Extensions.DependencyInjection;

namespace Ws.Database.Core;

public static class DependencyInjection
{
    public static void AddNhibernate(this IServiceCollection services)
    {
        SqlCoreHelper.Instance.SetSessionFactory();
    }
}