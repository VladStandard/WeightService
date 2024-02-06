using Microsoft.AspNetCore.Components;
using ScalesHybrid.Services;

namespace ScalesHybrid.Features.Pallet.Viewer;

public sealed partial class PalletSelectItem : ComponentBase
{
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Parameter, EditorRequired] public PalletModel Pallet { get; set; } = default!;
}