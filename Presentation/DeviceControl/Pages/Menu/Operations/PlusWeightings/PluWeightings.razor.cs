namespace DeviceControl.Pages.Menu.Operations.PlusWeightings;

public sealed partial class PluWeightings : SectionBase<SqlViewPluWeightingModel>
{
    #region Public and private fields, properties, constructor
    
    private SqlViewPluWeightingRepository ViewPluWeightingRepository { get; } = new();
    
    public PluWeightings() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ViewPluWeightingRepository.GetList(SqlCrudConfigSection).ToList();
    }

    #endregion
}
