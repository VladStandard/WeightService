using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Shared.DataGrid;

public sealed partial class DataGridWrapper<TItem>: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    [Parameter] public RenderFragment ChildContent { get; set; } = null!;
    [Parameter] public IEnumerable<TItem> GridData { get; set; } = new List<TItem>();
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public bool IsFilterable { get; set; }
}