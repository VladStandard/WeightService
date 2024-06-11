using System.Net;
using Phetch.Core;
using Ws.Desktop.Models;
using Ws.Desktop.Models.Features.Arms.Output;

namespace ScalesDesktop.Source.Shared.Services;

public class ArmApi(IDesktopApi desktopApi, PrinterService printerService)
{
    public ParameterlessEndpoint<ArmValue> ArmEndpoint { get; } = new(
        async() => await desktopApi.GetArmByName(Dns.GetHostName()),
        options: new()
        {
            DefaultStaleTime = TimeSpan.FromHours(1),
            OnSuccess = data => printerService.Setup(data.Result.Printer.Ip, 9100, data.Result.Printer.Type)
        });

    public void UpdateArmCounter(uint counter)
    {
        ArmEndpoint.UpdateQueryData(new(), q =>
        {
            if (q.Data != null)
                return q.Data with { Counter = counter };
            return q.Data!;
        });
    }
}