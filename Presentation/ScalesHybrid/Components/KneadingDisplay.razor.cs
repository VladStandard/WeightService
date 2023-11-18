using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Models;
using ScalesHybrid.Resources;

namespace ScalesHybrid.Components;

public sealed partial class KneadingDisplay: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    private KneadingModel Kneading { get; set; } = new();
}

