namespace DeviceControl.Pages.Menu.References1C.Brands;

public sealed partial class ItemBrand : ItemBase<WsSqlBrandEntity>
{
    public ItemBrand() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
