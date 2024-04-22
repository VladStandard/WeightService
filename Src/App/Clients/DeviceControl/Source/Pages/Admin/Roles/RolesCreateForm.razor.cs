using DeviceControl.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Claim;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.Admin.Roles;

public sealed partial class RolesCreateForm : SectionFormBase<ClaimEntity>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IClaimService ClaimService { get; set; } = default!;

    # endregion

    protected override ClaimEntity CreateItemAction(ClaimEntity item) =>
        ClaimService.Create(item);
}

public class RolesCreateFormValidator : AbstractValidator<ClaimEntity>
{
    public RolesCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
    }
}