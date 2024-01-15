// ReSharper disable ClassNeverInstantiated.Global

using DeviceControl.Features.Sections.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Shared.Enums;
using Ws.StorageCore.Entities.SchemaRef.Users;

namespace DeviceControl.Features.Sections.Admin.Users;

public sealed partial class UsersCreateDialog: SectionDialogBase<SqlUserEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override List<EnumTypeModel<string>> InitializeTabList() =>
        [new(Localizer["SectionUsers"], "main")];
}