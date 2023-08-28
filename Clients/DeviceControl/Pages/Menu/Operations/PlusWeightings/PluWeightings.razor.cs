namespace DeviceControl.Pages.Menu.Operations.PlusWeightings;

public sealed partial class PluWeightings : SectionBase<WsSqlViewPluWeightingModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlDeviceScaleFkModel> DeviceScaleFk { get; set; }

    private WsSqlViewPluWeightingRepository ViewPluWeightingRepository { get; } = new();
    
    public PluWeightings() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ViewPluWeightingRepository.GetList(SqlCrudConfigSection).ToList();
    }

    #endregion
}
