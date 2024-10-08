namespace Ws.DeviceControl.Models.Features.Admins.PalletMen.Commands.Create;

public sealed class PalletManCreateValidator : AbstractValidator<PalletManCreateDto>
{
    public PalletManCreateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Id1C).NotEmpty().WithName("UID 1C");
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Surname).NotEmpty().WithName(wsDataLocalizer["ColSurname"]);
        RuleFor(item => item.Patronymic).NotEmpty().WithName(wsDataLocalizer["ColPatronymic"]);
        RuleFor(item => item.Password).NotEmpty().WithName(wsDataLocalizer["ColPassword"]);
        RuleFor(item => item.WarehouseId).NotEmpty().WithName(wsDataLocalizer["ColWarehouse"]);
    }
}