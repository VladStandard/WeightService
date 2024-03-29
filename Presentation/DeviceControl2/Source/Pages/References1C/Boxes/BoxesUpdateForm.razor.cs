using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Plu;
using Ws.Domain.Services.Features.Template;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.References1C.Boxes;

public sealed partial class BoxesUpdateForm: SectionFormBase<BoxEntity>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
}