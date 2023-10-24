using WsStorageCore.Entities.SchemaScale.PlusTemplatesFks;
namespace DeviceControl.Pages.Menu.Operations.PlusLabels;

public sealed partial class ItemPluLabel : ItemBase<WsSqlPluLabelEntity>
{
    #region Public and private fields, properties, constructor

    private bool IsWeighted => SqlItemCast.PluScale.Plu.IsCheckWeight;
    private WsSqlPluTemplateFkRepository PluTemplateFkRepository { get; } = new();
    
    public ItemPluLabel() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion
}
