// ReSharper disable ClassNeverInstantiated.Global

using DeviceControl.Features.Sections.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Shared.Enums;
using Ws.StorageCore.Entities.SchemaRef.Claims;

namespace DeviceControl.Features.Sections.Admin.Roles;

public sealed partial class RolesCreateDialog: SectionDialogBase<SqlClaimEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override List<EnumTypeModel<string>> InitializeTabList() =>
        [new(Localizer["SectionRoles"], "main")];
}