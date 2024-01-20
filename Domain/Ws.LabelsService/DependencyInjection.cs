using Microsoft.Extensions.DependencyInjection;
using Ws.LabelsService.Features.PrintLabel;
using Ws.LabelsService.Features.RenderLabel;

namespace Ws.LabelsService;

public static class DependencyInjection
{
    public static void AddLabelsServices(this IServiceCollection services)
    {
        services.AddScoped<IPrintLabelService, PrintLabelService>();
        services.AddScoped<IRenderLabelService, RenderLabelService>();
    }
}