using WsStorageCore.TableScaleModels.Clips;

namespace DeviceControl.Pages.Menu.References1C.Clips;

public sealed partial class ItemClips: ItemBase<WsSqlClipModel>
{
    public ItemClips() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}