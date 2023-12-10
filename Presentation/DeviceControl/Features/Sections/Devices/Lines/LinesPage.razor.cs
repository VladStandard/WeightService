using DeviceControl.Features.Shared;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.Devices.Lines;

public sealed partial class LinesPage: SectionBase<SqlLineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    private SqlLineRepository LineRepository { get; } = new();

    protected override void SetSqlSectionCast() =>
        SqlSectionCast = LineRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}