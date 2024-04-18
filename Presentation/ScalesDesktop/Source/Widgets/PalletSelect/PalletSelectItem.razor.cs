using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Models.Entities.Print;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Widgets.PalletSelect;

public sealed partial class PalletSelectItem : ComponentBase
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private PalletContext PalletContext { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public ViewPallet Pallet { get; set; } = default!;

}