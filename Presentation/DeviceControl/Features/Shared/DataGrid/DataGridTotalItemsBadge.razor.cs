using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.DataGrid;

public sealed partial class DataGridTotalItemsBadge: ComponentBase
{
    [Parameter] public string Value { get; set; } = string.Empty;
}