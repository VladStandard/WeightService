using Ws.Shared.Constants;

namespace Ws.DeviceControl.Models.Features.Devices.Printers.Commands.Update;

public class PrinterUpdateValidator : AbstractValidator<PrinterUpdateDto>
{
    public PrinterUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().Matches("^[A-Z0-9-]*$").WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Ip).NotEmpty().NotEqual(DefaultConsts.IpLocal);
        RuleFor(item => item.Type).IsInEnum().WithName(wsDataLocalizer["ColType"]);
    }
}