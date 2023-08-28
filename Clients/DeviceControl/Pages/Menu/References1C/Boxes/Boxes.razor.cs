namespace DeviceControl.Pages.Menu.References1C.Boxes;

public sealed partial class Boxes : SectionBase<WsSqlBoxModel>
{
    #region Public and private fields, properties, constructor

    public Boxes() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlBoxRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }

    #endregion
}
