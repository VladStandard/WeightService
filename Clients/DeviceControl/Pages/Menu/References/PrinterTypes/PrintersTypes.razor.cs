namespace DeviceControl.Pages.Menu.References.PrinterTypes;

public sealed partial class PrintersTypes : SectionBase<WsSqlPrinterTypeModel>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlPrinterTypeRepository().GetList(SqlCrudConfigSection);
    }
}
