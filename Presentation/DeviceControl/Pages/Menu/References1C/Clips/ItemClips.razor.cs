namespace DeviceControl.Pages.Menu.References1C.Clips;

public sealed partial class ItemClips: ItemBase<SqlClipEntity>
{
    public ItemClips() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
