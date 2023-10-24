// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
namespace DeviceControl.Pages.Menu.References1C.Clips;

public sealed partial class ItemClips: ItemBase<WsSqlClipEntity>
{
    public ItemClips() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
