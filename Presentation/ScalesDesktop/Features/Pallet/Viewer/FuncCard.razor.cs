using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Features.Pallet.Resources;

namespace ScalesDesktop.Features.Pallet.Viewer;

public sealed partial class FuncCard : ComponentBase
{
    [Inject] private IStringLocalizer<PalletResources> PalletLocalizer { get; set; } = null!;
}