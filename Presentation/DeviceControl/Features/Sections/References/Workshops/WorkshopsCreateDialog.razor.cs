using DeviceControl.Features.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References.Workshops;

public sealed partial class WorkshopsCreateDialog: SectionDialogBase<SqlWorkShopEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        TabsList = new()
        {
            new(Localizer["SectionWorkshops"], "main"),
        };
    }
}