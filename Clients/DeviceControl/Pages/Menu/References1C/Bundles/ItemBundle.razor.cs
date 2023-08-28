namespace DeviceControl.Pages.Menu.References1C.Bundles;

public sealed partial class ItemBundle : ItemBase<WsSqlBundleModel>
{
    public ItemBundle() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
