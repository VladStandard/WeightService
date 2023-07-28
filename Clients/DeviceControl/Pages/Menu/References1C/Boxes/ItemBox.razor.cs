// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Settings;
using WsStorageCore.Tables.TableScaleModels.Boxes;

namespace DeviceControl.Pages.Menu.References1C.Boxes;

public sealed partial class ItemBox : ItemBase<WsSqlBoxModel>
{
    public ItemBox() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}