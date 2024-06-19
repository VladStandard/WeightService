using Ws.Domain.Models.Entities.Ref1c;

namespace DeviceControl.Source.Pages.References1C.Brands;

public sealed partial class BrandsUpdateForm : SectionFormBase<Brand>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
}