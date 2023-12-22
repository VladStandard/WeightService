namespace DeviceControl.Components.Nested.PlusNestingFks;

public sealed partial class PlusNestingFks : SectionBase<SqlPluNestingFkEntity>
{
    [Parameter] public SqlPluEntity Plu { get; set; }
    private SqlPluNestingFkRepository PluNestingFkRepository { get; } = new();
    public PlusNestingFks() : base()
    {
        IsGuiShowFilterMarked = false;
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = PluNestingFkRepository.GetEnumerableByPluUidActual(Plu).ToList();
    }
}
