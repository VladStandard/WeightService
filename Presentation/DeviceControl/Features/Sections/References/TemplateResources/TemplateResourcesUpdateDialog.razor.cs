using DeviceControl.Features.Sections.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Shared.Enums;
using Ws.StorageCore.Entities.SchemaScale.TemplatesResources;

namespace DeviceControl.Features.Sections.References.TemplateResources;

public sealed partial class TemplateResourcesUpdateDialog : SectionDialogBase<SqlTemplateResourceEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override List<EnumTypeModel<string>> InitializeTabList() =>
        new() { new(Localizer["SectionTemplatesResources"], "main") };
}