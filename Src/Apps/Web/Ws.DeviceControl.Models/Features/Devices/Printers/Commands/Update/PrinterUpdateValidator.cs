using System.Net;

namespace Ws.DeviceControl.Models.Features.Devices.Printers.Commands.Update;

public class PrinterUpdateValidator : AbstractValidator<PrinterUpdateDto>
{
    public PrinterUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().Matches("^[A-Z0-9-]*$").WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Ip).NotEmpty().NotEqual(IPAddress.Parse("127.0.0.1"));
        RuleFor(item => item.Type).IsInEnum().WithName(wsDataLocalizer["ColType"]);
    }
}