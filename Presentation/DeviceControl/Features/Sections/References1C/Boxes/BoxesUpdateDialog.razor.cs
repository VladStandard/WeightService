// ReSharper disable ClassNeverInstantiated.Global
using DeviceControl.Features.Sections.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Shared.Enums;
using Ws.StorageCore.Entities.SchemaRef1c.Boxes;

namespace DeviceControl.Features.Sections.References1C.Boxes;


public sealed partial class BoxesUpdateDialog: SectionDialogBase<SqlBoxEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override List<EnumTypeModel<string>> InitializeTabList() =>
        [new(Localizer["SectionBoxes"], "main")];
}
