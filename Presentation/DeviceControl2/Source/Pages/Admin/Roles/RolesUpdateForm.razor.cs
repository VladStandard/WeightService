using DeviceControl2.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Claim;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.Admin.Roles;

public sealed partial class RolesUpdateForm: SectionFormBase<ClaimEntity>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IClaimService ClaimService { get; set; } = default!;

    # endregion

    protected override ClaimEntity UpdateItemAction(ClaimEntity item) =>
        ClaimService.Update(item);

    protected override Task DeleteItemAction(ClaimEntity item)
    {
        ClaimService.Delete(item);
        return Task.CompletedTask;
    }
}

public class RolesUpdateFormValidator : AbstractValidator<ClaimEntity>
{
    public RolesUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
    }
}
