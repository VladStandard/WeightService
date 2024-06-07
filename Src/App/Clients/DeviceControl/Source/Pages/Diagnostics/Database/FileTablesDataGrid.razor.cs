using Ws.Domain.Models.Entities.Diag;

namespace DeviceControl.Source.Pages.Diagnostics.Database;

public sealed partial class FileTablesDataGrid : SectionDataGridPageBase<TableSize>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [CascadingParameter(Name = "DialogItem")] public DbFileSizeInfo DbFile { get; set; } = null!;

    protected override IEnumerable<TableSize> SetSqlSectionCast() =>
        SectionItems = DbFile.Tables;
}