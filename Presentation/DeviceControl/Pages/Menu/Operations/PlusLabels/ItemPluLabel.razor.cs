using Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;

namespace DeviceControl.Pages.Menu.Operations.PlusLabels;

public sealed partial class ItemPluLabel : ItemBase<SqlPluLabelEntity>
{
    private bool IsWeighted => SqlItemCast.PluScale.Plu.IsCheckWeight;
    private SqlPluTemplateFkRepository PluTemplateFkRepository { get; } = new();
    
    public ItemPluLabel() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}