namespace DeviceControl.Pages.Menu.References1C.Boxes;

public sealed partial class ItemBox : ItemBase<WsSqlBoxEntity>
{
    public ItemBox() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
