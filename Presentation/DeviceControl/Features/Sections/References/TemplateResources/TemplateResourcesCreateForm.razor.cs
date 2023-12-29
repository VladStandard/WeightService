using DeviceControl.Features.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaScale.TemplatesResources;

namespace DeviceControl.Features.Sections.References.TemplateResources;

public sealed partial class TemplateResourcesCreateForm : SectionFormBase<SqlTemplateResourceEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    protected override void OnInitialized()
    {
        SectionEntity.Type = "ZPL";
    }
}