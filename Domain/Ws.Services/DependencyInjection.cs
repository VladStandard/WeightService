using Microsoft.Extensions.DependencyInjection;
using Ws.Services.Services.Label;
using Ws.Services.Services.Line;
using Ws.Services.Services.Plu;
using Ws.Services.Services.PrintLabel;

namespace Ws.Services;

public static class DependencyInjection
{
    public static void AddVsServices(this IServiceCollection services)
    {
        services.AddScoped<ILineService, LineService>();
        services.AddScoped<IPluService, PluService>();
        services.AddScoped<IPrintLabelService, PrintLabelService>();
        services.AddScoped<ILabelService, LabelService>();
    }
}