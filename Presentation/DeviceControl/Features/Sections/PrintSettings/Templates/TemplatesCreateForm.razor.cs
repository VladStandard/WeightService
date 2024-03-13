using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Template;

namespace DeviceControl.Features.Sections.PrintSettings.Templates;

public sealed partial class TemplatesCreateForm : SectionFormBase<TemplateEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private ITemplateService TemplateService { get; set; } = null!;
}