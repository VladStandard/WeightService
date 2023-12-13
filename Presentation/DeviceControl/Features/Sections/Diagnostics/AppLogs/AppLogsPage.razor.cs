using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.Diagnostics.AppLogs;

public sealed partial class AppLogsPage: SectionBase<SqlLogEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlLogRepository SqlLogRepository { get; } = new();

    protected override void SetSqlSectionCast() =>
        SectionItems = SqlLogRepository.GetList(SqlCrudConfigSection).ToList();
}