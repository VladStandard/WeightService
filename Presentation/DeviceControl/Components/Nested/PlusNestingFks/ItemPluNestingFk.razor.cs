namespace DeviceControl.Components.Nested.PlusNestingFks;

public sealed partial class ItemPluNestingFk : ItemBase<SqlPluNestingFkEntity>
{
    public ItemPluNestingFk() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
