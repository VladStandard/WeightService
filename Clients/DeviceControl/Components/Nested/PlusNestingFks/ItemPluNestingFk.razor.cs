// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusNestingFks;

namespace DeviceControl.Components.Nested.PlusNestingFks;

public sealed partial class ItemPluNestingFk : ItemBase<WsSqlPluNestingFkModel>
{
    public ItemPluNestingFk() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}