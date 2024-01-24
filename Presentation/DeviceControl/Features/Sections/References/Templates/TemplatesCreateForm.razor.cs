using DeviceControl.Features.Sections.Shared;
using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Scale;

namespace DeviceControl.Features.Sections.References.Templates;

public sealed partial class TemplatesCreateForm : SectionFormBase<TemplateEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
}