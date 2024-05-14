using Ws.Domain.Models.Entities.Ref1c;

namespace DeviceControl.Source.Pages.References1C.Bundles;

public sealed partial class BundlesUpdateForm : SectionFormBase<Bundle>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
}