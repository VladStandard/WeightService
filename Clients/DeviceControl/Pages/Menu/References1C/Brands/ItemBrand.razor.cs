// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;
using WsStorageCore.TableScaleModels.Brands;

namespace DeviceControl.Pages.Menu.References1C.Brands;

public sealed partial class ItemBrand : ItemBase<WsSqlBrandModel>
{
    public ItemBrand() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}