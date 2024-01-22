using DeviceControl.Features.Sections.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Print;
using Ws.Shared.Enums;

namespace DeviceControl.Features.Sections.Operations.Labels;


public sealed partial class LabelsUpdateDialog: SectionDialogBase<LabelEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override List<EnumTypeModel<string>> InitializeTabList() =>
    [
        new(Localizer["SectionLabels"], "main"),
        new(Localizer["LabelsPreviewTitle"], "preview")
    ];
}