// using DeviceControl.Features.Shared;
//
// namespace DeviceControl.Pages.Menu.Devices.Lines;
//
// public sealed partial class Lines : SectionBase<SqlLineEntity>
// {
//     private SqlLineRepository LineRepository { get; } = new();
//
//     protected override void SetSqlSectionCast() =>
//         SqlSectionCast = LineRepository.GetEnumerable(SqlCrudConfigSection).ToList();
// }