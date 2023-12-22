namespace DeviceControl.Pages.Menu.References1C.Clips;

public sealed partial class Clips : SectionBase<SqlClipEntity>
{
    public Clips() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlClipRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
