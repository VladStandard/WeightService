using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;

namespace DeviceControl.Features.Sections.References.TemplateResources;

public sealed partial class TemplateResourcesUpdateForm : SectionFormBase<ZplResourceEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
}