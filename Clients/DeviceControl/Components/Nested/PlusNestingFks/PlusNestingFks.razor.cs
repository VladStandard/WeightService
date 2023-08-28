namespace DeviceControl.Components.Nested.PlusNestingFks;

public sealed partial class PlusNestingFks : SectionBase<WsSqlPluNestingFkModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlPluNestingFkRepository PluNestingFkRepository { get; } = new();
    public PlusNestingFks() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = PluNestingFkRepository.GetEnumerableByPluUid(SqlItem?.IdentityValueUid).ToList();
    }

    #endregion
}
