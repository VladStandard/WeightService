using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Claim;

namespace DeviceControl.Features.Sections.Admin.Roles;

public sealed partial class RolesUpdateForm : SectionFormBase<ClaimEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IClaimService ClaimService { get; set; } = null!;
}