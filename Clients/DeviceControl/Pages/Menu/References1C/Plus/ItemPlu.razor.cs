using WsStorageCore.Entities.SchemaScale.PlusStorageMethodsFks;
using WsStorageCore.Entities.SchemaScale.PlusTemplatesFks;
namespace DeviceControl.Pages.Menu.References1C.Plus;

public sealed partial class ItemPlu : ItemBase<WsSqlPluEntity>
{
    #region Public and private fields, properties, constructor
    
    private WsSqlPluStorageMethodEntity StorageMethod { get; set; }
    private WsSqlPluStorageMethodFkEntity StorageMethodFk { get; set; }
    private WsSqlPluTemplateFkEntity PluTemplateFk { get; set; }
    private List<WsSqlTemplateEntity> Templates { get; set; }

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
