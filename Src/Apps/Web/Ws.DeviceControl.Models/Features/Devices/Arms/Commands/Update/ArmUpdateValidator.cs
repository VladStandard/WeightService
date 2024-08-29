namespace Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Update;

public sealed class ArmUpdateValidator : AbstractValidator<ArmUpdateDto>
{
    public ArmUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().MaximumLength(64).WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Number).GreaterThan(10000).LessThan(100000).WithName(wsDataLocalizer["ColNumber"]);
        RuleFor(item => item.PcName).NotEmpty().Matches("^[A-Z0-9-]*$").WithName(wsDataLocalizer["ColPcName"]);
        RuleFor(item => item.Type).IsInEnum().WithName(wsDataLocalizer["ColType"]);
        RuleFor(item => item.Counter).GreaterThanOrEqualTo(0).WithName(wsDataLocalizer["ColCounter"]);
        RuleFor(item => item.PrinterId).NotEmpty().WithName(wsDataLocalizer["ColPrinter"]);
        RuleFor(item => item.WarehouseId).NotEmpty().WithName(wsDataLocalizer["ColWarehouse"]);
    }
}