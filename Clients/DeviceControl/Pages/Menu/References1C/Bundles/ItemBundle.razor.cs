// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.References1C.Bundles;

public sealed partial class ItemBundle : ItemBase<WsSqlBundleModel>
{
    public ItemBundle() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
