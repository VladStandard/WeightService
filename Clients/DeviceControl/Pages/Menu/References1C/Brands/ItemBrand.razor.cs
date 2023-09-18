using WsStorageCore.Tables.TableRef1cModels.Brands;

namespace DeviceControl.Pages.Menu.References1C.Brands;

public sealed partial class ItemBrand : ItemBase<WsSqlBrandModel>
{
    public ItemBrand() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
