using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities;
using Ws.Domain.Models.Entities.Diag;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.Diagnostics.Database;

public sealed partial class FileTablesDataGrid : SectionDataGridPageBase<TableSizeEntity>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [CascadingParameter(Name = "DialogItem")] public DbFileSizeInfoEntity DbFile { get; set; } = null!;

    protected override IEnumerable<TableSizeEntity> SetSqlSectionCast() =>
        SectionItems = DbFile.Tables;
}