using Ws.Domain.Models.Entities.Ref1c;

namespace DeviceControl.Source.Pages.References1C.Clips;

public sealed partial class ClipsUpdateForm : SectionFormBase<Clip>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
}