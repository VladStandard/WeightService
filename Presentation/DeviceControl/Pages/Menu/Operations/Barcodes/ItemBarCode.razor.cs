namespace DeviceControl.Pages.Menu.Operations.Barcodes;

public sealed partial class ItemBarCode : ItemBase<SqlBarCodeEntity>
{
    public ItemBarCode() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
