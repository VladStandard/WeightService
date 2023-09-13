namespace DeviceControl.Pages.Menu.References1C.Clips;

public sealed partial class Clips : SectionBase<WsSqlClipModel>
{
    #region Public and private fields, properties, constructor
    
    public Clips() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlClipRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
    
    #endregion
}
