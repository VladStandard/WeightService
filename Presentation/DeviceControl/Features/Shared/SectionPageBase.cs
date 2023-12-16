using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared;

public class SectionPageBase : ComponentBase
{
    [Parameter] public string SearchingSectionItemId { get; set; } = string.Empty;
}

