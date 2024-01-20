// ReSharper disable ClassNeverInstantiated.Global
using DeviceControl.Features.Sections.Shared.Modal;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Shared.Enums;

namespace DeviceControl.Features.Sections.References1C.Bundles;


public sealed partial class BundlesUpdateDialog: SectionDialogBase<BundleEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override List<EnumTypeModel<string>> InitializeTabList() =>
        [new(Localizer["SectionBundles"], "main")];
}
