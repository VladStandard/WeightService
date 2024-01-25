// using DeviceControl.Features.Sections.Shared.DataGrid;
// using DeviceControl.Resources;
// using Microsoft.AspNetCore.Components;
// using Microsoft.Extensions.Localization;
//
// namespace DeviceControl.Features.Sections.Diagnostics.AppLogs;
//
// public sealed partial class AppLogsDataGrid: SectionDataGridBase<SqlLogEntity>
// {
//     [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
//     
//     private SqlLogRepository SqlLogRepository { get; } = new();
//
//     protected override IEnumerable<SqlLogEntity> SetSqlSectionCast() => SqlLogRepository.GetList(SqlCrudConfigSection).ToList();
// }