using DeviceControl.Source.Shared.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Source.Pages;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class Home : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
}