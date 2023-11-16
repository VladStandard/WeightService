namespace DeviceControl.Pages.Menu.References1C.Boxes;

public sealed partial class ItemBox : ItemBase<SqlBoxEntity>
{
    public ItemBox() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
