using DeviceControl.Features.Shared;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.Devices.Line;

public sealed partial class LinePage: SectionBase<SqlLineEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    private SqlLineRepository LineRepository { get; } = new();

    protected override void SetSqlSectionCast() =>
        SqlSectionCast = LineRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}