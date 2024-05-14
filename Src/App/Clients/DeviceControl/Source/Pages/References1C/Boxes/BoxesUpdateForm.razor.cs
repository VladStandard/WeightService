using Ws.Domain.Models.Entities.Ref1c;

namespace DeviceControl.Source.Pages.References1C.Boxes;

public sealed partial class BoxesUpdateForm : SectionFormBase<Box>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
}