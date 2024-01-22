// ReSharper disable ClassNeverInstantiated.Global

using DeviceControl.Features.Sections.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Shared.Enums;

namespace DeviceControl.Features.Sections.Admin.Users;

public sealed partial class UsersCreateDialog: SectionDialogBase<UserEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override List<EnumTypeModel<string>> InitializeTabList() =>
        [new(Localizer["SectionUsers"], "main")];
}