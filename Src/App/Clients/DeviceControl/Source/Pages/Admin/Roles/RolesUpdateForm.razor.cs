using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Claim;

namespace DeviceControl.Source.Pages.Admin.Roles;

public sealed partial class RolesUpdateForm : SectionFormBase<Claim>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IClaimService ClaimService { get; set; } = default!;

    # endregion

    protected override Claim UpdateItemAction(Claim item) =>
        ClaimService.Update(item);

    protected override Task DeleteItemAction(Claim item)
    {
        ClaimService.Delete(item);
        return Task.CompletedTask;
    }
}

public class RolesUpdateFormValidator : AbstractValidator<Claim>
{
    public RolesUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
    }
}