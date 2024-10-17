using System.Globalization;

namespace Ws.Tablet.Api.App.Shared.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder SetupVsLocalization(this IApplicationBuilder app)
    {
        CultureInfo.DefaultThreadCurrentCulture = Cultures.Ru;
        CultureInfo.DefaultThreadCurrentUICulture = Cultures.Ru;

        CultureInfo[] supportedCultures = [Cultures.Ru, Cultures.En];
        RequestLocalizationOptions localizationOptions = new()
        {
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures,
            DefaultRequestCulture = new(Cultures.Ru.Name)
        };

        app.UseRequestLocalization(localizationOptions);
        return app;
    }
}