namespace DeviceControl.Pages.Menu.Devices.Printers;

public sealed partial class Printers : SectionBase<SqlPrinterEntity>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlPrinterRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
