using DeviceControl2.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.References1C.Bundles;

public sealed partial class BundlesUpdateForm: SectionFormBase<BundleEntity>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
}