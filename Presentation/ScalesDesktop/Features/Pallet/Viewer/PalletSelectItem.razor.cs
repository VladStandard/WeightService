using Microsoft.AspNetCore.Components;
using ScalesDesktop.Services;
using Ws.Domain.Models.Entities.Print;

namespace ScalesDesktop.Features.Pallet.Viewer;

public sealed partial class PalletSelectItem : ComponentBase
{
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Parameter, EditorRequired] public ViewPallet Pallet { get; set; } = default!;
}