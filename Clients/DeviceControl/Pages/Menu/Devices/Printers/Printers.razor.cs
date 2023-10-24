namespace DeviceControl.Pages.Menu.Devices.Printers;

public sealed partial class Printers : SectionBase<WsSqlPrinterEntity>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlPrinterRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
