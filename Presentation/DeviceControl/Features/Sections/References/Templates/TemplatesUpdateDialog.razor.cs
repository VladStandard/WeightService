using DeviceControl.Features.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaScale.Templates;

namespace DeviceControl.Features.Sections.References.Templates;

public sealed partial class TemplatesUpdateDialog : SectionDialogBase<SqlTemplateEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        TabsList = new() { new(Localizer["SectionTemplates"], "main") };
    }
}