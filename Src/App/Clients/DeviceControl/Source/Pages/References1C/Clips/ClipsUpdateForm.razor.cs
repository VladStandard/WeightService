using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.References1C.Clips;

public sealed partial class ClipsUpdateForm : SectionFormBase<ClipEntity>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
}