using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities;
using Ws.Domain.Models.Entities.Diag;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Plu;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.Diagnostics.Database;

public sealed partial class FileTablesDataGrid : SectionDataGridPageBase<TableSizeEntity>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [CascadingParameter(Name = "DialogItem")] public DbFileSizeInfoEntity DbFile { get; set; } = null!;

    protected override IEnumerable<TableSizeEntity> SetSqlSectionCast() =>
        SectionItems = DbFile.Tables;
}