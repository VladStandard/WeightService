namespace DeviceControl.Pages.Menu.Operations.PlusLabels;

public sealed partial class PlusLabels : SectionBase<WsSqlViewPluLabelModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlViewPluLabelRepository PluLabelRepository { get; } = new();
    
    public PlusLabels() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = PluLabelRepository.GetList(SqlCrudConfigSection).ToList();
    }

    #endregion
}
