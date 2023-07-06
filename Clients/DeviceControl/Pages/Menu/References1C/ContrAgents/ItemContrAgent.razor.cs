// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;
using WsStorageCore.TableScaleModels.Contragents;

namespace DeviceControl.Pages.Menu.References1C.ContrAgents;

public sealed partial class ItemContrAgent : ItemBase<WsSqlContragentModel>
{
    public ItemContrAgent() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}