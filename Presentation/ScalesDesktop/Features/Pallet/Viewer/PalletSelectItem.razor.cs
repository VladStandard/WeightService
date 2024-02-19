using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Services;
using Ws.Domain.Models.Entities.Print;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Features.Pallet.Viewer;

public sealed partial class PalletSelectItem : ComponentBase
{
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Parameter, EditorRequired] public ViewPallet Pallet { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
}