using DeviceControl.Source.Shared.Services;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Users;

namespace DeviceControl.Source.Pages.Admin.Users;

public sealed partial class UsersUpdateForm : SectionFormBase<UserWithProductionSite>
{
    #region Inject
    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private ReferencesEndpoints ReferencesEndpoints { get; set; } = default!;

    #endregion

    protected override UserWithProductionSite UpdateItemAction(UserWithProductionSite item)
    {
        User user = new()
        {
            Uid = item.KeycloakUser.Id,
            ProductionSite = item.ProductionSite!
        };
        try
        {
            UserService.Update(user);
        }
        catch
        {
            UserService.Create(user);
        }

        return item;
    }
}

public class UsersUpdateFormValidator : AbstractValidator<UserWithProductionSite>
{
    public UsersUpdateFormValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.ProductionSite).NotEmpty().WithName(wsDataLocalizer["ColProductionSite"]);
    }
}