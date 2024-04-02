using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Models.Entities.Print;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Widgets.PalletSelect;

public sealed partial class PalletSelectItem : ComponentBase
{
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Parameter, EditorRequired] public ViewPallet Pallet { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
}