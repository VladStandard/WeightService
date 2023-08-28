namespace DeviceControl.Pages.Menu.References1C.PlusBundlesFks;

public sealed partial class ItemPluBundleFk : ItemBase<WsSqlPluBundleFkModel>
{
    public ItemPluBundleFk() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
