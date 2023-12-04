using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;

namespace ScalesHybrid.Components.Controls;

public sealed partial class IndexControlBar : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
}
