namespace DeviceControl.Pages.Menu.References1C.Boxes;

public sealed partial class ItemBox : ItemBase<WsSqlBoxModel>
{
    public ItemBox() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
