using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Scale;

namespace DeviceControl.Features.Sections.References.TemplateResources;

public sealed partial class TemplateResourcesUpdateForm : SectionFormBase<TemplateResourceEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
}