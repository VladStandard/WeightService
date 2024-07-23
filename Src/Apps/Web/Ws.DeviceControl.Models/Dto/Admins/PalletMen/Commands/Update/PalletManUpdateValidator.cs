namespace Ws.DeviceControl.Models.Dto.Admins.PalletMen.Commands.Update;

public class PalletManUpdateValidator : AbstractValidator<PalletManUpdateDto>
{
    public PalletManUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Id1C).NotEmpty().WithName("UID 1C");
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Surname).NotEmpty().WithName(wsDataLocalizer["ColSurname"]);
        RuleFor(item => item.Patronymic).NotEmpty().WithName(wsDataLocalizer["ColPatronymic"]);
        RuleFor(item => item.Password).NotEmpty().WithName(wsDataLocalizer["ColPassword"]);
        RuleFor(item => item.WarehouseId).NotEmpty().WithName(wsDataLocalizer["ColWarehouse"]);
    }
}