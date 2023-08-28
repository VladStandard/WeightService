namespace DeviceControl.Pages.Menu.Devices.Printers;

public sealed partial class Printers : SectionBase<WsSqlPrinterModel>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlPrinterRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
