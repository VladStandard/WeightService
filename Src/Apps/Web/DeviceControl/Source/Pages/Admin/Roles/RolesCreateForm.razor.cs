using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Claims;

namespace DeviceControl.Source.Pages.Admin.Roles;

public sealed partial class RolesCreateForm : SectionFormBase<Claim>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IClaimService ClaimService { get; set; } = default!;

    # endregion

    protected override Claim CreateItemAction(Claim item) =>
        ClaimService.Create(item);
}

public class RolesCreateFormValidator : AbstractValidator<Claim>
{
    public RolesCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
    }
}