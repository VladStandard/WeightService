using Microsoft.Extensions.DependencyInjection;
using Ws.Shared.Utils;

namespace Ws.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceOrMock<TInterface, TImplementation, TMockImplementation>(
        this IServiceCollection services,
        bool useMock,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TInterface : class
        where TImplementation : TInterface
        where TMockImplementation : TInterface
    {
        Type implementationType = useMock && ConfigurationUtils.IsDevelop ? typeof(TMockImplementation) : typeof(TImplementation);

        services.Add(new(typeof(TInterface), implementationType, lifetime));

        return services;
    }

    public static IServiceCollection AddValidators<T>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(typeof(T))
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Validator")))
            .AsSelf()
            .WithScopedLifetime()
        );
    }

    public static IServiceCollection AddMiddlewares<T>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(typeof(T))
            .AddClasses(i => i.Where(type => type.Name.EndsWith("Middleware")))
            .AsSelf()
            .WithTransientLifetime()
        );
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

    public static IServiceCollection AddRefitEndpoints<T>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(typeof(T))
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Endpoints")))
            .AsSelf()
            .WithScopedLifetime()
        );
    }

    public static IServiceCollection AddHelpers<T>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(typeof(T))
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Helper")))
            .AsSelf()
            .WithTransientLifetime()
        );
    }

    public static IServiceCollection AddDelegatingHandlers<T>(this IServiceCollection services)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(typeof(T))
            .AddClasses(classes => classes.AssignableTo<DelegatingHandler>())
            .AsSelf()
            .WithTransientLifetime()
        );
    }
}