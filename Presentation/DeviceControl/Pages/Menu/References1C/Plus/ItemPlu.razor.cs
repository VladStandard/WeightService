using Ws.StorageCore.Entities.SchemaScale.PlusStorageMethodsFks;
using Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;

namespace DeviceControl.Pages.Menu.References1C.Plus;

public sealed partial class ItemPlu : ItemBase<SqlPluEntity>
{
    private SqlPluStorageMethodEntity StorageMethod { get; set; }
    private SqlPluStorageMethodFkEntity StorageMethodFk { get; set; }
    private SqlPluTemplateFkEntity PluTemplateFk { get; set; }
    private List<SqlTemplateEntity> Templates { get; set; }

    public ItemPlu() : base()
    {
        PluTemplateFk = new();
        ButtonSettings.IsShowSave = true;
    }
    
    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        PluTemplateFk = new SqlPluTemplateFkRepository().GetItemByPlu(SqlItemCast);
        StorageMethodFk = new SqlPluStorageMethodFkRepository().GetItemByPlu(SqlItemCast);
        StorageMethod = StorageMethodFk.Method;
        Templates = new SqlTemplateRepository().GetList(SqlCrudConfigFactory.GetCrudActual());
    }

    protected override void ItemSave()
    {
        SqlItemSave(PluTemplateFk);
    }
}
