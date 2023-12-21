using Ws.StorageCore.Entities.SchemaPrint.Labels;
using Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;

namespace DeviceControl.Pages.Menu.Operations.Labels;

public sealed partial class ItemLabel : ItemBase<SqlLabelEntity>
{
    #region Public and private fields, properties, constructor

    private bool IsWeighted => SqlItemCast.Pallet.Plu.IsCheckWeight;
    private SqlPluTemplateFkRepository PluTemplateFkRepository { get; } = new();
    
    public ItemLabel() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion
}
