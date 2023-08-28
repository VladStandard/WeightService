namespace DeviceControl.Pages.Menu.References1C.PlusGroups;

public sealed partial class ItemPluGroup : ItemBase<WsSqlPluGroupModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlPluGroupModel? ParentGroup { get; set; }

    public ItemPluGroup() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        ParentGroup = new WsSqlPluGroupRepository().GetItemParentFromChildGroup(SqlItemCast);
    }

    #endregion
}
