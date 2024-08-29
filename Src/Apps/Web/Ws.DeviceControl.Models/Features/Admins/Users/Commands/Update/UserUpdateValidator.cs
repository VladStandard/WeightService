
namespace Ws.DeviceControl.Models.Features.Admins.Users.Commands.Update;

public sealed class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.ProductionSiteId).NotEmpty().WithName(wsDataLocalizer["ColProductionSite"]);
    }
}