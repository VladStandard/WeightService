using System.Net;

namespace Ws.DeviceControl.Models.Features.Devices.Printers.Commands.Create;

public class PrinterCreateValidator : AbstractValidator<PrinterCreateDto>
{
    public PrinterCreateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().Matches("^[A-Z0-9-]*$").WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Ip).NotEmpty().NotEqual(IPAddress.Parse("127.0.0.1"));
        RuleFor(item => item.Type).IsInEnum().WithName(wsDataLocalizer["ColType"]);
        RuleFor(item => item.ProductionSiteId).NotEmpty().WithName(wsDataLocalizer["ColProductionSite"]);
    }
}