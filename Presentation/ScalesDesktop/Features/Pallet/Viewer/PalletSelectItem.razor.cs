using Microsoft.AspNetCore.Components;
using ScalesDesktop.Services;

namespace ScalesDesktop.Features.Pallet.Viewer;

public sealed partial class PalletSelectItem : ComponentBase
{
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Parameter, EditorRequired] public PalletModel Pallet { get; set; } = default!;
}