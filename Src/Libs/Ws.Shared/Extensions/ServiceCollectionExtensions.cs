using Microsoft.Extensions.DependencyInjection;
using Ws.Shared.Utils;

namespace Ws.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServiceOrMock<TInterface, TImplementation, TMockImplementation>(
        this IServiceCollection services,
        bool isMock,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TInterface : class
        where TImplementation : TInterface
        where TMockImplementation : TInterface

    {
        Type implementationType = isMock && ConfigurationUtil.IsDevelop ? typeof(TMockImplementation) : typeof(TImplementation);

        services.Add(new(typeof(TInterface), implementationType, lifetime));
    }
}