using Blazor.Heroicons;
using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef.Users;

namespace DeviceControl.Features.Sections.Admin.Users;

public sealed partial class UsersUpdateForm: SectionFormBase<SqlUserEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    private IEnumerable<ActionMenuEntry> AdditionalButtons { get; set; } = [];

    protected override void OnInitialized()
    {
        AdditionalButtons = AdditionalButtons.Append(
            new() { Name = Localizer["SectionFormRelogin"], IconName = HeroiconName.User });
    }
}