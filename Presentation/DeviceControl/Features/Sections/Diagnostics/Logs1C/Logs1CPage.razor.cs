using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Models;
using Ws.StorageCore.Utils;

namespace DeviceControl.Features.Sections.Diagnostics.Logs1C;

public sealed partial class Logs1CPage: SectionDataGridBase<SqlLogWebEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    private SqlLogWebRepository SqlLogWebRepository { get; } = new();
    
    protected override void SetSqlSectionCast() =>
        SectionItems = SqlLogWebRepository.GetList(SqlCrudConfigFactory.GetCrudAll()).ToList();
}