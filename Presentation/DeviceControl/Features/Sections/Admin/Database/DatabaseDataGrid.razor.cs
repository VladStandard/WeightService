using Blazorise;
using Blazorise.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities;

namespace DeviceControl.Features.Sections.Admin.Database;

public sealed partial class DatabaseDataGrid: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Parameter, EditorRequired] public IEnumerable<DbFileSizeInfoEntity> SectionItems { get; set; }
        = new List<DbFileSizeInfoEntity>();
    private DbFileSizeInfoEntity? SelectedItem { get; set; }
    
    private static void CustomRowStyling(DbFileSizeInfoEntity item, DataGridRowStyling styling) =>
        styling.Class = "transition-colors hover:bg-sky-100";
    
    private static DataGridRowStyling CustomHeaderRowStyling() =>
        new() { Class = "bg-sky-200 text-black overflow-hidden" };

    private static Color GetProgressColorByValue(int percentage)
    {
        return percentage switch
        {
            >= 80 => Color.Danger,
            >= 50 => Color.Warning,
            _ => Color.Success
        };
    }
}
