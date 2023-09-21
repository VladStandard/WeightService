using WsStorageCore.Tables.TableScaleFkModels.PlusStorageMethodsFks;
using WsStorageCore.Tables.TableScaleFkModels.PlusTemplatesFks;

namespace DeviceControl.Pages.Menu.References1C.Plus;

public sealed partial class ItemPlu : ItemBase<WsSqlPluModel>
{
    #region Public and private fields, properties, constructor
    
    private WsSqlPluStorageMethodModel StorageMethod { get; set; }
    private WsSqlPluStorageMethodFkModel StorageMethodFk { get; set; }
    private WsSqlPluTemplateFkModel PluTemplateFk { get; set; }
    private List<WsSqlTemplateModel> Templates { get; set; }

    public ItemPlu() : base()
    {
        PluTemplateFk = new();
        ButtonSettings.IsShowSave = true;
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        PluTemplateFk = new WsSqlPluTemplateFkRepository().GetItemByPlu(SqlItemCast);
        StorageMethodFk = new WsSqlPluStorageMethodFkRepository().GetItemByPlu(SqlItemCast);
        StorageMethod = StorageMethodFk.Method;
        Templates = new WsSqlTemplateRepository().GetList(WsSqlCrudConfigFactory.GetCrudActual());
    }

    protected override void ItemSave()
    {
        SqlItemSave(PluTemplateFk);
    }

    #endregion
}
