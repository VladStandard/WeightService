namespace DeviceControl.Components.Nested.PlusNestingFks;

public sealed partial class ItemPluNestingFk : ItemBase<WsSqlPluNestingFkEntity>
{
    public ItemPluNestingFk() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
