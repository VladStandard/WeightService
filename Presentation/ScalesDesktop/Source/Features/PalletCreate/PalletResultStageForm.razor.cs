using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Localization;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Features.PalletCreate;

public sealed partial class PalletResultStageForm : ComponentBase
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public PalletCreateModel FormModel { get; set; } = default!;
    [Parameter] public EventCallback OnSubmit { get; set; }
    [Parameter] public EventCallback OnCancelAction { get; set; }
}