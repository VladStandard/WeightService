using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl2.Source.Pages;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class Home: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
}