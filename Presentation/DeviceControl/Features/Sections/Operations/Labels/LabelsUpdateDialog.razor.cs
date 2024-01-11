using DeviceControl.Features.Sections.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Shared.Enums;
using Ws.StorageCore.Entities.SchemaPrint.Labels;

namespace DeviceControl.Features.Sections.Operations.Labels;


public sealed partial class LabelsUpdateDialog: SectionDialogBase<SqlLabelEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override List<EnumTypeModel<string>> InitializeTabList() =>
        new()
        {
            new(Localizer["SectionLabels"], "main"),
            new(Localizer["LabelsPreviewTitle"], "preview")
        };
}