using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;

namespace ScalesHybrid.Components.Modules.ProductDisplay;

public sealed partial class ProductDisplayPiece: ComponentBase
{
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
}