namespace DeviceControl.Features.Sections.Devices.Line;

public sealed partial class LinePage: SectionBase<SqlLineEntity>
{
    private SqlLineRepository LineRepository { get; } = new();

    protected override void SetSqlSectionCast() =>
        SqlSectionCast = LineRepository.GetEnumerable(SqlCrudConfigSection).ToList();
}