using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Localization;

namespace ScalesDesktop.Source.Pages.Pallet;

public sealed partial class FuncCard : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> PalletLocalizer { get; set; } = null!;
}