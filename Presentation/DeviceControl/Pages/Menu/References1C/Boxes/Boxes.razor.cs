namespace DeviceControl.Pages.Menu.References1C.Boxes;

public sealed partial class Boxes : SectionBase<SqlBoxEntity>
{
    public Boxes() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlBoxRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
