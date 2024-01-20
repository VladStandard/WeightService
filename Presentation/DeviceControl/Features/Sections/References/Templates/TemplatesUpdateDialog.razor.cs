// ReSharper disable ClassNeverInstantiated.Global
using DeviceControl.Features.Sections.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.Shared.Enums;

namespace DeviceControl.Features.Sections.References.Templates;

public sealed partial class TemplatesUpdateDialog : SectionDialogBase<TemplateEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override List<EnumTypeModel<string>> InitializeTabList() =>
        [new(Localizer["SectionTemplates"], "main")];
}