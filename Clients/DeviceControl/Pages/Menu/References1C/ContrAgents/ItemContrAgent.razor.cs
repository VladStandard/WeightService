namespace DeviceControl.Pages.Menu.References1C.ContrAgents;

public sealed partial class ItemContrAgent : ItemBase<WsSqlContragentModel>
{
    public ItemContrAgent() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}
