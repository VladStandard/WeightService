namespace DeviceControl.Pages.Menu.References1C.ContrAgents;

public sealed partial class ContrAgents : SectionBase<WsSqlContragentModel>
{
    #region Public and private fields, properties, constructor

    public ContrAgents() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlContragentRepository().GetList(SqlCrudConfigSection);
    }

    #endregion
}
