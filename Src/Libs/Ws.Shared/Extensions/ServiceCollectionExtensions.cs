using Microsoft.Extensions.DependencyInjection;
using Ws.Shared.Utils;

namespace Ws.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceOrMock<TInterface, TImplementation, TMockImplementation>(
        this IServiceCollection services,
        bool isMock,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TInterface : class
        where TImplementation : TInterface
        where TMockImplementation : TInterface

    {
        Type implementationType = isMock && ConfigurationUtil.IsDevelop ? typeof(TMockImplementation) : typeof(TImplementation);

        services.Add(new(typeof(TInterface), implementationType, lifetime));

        return services;
    }

    public static IServiceCollection AddApiServices<T>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(typeof(T))
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("ApiService")))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
    }
}