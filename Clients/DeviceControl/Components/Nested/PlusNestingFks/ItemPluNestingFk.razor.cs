namespace DeviceControl.Components.Nested.PlusNestingFks;

public sealed partial class ItemPluNestingFk : ItemBase<WsSqlPluNestingFkModel>
{
    public ItemPluNestingFk() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
